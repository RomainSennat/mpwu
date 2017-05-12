using System;
using MPWU.UserData;
using SQLite.Net.Attributes;

namespace MPWU.Database
{
    public class CustomSchedule
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
        public TimeSpan lundi { get; set; }
        public TimeSpan mardi { get; set; }
        public TimeSpan mercredi { get; set; }
        public TimeSpan jeudi { get; set; }
        public TimeSpan vendredi { get; set; }
        public TimeSpan samedi { get; set; }
        public TimeSpan dimanche { get; set; }

        public CustomSchedule()
        {
        }
    }
}
