using Rental_Movie_Xamarin.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rental_Movie_Xamarin.service {

    

    public static class CartService {

        private static Dictionary<int,MovieListModel> _moviesId = new Dictionary<int, MovieListModel>();

        private static void AddMovie(MovieListModel ml) {
            _moviesId.Add(ml.Id,ml);
        }

        public static void RemoveMovie(MovieListModel ml) {
            _moviesId.Remove(ml.Id);
        }

        public static void EmptyCart() {
            _moviesId.Clear();
        }

        public static int GetSize() {
            return _moviesId.Count;
        }

        public static bool AddOrRemoveMovieId(MovieListModel ml) {
            if (Contains(ml)) {
                RemoveMovie(ml);
                return false;
            } else {
                AddMovie(ml);
                return true;
            }
        }

        public static bool Contains(MovieListModel ml) {
            return _moviesId.ContainsKey(ml.Id);
        }

        public static IEnumerable<MovieListModel> GetMoviesList() {
            foreach (MovieListModel ml in _moviesId.Values) {
                yield return ml;
            }
        }

    }
}
