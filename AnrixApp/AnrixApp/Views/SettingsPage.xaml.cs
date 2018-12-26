using AnrixApp.Services;
using FFImageLoading;
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

        private int ColorVal;
        private object locker = new object();
        private bool CurrenLanguage = "ru".Equals(CrossSettings.Current.GetValueOrDefault("Language", "en"));
        private List<string> MainColor = new List<string> { "#3F51B5", "#673AB7", "#4CAF50", "#009688", "#E91E63", "#00BCD4" };
        private List<string> ColorsName = new List<string> { "Indigo", "Deep purple", "Green", "Teal ", "Pink ", "Cyan" };
        private List<string> Languages = new List<string> { "English", "Русский" };
        
        public SettingsPage ()
		{
			InitializeComponent ();
            ColorVal = CrossSettings.Current.GetValueOrDefault("ColorVal", 0);

            var gestureREcognizer = new TapGestureRecognizer();
            var gestureREcognizer2 = new TapGestureRecognizer();
            var gestureREcognizer3 = new TapGestureRecognizer();
            var gestureREcognizer4 = new TapGestureRecognizer();

            AdminNick.Text = CrossSettings.Current.GetValueOrDefault("AdminsNick", "Anrix_Official"); 

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

                DependencyService.Get<IMessage>().LongTime(
                   BotToggle.IsToggled ?
                   (CurrenLanguage ? "Выпускаем Кракена" : "Now kraken is free")
                  : (CurrenLanguage ? "Ну зачем вы так?:(" : "You save the world"));
            };
            BotsSettings_Stack.GestureRecognizers.Add(gestureREcognizer2);

            gestureREcognizer3.Tapped += (s,e) => {
                CrossSettings.Current.AddOrUpdateValue("Color", MainColor[ColorVal]);
                CrossSettings.Current.AddOrUpdateValue("ColorVal", ColorVal);
                BarColorUpdated(Color.FromHex(MainColor[ColorVal]));
            };
            Frame.GestureRecognizers.Add(gestureREcognizer3);

            gestureREcognizer4.Tapped += async (s, e) =>
            {
                if (!ThemeIcon.Source.ToString().Contains("down_icon.png"))
                {
                    await ThemeStack.TranslateTo(-ThemeStack.Width - 50, 0, 500);
                    ScrollAll.MinimumHeightRequest += 500;
                    OtherDataStack.MinimumHeightRequest += 500;
                    await ScrollAll.TranslateTo(0, -Separator1.Y + 10, 500);

                    ThemeIcon.Source = "down_icon.png";
                    CrossSettings.Current.AddOrUpdateValue("IsMainThemeVisible", false);
                }
                else
                {
                    ScrollAll.MinimumHeightRequest -= 500;
                    OtherDataStack.MinimumHeightRequest -= 500;
                    await ScrollAll.TranslateTo(0, 0, 500);
                    await ThemeStack.TranslateTo(Stack1.X, Stack1.Y, 500);

                    ThemeIcon.Source = "up_icon.png";
                    CrossSettings.Current.AddOrUpdateValue("IsMainThemeVisible", true);
                }
            };
            Stack1.GestureRecognizers.Add(gestureREcognizer4);

            ColorList.ItemsSource = ColorsName;

            BarColorUpdated += delegate (Color color)
            {
                
                Separator1.Color = color;
                Separator2.Color = color;
                Separator3.Color = color;
                Separator4.Color = color;
                Separator5.Color = color;
                Separator6.Color = color;

                ClearButton.BackgroundColor = color;

                Stack1.BackgroundColor = color;
                Stack2.BackgroundColor = color;
                Stack3.BackgroundColor = color;
                Stack4.BackgroundColor = color;
            };
            Picker1.ItemsSource = Languages;
            Picker1.SelectedIndexChanged += OnLanguageChanged;
            Picker1.SelectedIndex = "ru".Equals(CrossSettings.Current.GetValueOrDefault("Language", "English")) ? 1 :0;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("Language", "English".Equals(Picker1.SelectedItem) ? "en" : "ru");
        }

        protected async override void OnAppearing()
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
            Separator6.Color = color;

            ClearButton.BackgroundColor = color;

            Stack1.BackgroundColor = color;
            Stack2.BackgroundColor = color;
            Stack3.BackgroundColor = color;
            Stack4.BackgroundColor = color;

            if (!CrossSettings.Current.GetValueOrDefault("IsMainThemeVisible", true))
            {
                await ThemeStack.TranslateTo(-ThemeStack.Width - 50, 0, 500);
                ScrollAll.MinimumHeightRequest += 500;
                OtherDataStack.MinimumHeightRequest += 500;
                await ScrollAll.TranslateTo(0, -Separator1.Y + 10, 500);
                ThemeIcon.Source = "down_icon.png";
            }
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
                TelegramBot.TelegramBot_OnRecievingUpdate(e.Value);
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

        private void AdminNick_TextChanged(object sender, TextChangedEventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("AdminsNick", e.NewTextValue);
        }
    }
}