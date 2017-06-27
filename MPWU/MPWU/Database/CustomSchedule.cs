using System;
using SQLite.Net.Attributes;

namespace MPWU.Database
{
	public class CustomSchedule
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public TimeSpan Lundi { get; set; }
		public TimeSpan Mardi { get; set; }
		public TimeSpan Mercredi { get; set; }
		public TimeSpan Jeudi { get; set; }
		public TimeSpan Vendredi { get; set; }
		public TimeSpan Samedi { get; set; }
		public TimeSpan Dimanche { get; set; }

		public CustomSchedule()
		{
		}
	}
}
