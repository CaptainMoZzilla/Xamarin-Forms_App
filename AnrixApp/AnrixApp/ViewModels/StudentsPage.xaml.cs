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
            megaGroup.Add(new Student("Ведро", "Га га", "gaga",10, "000000"));
            megaGroup.Add(new Student("Анд", "Ы ы ы ы", "gaga", 10, "7234231213"));
            megaGroup.Add(new Student("Андрей", "Андрей", "gaga", 10, "7234231213"));
            megaGroup.Add(new Student("Dada ya", "Андрей", "gaga", 10, "7234231213"));
            megaGroup.Add(new Student("0000000", "Андрей", "gaga", 10, "7234231213"));


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
            var action = await DisplayActionSheet("Sort by", "Back","", "Name [A-Z]", "Group [0-9]", "Surname [A-Z]");
            Debug.WriteLine("Action: " + action);

            BindingContext = null;

            switch (action.ToString())
            {
                case "Name [A-Z]":
                    BindingContext = SortHelper.SortByName(allStudents); ;
                    break;

                case "Group [0-9]":
                    BindingContext = SortHelper.SortBySurname(allStudents); ;
                    break;

                case "Surname [A-Z]":
                    BindingContext = SortHelper.SortByGroup(allStudents); ;
                    break;
            }
        }
    }
}