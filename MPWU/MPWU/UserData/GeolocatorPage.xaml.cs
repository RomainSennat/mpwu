using System;
using Xamarin.Forms;



namespace MPWU.UserData
{
	public partial class GeolocatorPage : ContentPage
	{
		Geolocalisation geolocalisation = new Geolocalisation();
		Stif stif = new Stif();

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
			await this.stif.getItineraire(this.geolocalisation.coordAuto, this.geolocalisation.coordAddress);
			champHeure.Text = stif.getHeureArrive();
		}
	}
}
