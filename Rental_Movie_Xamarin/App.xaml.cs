
using Rental_Movie_Xamarin.service;
using Xamarin.Forms;

namespace Rental_Movie_Xamarin {
    partial class App: Application {

        public App() {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
