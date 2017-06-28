using Xamarin.Forms;
using AVFoundation;
using Foundation;
using MPWU.iOS;
using MPWU.Alarm;

[assembly: Dependency(typeof(Player))]
namespace MPWU.iOS
{
	/// <summary>
	/// Implementation of player for iOS platform
	/// </summary>
	public class Player : IPlayer
	{
		/// <summary>
		/// Media player for iOS.
		/// </summary>
		private AVAudioPlayer player;

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
				NSError err;
				this.player = new AVAudioPlayer(NSUrl.FromFilename("sound.wav"), "wav", out err);
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

		/// <summary>
		/// Stop the sound.
		/// </summary>
		public void Stop()
		{
			if (player != null && player.Playing)
			{
				this.player.Stop();
				this.player.Init();
				this.player.PrepareToPlay();
			}
		}
	}
}
