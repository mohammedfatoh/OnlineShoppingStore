using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string NameonCard { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required,CreditCard]
        public string PaymentType { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

    }
}
