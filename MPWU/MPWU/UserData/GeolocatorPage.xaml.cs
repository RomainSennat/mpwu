using System;
using Xamarin.Forms;
using MPWU.Database;
using System.Diagnostics;
using Xamarin.Forms.Xaml;



namespace MPWU.UserData
{
	public partial class GeolocatorPage : ContentPage
	{
		private Geolocalisation geolocalisation = new Geolocalisation();
		private Stif stif = new Stif();
		private Waze waze = new Waze();
		private ParamDB paramDB = new ParamDB();
		//public Param param = new Param();

		// Coordonnées a reconstruire à partir des float enregistrés
		private Coord coordDepart;
		private Coord coordArrive;

		public GeolocatorPage()
		{
			InitializeComponent();
			try
			{
				// retourne le dernier param enregistré, retourne une exception si aucune donnée n'a été save
				//var recupParam = paramDB.GetLastParam();
				//var recupParam = App.Params;
				//this.param = recupParam;
				this.coordDepart.Latitude = App.Params.CoordDepartLatitude;
				this.coordDepart.Longitude = App.Params.CoordDepartLongitude;
				this.coordArrive.Latitude = App.Params.CoordArriveLatitude;
				this.coordArrive.Longitude = App.Params.CoordArriveLongitude;
				//Assignation des Coordonnées enregistrés
				this.geolocalisation.CurrentCoord = this.coordDepart;
				this.geolocalisation.TargetCoord = this.coordArrive;
				// Bind tous les champs enregistrés en dernier dans les champs sur la vue
				ParamToShow.BindingContext = App.Params;
			}
			catch(Exception e) 
			{
				Debug.WriteLine("Aucune donnée pré-enregistrés");
			}
		}

		// Adresse -> Coordonnées
		async void SwitchCoord(object sender, EventArgs e)
		{
			await this.geolocalisation.GetCoord(TargetAdresseEntry.Text);
			App.Params.CoordArriveLatitude = (float)this.geolocalisation.TargetCoord.Latitude; 
			App.Params.CoordArriveLongitude = (float)this.geolocalisation.TargetCoord.Longitude;
			//.param.adresseArrive = TargetAdresseEntry.Text;
			TargetAdress.Text = string.Format("lat : {0}, long : {1}", this.geolocalisation.TargetCoord.Latitude, this.geolocalisation.TargetCoord.Longitude);
		}

		// Coordonnées actuelles
		async void GetGeolocalisation(object sender, EventArgs e)
		{
			try
			{
				await this.geolocalisation.GetGeolocalisation();
				App.Params.CoordDepartLatitude = (float)this.geolocalisation.CurrentCoord.Latitude;
				App.Params.CoordDepartLongitude = (float)this.geolocalisation.CurrentCoord.Longitude;
				//this.param.adresseDepart = this.geolocalisation.Location;
				CurrentPosition.Text = this.geolocalisation.Location;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message + "Hello");
			}
		}

		async void GetItineraire(object sender, EventArgs e)
		{
			try
			{
				ChampHeure.Text = (JourneyMode.SelectedSegment == 1)
									? (await stif.GetHeureArrive(this.geolocalisation.CurrentCoord, this.geolocalisation.TargetCoord)).ToString("c")
									: (await waze.GetHeureArrive(this.geolocalisation.CurrentCoord, this.geolocalisation.TargetCoord)).ToString("c");
				//this.param.modeTrajet = JourneyMode.SelectedSegment;
				//this.param.tempsTrajet = ChampHeure.Text;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		void JourneyModeChange(object sender, EventArgs e)
		{
			Debug.WriteLine(JourneyMode.SelectedText);
		}

		void SaveParam(object sender, EventArgs e)
		{
			Debug.WriteLine("Enregistrement des param");
			this.paramDB.UpdateParam(App.Params);
		}
	}
}