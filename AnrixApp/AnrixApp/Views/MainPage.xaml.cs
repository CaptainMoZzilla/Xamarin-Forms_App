using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using static AnrixApp.Views.SettingsPage;

namespace AnrixApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetBarSelectedItemColor((Color)Xamarin.Forms.Application.Current.Resources["ToolbarColor"]);

            InitializeComponent();

            BarColorUpdated += delegate (Color color)
            {
                Page1.BarBackgroundColor = color;
                Page2.BarBackgroundColor = color;
                Page3.BarBackgroundColor = color;

                On<Android>().SetBarSelectedItemColor(color);
            };
        }
    }
}