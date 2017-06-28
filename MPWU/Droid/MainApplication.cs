using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace MPWU.Droid
{
	/// <summary>
	/// Main application.
	/// </summary>
	[Application]
	public class MainApplication : Application, Application.IActivityLifecycleCallbacks
	{
		/// <summary>
		/// Initializes a new instance of the MainApplication class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		/// <param name="transer">Transer.</param>
		public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
		{
		}

		/// <summary>
		/// Function run when application start.
		/// </summary>
		public override void OnCreate()
		{
			base.OnCreate();
			RegisterActivityLifecycleCallbacks(this);
			//A great place to initialize Xamarin.Insights and Dependency Services!
		}

		/// <summary>
		/// Function run when application terminate.
		/// </summary>
		public override void OnTerminate()
		{
			base.OnTerminate();
			UnregisterActivityLifecycleCallbacks(this);
		}

		/// <summary>
		/// Function run when application is create.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="savedInstanceState">Saved instance state.</param>
		public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
		{
			CrossCurrentActivity.Current.Activity = activity;
		}

		/// <summary>
		/// Function run when application is destroy.
		/// </summary>
		/// <param name="activity">Activity.</param>
		public void OnActivityDestroyed(Activity activity)
		{
		}

		/// <summary>
		/// Function run when application go in background.
		/// </summary>
		/// <param name="activity">Activity.</param>
		public void OnActivityPaused(Activity activity)
		{
		}

		/// <summary>
		/// Function run when application exit background.
		/// </summary>
		/// <param name="activity">Activity.</param>
		public void OnActivityResumed(Activity activity)
		{
			CrossCurrentActivity.Current.Activity = activity;
		}

		public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
		{
		}

		public void OnActivityStarted(Activity activity)
		{
			CrossCurrentActivity.Current.Activity = activity;
		}

		public void OnActivityStopped(Activity activity)
		{
		}
	}
}