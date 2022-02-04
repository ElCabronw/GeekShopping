using System;
using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.Config
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
			});
			return mappingConfig;
		}
	}
}

