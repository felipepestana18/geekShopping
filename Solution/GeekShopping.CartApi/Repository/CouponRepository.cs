using AutoMapper;
using GeekShooping.CartApi.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;

namespace GeekShooping.CartApi.Model.Repository
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

            // public const string BasePath = "api/v1/Coupon";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response  = await _client.GetAsync($"/api/v1/Coupon{couponCode}");
            // para pegar só o contédo da requisição.
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK) return new CouponVO();
            return JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }
    }
}
