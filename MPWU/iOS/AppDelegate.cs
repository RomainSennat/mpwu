using Foundation;
using UIKit;
using SegmentedControl.FormsPlugin.iOS;
using UserNotifications;
using LocalNotificationsSample.iOS;

namespace MPWU.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
				// Ask the user for permission to get notifications on iOS 10.0+
				UNUserNotificationCenter.Current.RequestAuthorization(
					UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
					(approved, error) => { }
				);
				UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
			}
			else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				// Ask the user for permission to get notifications on iOS 8.0+
				var settings = UIUserNotificationSettings.GetSettingsForTypes(
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					new NSSet()
				);

				UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			}

			global::Xamarin.Forms.Forms.Init();
			global::Xamarin.FormsMaps.Init();
			SegmentedControlRenderer.Init();
			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}
	}
}
