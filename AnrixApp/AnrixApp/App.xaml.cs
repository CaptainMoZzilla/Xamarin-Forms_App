using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnrixApp.Views;
using AnrixApp.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AnrixApp
{
    public partial class App : Application
    {
        static Faculty faculty = new Faculty();

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
