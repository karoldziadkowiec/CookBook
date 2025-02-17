using AutoMapper;
using CookBook.Models;
using CookBook.Models.Entities;

namespace CookBook.DbManager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RecipeDTO, Recipe>().ReverseMap();
        }
    }
}