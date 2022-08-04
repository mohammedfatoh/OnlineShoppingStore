namespace OnlineShoppingStore.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public int CartStatusId { get; set; }
        public virtual CartStatus? CartStatus { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
     }
}
