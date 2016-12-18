using System;

namespace Slumming.Models
{
    public partial class Deal
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SalesmanId { get; set; }
        public int AppartmentId { get; set; }
        public int Sum { get; set; }
        public string RegisterId { get; set; }

        public virtual Apartment Appartment { get; set; }
        public virtual Salesman Salesman { get; set; }
    }
}
