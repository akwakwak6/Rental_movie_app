using Rental_Movie_Xamarin.model;
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
    public partial class OrderMovies: ContentPage {

        private ObservableCollection<RentalMovie> _movies = new ObservableCollection<RentalMovie>();

        public OrderMovies(IEnumerable<RentalMovie> movies,string orderTitle ) {
            InitializeComponent();
            ShowMovies(movies);
            title.Text = orderTitle;
            list.ItemsSource = _movies;
        }

        private void ShowMovies(IEnumerable<RentalMovie> movies) {

            foreach(RentalMovie rm in movies) {
                _movies.Add(rm);
            }

        }
    }
}