using System;
using MPWU.Alarm;
using Xamarin.Forms;

namespace MPWU
{
	public partial class MPWUPage : ContentPage
	{
		private AlarmManager manager = new AlarmManager();

		public MPWUPage()
		{
			InitializeComponent();
		}

		private void StopSound(object sender, EventArgs e)
		{
			StopButton.IsEnabled = false;
			this.manager.StopSound();
		}

	}
}
