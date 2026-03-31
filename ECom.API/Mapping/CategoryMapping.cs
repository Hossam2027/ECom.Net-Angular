using AutoMapper;
using ECom.Core.DTO;
using ECom.Core.Entites.Product;

namespace ECom.API.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
             CreateMap<Category, CategoryToUpdateDTO>().ReverseMap();

           
        }
    }
}
