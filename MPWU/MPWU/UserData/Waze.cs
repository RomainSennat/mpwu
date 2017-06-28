using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

namespace MPWU.UserData
{
	/// <summary>
	/// Waze API.
	/// </summary>
	public class Waze
	{
		public Waze()
		{
		}

		/// <summary>
		/// Targets the time.
		/// </summary>
		/// <returns>The time.</returns>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		public async Task<TimeSpan> TargetTimeAsync(Position start, Position end)
		{
			return await this.JourneyTimeAsync(start, end);
		}

		/// <summary>
		/// Obtain journey time.
		/// </summary>
		/// <returns>The time.</returns>
		/// <param name="depart">Depart.</param>
		/// <param name="arrive">Arrive.</param>
		public async Task<TimeSpan> JourneyTimeAsync(Position start, Position end)
		{
			string url = "http://speakgame.balastegui.com:1992/waze/routesXY?" +
				"endLat=" + end.Latitude.ToString().Trim().Replace(",", ".") +
							"&endLon=" + end.Longitude.ToString().Trim().Replace(",", ".") +
							"&startLat=" + start.Latitude.ToString().Trim().Replace(",", ".") +
							"&startLon=" + start.Longitude.ToString().Trim().Replace(",", ".");
			return await ParseJsonAsync(url);
		}

		/// <summary>
		/// Parses the json.
		/// </summary>
		/// <returns>The json.</returns>
		/// <param name="url">URL.</param>
		public async Task<TimeSpan> ParseJsonAsync(string url)
		{
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
			TimeSpan time = new TimeSpan(tempsTrajet / 60, tempsTrajet % 60, 0);
			return time;
		}
	}
}