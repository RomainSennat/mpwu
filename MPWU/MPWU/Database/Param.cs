using System;
using MPWU.UserData;

namespace MPWU.Database
{
	public class Param
	{
		public float coordDepartLatitude { get; set; }
		public float coordDepartLongitude { get; set; }

		public float coordArriveLatitude { get; set; }
		public float coordArriveLongitude { get; set; }

		public string adresseDepart { get; set; }
		public string adresseArrive { get; set; }
		public int modeTrajet { get; set; }
		public string tempsTrajet { get; set; }
		public string urlRss { get; set; }


		public Param()
		{
		}
	}
}
