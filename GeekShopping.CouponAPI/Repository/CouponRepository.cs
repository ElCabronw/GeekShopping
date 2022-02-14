using System;
using AutoMapper;
using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Model.Context;
using GeekShopping.CouponAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Repository
{
	public class CouponRepository : ICouponRepository
	{
        private readonly MySQLContext _context;
        private IMapper _mapper;
        public CouponRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _context.Coupons.Where(x => x.CouponCode.Equals(couponCode)).FirstOrDefaultAsync();
            return _mapper.Map<CouponVO>(coupon);
        }
    }
}

