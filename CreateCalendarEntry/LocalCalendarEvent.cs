using System;

namespace CreateCalendarEntry
{
	public class LocalCalendarEvent
	{
		public string Subject { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public DateTime? ReminderDate { get; set; }
	}
}

