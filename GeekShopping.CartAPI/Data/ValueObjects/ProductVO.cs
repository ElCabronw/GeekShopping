using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.CartAPI.Model.Base;

namespace GeekShopping.CartAPI.Data.ValueObjects
{
	
	public class ProductVO 
	{
		public long Id { get; set; }		
        public string Name { get; set; }
		public decimal Price { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
        public CategoryVO? Category { get; set; }


    }
}

