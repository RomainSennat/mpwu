using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace MPWU.EDT
{
	public partial class Rss : ContentPage
	{

		private readonly string[] jours = { "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi", "dimanche" };

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

			// Init à 1, car on a déjà la valeur 0
			int i = 1;
			// Get next match if needed
			while (i < list.Count() && (int.Parse(match.Value.Split(':')[0]) > 12 || ((int)DateTime.Today.DayOfWeek - 1 == Array.IndexOf(this.jours, matchJours.Value)) && int.Parse(match.Value.Split(':')[0]) < DateTime.Now.Hour))
			{
				match = regex.Match(list.ElementAtOrDefault(i).Value);
				matchJours = regexJours.Match(list.ElementAtOrDefault(i).Value);
				i++;
			}

			// Get current day
			DateTime today = DateTime.Today;
			// Get current day index in week
			int currentDay = (int)today.DayOfWeek;
			// Get monday on current week
			DateTime monday = today.AddDays(-(DateTime.Now.DayOfWeek - CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) % 7);
			// Get range between monday and target day
			int dayIndex = Array.FindIndex(this.jours, day => day.Equals(matchJours.Value));
			// Get target time
			TimeSpan target = monday.AddDays(dayIndex).AddDays(-(currentDay - 1)) - monday;

			heure = new TimeSpan(int.Parse(match.Value.Split(':')[0]), int.Parse(match.Value.Split(':')[1]), 0);

			// Heure du prochain cours * 24 ajouté à l'heure de la prochaine activité
			heure = heure.Add(new TimeSpan((int)((target).TotalDays * 24), 0, 0));
			Debug.WriteLine(heure.ToString());
			return heure;
		}
	}
}