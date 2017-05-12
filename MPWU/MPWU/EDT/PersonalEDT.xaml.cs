using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace MPWU
{
	public partial class PersonalEDT : ContentPage
	{
		public PersonalEDT()
		{
			InitializeComponent();
        }

        void SaveButton(object sender, EventArgs e)
        {
            Debug.WriteLine("click");
        }
    }
}
