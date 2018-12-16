using Plugin.Settings;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        public delegate void ColorUpdated(Color color);
        public static event ColorUpdated BarColorUpdated;

        private List<string> MainColor = new List<string> { "#3f51b5", "#800080", "#fd6571", "#2cb42c", "#5c6298" };

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

            BarColorUpdated += delegate (Color color)
            {
                FontSlider.ThumbColor = color;

                Separator1.Color = color;
                Separator2.Color = color;
                Separator3.Color = color;

                Stack1.BackgroundColor = color;
                Stack2.BackgroundColor = color;
                Stack3.BackgroundColor = color;
            };

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Toggle.IsToggled = bool.Parse(CrossSettings.Current.GetValueOrDefault("IsSearchBarisVisible", "false"));
            BotToggle.IsToggled = bool.Parse(CrossSettings.Current.GetValueOrDefault("IsBotEnabled", "false"));
            var color = Color.FromHex(CrossSettings.Current.GetValueOrDefault("Color", "000000"));

            FontSlider.ThumbColor = color;

            Separator1.Color = color;
            Separator2.Color = color;
            Separator3.Color = color;

            Stack1.BackgroundColor = color;
            Stack2.BackgroundColor = color;
            Stack3.BackgroundColor = color;
        }               

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("IsSearchBarisVisible", e.Value.ToString());
        }

        private void FontSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("Color", MainColor[Convert.ToInt32(e.NewValue)]);
            CrossSettings.Current.AddOrUpdateValue("ColorVal", e.NewValue);
            BarColorUpdated(Color.FromHex(MainColor[Convert.ToInt32(e.NewValue)]));
        }

        private void BotToggle_Toggled(object sender, ToggledEventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("IsBotEnabled", e.Value.ToString());
            Services.TelegramBot.TelegramBot_OnRecievingUpdate(e.Value);
        }
    }
}