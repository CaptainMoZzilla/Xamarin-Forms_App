using AnrixApp.Models;
using AnrixApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.ViewModels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentsPage : ContentPage
	{
        Group allStudents;

		public StudentsPage ()
		{
            Group megaGroup = new Group("000", 0); 

            foreach (var temp in MockFacultyData.getMockicngFaculty().getGroups())
            {
                foreach (var tempS in temp)
                    megaGroup.Add(tempS);
            }

            BindingContext = allStudents = megaGroup;
            InitializeComponent();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Student;
            Debug.WriteLine(content.Name);
            await Navigation.PushAsync(new StudentDetailPage(content));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Sort by", "Back","", "Name [A-Z]", "Name [Z-A]", "Surname [A-Z]", "Surname [Z-A]");
            Debug.WriteLine("Action: " + action);
            //picker.SelectedIndexChanged += picker.Se;


            //TODO сделать сортировку
        }
    }
}