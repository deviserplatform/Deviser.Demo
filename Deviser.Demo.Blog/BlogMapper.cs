using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Deviser.Demo.Blog.Models;

namespace Deviser.Demo.Blog
{
    public class BlogMapper
    {
        public static MapperConfiguration MapperConfiguration;
        public static IMapper Mapper;
        static BlogMapper()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, DTO.Post>().ReverseMap();
                cfg.CreateMap<Tag, DTO.Tag>().ReverseMap();
                cfg.CreateMap<Comments, DTO.Comments>().ReverseMap();
                cfg.CreateMap<Category, DTO.Category>().ReverseMap();
            });
            Mapper = MapperConfiguration.CreateMapper();
        }
    }
}
