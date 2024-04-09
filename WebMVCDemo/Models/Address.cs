using System.ComponentModel.DataAnnotations;


namespace WebMVCDemo.Models
{
    public class Address
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string City { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int? FlatNumber { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; } = [];

        public override string ToString()
        {
            return $"[{City}, {Street}, {HouseNumber}]";
        }
    }
}