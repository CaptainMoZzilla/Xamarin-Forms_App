using AnrixApp.Models;
using AnrixApp.Services;
using System;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Settings;

namespace AnrixApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentsPage : ContentPage
	{
        public static Group allStudents;
        public static string IsSearchVisible = "false";

		public StudentsPage ()
		{
            InitializeComponent();
            GroupsPage.OnListUpdated += delegate (Faculty faculty)
            {
                BindingContext = null;
                Group megaGroup = new Group("000", 0);
                foreach (var temp in faculty.getGroups())
                {
                    foreach (var tempS in temp)
                        megaGroup.Add(tempS);
                }
               
                BindingContext = allStudents = megaGroup;
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchBarStudents.IsVisible = bool.Parse(CrossSettings.Current.GetValueOrDefault("IsSearchBarisVisible", "false"));
        }

        public StudentsPage(Group group)
        {
            InitializeComponent();
            BindingContext = allStudents = group;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Student;
            Debug.WriteLine(content.Name);
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
    }
}