using System;
using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.CartAPI.Model.Base;

namespace GeekShopping.CartAPI.Model
{
	[Table("cart_detail")]
	public class CartDetail : BaseEntity
	{
        public long CartHeaderId { get; set; }
		public long ProductId { get; set; }
		[Column("count")]
		public int Count { get; set; }

        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }
		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
    }
}

