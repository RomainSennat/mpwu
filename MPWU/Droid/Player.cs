using MPWU.Droid;
using Xamarin.Forms;
using Android.Media;
using MPWU.Alarm;

[assembly: Dependency(typeof(Player))]
namespace MPWU.Droid
{
	public class Player : IPlayer
	{

		private MediaPlayer player;

		public Player()
		{
		}

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
