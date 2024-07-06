using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ApiOnlineShopWeb.Domain;
using static Azure.Core.HttpHeader;

namespace ApiOnlineShopWeb.Controllers;

public class CouponApiController(ICouponRepository _couponRepository) : ControllerBase
{
    [Route("couponlist")]
    [HttpGet]
    public async Task<ActionResult> GetCouponList()
    {
        var couponList = _couponRepository.GetCouponList();

        var couponDtoList = new List<CouponDto>();

        foreach (var element in couponList)
        {
            couponDtoList.Add(new CouponDto
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

        var response = JsonSerializer.Serialize(couponDtoList);

        return Ok(response);
    }

    [Route("couponbyid{id}")]
    [HttpGet]
    public async Task<ActionResult> GetCouponById(int id)
    {
        var coupon = _couponRepository.GetCouponById(id);

        var response = JsonSerializer.Serialize(new CouponDto
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount,
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate
        });

        return Ok(response);
    }

    [Route("coupondelete{id}")]
    [HttpGet]
    public async Task<ActionResult> DeleteCoupon(int id)
    {
        _couponRepository.DeleteCoupon(id);
        return Ok();
    }

    [Route("couponedit")]
    [HttpPost]
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

    [Route("couponadd")]
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

    [Route("coupongetbycode{code}")]
    [HttpGet]
    public async Task<ActionResult> GetCouponByCode(string code)
    {
        var coupon = _couponRepository.GetCouponByCode(code);

        var response = JsonSerializer.Serialize(new CouponDto
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount,
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate
        });

        return Ok(response);
    }
}
