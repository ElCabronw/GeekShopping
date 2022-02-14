using System;
using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.OrderAPI.Model.Base;

namespace GeekShopping.OrderAPI.Model
{
	[Table("order_detail")]
	public class OrderDetail : BaseEntity
	{
		public long OrderHeaderId { get; set; }
		[Column("product_id")]
		public long ProductId { get; set; }
		[Column("count")]
		public int Count { get; set; }

		[ForeignKey("OrderHeaderId")]
		public virtual OrderHeader? OrderHeader { get; set; }
		[Column("product_name")]
		public string ProductName { get; set; }
		[Column("price")]
		public decimal Price { get; set; }
    }
}

