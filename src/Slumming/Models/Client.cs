using System;
using System.Collections.Generic;

namespace Slumming.Models
{
    public partial class Client
    {
        public Client()
        {
            Apartment = new HashSet<Apartment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBusiness { get; set; }
        public string BillingAdsress { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Apartment> Apartment { get; set; }
    }
}
