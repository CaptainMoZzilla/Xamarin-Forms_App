using AnrixApp.Models;
using AnrixApp.Services;
using AnrixApp.Views;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Settings;
using System;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AnrixApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupsPage : ContentPage
	{
        public delegate void ListUpdated(Faculty faculty);
        public static event ListUpdated OnListUpdated;

        public GroupsPage ()
		{
            OnListUpdated += delegate (Faculty faculty)
            {
                BindingContext = null;
                BindingContext = faculty.getGroups();
            };
            InitializeComponent();        
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData filedata = await CrossFilePicker.Current.PickFile();
                var faculty = FileReaderService.ReadFromFile(filedata.FilePath);

                await CrossFilePicker.Current.SaveFile(filedata);

                OnListUpdated(faculty);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error occur", $"Something go wrong: {ex.Message}", "Back");
            }
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            var faculty = MockFacultyData.getMockicngFaculty();
            OnListUpdated(faculty);
        }

        private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Group;
            await Navigation.PushAsync(new StudentsPage(content));
        }
    }
}