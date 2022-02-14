using System;
using AutoMapper;
using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.Model.Context;
using GeekShopping.OrderAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Repository
{
	public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<MySQLContext> _context;
 
        public OrderRepository(DbContextOptions<MySQLContext> context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(OrderHeader header)
        {
            if (header == null) return false;

            await using var _db = new MySQLContext(_context);
            _db.Headers.Add(header);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid)
        {
           
            await using var _db = new MySQLContext(_context);

            var header = await _db.Headers.FirstOrDefaultAsync(x => x.Id == orderHeaderId);
            if (header != null)
            {
                header.PaymentStatus = paid;
                await _db.SaveChangesAsync();
            }
          
        }
    }
}

