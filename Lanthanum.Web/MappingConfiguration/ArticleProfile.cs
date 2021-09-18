using AutoMapper;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;

namespace Lanthanum.Web.MappingConfiguration
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, AdminArticleViewModel>()
                .ForMember(
                    dest => dest.TeamConference,
                    opt => opt.MapFrom(src => src.Team.Conference)
                )
                .ForMember(
                    dest => dest.TeamName,
                    opt => opt.MapFrom(src => src.Team.Name)
                )
                .ForMember(
                    dest => dest.TeamLocation,
                    opt => opt.MapFrom(src => src.Team.Location)
                )
                .ForMember(
                    dest => dest.KindOfSportName,
                    opt => opt.MapFrom(src => src.KindOfSport.Name)
                );
        }
    }
}
