using Freestyle.Bladezor.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Services
{
	public interface INotificationService
	{
		event EventHandler<NotificationsEventArgs> NotificationsAdded;
		event EventHandler<NotificationsEventArgs> NotificationsRemoved;
		event EventHandler<NotificationsEventArgs> NotificationsUpdated;
		event EventHandler<NotificationsEventArgs> ToastNotificationsAdded;
		event EventHandler<NotificationsEventArgs> ToastNotificationsRemoved;

		Task<IEnumerable<Notification>> GetNotifications();
		Task<IEnumerable<Notification>> GetToastNotifications();
		Task Connect(string baseUri, Func<Task<string>> accessTokenProvider);
		Task DismissNotification(Notification notification);
		Task DismissToastNotification(Notification notification);
		Task MarkAsRead(Notification notification);
		Task MarkAllAsRead();
		Task Clear();
		Task MarkAsUnread(Notification notification);
		Task AddLocalNotification(Notification notification);
	}
}