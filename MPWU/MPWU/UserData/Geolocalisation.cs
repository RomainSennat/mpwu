using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;

namespace MPWU.UserData
{
	public class Geolocalisation
	{
		public Coord coordAuto;
		public Coord coordAddress;
		private Geocoder geo;
		private String geoLocalisation;

		public Geolocalisation()
		{
			
			this.geo = new Geocoder();
		}

		public void setGeoLocalisation(String geo)
		{
			this.geoLocalisation = geo;
		}

		public String getGeoLocalisation()
		{
			return this.geoLocalisation;
		}

		public async Task<Boolean> recupCoord(String address)
		{
			var approximateLocations = await geo.GetPositionsForAddressAsync(address);
			this.coordAddress.lat = approximateLocations.First().Latitude;
			this.coordAddress.longi = approximateLocations.First().Longitude;
			Debug.WriteLine(String.Format("{0},{1}", this.coordAddress.lat, this.coordAddress.longi));
			return true;
		}

		/*Fonction asynchrone utilisant les plugins Xamarin.FormMaps et Geocoder.
		 * 
		 * Recuperation des données de localisation avec locacor.GetPositionAsync(timoutMilliseconds : 300000)
		 * 		position.Timestamp, position.Latitude, position.Longitude,
		 * 		position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed
		 * 
		 * 
		*/

		public async Task<Boolean> recupGeolocalisation()
		{
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;
			Debug.WriteLine("Getting GPS ...");
			try
			{
				var position = await locator.GetPositionAsync(timeoutMilliseconds: 300000);
				if (position == null)
				{
					Debug.WriteLine("null gps ");
					return false;
				}
				//Convertis les coord en adresses.
				this.coordAuto.lat = position.Latitude;
				this.coordAuto.longi = position.Longitude;
				Debug.WriteLine(String.Format("lat : {0}, long : {1}", this.coordAuto.lat, this.coordAuto.longi));
				if (this.coordAuto.lat != 0 && this.coordAuto.longi != 0)
				{
					var revposition = new Xamarin.Forms.Maps.Position(this.coordAuto.lat, this.coordAuto.longi);
					var possibleAddresses = await geo.GetAddressesForPositionAsync(revposition);
					// possibleAddresses est un Enum
					setGeoLocalisation(possibleAddresses.First());
					Debug.WriteLine(this.geoLocalisation);
					return true;
				}
				Debug.WriteLine(locator);
				return false;
			}
			catch (Exception eexc)
			{
				Debug.WriteLine(eexc.Message);
				return false;
			}
		}
	}
}

