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
		public Coord CurrentCoord;
		public Coord TargetCoord;
		private Geocoder geocoder;
		public string Location { get; set; }


		public Geolocalisation()
		{

			this.geocoder = new Geocoder();
		}

		public async Task<bool> GetCoord(string address)
		{
			try
			{
				var approximateLocations = await geocoder.GetPositionsForAddressAsync(address);
				this.TargetCoord.Latitude = approximateLocations.FirstOrDefault().Latitude;
				this.TargetCoord.Longitude = approximateLocations.FirstOrDefault().Longitude;
				Debug.WriteLine(string.Format("{0},{1}", this.TargetCoord.Latitude, this.TargetCoord.Longitude));
			}
			catch (Exception ex)
			{

			}
			return true;
		}

		public async Task<bool> GetGeolocalisation()
		{
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 10;
			Debug.WriteLine("Getting GPS ...");
			try
			{
				var position = await locator.GetPositionAsync(30000);
				if (position == null)
				{
					Debug.WriteLine("null gps ");
					return false;
				}
				//Convertis les coord en adresses.
				this.CurrentCoord.Latitude = position.Latitude;
				this.CurrentCoord.Longitude = position.Longitude;
				Debug.WriteLine(string.Format("lat : {0}, long : {1}", this.CurrentCoord.Latitude, this.CurrentCoord.Longitude));
				if ((int)this.CurrentCoord.Latitude != 0 && (int)this.CurrentCoord.Longitude != 0)
				{
					var revposition = new Position(this.CurrentCoord.Latitude, this.CurrentCoord.Longitude);
					var possibleAddresses = await geocoder.GetAddressesForPositionAsync(revposition);
					// possibleAddresses est une Collection
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

