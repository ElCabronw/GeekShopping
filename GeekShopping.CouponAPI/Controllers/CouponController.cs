using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CouponController : ControllerBase
{

    private readonly ILogger<CouponController> _logger;
    private readonly ICouponRepository _repository;
    public CouponController(ILogger<CouponController> logger, ICouponRepository couponRepository)
    {
        _logger = logger;
        _repository = couponRepository;
    }

    [HttpGet("find-coupon/{couponCode}")]
    public async Task<ActionResult<CouponVO>> GetCouponByCouponCode(string couponCode)
    {
        var coupon = await _repository.GetCouponByCouponCode(couponCode);
        if (coupon == null) return NotFound();

        return Ok(coupon);
    }
}

