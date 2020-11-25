using Newtonsoft.Json;
using Rental_Movie_Xamarin.model;
using Rental_Movie_Xamarin.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rental_Movie_Xamarin {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesList: ContentPage {

        private ObservableCollection<MovieListModel> movies = new ObservableCollection<MovieListModel>();

        public MoviesList() {
            InitializeComponent();
            for(int i = 0;i < 26;i++) {
                picker.Items.Add("Started by "+ Convert.ToChar(i+65).ToString());
            }
            picker.SelectedIndexChanged += this.myPickerSelectedIndexChanged;
            
            list.ItemsSource = movies;
            RefreshAsync(0);
        }


        private void myPickerSelectedIndexChanged(object sender, EventArgs e) {
            Picker p = (Picker)sender;
            p.Title = "started by " + Convert.ToChar(p.SelectedIndex + 65);
            RefreshAsync(p.SelectedIndex);
        }

        private async void showDetailMovie(object sender, ItemTappedEventArgs e) {

            MovieListModel ml = e.Item as MovieListModel;
            await Navigation.PushAsync(new MovieDetail(ml.Id));
        }

        private async void FillFilter(object sender, EventArgs e) {
            await Navigation.PushAsync(new FillFilter(this));
        }

        private void AddInCart(object sender, EventArgs e) {
            Button b = (Button)sender;
            MovieListModel ml = b.CommandParameter as MovieListModel;
            if (CartService.AddOrRemoveMovieId(ml)) {
                b.Text = "Remove";
                b.BackgroundColor = Color.Red;
            } else {
                b.Text = "Add";
                b.BackgroundColor = Color.Green;
            }
            btnMyCart.Text = "My cart (" + CartService.GetSize() + ")";
        }

        private void GoToMyCart(object sender, EventArgs e) {
            Navigation.PushAsync(new Cart());
        }


        private async void RefreshAsync(int v) {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            List<MovieListModel> l = await MovieApiClientService.GetMovieListStartedBy(Convert.ToChar(v + 65));
            AddToViewList(l);
            btnMyCart.Text = "My cart (" + CartService.GetSize() + ")";
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        public void AddToViewList(List<MovieListModel> l) {

            movies.Clear();
            countMovieList.Text = l.Count() + " movies";
            noMovieLabel.IsVisible = l.Count == 0;
            
            foreach (var m in l) {
                var ml = new MovieListModel() {
                    Title = m.Title,
                    Price = m.Price,
                    Id = m.Id
                };
                if (CartService.Contains(m)) {
                    ml.BtnText = "Remove";//// added in model btnText and btnColor too UGLY
                    ml.BtnColor = "Red"; //// To change but how ? put expression in xaml ? change btn, but how reach them ?
                }

                movies.Add(ml);
            }
        }
    }
}