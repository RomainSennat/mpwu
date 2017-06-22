using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MPWU.Alarm;
using MPWU.Database;
using MPWU.EDT;
using System.Diagnostics;
using Plugin.LocalNotifications;
using MPWU.UserData;

namespace MPWU
{
	public partial class App : Application
	{
		public static Param Params { get; set; }
		public static CustomSchedule Schedule { get; set; }
		public static RSSData Data { get; set; }
		private MPWUPage alarmPage;

		public App()
		{
			InitializeComponent();
			App.Params = new ParamDB().InitParam();
			App.Schedule = new ParamDB().InitCustomSchedule();
			Debug.WriteLine(Params.CoordArriveLatitude);
			TabbedPage page = new TabbedPage();
			page.Children.Add(new Parametres()
			{
				Icon = "logoParam.png"
			});
			page.Children.Add(this.alarmPage = new MPWUPage()
			{
				Icon = "alarm.png"
			});
			page.Children.Add(new PersonalEDT()
			{
				Icon = "schedule.png"
			});
			page.CurrentPage = page.Children[1];
			MainPage = page;
		}

		protected override async void OnStart()
		{
			try
			{
				// Get Data
				App.Data = await new Rss().RecupData(App.Params.UrlRss);
				// Set next course informations on UI
				string[] hour = new string[3];
				try
				{
					hour = App.Data.heure.ToString("c").Split('.')[1].Split(':');
				}
				catch (Exception ex)
				{
					hour = App.Data.heure.ToString("c").Split(':');

				}
				this.alarmPage.FindByName<Label>("Hour").Text = String.Format("{0}:{1}", hour[0], hour[1]);
				this.alarmPage.FindByName<Label>("Title").Text = App.Data.titre;

				// Get actual date and time
				DateTime now = DateTime.Now;
				// Get start day time
				DateTime start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

				start = start.Add(App.Data.heure);
				// Substract journey and prepare time
				TimeSpan journey = await new Geolocalisation().GetJourneyTime();
				TimeSpan prepare = App.Params.PrepTime;
				start = start.Subtract(journey).Subtract(prepare);

				// Set time to UI
				this.alarmPage.FindByName<Label>("AlarmHour").Text = start.ToString("t");

				// Notify time to user
				String body = String.Format("L'alarme sonnera {0} {1} à {2}.", start.ToString("dddd"), start.ToString("M"), start.ToString("t"));
				CrossLocalNotifications.Current.Show("My Personal Wake Up", body);
				// Get time between start time and now time
				TimeSpan time = start - now;
				Debug.WriteLine(time.ToString());
				// Time for test
				//TimeSpan timeForTest = new TimeSpan(0, 0, 4);
				// Wait to reach time
				//await Task.Delay((int)timeForTest.TotalMilliseconds);
				await Task.Delay((int)time.TotalMilliseconds);

				// Notify user to wake up
				CrossLocalNotifications.Current.Show("My Personal Wake Up", "Reveil toi !");
				// Play sound
				//Task.Run(await () => {
				DependencyService.Get<IPlayer>().Play();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
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
