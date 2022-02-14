using System;
using GeekShopping.OrderAPI.Model;

namespace GeekShopping.OrderAPI.Repository.Interfaces
{
	public interface IOrderRepository
	{
		Task<bool> AddOrder(OrderHeader header);
		Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid);
	}
}

