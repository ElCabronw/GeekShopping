using System;
using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.CartAPI.Model.Base;

namespace GeekShopping.CartAPI.Data.ValueObjects
{
	
	public class CartDetailVO 
	{
		public long Id { get; set; }
		public long CartHeaderId { get; set; }
		public long ProductId { get; set; }
		
		public int Count { get; set; }

    
        public CartHeaderVO? CartHeader { get; set; }
	
		public ProductVO Product { get; set; }
        public CategoryVO Category { get; set; }
    }
}

