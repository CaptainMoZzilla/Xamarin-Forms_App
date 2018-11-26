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
	public partial class GroupsPage : ContentPage
	{
		public GroupsPage ()
		{
            BindingContext = MockFacultyData.getMockicngFaculty().getGroups();
            InitializeComponent(); //TODO добавить страницу группы 
        }
    }
}