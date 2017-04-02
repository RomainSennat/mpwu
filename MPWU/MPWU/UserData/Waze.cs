using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MPWU.UserData;
using Newtonsoft.Json;

namespace MPWU
{
	public class Waze
	{
		String tempsTrajetLabel;

		public Waze()
		{

		}

		public String getHeureArrive()
		{
			return this.tempsTrajetLabel;
		}

		public async Task<Boolean> getItineraire(double startLat, double startLon, double endLat, double endLon)
		{
			//string url = "http://localhost:8080/waze/routesXY?" +
			string url = "http://speakgame.balastegui.com:1992/waze/routesXY?" +
							"endLat=" + endLat.ToString().Replace(",", ".") +
							"&endLon=" + endLon.ToString().Replace(",", ".") +
							"&startLat=" + startLat.ToString().Replace(",", ".") +
							"&startLon=" + startLon.ToString().Replace(",", ".");

			Debug.WriteLine(url);
			var response = await getJson(url);
			this.tempsTrajetLabel = response.ToString();
			return true;
		}

		public async Task<TimeSpan> getJson(string url)
		{
			Debug.WriteLine("Begin");
			bool recupValeur = false;
			int tempsTrajet = 0;
			HttpClient client = new HttpClient();
			HttpResponseMessage resp = await client.GetAsync(url);
			HttpContent content = resp.Content;
			String result = await content.ReadAsStringAsync();
			JsonTextReader reader = new JsonTextReader(new StringReader(result));

			while (reader.Read())
			{
				if (reader.Value != null)
				{
					Debug.WriteLine("While");
					if (reader.Value.ToString().Equals("routeDurationInMinutes"))
					{
						recupValeur = true;
					}
					else if (recupValeur)
					{
						tempsTrajet += int.Parse(reader.Value.ToString());
						recupValeur = false;
					}
				}
			}
			Debug.WriteLine("End");
			// Format HH:mm:ss
			TimeSpan ts = new TimeSpan(tempsTrajet / 60, tempsTrajet % 60, 0);
			Debug.WriteLine(ts);
			return ts;
		}
	}
}
