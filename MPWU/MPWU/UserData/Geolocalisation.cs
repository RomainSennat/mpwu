using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MPWU.UserData
{
	public class Geolocalisation
	{
		public Position CurrentCoord;
		public Position TargetCoord;
		private Geocoder geocoder = new Geocoder();
		private Stif stif = new Stif();
		private Waze waze = new Waze();
		public string Location { get; set; }
		public string Destination { get; set; }

		public Geolocalisation()
		{
		}

		public async Task<bool> UpdateTargetLocationAsync(string address)
		{

			try
			{
				var approximateLocations = await geocoder.GetPositionsForAddressAsync(address);
				var location = approximateLocations.FirstOrDefault();
				this.TargetCoord = new Position(location.Latitude, location.Longitude);
				Debug.WriteLine(string.Format("{0},{1}", this.TargetCoord.Latitude, this.TargetCoord.Longitude));
				if ((int)this.CurrentCoord.Latitude != 0 && (int)this.CurrentCoord.Longitude != 0)
				{
					var possibleAddresses = await geocoder.GetAddressesForPositionAsync(new Position(this.TargetCoord.Latitude, this.TargetCoord.Longitude));
					this.Destination = possibleAddresses.FirstOrDefault();
					Debug.WriteLine(this.Location);
				}
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return false;
			}
		}

		public async Task<bool> UpdateCurrentLocationAsync()
		{
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 5;
			Debug.WriteLine("Getting GPS...");
			try
			{
				var position = await locator.GetPositionAsync(3000);
				if (position != null)
				{
					// Convert coordinate to address
					this.CurrentCoord = new Position(position.Latitude, position.Longitude);
					Debug.WriteLine(string.Format("lat : {0}, long : {1}", this.CurrentCoord.Latitude, this.CurrentCoord.Longitude));
					if ((int)this.CurrentCoord.Latitude != 0 && (int)this.CurrentCoord.Longitude != 0)
					{
						var possibleAddresses = await geocoder.GetAddressesForPositionAsync(new Position(this.CurrentCoord.Latitude, this.CurrentCoord.Longitude));
						this.Location = possibleAddresses.FirstOrDefault();
						Debug.WriteLine(this.Location);
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return false;
			}
		}

		public async Task<TimeSpan> ComputeJourneyDurationAsync()
		{
			if (App.Params.CoordArriveLatitude == 0 || App.Params.CoordArriveLongitude == 0 || App.Params.CoordDepartLatitude == 0 || App.Params.CoordDepartLongitude == 0)
			{
				return new TimeSpan(0, 0, 0);
			}
			Position start = new Position(App.Params.CoordDepartLatitude, App.Params.CoordDepartLongitude);
			Position end = new Position(App.Params.CoordArriveLatitude, App.Params.CoordArriveLongitude);
			return (App.Params.ModeTrajet == 1) ? (await stif.TargetTimeAsync(start, end)) : (await waze.TargetTimeAsync(start, end));
		}
	}
}

