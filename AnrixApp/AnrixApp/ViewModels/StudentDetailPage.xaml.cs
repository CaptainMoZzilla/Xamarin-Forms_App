using AnrixApp.Models;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
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
            var a = GroupsPage.GlobalFaculty;
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
            var a = GroupsPage.GlobalFaculty;
            popupImageView.IsVisible = false;
            CurrentStudent.PhotoUrl = UserLink.Text != null ? UserLink.Text : "big_student_face.png";

            UserLink.Text = null;
            BindingContext = null;
            BindingContext = CurrentStudent;
            UpdateList(a);
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = $"Name: {CurrentStudent.Name}\n" +
                $"Surname: {CurrentStudent.Surname}\n" +
                $"Patronymic: {CurrentStudent.Patronymic}\n" +
                $"Average mark: {CurrentStudent.AverageMark}\n" +
                $"Group: {CurrentStudent.NumberOfGroup}\n"  
                + (CurrentStudent.PhotoUrl.Equals("big_student_face.png") ? 
                "No photo" : $"{CurrentStudent.PhotoUrl}")
            });
        }
    }
}