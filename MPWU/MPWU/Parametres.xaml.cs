using System;
using MPWU.UserData;
using MPWU.EDT;
using MPWU.Database;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics;

namespace MPWU
{
	public partial class Parametres : ContentPage
	{
		private GeneralSettings pageLocalisation = new GeneralSettings();
		private Rss pageFluxRss = new Rss();
		private ParamDB paramDB = new ParamDB();

		public Parametres()
		{
			InitializeComponent();
		}

		private void TabChange(object o, EventArgs e)
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

		private async void SaveParameters(object sender, EventArgs e)
		{
			this.paramDB.UpdateParam(App.Params);
			try
			{
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var request = new HttpRequestMessage(HttpMethod.Post, "http://speakgame.balastegui.com:4200/alarm/");
				var content = new StringContent("{\"hour\":\"9:35\"}", Encoding.UTF8, "application/json");
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var response = await client.PostAsync("http://speakgame.balastegui.com:4200/alarm/", content);
				Debug.WriteLine(response);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}