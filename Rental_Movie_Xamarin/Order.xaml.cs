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
    public partial class Order: ContentPage {

        private ObservableCollection<OrderModel> orders = new ObservableCollection<OrderModel>();

        public Order() {
            InitializeComponent();
            list.ItemsSource = orders;
            GetOrder();
        }

        private void showDetailOrder(object sender, ItemTappedEventArgs e) {
            OrderModel om = e.Item as OrderModel;
            Navigation.PushAsync(new OrderMovies(om.Movies,om.ToString()));
        }

        private async void GetOrder() {
            spinner.IsVisible = true;
            spinner.IsRunning = true;

            List<OrderModel> l = await MovieApiClientService.GetMyOrder();
            foreach (var m in l) {

                var ml = new OrderModel() {
                    Id = m.Id,
                    TotalPrice = m.TotalPrice,
                    Date = m.Date,
                    Movies = m.Movies
                };
                orders.Add(ml);
            }

            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }
    }
}