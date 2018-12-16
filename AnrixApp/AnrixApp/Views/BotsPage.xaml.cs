using AnrixApp.Models;
using AnrixApp.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BotsPage : ContentPage
	{
        private List<Student> students = new List<Student>();

		public BotsPage ()
		{
            GroupsPage.OnListUpdated += delegate (Faculty faculty)
            {
                BindingContext = null; 
                BindingContext = GroupsPage.GlobalFaculty.GetMegaGroup();
            };
           
            InitializeComponent();
		}

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}