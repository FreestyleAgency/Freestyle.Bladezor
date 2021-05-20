using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Shared
{
	public class Notification
	{
		public virtual string Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual NotificationSeverity Severity { get; set; }
		public virtual int? ProgressPercent { get; set; }
		public virtual string ProgressDescription { get; set; }
		public virtual bool Read { get; set; }
		public virtual DateTime CreatedUtc { get; set; }
		public virtual DateTime UpdatedUtc { get; set; }
		public virtual NotificationGroup NotificationGroup { get; set; }
		public virtual List<NotificationActionLink> ActionLinks { get; set; }
	}
}
