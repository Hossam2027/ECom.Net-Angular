using AutoMapper;
using ECom.Core.DTO;
using ECom.Core.Entites.Product;

namespace ECom.API.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>()
     .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.NewPrice))
     .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
     .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos));

            CreateMap<Photo, PhotoDto>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Product, addProductDTO>().ReverseMap();
            //CreateMap<Product, addProductDTO>().ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Photos)).ReverseMap();

        }
    }
}
