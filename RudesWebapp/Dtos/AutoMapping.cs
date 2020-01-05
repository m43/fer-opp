using AutoMapper;
using RudesWebapp.Models;

namespace RudesWebapp.Dtos
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Player, PlayerDTO>();
            CreateMap<PlayerDTO, Player>();
        }
    }
}