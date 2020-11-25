using Rental_Movie_Xamarin.service;
using System;
using Xamarin.Forms;

namespace Rental_Movie_Xamarin {
    public partial class MainPage: ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            if (MovieApiClientService.IsConnected)
                loginSuccess();
            else
                showBtn();
        }

        private void showBtn() {
            if (CartService.GetSize() > 0) {
                btn_cart.IsVisible = true;
                btn_cart.Text = "My cart (" + CartService.GetSize() + ")";
            }else
                btn_cart.IsVisible = false;

            if (MovieApiClientService.IsConnected) {
                btn_order.IsVisible = true;
            } else
                btn_order.IsVisible = false;
        }

        private void showMyOrder(object sender, EventArgs e) {
            Navigation.PushAsync(new Order());
        }




        private async void logIn(object sender, EventArgs e) {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            bool ret = await MovieApiClientService.LogIn(mail.Text, pwd.Text);
            spinner.IsRunning = false;
            spinner.IsVisible = false;
            if ( ret ) 
                loginSuccess();
            else 
                LoginError();
            
        }

        private void loginSuccess() {
            mainLabel.Text = " Hello " + MovieApiClientService.User.FirstName;
            mail.IsVisible = false;
            pwd.IsVisible = false;
            btn_login_in.IsVisible = false;
            btn_register.IsVisible = false;
            showBtn();
        }

        private async void LoginError() {
            await DisplayAlert("Login", "Login error", "OK");
            mail.Text = null;
            pwd.Text = null;
            mail.Focus();
        }

        private void showMyCart(object sender, EventArgs args) {
            Navigation.PushAsync(new Cart());
        }

        private void MoviesButtton(object sender, EventArgs args) {
            Navigation.PushAsync(new MoviesList());
        }  
        
        private void RegisterButtton(object sender, EventArgs args) {
            Navigation.PushAsync(new Register());
        }
    }
}
