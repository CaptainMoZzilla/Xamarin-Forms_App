using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SchedulePage : ContentPage
	{
        private string GroupNumber;
        private string BASE_UI = "https://iis.bsuir.by/#/schedule;groupName=";

        public SchedulePage ()
		{
			InitializeComponent ();
		}

        public SchedulePage(string groupNumber)
        {
            InitializeComponent();
            WebView1.Source = BASE_UI + groupNumber;
            GroupNumber = groupNumber;
        }

        private void WebView1_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Title = GroupNumber;
            Animation.Pause();
            Animation.IsVisible = false;
            WebView1.IsVisible = true;
        }

        private void WebView1_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Title = "Loading...";
                WebView1.IsVisible = false;
            if (!Animation.IsVisible)
                Animation.IsVisible = true;
            if (Animation.IsTabStop)
                Animation.IsTabStop = false;
            if (!Animation.IsPlaying)
                Animation.Play();
        }
    }
}