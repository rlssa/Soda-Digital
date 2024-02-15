namespace RoyalLifeSavings.Data
{
    public class Order
    {
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public Guid UserId { get; set; }
        public string StripeSessionId { get; set; }
        public string StripePaymentStatus { get; set; }
        public string StripePriceId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string EbookId { get; set; }
        public EBook EBook { get; set; }
    }
}
