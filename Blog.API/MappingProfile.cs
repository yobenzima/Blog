using AutoMapper;
using Blog.Entities.DataTransferObjects;
using Blog.Entities.Models;

namespace Blog.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostCreateDTO, Post>();
            CreateMap<PostUpdateDTO, Post>();

            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactCreateDTO, Contact>();
            CreateMap<ContactUpdateDTO, Contact>();
        }
    }
}