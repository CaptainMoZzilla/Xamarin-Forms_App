using AnrixApp.Models;
using AnrixApp.Services;
using System;
using System.Collections.Generic;
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
		public StudentsPage ()
		{
            Group megaGroup = new Group("000", 0); 

            foreach (var temp in MockFacultyData.getMockicngFaculty().getGroups())
            {
                foreach (var tempS in temp)
                    megaGroup.Add(tempS);
            }

            BindingContext = megaGroup;
			InitializeComponent ();
		}
	}
}