using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MPWU.UserData
{
	public class Waze
	{

		public Waze()
		{

		}

		public async Task<TimeSpan> GetHeureArrive(Coord start, Coord end)
		{
			return await this.GetItineraire(start, end);
		}

		public async Task<TimeSpan> GetItineraire(Coord start, Coord end)
		{
			//string url = "http://localhost:8080/waze/routesXY?" +
			string url = "http://speakgame.balastegui.com:1992/waze/routesXY?" +
				"endLat=" + end.Latitude.ToString().Trim().Replace(",", ".") +
							"&endLon=" + end.Longitude.ToString().Trim().Replace(",", ".") +
							"&startLat=" + start.Latitude.ToString().Trim().Replace(",", ".") +
							"&startLon=" + start.Longitude.ToString().Trim().Replace(",", ".");

			Debug.WriteLine(url);
			return await GetJson(url);
		}

		public async Task<TimeSpan> GetJson(string url)
		{
			Debug.WriteLine("Begin");
			int tempsTrajet = 0;
			HttpClient client = new HttpClient();
			HttpResponseMessage resp = await client.GetAsync(url);
			HttpContent content = resp.Content;
			string result = await content.ReadAsStringAsync();

			JsonTextReader reader = new JsonTextReader(new StringReader(result));

			while (reader.Read())
			{
				if (reader.Value != null)
				{
					if (reader.Value.ToString().Equals("routeDurationInMinutes"))
					{
						reader.Read();
						tempsTrajet += int.Parse(reader.Value.ToString());
					}
				}
			}
			reader.Close();
			Debug.WriteLine("End");
			// Format HH:mm:ss
			TimeSpan time = new TimeSpan(tempsTrajet / 60, tempsTrajet % 60, 0);
			Debug.WriteLine(time);
			return time;
		}
	}
}