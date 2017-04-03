using System;
using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Forms.Xaml;



namespace MPWU.UserData
{
	public partial class GeolocatorPage : ContentPage
	{
		Geolocalisation Geolocalisation = new Geolocalisation();
		Stif Stif = new Stif();
		Waze Waze = new Waze();

		public GeolocatorPage()
		{
			InitializeComponent();
		}

		async void SwitchCoord(object sender, EventArgs e)
		{
			await this.Geolocalisation.recupCoord(entryAdresse.Text);
			champAdresse.Text = string.Format("lat : {0}, long : {1}", this.Geolocalisation.CoordAddress.Latitude, this.Geolocalisation.CoordAddress.Longitude);
		}

		async void OnClickGeo(object sender, EventArgs e)
		{
			await this.Geolocalisation.recupGeolocalisation();
			xmlPos.Text = this.Geolocalisation.Location;
		}

		async void GetItineraire(object sender, EventArgs e)
		{
			try
			{
				champHeure.Text = (JourneyMode.SelectedSegment == 1)
									? (await Stif.GetHeureArrive(this.Geolocalisation.CoordAuto, this.Geolocalisation.CoordAddress)).ToString("c")
									: champHeure.Text = (await Waze.GetHeureArrive(this.Geolocalisation.CoordAuto, this.Geolocalisation.CoordAddress)).ToString("c");
			}
			catch (Exception ex)
			{

			}
		}

		void Toggle(object sender, ToggledEventArgs e)
		{

			if (e.Value)
			{
				foreach (var el in this.contentToHide.Children)
				{
					el.IsEnabled = true;
				}
			}
			else
			{
				foreach (var el in this.contentToHide.Children)
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