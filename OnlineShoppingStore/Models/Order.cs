using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public decimal AmountPaid { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string PaymentType { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }


    }
}
