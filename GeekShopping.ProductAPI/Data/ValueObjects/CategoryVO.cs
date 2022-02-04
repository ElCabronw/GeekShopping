using System;
namespace GeekShopping.ProductAPI.Data.ValueObjects
{
	public class CategoryVO
	{
		public CategoryVO()
		{
		}
        public long Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
	}
}

