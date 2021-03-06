namespace RudesWebapp.Models
{
    public class OrderArticle
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ArticleId { get; set; }
        public int? Quantity { get; set; }
        public string Size { get; set; }
        public decimal PurchasePrice { get; set; }
        public int? PurchaseDiscount { get; set; }

        public virtual Article Article { get; set; }
        public virtual Order Order { get; set; }
    }
}