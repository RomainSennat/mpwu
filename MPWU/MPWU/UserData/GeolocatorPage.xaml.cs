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
		private Param param = new Param();

		// Coordonnées a reconstruire à partir des float enregistrés
		private Coord coordDepart;
		private Coord coordArrive;

		public GeolocatorPage()
		{
			InitializeComponent();
			try
			{
				// retourne le dernier param enregistré, retourne une exception si aucune donnée n'a été save
				var recupParam = paramDB.GetLastParam();
				this.param = recupParam;
				this.coordDepart.Latitude = recupParam.coordDepartLatitude;
				this.coordDepart.Longitude = recupParam.coordDepartLongitude;
				this.coordArrive.Latitude = recupParam.coordArriveLatitude;
				this.coordArrive.Longitude = recupParam.coordArriveLongitude;
				//Assignation des Coordonnées enregistrés
				this.geolocalisation.CurrentCoord = this.coordDepart;
				this.geolocalisation.TargetCoord = this.coordArrive;
				// Bind tous les champs enregistrés en dernier dans les champs sur la vue
				ParamToShow.BindingContext = recupParam;
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
			this.param.coordArriveLatitude = (float)this.geolocalisation.TargetCoord.Latitude; 
			this.param.coordArriveLongitude = (float)this.geolocalisation.TargetCoord.Longitude;
			this.param.adresseArrive = TargetAdresseEntry.Text;
			TargetAdress.Text = string.Format("lat : {0}, long : {1}", this.geolocalisation.TargetCoord.Latitude, this.geolocalisation.TargetCoord.Longitude);
		}

		// Coordonnées actuelles
		async void GetGeolocalisation(object sender, EventArgs e)
		{
			try
			{
				await this.geolocalisation.GetGeolocalisation();
				this.param.coordDepartLatitude = (float)this.geolocalisation.CurrentCoord.Latitude;
				this.param.coordDepartLongitude = (float)this.geolocalisation.CurrentCoord.Longitude;
				this.param.adresseDepart = this.geolocalisation.Location;
				CurrentPosition.Text = this.geolocalisation.Location;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		async void GetItineraire(object sender, EventArgs e)
		{
			try
			{
				ChampHeure.Text = (JourneyMode.SelectedSegment == 1)
									? (await stif.GetHeureArrive(this.geolocalisation.CurrentCoord, this.geolocalisation.TargetCoord)).ToString("c")
									: (await waze.GetHeureArrive(this.geolocalisation.CurrentCoord, this.geolocalisation.TargetCoord)).ToString("c");
				this.param.modeTrajet = JourneyMode.SelectedSegment;
				this.param.tempsTrajet = ChampHeure.Text;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
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

		void SaveParam(object sender, EventArgs e)
		{
			Debug.WriteLine("Enregistrement des param");
			this.paramDB.AddParam(this.param);
		}
	}
}