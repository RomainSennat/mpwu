using Foundation;
using UIKit;
using SegmentedControl;
using SegmentedControl.FormsPlugin.iOS;

namespace MPWU.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			global::Xamarin.FormsMaps.Init();
			SegmentedControlRenderer.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
