using System;
using Android.Content;
using Android.Provider;
using CreateCalendarEntry.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(LocalCalendarAndroid))]

namespace CreateCalendarEntry.Droid
{
	public class LocalCalendarAndroid : ILocalCalendar
	{
		public void CreateCalendarEntry (LocalCalendarEvent localCalendarEvent)
		{
//			ContentValues eventValues = new ContentValues ();
//
//			//eventValues.Put (CalendarContract.Events.InterfaceConsts.CalendarId, _calId);
//			eventValues.Put (CalendarContract.Events.InterfaceConsts.Title, localCalendarEvent.Subject);
//			eventValues.Put (CalendarContract.Events.InterfaceConsts.Description, localCalendarEvent.Description);
//			eventValues.Put("beginTime", GetDateTimeMS(localCalendarEvent.StartDate));
//			if (localCalendarEvent.EndDate.HasValue)
//				eventValues.Put("endTime", GetDateTimeMS (localCalendarEvent.EndDate.Value));
//			eventValues.Put (CalendarContract.Events.InterfaceConsts.EventLocation, localCalendarEvent.Location);
//
//			//eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
//			//eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
//
//			var uri = ContentResolver.Insert (CalendarContract.Events.ContentUri, eventValues);

			Intent intent = new Intent (Intent.ActionInsert, ContentUris.WithAppendedId (Android.Provider.CalendarContract.Events.ContentUri, 765));
			intent.SetData(Android.Provider.CalendarContract.Events.ContentUri);

			// Add Event Details
			intent.PutExtra(Android.Provider.CalendarContract.ExtraEventBeginTime, GetDateTimeMS(localCalendarEvent.StartDate));

			if (localCalendarEvent.EndDate.HasValue)
				intent.PutExtra(Android.Provider.CalendarContract.ExtraEventEndTime, GetDateTimeMS(localCalendarEvent.EndDate.Value));
			
			intent.PutExtra(Android.Provider.CalendarContract.EventsColumns.AllDay, false);
			intent.PutExtra(Android.Provider.CalendarContract.EventsColumns.EventLocation, localCalendarEvent.Location);
			intent.PutExtra(Android.Provider.CalendarContract.EventsColumns.Description, localCalendarEvent.Description);
			intent.PutExtra(Android.Provider.CalendarContract.EventsColumns.Title, localCalendarEvent.Subject);

			// Add Alarm reminder
			//intent.PutExtra (Android.Provider.CalendarContract.EventsColumns.Availability, (int) Android.Provider.EventsAvailability.Busy);
			//intent.PutExtra (Android.Provider.CalendarContract.RemindersColumns.EventId, 123);
			//intent.PutExtra (Android.Provider.CalendarContract.RemindersColumns.Minutes, AlertTimeSettingsToMinutes (settings.AlertTimeBefore));

			Forms.Context.StartActivity(intent);
		}

		protected long GetDateTimeMS(DateTime dt)
		{
			return (long)(dt - new DateTime (1970, 1, 1)).TotalMilliseconds;
		}
	}
}

