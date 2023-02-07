using AutoMapper;

using GeekShooping.CouponApi.data.ValueObjects;
using GeekShooping.CouponApi.Model;



namespace GeekShooping.CouponApi.Config 
{ 

        public class MappingConfig
        {

            public static MapperConfiguration RegisterMaps()
            {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();

            });
                return mappingConfig;
            }
        }
  
}
