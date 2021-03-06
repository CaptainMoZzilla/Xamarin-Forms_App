﻿using AnrixApp.Models;
using AnrixApp.Services;
using AnrixApp.ViewModels;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BotsPage : ContentPage
	{
        private ObservableCollection<StudentRequest> students;
        public delegate void BotListUpdated();
        public static event BotListUpdated OnBotListUpdated;

        public BotsPage ()
		{
            TelegramBot.OnMessagePut += AddStudent;
            OnBotListUpdated += delegate ()
            {
            
                CrossSettings.Current.AddOrUpdateValue("BotsList", JsonConvert.SerializeObject(students, Formatting.Indented));

                if (students.Count == 0)
                {
                    if (StudentsList.IsVisible)
                        StudentsList.IsVisible = false;
                    if (!Animation.IsVisible)
                        Animation.IsVisible = true;
                    if (Animation.IsTabStop)
                        Animation.IsTabStop = false;
                    if (!Animation.IsPlaying)
                        Animation.Play();
                } else if (!StudentsList.IsVisible)
                {
                    Animation.Pause();
                    Animation.IsVisible = false;
                    StudentsList.IsVisible = true;
                }
                BindingContext = students;
            };

            InitializeComponent();

            students = JsonConvert.DeserializeObject<ObservableCollection<StudentRequest>>(CrossSettings.
               Current.GetValueOrDefault("BotsList", "null")) ?? new ObservableCollection<StudentRequest>();
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
            a.AddStudent(student.ToStudent());

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
                try
                {
                    Vibration.Vibrate();
                    Vibration.Vibrate();
                }
                catch (Exception) { }

                students.Add(student);
                OnBotListUpdated();
            });
        }
    }
}