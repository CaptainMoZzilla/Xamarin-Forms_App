using AnrixApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnrixApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentDetailPage : ContentPage
	{
		public StudentDetailPage (Student student)
		{
            BindingContext = student;
			InitializeComponent ();
		}
	}
}