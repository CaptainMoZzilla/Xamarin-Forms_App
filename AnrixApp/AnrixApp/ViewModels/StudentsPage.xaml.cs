using AnrixApp.Models;
using AnrixApp.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Settings;
using static AnrixApp.ViewModels.GroupsPage;
using AnrixApp.Views;

namespace AnrixApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentsPage : ContentPage
	{
        public Group allStudents;
        private bool CurrenLanguage = "ru".Equals(CrossSettings.Current.GetValueOrDefault("Language", "en"));
        public StudentsPage ()
		{
            InitializeComponent();
            ToolbarItems.Remove(ThirdBarItem);
            ToolbarItems.Remove(ScheduleItem);
            OnListUpdated += delegate (Faculty faculty)
            {
                BindingContext = null;
                BindingContext = allStudents = faculty.GetMegaGroup();
            };
        }

        public StudentsPage(Group group)
        {
            InitializeComponent();
            BindingContext = allStudents = group;
            Title = group.NumberOfGroup;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchBarStudents.IsVisible = bool.Parse(CrossSettings.Current.GetValueOrDefault("IsSearchBarisVisible", "false"));
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Student;
            await Navigation.PushAsync(new StudentDetailPage(content));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var action =  CurrenLanguage ?
                await DisplayActionSheet("Отсортировать по", "Назад", "", "Группе [0-9]", "Имеми [A-Z]", "Фамилии [A-Z]") :
                await DisplayActionSheet("Sort by", "Back", "", "Group [0-9]", "Name [A-Z]", "Surname [A-Z]");

            if (action != null)
            { 
                BindingContext = null;
                switch (action.ToString())
                {
                    case "Имеми [A-Z]":
                    case "Name [A-Z]":
                        BindingContext = SortHelper.SortByName(allStudents); ;
                        break;

                    case "Фамилии [A-Z]":
                    case "Surname [A-Z]":
                        BindingContext = SortHelper.SortBySurname(allStudents); ;
                        break;

                    case "Группе [0-9]":
                    case "Group [0-9]":
                        BindingContext = SortHelper.SortByGroup(allStudents); ;
                        break;
                }
            }
        }

        private void SearchBarStudents_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingContext = null;
            if (e.NewTextValue.Length > 0)
            {
                BindingContext = SortHelper.Search(allStudents, e.NewTextValue);
            } else
            {
                BindingContext = allStudents;
            }    
        }

        private async void ThirdBarItem_Clicked(object sender, EventArgs e)
        {
            var a = GlobalFaculty;
            a.Remove(allStudents);
            UpdateList(a);

            await Navigation.PopAsync();
        }

        private void ScheduleItem_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new SchedulePage(Title));

        }
    }
}