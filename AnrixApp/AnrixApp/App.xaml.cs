using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnrixApp.Views;
using AnrixApp.Models;
using static AnrixApp.ViewModels.GroupsPage;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using AnrixApp.Services;
using System;
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
         
            OnListUpdated += delegate (Faculty faculty2)
            {
                faculty = faculty2;
                CrossSettings.Current.AddOrUpdateValue("Faculty", JsonConvert.SerializeObject(faculty2.getMegaGroup().getStudents(), Formatting.Indented));
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
