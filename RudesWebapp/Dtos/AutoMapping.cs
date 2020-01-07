using AutoMapper;
using RudesWebapp.Models;

namespace RudesWebapp.Dtos
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Article, ArticleDTO>();
            CreateMap<ArticleDTO, Article>();

            CreateMap<Player, PlayerDTO>();
            CreateMap<PlayerDTO, Player>();

            CreateMap<Discount, DiscountDTO>();
            CreateMap<DiscountDTO, Discount>();

            CreateMap<ShoppingCartArticle, ShoppingCartArticleDTO>();
            CreateMap<ShoppingCartArticleDTO, ShoppingCartArticle>();

            CreateMap<Image, ImageDTO>();
            CreateMap<ImageDTO, Image>();

            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewDTO, Review>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();

            CreateMap<OrderArticle, OrderArticleDTO>();
            CreateMap<OrderArticleDTO, OrderArticle>();

            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();

            CreateMap<Match, MatchDTO>();
            CreateMap<MatchDTO, Match>();

            CreateMap<ArticleAvailability, ArticleAvailabilityDTO>();
            CreateMap<ArticleAvailabilityDTO, ArticleAvailability>();

            CreateMap<Article, ArticleDTO>();
            CreateMap<ArticleDTO, Article>();
        }
    }
}