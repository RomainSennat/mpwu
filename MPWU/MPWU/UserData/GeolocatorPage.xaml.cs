using System;
using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Forms.Xaml;



namespace MPWU.UserData
{
	public partial class GeolocatorPage : ContentPage
	{
		Geolocalisation geolocalisation = new Geolocalisation();
		Stif stif = new Stif();
		Waze waze = new Waze();

		public GeolocatorPage()
		{
			InitializeComponent();
		}

		async void SwitchCoord(object sender, EventArgs e)
		{
			await this.geolocalisation.recupCoord(entryAdresse.Text);
			champAdresse.Text = String.Format("lat : {0}, long : {1}", this.geolocalisation.coordAddress.lat, this.geolocalisation.coordAddress.longi);
		}

		async void OnClickGeo(object sender, EventArgs e)
		{
			await this.geolocalisation.recupGeolocalisation();
			xmlPos.Text = this.geolocalisation.getGeoLocalisation();
		}

		async void getItineraire(object sender, EventArgs e)
		{
			try
			{
				if (JourneyMode.SelectedSegment == 1)
				{
					await this.stif.getItineraire(this.geolocalisation.coordAuto, this.geolocalisation.coordAddress);
					champHeure.Text = stif.getHeureArrive().ToString("c");
				}
				else
				{
					await this.waze.getItineraire(this.geolocalisation.coordAuto, this.geolocalisation.coordAddress);
					Debug.WriteLine(waze.getHeureArrive());
					champHeure.Text = waze.getHeureArrive().ToString("c");
				}
			}
			catch (Exception ex)
			{

			}
		}

		void Toggle(object sender, ToggledEventArgs e)
		{
			bool estActive = e.Value;

			if (estActive)
			{
				getIti.IsEnabled = true;
				getPos.IsEnabled = true;
				getGeo.IsEnabled = true;
				JourneyMode.IsEnabled = true;
			}
			else
			{
				getIti.IsEnabled = false;
				getPos.IsEnabled = false;
				getGeo.IsEnabled = false;
				JourneyMode.IsEnabled = false;
			}
		}

		void JourneyModeChange(object sender, EventArgs e)
		{
			Debug.WriteLine(JourneyMode.SelectedText);
		}
	}
}