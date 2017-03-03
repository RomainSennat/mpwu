using System;
using MPWU.Alarm;
using MPWU.UserData;
using Xamarin.Forms;

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

		async void OnNavigateGeo(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new GeolocatorPage());
		}
	}
}
