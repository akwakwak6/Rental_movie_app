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
    public partial class FillFilter: ContentPage {


        private ObservableCollection<Actor> actors = new ObservableCollection<Actor>();
        private MoviesList _movieList;

        public FillFilter(MoviesList ml) {
            _movieList = ml;
            InitializeComponent();
            loadCategory();
            loadLangage();
            loadActors();
        }


        private async void loadLangage() {
            List<Langage> langageList = await MovieApiClientService.GetObjetc<List<Langage>>("language");
            if (langageList == null) return;
            pickerLangage.Items.Add("All langage");
            langageList.ForEach(c => pickerLangage.Items.Add(c.Name));
            pickerLangage.SelectedIndex = 0;
        }
        

        private void loadActors() {
            for (int i = 0;i < 26;i++) {
                picActorStartBy.Items.Add(Convert.ToChar(i + 65).ToString());
            }
            picActorStartBy.SelectedIndex = 0;
            picActorStartBy.SelectedIndexChanged += this.loadActorStartedBy;
            picActorName.ItemsSource = actors;
            SetActorInPicker(0);
        }

        private async void SetActorInPicker(int v) {
            List<Actor> actorList = await MovieApiClientService.GetObjetc<List<Actor>>("actor/" + Convert.ToChar(v + 65));
            actors.Clear();
            actors.Add(new Actor() { FirstName = "ACTORS", LastName = "ALL", Id = 0 });
            picActorName.SelectedIndex = 0;
            actorList.ForEach(a => actors.Add(a));
        }

        private void loadActorStartedBy(object sender, EventArgs e) {
            Picker p = (Picker)sender;
            SetActorInPicker(p.SelectedIndex);
        }

        private async void loadCategory() {
            List<Category> catList = await MovieApiClientService.GetObjetc<List<Category>>("category");
            if (catList == null) return;
            pickerCategory.Items.Add("All category");
            catList.ForEach(c => pickerCategory.Items.Add(c.Name));
            pickerCategory.SelectedIndex = 0;
        }


        private async void GetMovieListFilted(object sender, EventArgs e) {

            int index = pickerCategory.SelectedIndex;
            int? cat = null;
            if (index != 0)
                cat = index;

            index = pickerLangage.SelectedIndex;
            int? language = null;
            if (index != 0)
                language = index;

            index = picActorName.SelectedIndex;
            int? actor = null;
            if (index != 0) {
                Actor a = picActorName.SelectedItem as Actor;
                actor = a.Id;
            }
                
            FiltterModel filtre = new FiltterModel() {
                Category = cat,
                Title = title.Text,
                KeyWord = keyWords.Text,
                Actor = actor,
                Langage = language
            };

            List<MovieListModel> list = await MovieApiClientService.GetFilmListFilted(filtre);

            if (list == null) {
                await DisplayAlert("Error", "Movie list is empty", "OK");
                return;
            }

            _movieList.AddToViewList(list);
            await Navigation.PopAsync();

        }



    }
}