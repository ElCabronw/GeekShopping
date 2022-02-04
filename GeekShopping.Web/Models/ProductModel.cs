using System;
namespace GeekShopping.Web.Models
{
	public class ProductModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public long CategoryId { get; set; }
		public string ImageURL { get; set; }
       public string CategoryName { get; set; }
    }
}

