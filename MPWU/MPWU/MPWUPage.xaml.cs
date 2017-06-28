using System;
using MPWU.Alarm;
using Xamarin.Forms;

namespace MPWU
{
	/// <summary>
	/// MPWU Page.
	/// </summary>
	public partial class MPWUPage : ContentPage
	{
		/// <summary>
		/// The sound manager.
		/// </summary>
		private AlarmManager manager = new AlarmManager();

		public MPWUPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Stops the sound.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void StopSound(object sender, EventArgs e)
		{
			StopButton.IsEnabled = false;
			this.manager.StopSound();
		}

	}
}
