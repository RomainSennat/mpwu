using System;
using Xamarin.Forms;
using MPWU.Database;
using System.Diagnostics;

namespace MPWU.UserData
{
	public partial class GeneralSettings : ContentPage
	{
		private Geolocalisation geolocalisation = new Geolocalisation();
		private Stif stif = new Stif();
		private Waze waze = new Waze();
		private ParamDB paramDB = new ParamDB();
		private Coord coordDepart;
		private Coord coordArrive;

		public GeneralSettings()
		{
			InitializeComponent();
			try
			{
				// Read parameters
				this.coordDepart.Latitude = App.Params.CoordDepartLatitude;
				this.coordDepart.Longitude = App.Params.CoordDepartLongitude;
				this.coordArrive.Latitude = App.Params.CoordArriveLatitude;
				this.coordArrive.Longitude = App.Params.CoordArriveLongitude;
				//Assignation des Coordonnées enregistres
				this.geolocalisation.CurrentCoord = this.coordDepart;
				this.geolocalisation.TargetCoord = this.coordArrive;
				// Bind tous les champs enregistres en dernier dans les champs sur la vue
				ParamToShow.BindingContext = App.Params;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private async void UpdateDestinationPositionAsync(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(TargetAdresseEntry.Text))
			{
				await this.geolocalisation.UpdateTargetLocationAsync(TargetAdresseEntry.Text);
				App.Params.CoordArriveLatitude = (float)this.geolocalisation.TargetCoord.Latitude;
				App.Params.CoordArriveLongitude = (float)this.geolocalisation.TargetCoord.Longitude;
				await App.Current.MainPage.DisplayAlert("Votre destination", this.geolocalisation.Destination, "Ok");
			}
		}

		private async void UpdateCurrentPositionAsync(object sender, EventArgs e)
		{
			try
			{
				await this.geolocalisation.UpdateCurrentLocationAsync();
				App.Params.CoordDepartLatitude = (float)this.geolocalisation.CurrentCoord.Latitude;
				App.Params.CoordDepartLongitude = (float)this.geolocalisation.CurrentCoord.Longitude;
				await App.Current.MainPage.DisplayAlert("Position de départ", this.geolocalisation.Location, "Ok");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}