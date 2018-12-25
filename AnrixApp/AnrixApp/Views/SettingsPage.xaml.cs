using FFImageLoading;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        public delegate void ColorUpdated(Color color);
        public static event ColorUpdated BarColorUpdated;

        private int ColorVal;
        private List<string> MainColor = new List<string> { "#3f51b5", "#800080", "#fd6571", "#2cb42c", "#5c6298", "#ffb0b0" };
        private List<string> ColorsName = new List<string> { "Classic", "Blue", "Red", "Cyan", "Green", "Pink" };
        private List<string> Languages = new List<string> { "English", "Русский" };

		public SettingsPage ()
		{
			InitializeComponent ();
            ColorVal = CrossSettings.Current.GetValueOrDefault("ColorVal", 0);

            var gestureREcognizer = new TapGestureRecognizer();
            var gestureREcognizer2 = new TapGestureRecognizer();
            var gestureREcognizer3 = new TapGestureRecognizer();


            gestureREcognizer.Tapped += (s, e) =>
            {
                Switch_Toggled(s, new ToggledEventArgs(!Toggle.IsToggled));
                Toggle.IsToggled = !Toggle.IsToggled;
            };
            SearchLine_Stack.GestureRecognizers.Add(gestureREcognizer);

            gestureREcognizer2.Tapped += (s, e) =>
            {
                BotToggle_Toggled(s, new ToggledEventArgs(!BotToggle.IsToggled));
                BotToggle.IsToggled = !BotToggle.IsToggled;
            };
            BotsSettings_Stack.GestureRecognizers.Add(gestureREcognizer2);

            gestureREcognizer3.Tapped += (s,e) => {
                CrossSettings.Current.AddOrUpdateValue("Color", MainColor[ColorVal]);
                CrossSettings.Current.AddOrUpdateValue("ColorVal", ColorVal);
                BarColorUpdated(Color.FromHex(MainColor[ColorVal]));
            };
            Frame.GestureRecognizers.Add(gestureREcognizer3);

            ColorList.ItemsSource = ColorsName;

            BarColorUpdated += delegate (Color color)
            {
                
                Separator1.Color = color;
                Separator2.Color = color;
                Separator3.Color = color;
                Separator4.Color = color;
                Separator5.Color = color;

                ClearButton.BackgroundColor = color;

                Stack1.BackgroundColor = color;
                Stack2.BackgroundColor = color;
                Stack3.BackgroundColor = color;
            };
            Picker1.ItemsSource = Languages;
            Picker1.SelectedIndexChanged += OnLanguageChanged;
            Picker1.SelectedIndex = "ru".Equals(CrossSettings.Current.GetValueOrDefault("Language", "English")) ? 1 :0;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("Language", "English".Equals(Picker1.SelectedItem) ? "en" : "ru");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Toggle.IsToggled = bool.Parse(CrossSettings.Current.GetValueOrDefault("IsSearchBarisVisible", "false"));
            BotToggle.IsToggled = bool.Parse(CrossSettings.Current.GetValueOrDefault("IsBotEnabled", "false"));
            var color = Color.FromHex(CrossSettings.Current.GetValueOrDefault("Color", "000000"));

            Separator1.Color = color;
            Separator2.Color = color;
            Separator3.Color = color;
            Separator4.Color = color;
            Separator5.Color = color;

            ClearButton.BackgroundColor = color;

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

        private async void ClearButton_Clicked(object sender, EventArgs e)
        {
            var CurrenLanguage = "ru".Equals(CrossSettings.Current.GetValueOrDefault("Language", "en"));
            var a = await DisplayAlert(CurrenLanguage ? "Внимание!" :"Attention!" 
                        , CurrenLanguage ? "Возможно неполное удаление, т.к. ListView имеет собственный кэш" 
                        : "Perhaps incomplete removal, because ListView has own cache"
                        , CurrenLanguage ? "Удалить" : "Clear"
                        , CurrenLanguage ? "Назад" : "Back");
            if (a)
            {
                await ImageService.Instance.InvalidateDiskCacheAsync();
                ImageService.Instance.InvalidateMemoryCache();
            }
        }

        private void ColorList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ColorVal = ColorsName.IndexOf(e.Item as string);
            Frame.BackgroundColor = (Color.FromHex(MainColor[ColorVal]));
        }
    }
}