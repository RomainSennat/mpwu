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
			await this.geolocalisation.recupCoord(entryAdresse.Text);
			champAdresse.Text = string.Format("lat : {0}, long : {1}", this.geolocalisation.CoordAddress.Latitude, this.geolocalisation.CoordAddress.Longitude);
		}

		async void OnClickGeo(object sender, EventArgs e)
		{
			await this.geolocalisation.recupGeolocalisation();
			xmlPos.Text = this.geolocalisation.Location;
		}

		async void GetItineraire(object sender, EventArgs e)
		{
			try
			{
				champHeure.Text = (JourneyMode.SelectedSegment == 1)
									? (await stif.GetHeureArrive(this.geolocalisation.CoordAuto, this.geolocalisation.CoordAddress)).ToString("c")
									: (await waze.GetHeureArrive(this.geolocalisation.CoordAuto, this.geolocalisation.CoordAddress)).ToString("c");
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