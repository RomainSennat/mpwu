using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MPWU.UserData
{
	/// <summary>
	/// Geolocalisation.
	/// </summary>
	public class Geolocalisation
	{
		/// <summary>
		/// The current coordinate.
		/// </summary>
		public Position CurrentCoord;
		/// <summary>
		/// The target coordinate.
		/// </summary>
		public Position TargetCoord;
		/// <summary>
		/// The geocoder.
		/// </summary>
		private Geocoder geocoder = new Geocoder();
		/// <summary>
		/// The stif API.
		/// </summary>
		private Stif stif = new Stif();
		/// <summary>
		/// The waze API.
		/// </summary>
		private Waze waze = new Waze();
		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		public string Location { get; set; }
		/// <summary>
		/// Gets or sets the destination.
		/// </summary>
		/// <value>The destination.</value>
		public string Destination { get; set; }

		public Geolocalisation()
		{
		}

		/// <summary>
		/// Updates the target location.
		/// </summary>
		/// <returns>The target location.</returns>
		/// <param name="address">Address.</param>
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

		/// <summary>
		/// Updates the current location.
		/// </summary>
		/// <returns>The current location.</returns>
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

		/// <summary>
		/// Computes the journey duration.
		/// </summary>
		/// <returns>The journey duration.</returns>
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

