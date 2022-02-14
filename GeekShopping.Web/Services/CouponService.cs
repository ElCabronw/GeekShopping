using System;
using System.Net;
using System.Net.Http.Headers;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
	public class CouponService : ICouponService
	{
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/coupon";

        public CouponService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CouponViewModel> GetCoupon(string code, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/find-coupon/{code}");
            if (response.StatusCode != HttpStatusCode.OK) return new CouponViewModel();
            return await response.ReadContentAs<CouponViewModel>();
        }
    }
}

