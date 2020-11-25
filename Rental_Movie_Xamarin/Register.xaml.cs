using Rental_Movie_Xamarin.model;
using Rental_Movie_Xamarin.service;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rental_Movie_Xamarin {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register: ContentPage {
        public Register() {
            InitializeComponent();
        }

        private async void OnRegister(object sender, EventArgs args) {

            if (String.IsNullOrEmpty(firstName.Text)) {
                await DisplayAlert("error in firstName", "Your firstName is empty", "OK");
                firstName.Focus();
                return;
            }

            if (String.IsNullOrEmpty(lastName.Text)) {
                await DisplayAlert("error in lastname", "Your lastname is empty", "OK");
                lastName.Focus();
                return;
            }

            if (String.IsNullOrEmpty(mail.Text)) {
                await DisplayAlert("error in mail", "Your mail is empty", "OK");
                mail.Focus();
                return;
            }

            if (String.IsNullOrEmpty(pwd.Text) || String.IsNullOrEmpty(pwd2.Text)) {
                await DisplayAlert("error in password", "Your password is empty", "OK");
                pwd.Focus();
                return;
            }

            if ( pwd.Text != pwd2.Text ) {
                await DisplayAlert("error in passwords", "Your passwords are not the same", "OK");
                pwd.Focus();
                return;
            }

            spinner.IsVisible = true;
            spinner.IsRunning = true;

            User user = new User() {
                Email = mail.Text,
                Password = pwd.Text,
                FirstName = firstName.Text,
                LastName = lastName.Text
            };

            if (await MovieApiClientService.Register(user)) {
                await MovieApiClientService.LogIn(user.Email,user.Password);
                await Navigation.PopToRootAsync();
            } else
                await DisplayAlert("Error", "Sorry, an arror in your register", "OK");

            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }
    }
}