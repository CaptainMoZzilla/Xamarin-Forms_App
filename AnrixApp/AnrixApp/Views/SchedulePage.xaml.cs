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
        private string BASE_UI = "https://iis.bsuir.by/#/schedule;groupName=";

        public SchedulePage ()
		{
			InitializeComponent ();
		}

        public SchedulePage(string groupNumber)
        {
            InitializeComponent();
            WebView1.Source = BASE_UI + groupNumber;
            //WebView1.Reload();
        }
	}
}