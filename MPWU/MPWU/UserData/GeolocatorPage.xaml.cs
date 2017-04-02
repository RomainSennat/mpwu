using System;
using Xamarin.Forms;



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
				if (true)
				{
					await this.stif.getItineraire(this.geolocalisation.coordAuto, this.geolocalisation.coordAddress);
					champHeure.Text = stif.getHeureArrive();
				}
				else
				{
					await this.waze.getItineraire(this.geolocalisation.coordAuto.lat, this.geolocalisation.coordAuto.longi, this.geolocalisation.coordAddress.lat, this.geolocalisation.coordAddress.longi);
					champHeure.Text = waze.getHeureArrive();
				}
			}
			catch (Exception ex)
			{

			}
		}

		void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
		{
			bool estActive = e.Value;

			if (estActive)
			{
				getIti.IsEnabled = true;
				getPos.IsEnabled = true;
				getGeo.IsEnabled = true;
			}
			else
			{
				getIti.IsEnabled = false;
				getPos.IsEnabled = false;
				getGeo.IsEnabled = false;
				//Faire update et insert ici ? 
			}
		}
	}
}