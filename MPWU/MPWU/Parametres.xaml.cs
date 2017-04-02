using System;
using MPWU.UserData;
using MPWU.EDT;
using Xamarin.Forms;

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

		public void Handle_ValueChanged(object o, EventArgs e)
		{
			SegContent.Children.Clear();

			switch (SegControl.SelectedSegment)
			{
				case 0:
					SegContent.Children.Add(this.pageLocalisation.Content);
					break;
				case 1:
					SegContent.Children.Add(this.pageFluxRss.Content);
					break;
			}
		}

		public void Handle_ValueChangedJourney(object o, EventArgs e)
		{

		}
	}
}