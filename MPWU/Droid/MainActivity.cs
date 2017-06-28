using Android.App;
using Android.Content.PM;
using Android.OS;
using SegmentedControl.FormsPlugin.Android;

namespace MPWU.Droid
{
	/// <summary>
	/// Main activity.
	/// </summary>
	[Activity(Label = "MPWU.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		/// <summary>
		/// Function run when activity is create.
		/// </summary>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			global::Xamarin.FormsMaps.Init(this, bundle);
			SegmentedControlRenderer.Init();
			LoadApplication(new App());
		}
	}
}
