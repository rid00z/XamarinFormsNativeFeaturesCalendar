using System;

using Xamarin.Forms;

namespace CreateCalendarEntry
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			var button = new Button();
			button.Text = "Test";
			button.Clicked += Button_Clicked;

			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						},
						button
					}
				}
			};
		}

		void Button_Clicked (object sender, EventArgs e)
		{
			var calendarEntry = new LocalCalendarEvent ();
			calendarEntry.Description = "This is a description";
			calendarEntry.StartDate = DateTime.Now.AddDays (1);
			calendarEntry.Location = "Sydney";
			calendarEntry.Subject = "This is the subject";
			calendarEntry.EndDate = DateTime.Now.AddDays (1).AddHours (1);

			var localCalendar = Xamarin.Forms.DependencyService.Get<ILocalCalendar> ();
			localCalendar.CreateCalendarEntry (calendarEntry);

		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

