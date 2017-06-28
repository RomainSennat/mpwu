using System;
using SQLite.Net.Attributes;

namespace MPWU.Database
{
	/// <summary>
	/// Parameters.
	/// </summary>
	public class Param
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		/// <summary>
		/// Gets or sets the coordinate depart latitude.
		/// </summary>
		/// <value>The coordinate depart latitude.</value>
		public float CoordDepartLatitude { get; set; }
		/// <summary>
		/// Gets or sets the coordinate depart longitude.
		/// </summary>
		/// <value>The coordinate depart longitude.</value>
		public float CoordDepartLongitude { get; set; }
		/// <summary>
		/// Gets or sets the coordinate arrive latitude.
		/// </summary>
		/// <value>The coordinate arrive latitude.</value>
		public float CoordArriveLatitude { get; set; }
		/// <summary>
		/// Gets or sets the coordinate arrive longitude.
		/// </summary>
		/// <value>The coordinate arrive longitude.</value>
		public float CoordArriveLongitude { get; set; }
		/// <summary>
		/// Gets or sets the adresse depart.
		/// </summary>
		/// <value>The adresse depart.</value>
		public string AdresseDepart { get; set; }
		/// <summary>
		/// Gets or sets the adresse arrive.
		/// </summary>
		/// <value>The adresse arrive.</value>
		public string AdresseArrive { get; set; }
		/// <summary>
		/// Gets or sets the mode trajet.
		/// </summary>
		/// <value>The mode trajet.</value>
		public int ModeTrajet { get; set; }
		/// <summary>
		/// Gets or sets the temps trajet.
		/// </summary>
		/// <value>The temps trajet.</value>
		public string TempsTrajet { get; set; }
		/// <summary>
		/// Gets or sets the preparation time.
		/// </summary>
		/// <value>The preparation time.</value>
		public TimeSpan PreparationTime { get; set; }
		/// <summary>
		/// Gets or sets the URL rss.
		/// </summary>
		/// <value>The URL rss.</value>
		public string UrlRss { get; set; }

		public Param()
		{
		}
	}
}
