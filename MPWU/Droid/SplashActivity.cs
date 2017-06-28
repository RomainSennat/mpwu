using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace MPWU.Droid
{
	/// <summary>
	/// Splash activity.
	/// </summary>
	[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		/// <summary>
		/// Tag to identify splash screen.
		/// </summary>
		static readonly string TAG = "X:" + typeof(SplashActivity).Name;

		/// <summary>
		/// Function run when application start.
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		/// <param name="persistentState">Persistent state.</param>
		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);
			Log.Debug(TAG, "SplashActivity.OnCreate");
		}

		/// <summary>
		/// Launch the startup task.
		/// </summary>
		protected override void OnResume()
		{
			base.OnResume();
			Task startupWork = new Task(() => { SimulateStartup(); });
			startupWork.Start();
		}

		/// <summary>
		/// Simulates the startup.
		/// </summary>
		private async void SimulateStartup()
		{
			Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
			await Task.Delay(2500); // Simulate a bit of startup work.
			Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
	}
}