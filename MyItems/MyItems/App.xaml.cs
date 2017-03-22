using System.Collections.Generic;

using MyItems.Helpers;
using MyItems.Services;
using MyItems.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyItems
{
    public partial class App : Application
    {
        //MUST use HTTPS, neglecting to do so will result in runtime errors on iOS
        public static bool AzureNeedsSetup => AzureMobileAppUrl == "https://CONFIGURE-THIS-URL.azurewebsites.net";
        public static string AzureMobileAppUrl = "https://CONFIGURE-THIS-URL.azurewebsites.net";
        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();

            if (AzureNeedsSetup)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (!AzureNeedsSetup && !Settings.IsLoggedIn && AzureDataStore.UseAuthentication)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
        }

        public static void GoToMainPage()
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                Current.MainPage = new TabbedPage
                {
                    Children =
                    {
                        new NavigationPage(new ItemsPage())
                        {
                            Title = "Browse",
                            Icon = Device.OnPlatform("tab_feed.png",null,null)
                        },
                        new NavigationPage(new AboutPage())
                        {
                            Title = "About",
                            Icon = Device.OnPlatform("tab_about.png",null,null)
                        },
                    }
                };
            }
            else
            {
                Current.MainPage = new NavigationPage(new TabbedPage
                {
                    Title = "My Items",
                    Children =
                    {
                        new ItemsPage()
                        {
                            Title = "Browse",
                            Icon = Device.OnPlatform("tab_feed.png",null,null)
                        },
                        new AboutPage()
                        {
                            Title = "About",
                            Icon = Device.OnPlatform("tab_about.png",null,null)
                        },
                    }
                });
            }


        }
    }
}
