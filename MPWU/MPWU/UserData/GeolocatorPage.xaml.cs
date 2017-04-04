using System;
using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Forms.Xaml;



namespace MPWU.UserData
{
	public partial class GeolocatorPage : ContentPage
	{
		private Geolocalisation geolocalisation = new Geolocalisation();
		private Stif stif = new Stif();
		private Waze waze = new Waze();

		public GeolocatorPage()
		{
			InitializeComponent();
		}

		async void SwitchCoord(object sender, EventArgs e)
		{
			await this.geolocalisation.GetCoord(TargetAdresseEntry.Text);
			TargetAdress.Text = string.Format("lat : {0}, long : {1}", this.geolocalisation.TargetCoord.Latitude, this.geolocalisation.TargetCoord.Longitude);
		}

		async void GetGeolocalisation(object sender, EventArgs e)
		{
			try
			{
				await this.geolocalisation.GetGeolocalisation();
				CurrentPosition.Text = this.geolocalisation.Location;
			}
			catch (Exception ex)
			{
			}
		}

		async void GetItineraire(object sender, EventArgs e)
		{
			try
			{
				ChampHeure.Text = (JourneyMode.SelectedSegment == 1)
									? (await stif.GetHeureArrive(this.geolocalisation.CurrentCoord, this.geolocalisation.TargetCoord)).ToString("c")
									: (await waze.GetHeureArrive(this.geolocalisation.CurrentCoord, this.geolocalisation.TargetCoord)).ToString("c");
			}
			catch (Exception ex)
			{

			}
		}

		void ToggleStackLayout(object sender, ToggledEventArgs e)
		{

			if (e.Value)
			{
				foreach (var el in this.ContentToHide.Children)
				{
					el.IsEnabled = true;
				}
			}
			else
			{
				foreach (var el in this.ContentToHide.Children)
				{
					el.IsEnabled = false;
				}
			}
		}

		void JourneyModeChange(object sender, EventArgs e)
		{
			Debug.WriteLine(JourneyMode.SelectedText);
		}
	}
}