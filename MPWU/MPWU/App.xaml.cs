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
				start = start.Add(await new Rss().recupProchaineHeure("http://agendas.iut.univ-paris8.fr/indexRSS.php?login=rsennat"));
				start = start.AddDays(start.Hour % 24);
				// Add journey and prepare time
				TimeSpan journey = new TimeSpan(0, 0, 5);
				TimeSpan prepare = new TimeSpan(0, 0, 15);
				start = start.Add(journey).Add(prepare);
				// Get time
				TimeSpan time = start - now;
				TimeSpan timeForTest = new TimeSpan(0, 0, 4);
				Debug.WriteLine(time.ToString());
				String body = String.Format("L'alarme sonnera {0} {1} à {2}:{3}.", start.ToString("dddd"), start.ToString("M"), time.Hours, time.Minutes);
				CrossLocalNotifications.Current.Show("My Personnal Wake Up", body);
				//await Task.Delay((int)time.TimeOfDay.TotalMilliseconds);
				await Task.Delay((int)timeForTest.TotalMilliseconds);
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
