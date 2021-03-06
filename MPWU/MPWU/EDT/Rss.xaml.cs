using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace MPWU.EDT
{
	/// <summary>
	/// Rss.
	/// </summary>
	public partial class Rss : ContentPage
	{
		/// <summary>
		/// The days.
		/// </summary>
		private readonly string[] jours = { "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi", "dimanche" };
		/// <summary>
		/// The Rss data.
		/// </summary>
		private RSSData data = new RSSData();

		public Rss()
		{
			InitializeComponent();
			ParamToShow.BindingContext = App.Params;
		}

		/// <summary>
		/// Datas from Rss.
		/// </summary>
		/// <returns>Rss data.</returns>
		/// <param name="url">URL.</param>
		public async Task<RSSData> DataFromRssAsync(string url)
		{
			TimeSpan heure = new TimeSpan();
			HttpClient client = new HttpClient();
			var request = client.GetAsync(new Uri(url));
			var result = request.Result.Content;
			XDocument xml = XDocument.Parse(await result.ReadAsStringAsync());

			// Init de la liste des éléments qui correpondent au titre du flux rss
			var list = xml.Descendants("item").Cast<XElement>().ToList();

			//Regex heures
			Regex regexHeure = new Regex("(([0-1]){1,}([0-9]{1,})|(2[0-3]))(:)([0-5]{1}[0-9]{1})");
			Match matchHeure = regexHeure.Match(list.FirstOrDefault().Value);

			//Regex jours
			Regex regexJour = new Regex("lundi|mardi|mercredi|jeudi|vendredi|samedi|dimanche");
			Match matchJour = regexJour.Match(list.FirstOrDefault().Value);

			// Init de la variable du return
			heure = new TimeSpan(int.Parse(matchHeure.Value.Split(':')[0]), int.Parse(matchHeure.Value.Split(':')[1]), 0);

			// Init à 1, car on a déjà la valeur 0
			int i = 1;
			// Get next match if needed
			while (i < list.Count() && (int.Parse(matchHeure.Value.Split(':')[0]) > 12 || ((int)DateTime.Today.DayOfWeek - 1 == Array.IndexOf(this.jours, matchJour.Value) && (int.Parse(matchHeure.Value.Split(':')[0]) <= DateTime.Now.Hour && int.Parse(matchHeure.Value.Split(':')[1]) <= DateTime.Now.Minute))))
			{
				matchHeure = regexHeure.Match(list.ElementAtOrDefault(i).Value);
				matchJour = regexJour.Match(list.ElementAtOrDefault(i).Value);
				i++;
			}
			// i - 1 because i is incremented on previous loop or 1 was affected before
			TitleActivity(list.Descendants("title").ElementAtOrDefault(i - 1).Value);

			// Get current day
			DateTime today = DateTime.Today;
			// Get current day index in week
			int currentDay = (int)today.DayOfWeek;
			// Get monday on current week
			DateTime monday = today.AddDays(-(DateTime.Now.DayOfWeek - CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) % 7);
			// Get range between monday and target day
			int dayIndex = Array.IndexOf(this.jours, matchJour.Value);
			// Get target time
			TimeSpan target = monday.AddDays(dayIndex).AddDays(-(currentDay - 1)) - monday;

			heure = new TimeSpan(int.Parse(matchHeure.Value.Split(':')[0]), int.Parse(matchHeure.Value.Split(':')[1]), 0);

			// If alarm is next week add 7 days
			if ((int)target.TotalDays < 0)
			{
				target = target.Add(new TimeSpan(7 * 24, 0, 0));
			}
			// Heure du prochain cours * 24 ajouté à l'heure de la prochaine activité
			heure = heure.Add(new TimeSpan((int)((target).TotalDays * 24), 0, 0));
			this.data.Hour = heure;
			return this.data;
		}

		/// <summary>
		/// Update Rss data with title
		/// </summary>
		/// <param name="element">Element.</param>
		private void TitleActivity(string element)
		{
			this.data.Title = element.Split(':')[3].Substring(1);
		}
	}
}
