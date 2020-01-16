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

            CreateMap<Image, AddImageDTO>();
            CreateMap<AddImageDTO, Image>();

            CreateMap<Image, ImageDTO>().ForMember(d => d.Path, m => m.MapFrom(s => s.GetPath()));

            CreateMap<Review, AddReviewDTO>();
            CreateMap<AddReviewDTO, Review>();

            CreateMap<Order, EditOrderDTO>();
            CreateMap<EditOrderDTO, Order>();

            CreateMap<OrderArticle, OrderArticleDTO>();
            CreateMap<OrderArticleDTO, OrderArticle>();

            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();

            CreateMap<Match, MatchDTO>();
            CreateMap<MatchDTO, Match>();

            CreateMap<ArticleAvailability, ArticleAvailabilityDTO>();
            CreateMap<ArticleAvailabilityDTO, ArticleAvailability>();

            CreateMap<User, ManageUserDTO>();
            CreateMap<ManageUserDTO, User>();
        }
    }
}