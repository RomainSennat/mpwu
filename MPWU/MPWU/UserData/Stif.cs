using System;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Globalization;

namespace MPWU.UserData
{
	public class Stif
	{

		public Stif()
		{

		}

		public async Task<TimeSpan> GetHeureArrive(Coord depart, Coord arrive)
		{

			return await this.GetItineraire(depart, arrive);
		}

		public async Task<TimeSpan> GetItineraire(Coord depart, Coord arrive)
		{

			string date = DateTime.Now.ToString("yyyyMMddTHHmmss");
			string url = "https://opendata.stif.info/service/api-stif-recherche-itineraires/journeys" +
				"?from=" + depart.Longitude.ToString().Replace(",", ".") + "%3B" + depart.Latitude.ToString().Replace(",", ".") +
				"&to=" + arrive.Longitude.ToString().Replace(",", ".") + "%3B" + arrive.Latitude.ToString().Replace(",", ".") +
				"&datetime=" + date +
				"&apikey=05dd8acbae99e3b9959076d38ab5cc472c3b6ca0452a97b5ea838c5d";
			Debug.WriteLine(url);
			var response = await GetJson(url);
			Debug.WriteLine(ConvertToDate(response).ToString("HH:mm"));
			return ConvertToDate(response).TimeOfDay;
		}


		public async Task<string> GetJson(string url)
		{
			string arrive = "null";
			HttpClient client = new HttpClient();
			HttpResponseMessage resp = await client.GetAsync(url);
			HttpContent content = resp.Content;
			string result = await content.ReadAsStringAsync();
			JsonTextReader reader = new JsonTextReader(new StringReader(result));

			while (reader.Read())
			{
				if (reader.Value != null)
				{
					if (reader.Value.ToString().Equals("arrival_date_time"))
					{
						break;
					}
				}
			}
			reader.Read();
			arrive = reader.Value.ToString();
			reader.Close();
			return arrive;
		}

		public DateTime ConvertToDate(string dateString)
		{
			Debug.WriteLine(dateString);
			DateTime now = DateTime.Now;
			DateTime date = DateTime.ParseExact(dateString, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture);
			TimeSpan duree = date - now;

			DateTime result = new DateTime(now.Year, now.Month, now.Day, duree.Hours, duree.Minutes, duree.Seconds);
			return result;
		}
	}
}
