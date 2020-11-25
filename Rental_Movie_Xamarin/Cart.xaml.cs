using Rental_Movie_Xamarin.model;
using Rental_Movie_Xamarin.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rental_Movie_Xamarin {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart: ContentPage {

        private ObservableCollection<MovieListModel> movies = new ObservableCollection<MovieListModel>();

        public Cart() {
            InitializeComponent();
            list.ItemsSource = movies;
            ShowMovies();
        }

        private void ShowMovies() {

            IEnumerable<MovieListModel> l = CartService.GetMoviesList();

            decimal total = 0;
            movies.Clear();
            foreach (MovieListModel m in l) {
                var ml = new MovieListModel() {
                    Title = m.Title,
                    Price = m.Price,
                    Id = m.Id
                };
                movies.Add(ml);
                total+=m.Price;
            }

            totalValue.Text = total + " $ ";
        } 

        private void OnRemove(object sender, EventArgs e) {
            Button b = (Button)sender;
            MovieListModel ml = b.CommandParameter as MovieListModel;
            CartService.RemoveMovie(ml);
            ShowMovies();

        }

        private async void OnBtnOk(object sender, EventArgs e) {
            if( MovieApiClientService.IsConnected) {
                Console.WriteLine("send rent");
                if( await MovieApiClientService.SendRental() ) {
                    CartService.EmptyCart();
                    await DisplayAlert("Confirm", "Thanks for your order", "OK");
                    await Navigation.PopToRootAsync();
                } else {
                    await DisplayAlert("Error", "Sorry an error happened during ordering, please contact our customer service", "OK");
                }
            } else {
                await DisplayAlert("Not connected", "Please log in to rent movies", "OK");
                await Navigation.PopToRootAsync();
            }
        }


    }
}