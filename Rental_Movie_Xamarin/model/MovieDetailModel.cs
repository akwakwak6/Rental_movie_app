using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Movie_Xamarin.model {
    public class MovieDetailModel {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string Language { get; set; }
        public decimal Price { get; set; }
        public int RentalDuration { get; set; }
        public decimal Length { get; set; }
        public decimal ReplacementCost { get; set; }
        public string Rating { get; set; }

        public IEnumerable<Actor> Actors { get; set; }
        public IEnumerable<Category> Category { get; set; }

    }
}
