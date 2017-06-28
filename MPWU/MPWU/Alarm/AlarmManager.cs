using Xamarin.Forms;

namespace MPWU.Alarm
{
	/// <summary>
	/// Alarm manager.
	/// </summary>
	public class AlarmManager
	{
		public AlarmManager()
		{
		}

		/// <summary>
		/// Play the sound.
		/// </summary>
		public void PlaySound()
		{
			DependencyService.Get<IPlayer>().Play();
		}

		/// <summary>
		/// Stop the sound.
		/// </summary>
		public void StopSound()
		{
			DependencyService.Get<IPlayer>().Stop();
		}
	}
}
