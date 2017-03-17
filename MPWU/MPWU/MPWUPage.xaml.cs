using System;
using MPWU.Alarm;
using MPWU.UserData;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.LocalNotifications;

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
			this.manager.StopSound();
		}
	}
}
