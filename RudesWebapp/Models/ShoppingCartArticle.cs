namespace RudesWebapp.Models
{
    public class ShoppingCartArticle
    {
        public int ShoppingCartId { get; set; }
        public int ArticleId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }

        public virtual Article Article { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}