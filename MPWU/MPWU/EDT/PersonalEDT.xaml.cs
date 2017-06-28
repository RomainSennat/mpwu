using System;
using MPWU.Database;
using Xamarin.Forms;

namespace MPWU.EDT
{
	/// <summary>
	/// Personal edt.
	/// </summary>
	public partial class PersonalEDT : ContentPage
	{
		/// <summary>
		/// The parameter database.
		/// </summary>
		private ParamDB paramDB = new ParamDB();

		public PersonalEDT()
		{
			InitializeComponent();
			ScheduleToShow.BindingContext = App.Schedule;
		}

		/// <summary>
		/// Saves the schedule.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void SaveSchedule(object sender, EventArgs e)
		{
			this.paramDB.UpdateSchedule(App.Schedule);
		}
	}
}
