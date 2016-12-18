using System;
using System.Collections.Generic;

namespace Slumming.Models
{
    public partial class Salesman
    {
        public Salesman()
        {
            Deal = new HashSet<Deal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Commision { get; set; }

        public virtual ICollection<Deal> Deal { get; set; }
    }
}
