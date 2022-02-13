using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.CartAPI.Model.Base;

namespace GeekShopping.CartAPI.Data.ValueObjects
{
	
	public class CategoryVO
	{
		public long Id { get; set; }
		public string Name { get; set; }	
        public string Description { get; set; }
		public virtual ICollection<ProductVO>? Products { get; set; }

	}
}

