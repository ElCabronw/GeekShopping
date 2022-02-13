using System;
using AutoMapper;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Data.ValueObjects;


namespace GeekShopping.CartAPI.Config
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config => {
				config.CreateMap<Product, ProductVO>()
				.ForMember(x => x.CategoryName, opt =>
				opt.MapFrom(src =>
				src.Category.Name));
				
				config.CreateMap<ProductVO, Product>();
				
				config.CreateMap<Category, CategoryVO>().ReverseMap();

				config.CreateMap<CartHeader, CartHeaderVO>().ReverseMap();

				config.CreateMap<CartDetail, CartDetailVO>()
				.ReverseMap();
				config.CreateMap<Cart, CartVO>().ReverseMap();
			});
			return mappingConfig;
		}
	}
}

