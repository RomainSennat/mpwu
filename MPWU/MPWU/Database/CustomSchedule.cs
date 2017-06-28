using System;
using SQLite.Net.Attributes;

namespace MPWU.Database
{
	/// <summary>
	/// Custom schedule.
	/// </summary>
	public class CustomSchedule
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		/// <summary>
		/// Gets or sets the monday.
		/// </summary>
		/// <value>The monday.</value>
		public TimeSpan Lundi { get; set; }
		/// <summary>
		/// Gets or sets the tuedsay.
		/// </summary>
		/// <value>The tuedsay.</value>
		public TimeSpan Mardi { get; set; }
		/// <summary>
		/// Gets or sets the wednesday.
		/// </summary>
		/// <value>The wednesday.</value>
		public TimeSpan Mercredi { get; set; }
		/// <summary>
		/// Gets or sets the thursday.
		/// </summary>
		/// <value>The thursday.</value>
		public TimeSpan Jeudi { get; set; }
		/// <summary>
		/// Gets or sets the friday.
		/// </summary>
		/// <value>The friday.</value>
		public TimeSpan Vendredi { get; set; }
		/// <summary>
		/// Gets or sets the saturday.
		/// </summary>
		/// <value>The saturday.</value>
		public TimeSpan Samedi { get; set; }
		/// <summary>
		/// Gets or sets the sunday.
		/// </summary>
		/// <value>The sunday.</value>
		public TimeSpan Dimanche { get; set; }

		public CustomSchedule()
		{
		}
	}
}
