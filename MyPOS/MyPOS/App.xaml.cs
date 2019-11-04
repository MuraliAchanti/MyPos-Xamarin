using MyPOS.Helpers;
using MyPOS.ViewModels;
using MyPOS.Views;
using Xamarin.Forms;

namespace MyPOS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (!Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage(new LoginViewModel()));
            }
            else
            {
                GoToMainPage();
            }
        }
        public static void GoToMainPage()
        {
            Current.MainPage = new MainPage();
        }
        public static void GoToLoginPage()
        {
            Current.MainPage = new NavigationPage(new LoginPage(new LoginViewModel()));
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
