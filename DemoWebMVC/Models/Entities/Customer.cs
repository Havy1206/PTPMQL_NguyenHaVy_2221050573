using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string? FullName { get; set; }

        public string? Address { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}