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
        private Student CurrentStudent;
		public StudentDetailPage (Student student)
		{
            BindingContext = student;
            CurrentStudent = student;
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

        private void Show_Popup(object sender, EventArgs e)
        {
            popupImageView.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var a = GroupsPage.Faculty;
            popupImageView.IsVisible = false;
            CurrentStudent.PhotoUrl = UserLink.Text != null ? UserLink.Text : "big_student_face.png";

            UserLink.Text = null;
            BindingContext = null;
            BindingContext = CurrentStudent;
            UpdateList(a);
        }
    }
}