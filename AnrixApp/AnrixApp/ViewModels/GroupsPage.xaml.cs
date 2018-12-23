using AnrixApp.Models;
using AnrixApp.Services;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupsPage : ContentPage
	{
        public delegate void ListUpdated(Faculty faculty);
        public static event ListUpdated OnListUpdated;
        public static Faculty GlobalFaculty;
        private static string PATH_TO_DOCS = "storage/emulated/0/Documents/Base.json";

        public GroupsPage ()
		{
            OnListUpdated += delegate (Faculty faculty)
            {
               BindingContext = null;
               GlobalFaculty = faculty;
               BindingContext = faculty.GetGroups();
            };
            InitializeComponent();        
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var CurrenLanguage = "ru".Equals(CrossSettings.Current.GetValueOrDefault("Language", "en"));
            try
            {
                FileData filedata = await CrossFilePicker.Current.PickFile();
                GlobalFaculty = FileReaderService.ReadFromFile(filedata.FilePath);

                OnListUpdated(GlobalFaculty);
            }
            catch (Exception ex)
            {
                await DisplayAlert(CurrenLanguage ? "Произошла ошибка!" : "Error occurred!"
                     , CurrenLanguage ? $"Что-то пошло не так: {ex.Message}"
                     : $"Something go wrong: {ex.Message}"
                     , "Ok");
            }
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Group;
            await Navigation.PushAsync(new StudentsPage(content));
        }

        public static void UpdateList(Faculty faculty) => OnListUpdated(faculty);

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            var CurrenLanguage = "ru".Equals(CrossSettings.Current.GetValueOrDefault("Language", "en"));

            try
            { 
                var fs = new FileStream(PATH_TO_DOCS, FileMode.Create);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(JsonConvert.SerializeObject(GlobalFaculty.GetMegaGroup().GetStudents(), Formatting.Indented));
                sr.Close();
                fs.Close();

                await DisplayAlert(CurrenLanguage ? "Успешно" : "Success"
                    , CurrenLanguage ? "Сохранено в документы" : "Saved to docs", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert(CurrenLanguage ? "Произошла ошибка!" : "Error occurred!"
                        , CurrenLanguage ? $"Что-то пошло не так: {ex.Message}"
                        : $"Something go wrong: {ex.Message}"
                        , "Ok");
            }
        }

        private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            try { 
                FileData filedata = await CrossFilePicker.Current.PickFile();
                var fs = new FileStream(filedata.FilePath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                GlobalFaculty = new Faculty(JsonConvert.DeserializeObject<List<Student>>(await sr.ReadToEndAsync()));

                OnListUpdated(GlobalFaculty);
            }
            catch (Exception ex)
            {
                var CurrenLanguage = "ru".Equals(CrossSettings.Current.GetValueOrDefault("Language", "en"));
                await DisplayAlert(CurrenLanguage ? "Произошла ошибка!" : "Error occurred!"
                        , CurrenLanguage ? $"Что-то пошло не так: {ex.Message}"
                        : $"Something go wrong: {ex.Message}"
                        , "Ok");
            }
        }
    }
}