using System;
using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;

namespace GeekShopping.Email.Repository.Interfaces
{
	public interface IEmailRepository
	{
		Task LogEmail(UpdatePaymentResultMessage message);
	}
}

