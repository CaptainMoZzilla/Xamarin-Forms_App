using Plugin.Settings;
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
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
            var gestureREcognizer = new TapGestureRecognizer();

            gestureREcognizer.Tapped += (s, e) =>
            {
                Switch_Toggled(s, new ToggledEventArgs(!Toggle.IsToggled));
                Toggle.IsToggled = !Toggle.IsToggled;
            };
            SearchLine_Stack.GestureRecognizers.Add(gestureREcognizer);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Toggle.IsToggled = bool.Parse(CrossSettings.Current.GetValueOrDefault("IsSearchBarisVisible", "false"));
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("IsSearchBarisVisible", e.Value.ToString());
        }
    }
}