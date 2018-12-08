using AnrixApp.Models;
using AnrixApp.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Settings;

namespace AnrixApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentsPage : ContentPage
	{
        public Group allStudents;
   
		public StudentsPage ()
		{
            InitializeComponent();
            GroupsPage.OnListUpdated += delegate (Faculty faculty)
            {
                BindingContext = null;
                BindingContext = allStudents = faculty.getMegaGroup();
            };
        }

        public StudentsPage(Group group)
        {
            InitializeComponent();
            BindingContext = allStudents = group;
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
            var action = await DisplayActionSheet("Sort by", "Back","", "Group [0-9]", "Name [A-Z]", "Surname [A-Z]");

            if (action != null)
            { 
                BindingContext = null;
                switch (action.ToString())
                {
                    case "Name [A-Z]":
                        BindingContext = SortHelper.SortByName(allStudents); ;
                        break;

                    case "Surname [A-Z]":
                        BindingContext = SortHelper.SortBySurname(allStudents); ;
                        break;

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
    }
}