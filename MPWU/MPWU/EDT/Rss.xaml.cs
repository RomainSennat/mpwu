using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace MPWU
{
	public partial class Rss : ContentPage
	{
		public Rss()
		{
			InitializeComponent();
		}

		public async void onClick(object sender, EventArgs e)
		{
			Debug.WriteLine(await recupProchaineHeure());
		} 

		public async Task<TimeSpan> recupProchaineHeure()
		{
			TimeSpan heure = new TimeSpan();

			if (await DisplayAlert("Est-ce la bonne adresse ?", addressRss.Text, "Oui", "Non"))
			{
				string url = addressRss.Text;

				HttpClient webc = new HttpClient();

				var request = webc.GetAsync(new Uri(url));
				var result = request.Result.Content;
				string xmlString = await result.ReadAsStringAsync();
				XDocument xml = XDocument.Parse(xmlString);
				Regex regex = new Regex("(([0-1]){1,}([0-9]{1,})|(2[0-3]))(:)([0-5]{1}[0-9]{1})");

				Match match = regex.Match(xml.Descendants("item").Cast<XElement>().FirstOrDefault().Value);

				heure = new TimeSpan(int.Parse(match.Value.Split(':')[0]), int.Parse(match.Value.Split(':')[1]), 0);

				var i = 1;
				while (i < xml.Descendants("item").Count() && (DateTime.Now.Hour >= heure.Hours && int.Parse(match.Value.Split(':')[0]) >= 12))
				{
					i++;
					match = regex.Match(xml.Descendants("item").Cast<XElement>().ElementAtOrDefault(i).Value);
				}
				heure = new TimeSpan(int.Parse(match.Value.Split(':')[0]), int.Parse(match.Value.Split(':')[1]), 0);
			}
			return heure;
		}
	}
}
