using Xamarin.Forms;
using AVFoundation;
using Foundation;
using MPWU.iOS;
using MPWU.Alarm;

[assembly: Dependency(typeof(Player))]
namespace MPWU.iOS
{
	public class Player : IPlayer
	{
		private AVAudioPlayer player;

		public Player()
		{
		}

		public void Play()
		{

			if (this.player == null)
			{
				NSError err;
				this.player = new AVAudioPlayer(NSUrl.FromFilename("ding_persevy.wav"), "wav", out err);
			}

			if (!this.player.Playing)
			{
				AVAudioSession session = AVAudioSession.SharedInstance();
				session.SetCategory(AVAudioSessionCategory.Playback);
				session.SetActive(true);
				this.player.NumberOfLoops = -1;
				this.player.Play();
				this.player.Volume = 1.0f;
			}

		}

		public void Stop()
		{
			if (player.Playing)
			{
				this.player.Stop();
				this.player.Init();
				this.player.PrepareToPlay();
			}
		}
	}
}
