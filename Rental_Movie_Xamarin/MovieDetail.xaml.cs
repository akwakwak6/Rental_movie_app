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
    public partial class MovieDetail: ContentPage {

        public MovieDetail(int idFilm) {
            InitializeComponent();
            showMovieDetail(idFilm);
        }

        private async void showMovieDetail(int id) {
            MovieDetailModel md = await MovieApiClientService.GetMovieDetailById(id);
            if(md == null) {
                Console.WriteLine("no detail for ID "+id);
                return;
            }
            Console.WriteLine(md.Description);
            title.Text = md.Title;
            description.Text = md.Description;
            ObservableCollection<Actor> actors = new ObservableCollection<Actor>();
            actorslist.ItemsSource = actors;
            foreach (Actor a in md.Actors) {
                actors.Add(a);
            }
        }
    }
}