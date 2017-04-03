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
		public Coord CoordAuto;
		public Coord CoordAddress;
		private Geocoder Geocoder;
		public string Location { get; set; }


		public Geolocalisation()
		{

			this.Geocoder = new Geocoder();
		}

		public async Task<bool> recupCoord(string address)
		{
			var approximateLocations = await Geocoder.GetPositionsForAddressAsync(address);
			this.CoordAddress.Latitude = approximateLocations.FirstOrDefault().Latitude;
			this.CoordAddress.Longitude = approximateLocations.FirstOrDefault().Longitude;
			Debug.WriteLine(String.Format("{0},{1}", this.CoordAddress.Latitude, this.CoordAddress.Longitude));
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

		public async Task<bool> recupGeolocalisation()
		{
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 10;
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
				this.CoordAuto.Latitude = position.Latitude;
				this.CoordAuto.Longitude = position.Longitude;
				Debug.WriteLine(String.Format("lat : {0}, long : {1}", this.CoordAuto.Latitude, this.CoordAuto.Longitude));
				if ((int)this.CoordAuto.Latitude != 0 && (int)this.CoordAuto.Longitude != 0)
				{
					var revposition = new Position(this.CoordAuto.Latitude, this.CoordAuto.Longitude);
					var possibleAddresses = await Geocoder.GetAddressesForPositionAsync(revposition);
					// possibleAddresses est un Enum
					this.Location = possibleAddresses.FirstOrDefault();
					Debug.WriteLine(this.Location);
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

