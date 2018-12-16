using AnrixApp.Models;
using AnrixApp.Services;
using AnrixApp.ViewModels;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BotsPage : ContentPage
	{
        private List<StudentRequest> students;
        public delegate void BotListUpdated();
        public static event BotListUpdated OnBotListUpdated;

        public BotsPage ()
		{
            TelegramBot.OnMessagePut += AddStudent;
            OnBotListUpdated += delegate ()
            {
                BindingContext = null;
                BindingContext = students ;
                CrossSettings.Current.AddOrUpdateValue("BotsList", JsonConvert.SerializeObject(students, Formatting.Indented));
            };

            InitializeComponent();

            students = JsonConvert.DeserializeObject<List<StudentRequest>>(CrossSettings.
               Current.GetValueOrDefault("BotsList", "null")) ?? new List<StudentRequest>();
            OnBotListUpdated();

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var student = ((Image)sender).Parent.Parent.BindingContext as StudentRequest;
            students.Remove(student);
            var a = GroupsPage.GlobalFaculty;
            a.AddStudent(student as Student);

            GroupsPage.UpdateList(a);
            OnBotListUpdated();
            await TelegramBot.SendSuccessMessage(student);
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var student = ((Image)sender).Parent.Parent.BindingContext as StudentRequest;
            students.Remove(student);
            OnBotListUpdated();
            await TelegramBot.SendErrorMessage(student);
        }

        private async void StudentsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Student;
            await Navigation.PushAsync(new StudentDetailPage(content));
        }

        private void AddStudent(StudentRequest student)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                students.Add(student);
                OnBotListUpdated();
            });
        }
    }
}