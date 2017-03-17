using System;
using System.Diagnostics;
using System.Globalization;
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

		private readonly String[] jours = { "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi", "dimanche" };

		public Rss()
		{
			InitializeComponent();
		}

		public async void onClick(object sender, EventArgs e)
		{
			if (await Application.Current.MainPage.DisplayAlert("Est-ce la bonne adresse ?", addressRss.Text, "Oui", "Non"))
			{
				Debug.WriteLine(await recupProchaineHeure(addressRss.Text));
			}
		}

		public async Task<TimeSpan> recupProchaineHeure(string url)
		{
			TimeSpan heure = new TimeSpan();
			HttpClient webc = new HttpClient();
			var request = webc.GetAsync(new Uri(url));
			var result = request.Result.Content;

			string xmlString = await result.ReadAsStringAsync();
			XDocument xml = XDocument.Parse(xmlString);

			// Init de la liste des éléments qui correpondent au titre du flux rss
			var list = xml.Descendants("item").Cast<XElement>().ToList();

			//Regex heures
			Regex regex = new Regex("(([0-1]){1,}([0-9]{1,})|(2[0-3]))(:)([0-5]{1}[0-9]{1})");
			Match match = regex.Match(list.FirstOrDefault().Value);

			//Regex jours
			Regex regexJours = new Regex("lundi|mardi|mercredi|jeudi|vendredi|samedi|dimanche");
			Match matchJours = regexJours.Match(list.FirstOrDefault().Value);

			// Init de la variable du return
			heure = new TimeSpan(int.Parse(match.Value.Split(':')[0]), int.Parse(match.Value.Split(':')[1]), 0);

			var i = 1; // Init à 1, car on a déjà la valeur 0
					   // Mettre à jour les Matchs si : heure actuelle >= 0 OU >= 12
			while (i < list.Count() - 1 && (DateTime.Now.Hour >= int.Parse(match.Value.Split(':')[0]) || int.Parse(match.Value.Split(':')[0]) >= 12))
			{
				i++;
				match = regex.Match(list.ElementAtOrDefault(i).Value);
				matchJours = regexJours.Match(list.ElementAtOrDefault(i).Value);
			}
			// Gérer le jour de début de semaine en fonction du pays
			DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
			// Calculer le jour de la semaine
			int indexDay = 7 - (DateTime.Now.DayOfWeek + 7 - firstDayOfWeek) % 7;

			// Heure à laquelle on commence la prochaine activité
			heure = new TimeSpan(int.Parse(match.Value.Split(':')[0]), int.Parse(match.Value.Split(':')[1]), 0);

			// Heure du prochain cours * 24 ajouté à l'heure de la prochaine activité
			heure = heure.Add(new TimeSpan((Array.IndexOf(jours, matchJours.Value) + indexDay) * 24, 0, 0));

			return heure;
		}
	}
}