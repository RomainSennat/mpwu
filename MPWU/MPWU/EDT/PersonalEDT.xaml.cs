using System;
using MPWU.Database;
using Xamarin.Forms;

namespace MPWU.EDT
{
	public partial class PersonalEDT : ContentPage
	{
		private ParamDB paramDB = new ParamDB();

		public PersonalEDT()
		{
			InitializeComponent();
			ScheduleToShow.BindingContext = App.Schedule;
		}

		void SaveSchedule(object sender, EventArgs e)
		{
			this.paramDB.UpdateSchedule(App.Schedule);
		}
	}
}
