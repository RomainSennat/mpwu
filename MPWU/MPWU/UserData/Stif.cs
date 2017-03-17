using System;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;

using System.Net.Http.Headers;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace MPWU.UserData
{
	public class Stif
	{
		String tempsTrajet;

		public Stif()
		{

		}

		public String getHeureArrive() 
		{
			return this.tempsTrajet;
		}

		public async Task<Boolean> getItineraire(Coord depart, Coord arrive)
		{

			String date = DateTime.Now.ToString("yyyyMMddTHHmmss");
			string url = "https://opendata.stif.info/service/api-stif-recherche-itineraires/journeys" +
				"?from=" + depart.longi.ToString().Replace(",", ".") + "%3B" + depart.lat.ToString().Replace(",", ".") +
				"&to=" + arrive.longi.ToString().Replace(",", ".") + "%3B" + arrive.lat.ToString().Replace(",", ".") +
				"&datetime=" + date +
				"&apikey=05dd8acbae99e3b9959076d38ab5cc472c3b6ca0452a97b5ea838c5d";
			Debug.WriteLine(url);
			var response = await getJson(url);
			this.tempsTrajet = convertToDate(response).ToString("HH:mm");
			Debug.WriteLine(convertToDate(response).ToString("HH:mm"));
			return true;
		}


		public async Task<string> getJson(string url)
		{
			//Uri uri = new Uri(url);
			//HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			//string received;

			//using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
			//{
			//	using (var responseStream = response.GetResponseStream())
			//	{
			//		using (var sr = new StreamReader(responseStream))
			//		{

			//			received = await sr.ReadToEndAsync();
			//		}
			//	}
			//}

			//return received;
			bool recupValeurDateTime = false;
			string arrive = "null";
			HttpClient client = new HttpClient();
			HttpResponseMessage resp = await client.GetAsync(url);
			HttpContent content = resp.Content;
			String result = await content.ReadAsStringAsync();


			JsonTextReader reader = new JsonTextReader(new StringReader(result));
			while (reader.Read())
			{
				if (reader.Value != null) 
				{
					if (reader.Value.ToString().Equals("arrival_date_time"))
					{
						recupValeurDateTime = true;
					}
					else if (recupValeurDateTime) 
					{
						arrive = reader.Value.ToString();
						recupValeurDateTime = false;
					}
				}
			}
			return arrive;
		}

		public DateTime convertToDate(string dateString)
		{
			Debug.WriteLine(dateString);
			DateTime now = DateTime.Now;
			DateTime date = DateTime.ParseExact(dateString, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture);
			TimeSpan duree = date - now;

			DateTime reponse = new DateTime(now.Year, now.Month, now.Day, duree.Hours, duree.Minutes, duree.Seconds);
			return reponse;
		}
	}
}
