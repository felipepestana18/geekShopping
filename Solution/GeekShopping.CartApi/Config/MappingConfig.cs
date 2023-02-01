using AutoMapper;
using GeekShooping.CartApi.Data.ValueObjects;
using GeekShooping.CartApi.Model;

namespace GeekShooping.CartApi.Config
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig =  new MapperConfiguration(config => {
                config.CreateMap<ProductVO, Product>().ReverseMap();
                config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                config.CreateMap<CartVO, Cart>().ReverseMap();


            });
            return mappingConfig;
        }
    }
}
