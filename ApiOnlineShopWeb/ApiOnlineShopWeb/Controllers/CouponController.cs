using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Dtos;
using Microsoft.AspNetCore.Mvc;
using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Dtos.Mapping;

namespace ApiOnlineShopWeb.Controllers;
//mappin dto to domain, domain to dto
public class CouponController(ICouponRepository _couponRepository) : ControllerBase
{
    [Route("coupon/list")]
    [HttpGet]
    public async Task<ActionResult> GetCouponList()
    {
        var couponList = _couponRepository.GetCouponList();

        if(couponList == null)
        {
            return NotFound();
        }

        var response = new List<CouponDto>();

        foreach (var element in couponList)
        {
            response.Add(new CouponDto
            {
                CouponId = element.Id,
                Code = element.Code,
                AmountOfDiscount = element.AmountOfDiscount,
                TypeOfDiscount = element.TypeOfDiscount,
                MaxNumberOfUses = element.MaxNumberOfUses,
                StartDate = element.StartDate,
                EndDate = element.EndDate
            });
        }

        return Ok(response);
    }

    [Route("coupon/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetCouponById(int id)
    {
        var coupon = _couponRepository.GetCouponById(id);

        if (coupon == null)
        {
            return NotFound();
        }

        var response = new CouponDto
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount,
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate
        };

        return Ok(response);
    }

    [Route("coupon/{id}")]
    [HttpDelete]
    public async Task<ActionResult> DeleteCoupon(int id)
    {
        _couponRepository.DeleteCoupon(id);
        return Ok();
    }

    [Route("coupon")]
    [HttpPut]
    public async Task<ActionResult> EditCoupon([FromBody] CouponDto couponDto)
    {
        var couponToEdit = new Coupon
        {
            Id = couponDto.CouponId.Value,
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = couponDto.TypeOfDiscount,
            MaxNumberOfUses = couponDto.MaxNumberOfUses,
            StartDate = couponDto.StartDate,
            EndDate = couponDto.EndDate
        };

        _couponRepository.EditCoupon(couponToEdit);

        return Ok();
    }

    [Route("coupon")]
    [HttpPost]
    public async Task<ActionResult> AddCoupon([FromBody] CouponDto couponDto)
    {
        var couponToAdd = new Coupon
        {
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = couponDto.TypeOfDiscount,
            MaxNumberOfUses = couponDto.MaxNumberOfUses,
            StartDate = couponDto.StartDate,
            EndDate = couponDto.EndDate

        };

        _couponRepository.AddCoupon(couponToAdd);

        return Ok();
    }

    [Route("coupon/code/{code}")]
    [HttpGet]
    public async Task<ActionResult> GetCouponByCode(string code)
    {
        var coupon = _couponRepository.GetCouponByCode(code);

        if (coupon == null)
        {
            return NotFound();
        }

        return Ok(coupon.MapToDto());
    }
}
