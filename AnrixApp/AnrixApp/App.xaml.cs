using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnrixApp.Views;
using AnrixApp.Models;
using static AnrixApp.ViewModels.GroupsPage;
using Newtonsoft.Json;
using AnrixApp.Services;
using Plugin.Settings;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AnrixApp
{
    public partial class App : Application
    {
        static Faculty faculty = new Faculty();

        public App()
        {
            InitializeComponent();
            Current.Resources["ToolbarColor"] = Color.FromHex(CrossSettings.Current.GetValueOrDefault("Color", "#3f51b5"));
            MainPage = new MainPage();

            string list = CrossSettings.Current.GetValueOrDefault("Faculty", "null");
            if (!"null".Equals(list))
                UpdateList(new Faculty(JsonConvert.DeserializeObject<List<Student>>(list)));

            TelegramBot.TelegramBot_OnRecievingUpdate(bool.Parse(CrossSettings.
                Current.GetValueOrDefault("IsBotEnabled", "false")));

            OnListUpdated += delegate (Faculty faculty2)
            {
                faculty = faculty2;
                CrossSettings.Current.AddOrUpdateValue("Faculty", JsonConvert.SerializeObject(faculty2.GetMegaGroup().GetStudents(), Formatting.Indented));
            };
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
