using AnrixApp.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static AnrixApp.ViewModels.GroupsPage;

namespace AnrixApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentDetailPage : ContentPage
	{
		public StudentDetailPage (Student student)
		{
            BindingContext = student;

            try { 
		    	InitializeComponent ();
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            var a = GroupsPage.Faculty;
            a.RemoveStudent(BindingContext as Student);
            UpdateList(a);
            await Navigation.PopAsync();
        }
    }
}