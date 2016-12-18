namespace Slumming.Models
{
    public class FullDeal
    {
        public Deal Deal { get; set; }
        public Salesman Salesman { get; set; }
        public Apartment Apartment { get; set; }
        public Client Client { get; set; }
        public bool IsValid { get; set; }
        public string Reason { get; set; }
    }
}
