using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace GeekShopping.Web.Models
{
	
	public class CartDetailViewModel
	{
		public long Id { get; set; }
		public long CartHeaderId { get; set; }
		public long ProductId { get; set; }
	
		public int Count { get; set; }

    
        public CartHeaderViewModel CartHeader { get; set; }
	
		public ProductViewModel Product { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}

