using Freestyle.Bladezor.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public class DefaultNotificationService : INotificationService
	{
		private const int DefaultToastPopTimerPeriod = 5000;

		private readonly IServiceProvider _serviceProvider;
		private readonly Microsoft.Extensions.Options.IOptions<Freestyle.Bladezor.Client.BladezorOptions> _options;

		private object _lock = new object();
		private object _toastLock = new object();

		private HubConnection _notificationsConnection;
		private List<Notification> _toastNotifications = new List<Notification>();
		private List<Notification> _allRemoteNotifications = new List<Notification>();
		private List<Notification> _localToastNotifications = new List<Notification>();
		private List<Notification> _allLocalNotifications = new List<Notification>();
		private List<string> _surpressedNotifications = new List<string>();

		public event EventHandler<NotificationsEventArgs> NotificationsAdded;
		public event EventHandler<NotificationsEventArgs> NotificationsUpdated;
		public event EventHandler<NotificationsEventArgs> NotificationsRemoved;
		public event EventHandler<NotificationsEventArgs> ToastNotificationsRemoved;
		public event EventHandler<NotificationsEventArgs> ToastNotificationsAdded;

		public virtual async Task<IEnumerable<Notification>> GetToastNotifications()
		{
			return _toastNotifications.Where(x => !_surpressedNotifications.Contains(x.Id)).Union(_localToastNotifications).ToArray();
		}

		public virtual async Task<IEnumerable<Notification>> GetNotifications()
		{
			return _allRemoteNotifications.Where(x => !_surpressedNotifications.Contains(x.Id)).Union(_allLocalNotifications).ToArray();
		}

		public DefaultNotificationService(IServiceProvider serviceProvider, Microsoft.Extensions.Options.IOptions<Freestyle.Bladezor.Client.BladezorOptions> options)
		{
			_serviceProvider = serviceProvider;
			_options = options;
		}

		public virtual async Task Connect(string baseUri, Func<Task<string>> accessTokenProvider)
		{
			_notificationsConnection = new HubConnectionBuilder()
				.WithUrl(baseUri + _options.Value.Notifications.HubPath, options =>
				{
					options.AccessTokenProvider = accessTokenProvider;
				})
				.WithAutomaticReconnect()
				.Build();

			_notificationsConnection.On<string, Notification>(nameof(INotificationClient.ReceiveNotification), (user, notification) => _portalService_NotificationReceived(this, new NotificationEventArgs(notification)));

			await _notificationsConnection.StartAsync();
		}

		private void _portalService_NotificationReceived(object sender, Shared.NotificationEventArgs e)
		{
			AddOrUpdateNotification(e.Notification);
		}

		public virtual async Task DismissToastNotification(Notification notification)
		{
			lock (_toastLock)
			{
				_toastNotifications.Remove(notification);
				_localToastNotifications.Remove(notification);
			}

			ToastNotificationsRemoved?.Invoke(this, new NotificationsEventArgs(notification));
		}

		public virtual async Task DismissNotification(Notification notification)
		{
			bool toastRemoved = false;
			bool notificationRemoved = false;

			lock (_lock)
			{
				_surpressedNotifications.Add(notification.Id);

				toastRemoved = _toastNotifications.Remove(notification) || _localToastNotifications.Remove(notification);
				notificationRemoved = _allRemoteNotifications.Remove(notification) || _allLocalNotifications.Remove(notification);
			}

			if (toastRemoved)
				ToastNotificationsRemoved?.Invoke(this, new NotificationsEventArgs(notification));

			if (notificationRemoved)
				NotificationsRemoved?.Invoke(this, new NotificationsEventArgs(notification));
		}

		public virtual async Task MarkAsRead(Notification notification)
		{
			if (notification.Read) return;

			notification.Read = true;

			NotificationsUpdated?.Invoke(this, new NotificationsEventArgs(notification));
		}

		public virtual async Task MarkAsUnread(Notification notification)
		{
			if (!notification.Read) return;

			notification.Read = false;

			NotificationsUpdated?.Invoke(this, new NotificationsEventArgs(notification));
		}

		public virtual async Task MarkAllAsRead()
		{
			foreach (var notification in _allRemoteNotifications)
				notification.Read = true;

			NotificationsUpdated?.Invoke(this, new NotificationsEventArgs(_allRemoteNotifications));
		}

		public virtual async Task Clear()
		{
			var oldNotifications = _allRemoteNotifications.Union(_allLocalNotifications);
			var oldToastNotifications = _toastNotifications.Union(_localToastNotifications);

			_allRemoteNotifications = new List<Notification>();
			_toastNotifications = new List<Notification>();
			_allLocalNotifications = new List<Notification>();
			_localToastNotifications = new List<Notification>();

			NotificationsRemoved?.Invoke(this, new NotificationsEventArgs(oldNotifications));
			ToastNotificationsRemoved?.Invoke(this, new NotificationsEventArgs(oldToastNotifications));
		}

		public virtual async Task AddOrUpdateNotification(Notification notification)
		{
			if (_surpressedNotifications.Contains(notification.Id)) return;

			Notification existing = null;
			bool isNew = false;

			lock (_lock)
			{
				existing = _allRemoteNotifications.FirstOrDefault(x => x.Id == notification.Id);
				isNew = existing == null;

				if (isNew)
				{
					_allRemoteNotifications.Add(notification);

					lock (_toastLock)
						_toastNotifications.Add(notification);

					new Timer(s => DismissToastNotification(notification), null, DefaultToastPopTimerPeriod, Timeout.Infinite);
				}
				else
				{
					existing.Description = notification.Description;
					existing.ActionLinks = notification.ActionLinks;
					existing.ProgressPercent = notification.ProgressPercent;
					existing.ProgressDescription = notification.ProgressDescription;
					existing.Read = existing.Read || notification.Read;
					existing.Severity = notification.Severity;
					existing.Title = notification.Title;
					existing.CreatedUtc = notification.CreatedUtc;
					existing.UpdatedUtc = notification.UpdatedUtc;
				}
			}

			if (isNew)
			{
				NotificationsAdded?.Invoke(this, new NotificationsEventArgs(notification));
				ToastNotificationsAdded?.Invoke(this, new NotificationsEventArgs(notification));
			}
			else
				NotificationsUpdated?.Invoke(this, new NotificationsEventArgs(existing));
		}

		public virtual async Task AddLocalNotification(Notification notification)
		{
			notification.Id = Guid.NewGuid().ToString();

			_allLocalNotifications.Add(notification);

			lock (_toastLock)
				_localToastNotifications.Add(notification);

			new Timer(s => DismissToastNotification(notification), null, DefaultToastPopTimerPeriod, Timeout.Infinite);

			NotificationsAdded?.Invoke(this, new NotificationsEventArgs(notification));
			ToastNotificationsAdded?.Invoke(this, new NotificationsEventArgs(notification));
		}
	}
}
