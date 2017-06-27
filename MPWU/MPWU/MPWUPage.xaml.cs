using System;
using MPWU.Alarm;
using MPWU.UserData;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.LocalNotifications;
using MPWU.EDT;
using System.Diagnostics;

namespace MPWU
{
	public partial class MPWUPage : ContentPage
	{
		private AlarmManager manager;

		public MPWUPage()
		{

			InitializeComponent();
			this.manager = new AlarmManager();
		}

		public void StopSound(object sender, EventArgs e)
		{
			StopButton.IsEnabled = false;
			this.manager.StopSound();
		}

	}
}
