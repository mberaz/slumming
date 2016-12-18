using System;
using System.Collections.Generic;

namespace Slumming.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            Deal = new HashSet<Deal>();
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string Appartment { get; set; }
        public int Floor { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public decimal? Price { get; set; }
        public bool IsInsured { get; set; }
        public int? ClientId { get; set; }

        public virtual ICollection<Deal> Deal { get; set; }
        public virtual City City { get; set; }
        public virtual Client Client { get; set; }
        public virtual State State { get; set; }
    }
}
