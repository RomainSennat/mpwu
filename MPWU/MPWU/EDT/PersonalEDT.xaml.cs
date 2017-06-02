using System;
using System.Collections.Generic;
using System.Diagnostics;
using MPWU.Database;
using Xamarin.Forms;

namespace MPWU
{
	public partial class PersonalEDT : ContentPage
	{
        private ParamDB paramDB = new ParamDB();

		public PersonalEDT()
		{
			InitializeComponent();
            ScheduleToShow.BindingContext = App.Schedule;
        }

        void SaveButton(object sender, EventArgs e)
        {
            Debug.WriteLine("Enregistrement du CustomSchedule");
            this.paramDB.AddSchedule(App.Schedule);
        }
    }
}
