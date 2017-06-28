using System;
using UserNotifications;

namespace LocalNotificationsSample.iOS
{
	/// <summary>
	/// User notification center delegate.
	/// </summary>
	public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
	{
		/// <summary>
		/// Define how present notifications.
		/// </summary>
		/// <param name="center">Center.</param>
		/// <param name="notification">Notification.</param>
		/// <param name="completionHandler">Completion handler.</param>
		public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
		{
			// Tell system to display the notification anyway or use
			// `None` to say we have handled the display locally.
			completionHandler(UNNotificationPresentationOptions.Alert);
		}
	}
}