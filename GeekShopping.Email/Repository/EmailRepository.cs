using System;
using AutoMapper;
using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;

using GeekShopping.Email.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository
{
	public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MySQLContext> _context;
 
        public EmailRepository(DbContextOptions<MySQLContext> context)
        {
            _context = context;
        }


        public async Task LogEmail(UpdatePaymentResultMessage message)
        {
            try
            {
                EmailLog email = new EmailLog()
                {
                    Email = message.Email,
                    SentDate = DateTime.Now,
                    Log = $"Order - {message.OrderId} has been created succesfully!"
                };
                await using var _db = new MySQLContext(_context);
                _db.EmailLogs.Add(email);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
     

        }
    }
}

