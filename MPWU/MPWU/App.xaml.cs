using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MPWU.Alarm;
using MPWU.UserData;

namespace MPWU
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();
			MainPage = new TabbedPage
			{
				Children =
				{
					new GeolocatorPage(),
					new MPWUPage(),

				}
			};
		}

		protected override async void OnStart()
		{
			await Task.Run(async () =>
			{
				DateTime now = DateTime.Now;
				DateTime target = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second + 4);
				TimeSpan time = target - now;
				await Task.Delay((int)time.TotalMilliseconds);
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
