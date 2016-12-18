using System;
using System.Collections.Generic;

namespace Slumming.Models
{
    public partial class State
    {
        public State()
        {
            Apartment = new HashSet<Apartment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Apartment> Apartment { get; set; }
    }
}
