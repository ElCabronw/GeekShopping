using System;
using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model.Context;
using GeekShopping.CartAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
	public class CouponRepository : ICouponRepository
	{
        private readonly HttpClient _client;

        public CouponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"api/v1/coupon/find-coupon/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK) return new CouponVO();

            return JsonSerializer.Deserialize<CouponVO>(content,
                new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
        }

    }
}

