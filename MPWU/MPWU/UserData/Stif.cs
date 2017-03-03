using System;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MPWU.UserData
{
	public class Stif
	{
		

		public Stif()
		{

		}

		public async Task<Boolean> getItineraire(Coord depart, Coord arrive)
		{
			
			String date = DateTime.Now.ToString("yyyyMMddTHHmmss");
			string url = "https://opendata.stif.info/service/api-stif-recherche-itineraires/journeys" +
				"?from=" + depart.longi.ToString().Replace(",",".") + "%3B" + depart.lat.ToString().Replace(",", ".") +
				"&to=" + arrive.longi.ToString().Replace(",", ".") + "%3B" + arrive.lat.ToString().Replace(",", ".") +
				"&datetime=" + date;
			Debug.WriteLine(url);
			var response = await httpRequest(url);
			Debug.WriteLine(response);
			return true;
		}


		public async Task<string> httpRequest(string url)
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

			HttpClient client = new HttpClient();
			HttpResponseMessage resp = await client.GetAsync(url);
			HttpContent content = resp.Content;
			String result = await content.ReadAsStringAsync();
			Debug.WriteLine(result);
			return "a";
		}

	}
}
