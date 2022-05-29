using AutoMapper;
using blog.Models;
using blog.ViewModels;

namespace blog.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleViewModel>()
            .ForMember(
            dest => dest.Thumbnail,
            opt => opt.MapFrom(src => $"{src.ThumbnailUrl}")
        );
        
        CreateMap<ArticleViewModel, Article>()
            .ForMember(
                dest => dest.ThumbnailUrl,
                opt => opt.MapFrom(src => $"{src.Thumbnail}")
            );
    }
}