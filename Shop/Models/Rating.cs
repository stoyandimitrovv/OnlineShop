namespace ShoppingCart.Models
{
    public class Rating
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public long RatingValue { get; set; }
    }
}
