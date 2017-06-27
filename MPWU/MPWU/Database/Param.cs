using System;
using SQLite.Net.Attributes;

namespace MPWU.Database
{
	public class Param
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public float CoordDepartLatitude { get; set; }
		public float CoordDepartLongitude { get; set; }
		public float CoordArriveLatitude { get; set; }
		public float CoordArriveLongitude { get; set; }
		public string AdresseDepart { get; set; }
		public string AdresseArrive { get; set; }
		public int ModeTrajet { get; set; }
		public string TempsTrajet { get; set; }
		public TimeSpan PreparationTime { get; set; }
		public string UrlRss { get; set; }

		public Param()
		{
		}
	}
}
