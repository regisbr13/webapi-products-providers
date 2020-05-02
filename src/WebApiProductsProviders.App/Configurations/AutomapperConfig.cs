using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApiProductsProviders.App.DTOs;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.App.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Provider, ProviderDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
