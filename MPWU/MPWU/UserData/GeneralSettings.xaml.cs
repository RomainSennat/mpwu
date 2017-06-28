using System;
using Xamarin.Forms;
using MPWU.Database;
using System.Diagnostics;
using Xamarin.Forms.Maps;

namespace MPWU.UserData
{
	/// <summary>
	/// General settings.
	/// </summary>
	public partial class GeneralSettings : ContentPage
	{
		/// <summary>
		/// The geolocalisation.
		/// </summary>
		private Geolocalisation geolocalisation = new Geolocalisation();
		/// <summary>
		/// The stif API.
		/// </summary>
		private Stif stif = new Stif();
		/// <summary>
		/// The waze API.
		/// </summary>
		private Waze waze = new Waze();
		/// <summary>
		/// The parameters databse.
		/// </summary>
		private ParamDB paramDB = new ParamDB();
		/// <summary>
		/// The start coordinate.
		/// </summary>
		private Position coordDepart;
		/// <summary>
		/// The end coordinate.
		/// </summary>
		private Position coordArrive;

		public GeneralSettings()
		{
			InitializeComponent();
			try
			{
				// Read parameters
				this.coordDepart = new Position(App.Params.CoordDepartLatitude, App.Params.CoordDepartLongitude);
				this.coordArrive = new Position(App.Params.CoordArriveLatitude, App.Params.CoordArriveLongitude);
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

		/// <summary>
		/// Updates the destination position.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
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

		/// <summary>
		/// Updates the current position.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
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