using System;
using AutoMapper;
using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Model;

namespace GeekShopping.CouponAPI.Config
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config => {
			//	config.CreateMap<Product, ProductVO>()
			//	.ForMember(x => x.CategoryName, opt =>
			//	opt.MapFrom(src =>
			//	src.Category.Name));
			//	
				config.CreateMap<Coupon, CouponVO>().ReverseMap();
			//	
			//	config.CreateMap<Category, CategoryVO>().ReverseMap();

				
			});
			return mappingConfig;
		}
	}
}

