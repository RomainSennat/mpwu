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
			TabbedPage page = new TabbedPage();
			page.BarTextColor = Color.DarkBlue;
			page.Children.Add(new Parametres()
			{
				BackgroundColor = Color.FromHex("#DEDEDE"),
				Icon = "logoParam.png"
			});
			page.Children.Add(this.alarmPage = new MPWUPage()
			{
				BackgroundColor = Color.FromHex("#DEDEDE"),
				Icon = "alarm.png"
			});
			page.Children.Add(new PersonalEDT()
			{
				BackgroundColor = Color.FromHex("#DEDEDE"),
				Icon = "schedule.png"
			});
			page.CurrentPage = page.Children[1];
			MainPage = page;
		}

		protected override async void OnStart()
		{
			Debug.WriteLine(App.Schedule.Lundi);
			try
			{
				// Get Data
				App.Data = await new Rss().DataFromRssAsync(App.Params.UrlRss);
				// Set next course informations on UI
				string[] hour = new String[3];
				try
				{
					hour = App.Data.hour.ToString("c").Split('.')[1].Split(':');
				}
				catch (Exception ex)
				{
					hour = App.Data.hour.ToString("c").Split(':');
					Debug.WriteLine(ex.Message);
				}
				alarmPage.FindByName<Label>("Hour").Text = String.Format("{0}:{1}", hour[0], hour[1]);
				alarmPage.FindByName<Label>("Title").Text = App.Data.title;

				// Get actual date and time
				DateTime now = DateTime.Now;
				// Get start day time
				DateTime start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
				start = start.Add(App.Data.hour);
				// Substract journey and prepare time
				TimeSpan journey = await new Geolocalisation().ComputeJourneyDurationAsync();
				TimeSpan prepare = App.Params.PreparationTime;
				start = start.Subtract(journey).Subtract(prepare);

				// Set time to UI
				this.alarmPage.FindByName<Label>("AlarmHour").Text = start.ToString("t");
				// Notify time to user
				String body = String.Format("L'alarme sonnera {0} {1} à {2}.", start.ToString("dddd"), start.ToString("M"), start.ToString("t"));
				CrossLocalNotifications.Current.Show("My Personal Wake Up", body);

				// Get time between start time and now time
				TimeSpan time = start - now;
				// Wait to reach time
				await Task.Delay((int)time.TotalMilliseconds);
				//await Task.Delay((int)new TimeSpan(0, 0, 4).TotalMilliseconds);

				// Notify user to wake up
				CrossLocalNotifications.Current.Show("My Personal Wake Up", "Reveil toi !");

				// Play sound and enable Stop button
				this.alarmPage.FindByName<Button>("StopButton").IsEnabled = true;
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
