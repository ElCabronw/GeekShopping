using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.CartAPI.Model.Base;

namespace GeekShopping.CartAPI.Model
{
	[Table("category")]
	public class Category : BaseEntity
	{
		[Column("name")]
		[Required]
		[StringLength(50)]
        public string Name { get; set; }

		[Column("description")]
		[StringLength(500)]
		
        public string Description { get; set; }

		public ICollection<Product> Products { get; set; }

	}
}

