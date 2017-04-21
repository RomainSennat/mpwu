using System;
using MPWU.UserData;
using MPWU.EDT;
using MPWU.Database;
using Xamarin.Forms;
using System.Diagnostics;

namespace MPWU
{
	public partial class Parametres : ContentPage
	{
		private GeolocatorPage pageLocalisation;
		private Rss pageFluxRss;

		public Parametres()
		{
			InitializeComponent();
			this.pageLocalisation = new GeolocatorPage();
			this.pageFluxRss = new Rss();
		}

		public void TabChange(object o, EventArgs e)
		{
			SegmentContent.Children.Clear();

			switch (SegmentControl.SelectedSegment)
			{
				case 0:
					SegmentContent.Children.Add(this.pageLocalisation.Content);
					break;
				case 1:
					SegmentContent.Children.Add(this.pageFluxRss.Content);
					break;
			}
		}
		void SaveParam(object sender, EventArgs e)
		{
			Debug.WriteLine("Enregistrement des param");
			new ParamDB().UpdateParam(App.Params);
		}
	}
}