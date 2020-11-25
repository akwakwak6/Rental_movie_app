using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Movie_Xamarin.model {
    public class Actor {

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public override string ToString() {
            return LastName+" "+FirstName;
        }

    }
}
