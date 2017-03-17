using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MPWU.Alarm;
using System.Diagnostics;
using Plugin.LocalNotifications;

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
				DateTime now = DateTime.Now;
				DateTime start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
				start = start.Add(await new Rss().recupProchaineHeure("http://agendas.iut.univ-paris8.fr/indexRSS.php?login=rsennat"));
				CrossLocalNotifications.Current.Show("Alarme", "L'alarme sonnera à " + start.ToString("hh:mm"));
				TimeSpan journey = new TimeSpan(0, 0, 5);
				TimeSpan prepare = new TimeSpan(0, 0, 15);
				TimeSpan time = start.Add(journey).Add(prepare) - now;
				TimeSpan timeForTest = new TimeSpan(0, 0, 4);
				Debug.WriteLine(time.ToString());
				//await Task.Delay((int)time.TotalMilliseconds);
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
