using AutoMapper;
using WebApiProductsProviders.App.DTOs;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.App.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Provider, ProviderDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
