using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Freestyle.Bladezor.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Freestyle.Bladezor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DemoController : ControllerBase
	{
		private readonly IHubContext<NotificationHub, INotificationClient> _notificationHub;

		public DemoController(IHubContext<NotificationHub, INotificationClient> hubContext)
		{
			_notificationHub = hubContext;
		}

		[HttpPost("RaiseNotification")]
		public async Task<ActionResult> RaiseNotification()
		{
			var groupNumber = Faker.RandomNumber.Next(1, 5);

			var notification = new Notification()
			{
				Id = Guid.NewGuid().ToString(),
				Title = "This is a test",
				Description = Faker.Lorem.Sentence(),
				Severity = (NotificationSeverity)Faker.RandomNumber.Next(0, 5),
				CreatedUtc = DateTime.UtcNow,
				UpdatedUtc = DateTime.UtcNow,
				ProgressPercent = Faker.Boolean.Random() ? (int?)null : 0,
				ProgressDescription = Faker.Lorem.Paragraph(),
				NotificationGroup = Faker.Boolean.Random() ? new NotificationGroup() { Id = $"group{groupNumber}", Title = $"Group {groupNumber}" } : null
			};

			await _notificationHub.Clients.All.ReceiveNotification(null, notification);

			if (notification.ProgressPercent.HasValue)
			{
				new Timer(o =>
				{
					if (notification.ProgressPercent.Value >= 100) return;

					var newPercent = Math.Min(100, notification.ProgressPercent.Value + Faker.RandomNumber.Next(2, 30));
					notification.ProgressPercent = newPercent;
					notification.ProgressDescription = Faker.Lorem.Sentence();
					_notificationHub.Clients.All.ReceiveNotification(null, notification);
				}, null, Faker.RandomNumber.Next(1000, 10000), Faker.RandomNumber.Next(250, 5000));
			}

			return Ok();
		}
	}
}
