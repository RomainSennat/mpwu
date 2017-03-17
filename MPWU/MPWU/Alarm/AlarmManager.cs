using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace MPWU.Alarm
{
	public class AlarmManager
	{
		public AlarmManager()
		{
		}

		public void PlaySound()
		{
			DependencyService.Get<IPlayer>().Play();
		}

		public void StopSound()
		{
			DependencyService.Get<IPlayer>().Stop();
		}
	}
}
