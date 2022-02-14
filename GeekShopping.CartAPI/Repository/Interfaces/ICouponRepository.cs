using System;
using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository.Interfaces
{
	public interface ICouponRepository
	{
		Task<CouponVO> GetCouponByCouponCode(string couponCode, string token);
	}
}

