using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Rental_Movie_Xamarin.model {
    public class MovieListModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string BtnColor { get; set; } = "Green"; // find a better way to init btn
        public string BtnText { get; set; } = "Add"; // find a better way to init btn
    }
}
