using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MPWU.Alarm;
using MPWU.EDT;
using System.Diagnostics;
using Plugin.LocalNotifications;
using System.Globalization;

namespace MPWU
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			TabbedPage page = new TabbedPage();
			page.Children.Add(new Parametres()
			{
				Icon = "logoParam.png"
			});
			page.Children.Add(new MPWUPage()
			{
				Icon = "alarm.png"
			});
			page.Children.Add(new ContentPage()
			{
				Icon = "schedule.png"
			});
			page.CurrentPage = page.Children[1];
			MainPage = page;
		}

		protected override async void OnStart()
		{
			await Task.Run(async () =>
			{
				// Get actual date and time
				DateTime now = DateTime.Now;
				// Get start day time
				DateTime start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
				start = start.Add(await new Rss().RecupProchaineHeure("http://agendas.iut.univ-paris8.fr/indexRSS.php?login=rsennat"));
				// Substract journey and prepare time
				TimeSpan journey = new TimeSpan(0, 5, 0);
				TimeSpan prepare = new TimeSpan(0, 10, 0);
				start = start.Subtract(journey).Subtract(prepare);
				// Notify time to user
				String body = String.Format("L'alarme sonnera {0} {1} à {2}.", start.ToString("dddd"), start.ToString("M"), start.ToString("t"));
				CrossLocalNotifications.Current.Show("My Personnal Wake Up", body);
				// Get time between start time and now time
				TimeSpan time = start - now;
				Debug.WriteLine(time.ToString());
				// Time for test
				TimeSpan timeForTest = new TimeSpan(0, 0, 4);
				//await Task.Delay((int)time.TotalMilliseconds);
				// Wait to reach time
				await Task.Delay((int)timeForTest.TotalMilliseconds);
				// Notify user to wake up
				CrossLocalNotifications.Current.Show("My Personnal Wake Up", "Reveil toi !");
				// Play sound
				DependencyService.Get<IPlayer>().Play();
			});
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
