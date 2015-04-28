using System;
using EventKit;
using Foundation;
using UIKit;
using EventKitUI;
using CreateCalendarEntry.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(LocalCalendariOS))]

namespace CreateCalendarEntry.iOS
{
	public class LocalCalendariOS : ILocalCalendar
	{
		protected EKEventStore eventStore;

		public LocalCalendariOS ()
		{
			eventStore = new EKEventStore ();
		}

		public void CreateCalendarEntry (LocalCalendarEvent localCalendarEvent)
		{
			eventStore.RequestAccess (EKEntityType.Event, 
				(bool granted, NSError e) => {
					if (granted)
					{
						UIApplication.SharedApplication.InvokeOnMainThread(() => {

							EventKitUI.EKEventEditViewController eventController = new EventKitUI.EKEventEditViewController ();

							eventController.EventStore = eventStore;

							// wire up a delegate to handle events from the controller
							var eventControllerDelegate = new CreateEventEditViewDelegate ( eventController, eventStore );
							eventController.EditViewDelegate = eventControllerDelegate;

							EKEvent newEvent = EKEvent.FromStore ( eventStore );

							newEvent.StartDate = (NSDate)(DateTime.SpecifyKind(localCalendarEvent.StartDate, DateTimeKind.Local));
							if (localCalendarEvent.EndDate.HasValue)
								newEvent.EndDate = (NSDate)(DateTime.SpecifyKind(localCalendarEvent.EndDate.Value, DateTimeKind.Local));

							newEvent.Title = localCalendarEvent.Subject;
							newEvent.Notes = localCalendarEvent.Description;
							newEvent.Location = localCalendarEvent.Location;

							if (localCalendarEvent.ReminderDate.HasValue)
								newEvent.AddAlarm ( EKAlarm.FromDate ( (NSDate)(DateTime.SpecifyKind(localCalendarEvent.ReminderDate.Value, DateTimeKind.Local))));

							eventController.Event = newEvent;

							// show the event controller
							UIApplication.SharedApplication.Windows[0].RootViewController.PresentViewController ( eventController, true, null );

						});
					}
					else
						new UIAlertView ( "Access Denied", "User Denied Access to Calendar Data", null, "ok", null).Show ();
				} );
		}

	}		
}
