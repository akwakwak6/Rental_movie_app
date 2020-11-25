using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Movie_Xamarin.model {
    class OrderModel {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }

        public IEnumerable<RentalMovie> Movies { get; set; }

        public override string ToString() {
            return " Order of " + Date.ToString("dd/MM/yyy") + " for " + TotalPrice + " $";
        }

    }
}
