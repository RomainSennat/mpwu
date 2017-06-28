using MPWU.Droid;
using Xamarin.Forms;
using Android.Media;
using MPWU.Alarm;

[assembly: Dependency(typeof(Player))]
namespace MPWU.Droid
{
	/// <summary>
	/// Player implementation for Android platform.
	/// </summary>
	public class Player : IPlayer
	{
		/// <summary>
		/// Media player for Android.
		/// </summary>
		private MediaPlayer player;

		public Player()
		{
		}

		/// <summary>
		/// Play the sound.
		/// </summary>
		public void Play()
		{
			if (this.player == null)
			{
				this.player = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.sound);
			}

			if (!this.player.IsPlaying)
			{
				this.player.Looping = true;
				this.player.Start();
				this.player.SetVolume(1.0f, 1.0f);
			}
		}

		/// <summary>
		/// Stop the sound.
		/// </summary>
		public void Stop()
		{
			if (this.player != null && this.player.IsPlaying)
			{
				this.player.Stop();
				this.player.Reset();
			}
		}
	}
}
