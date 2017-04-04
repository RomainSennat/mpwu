using System;
using MPWU.UserData;

namespace MPWU.Database
{
	public class Param
	{
		public Coord coordDepart { get; set; }
		public Coord coordArrive { get; set; }
		public string adresseDepart { get; set; }
		public string adresseArrive { get; set; }
		public string tempsTrajet { get; set; }
		public string urlRss { get; set; }


		public Param()
		{
		}
	}
}
