using System;
using EventKit;
using EventKitUI;
using Foundation;

namespace CreateCalendarEntry.iOS
{
	public class CreateEventEditViewDelegate : EventKitUI.EKEventEditViewDelegate
	{
		EKEventStore eventStore;
		EventKitUI.EKEventEditViewController eventController;

		public CreateEventEditViewDelegate (EventKitUI.EKEventEditViewController eventController, EKEventStore eventStore)
		{
			this.eventController = eventController;
			this.eventStore = eventStore;
		}

		// completed is called when a user eith
		public override void Completed (EventKitUI.EKEventEditViewController controller, EKEventEditViewAction action)
		{
			eventController.DismissViewController (true, null);

			switch ( action ) {

			case EKEventEditViewAction.Canceled:
				break;
			case EKEventEditViewAction.Deleted:
				break;
			case EKEventEditViewAction.Saved:
				NSError error;
				eventStore.SaveEvent (controller.Event, EKSpan.ThisEvent, out error);
				break;
			}
		}
	}
}

