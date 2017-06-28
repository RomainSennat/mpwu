namespace MPWU.Alarm
{
	/// <summary>
	/// Audio player.
	/// </summary>
	public interface IPlayer
	{
		/// <summary>
		/// Play the sound.
		/// </summary>
		void Play();
		/// <summary>
		/// Stop the sound.
		/// </summary>
		void Stop();
	}
}