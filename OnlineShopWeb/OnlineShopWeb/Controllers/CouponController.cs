using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Text.Json;
using OnlineShopWeb.Dtos;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using OnlineShopWeb.Misc;

namespace OnlineShopWeb.Controllers;
// mappint dto to model, and other way around
public class CouponController : Controller
{
    public IHttpClientWrapper _httpClientWrapper;

    public CouponController(IConfiguration configuration
        , IHttpClientWrapper clientWrapper)
    {
        _httpClientWrapper = clientWrapper;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {

        var couponDtoList = await _httpClientWrapper.Get<List<CouponDto>>("coupon", "list");

        List<Coupon> couponList = new List<Coupon>();

        foreach (var element in couponDtoList)
        {
            couponList.Add(new Coupon
            {
                Id = element.CouponId.Value,
                Code = element.Code,
                AmountOfDiscount = element.AmountOfDiscount,
                TypeOfDiscount = element.TypeOfDiscount,
                MaxNumberOfUses = element.MaxNumberOfUses,
                StartDate = element.StartDate,
                EndDate = element.EndDate
            });
        }

        var model = new CouponListModel();

        foreach (var coupon in couponList)
        {
            model.CouponModelList.Add(
                new CouponModel
                {
                    CouponId = coupon.Id,
                    Code = coupon.Code,
                    AmountOfDiscount = coupon.AmountOfDiscount,
                    TypeOfDiscount = coupon.TypeOfDiscount,
                    MaxNumberOfUses = coupon.MaxNumberOfUses,
                    StartDate = coupon.StartDate,
                    EndDate = coupon.EndDate,
                }
            );
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _httpClientWrapper.Delete("coupon", id.ToString());
        return RedirectToAction("Index", "Coupon");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var couponDto = await _httpClientWrapper.Get<CouponDto>("coupon", id.ToString());

        var model = new CouponModel
        {
            CouponId = couponDto.CouponId,
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = couponDto.TypeOfDiscount,
            MaxNumberOfUses = couponDto.MaxNumberOfUses,
            StartDate = couponDto.StartDate,
            EndDate = couponDto.EndDate,
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        var model = new CouponModel();

        if (id is not null)
        {
            var couponDto = await _httpClientWrapper.Get<CouponDto>("coupon", id.ToString());

            model.CouponId = couponDto.CouponId;
            model.Code = couponDto.Code;
            model.AmountOfDiscount = couponDto.AmountOfDiscount;
            model.TypeOfDiscount = couponDto.TypeOfDiscount;
            model.MaxNumberOfUses = couponDto.MaxNumberOfUses;
            model.StartDate = couponDto.StartDate;
            model.EndDate = couponDto.EndDate;
        }

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Update(CouponModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.CouponId is not null)
            {
                var couponToEdit = new CouponDto
                {
                    CouponId = model.CouponId.Value,
                    Code = model.Code,
                    AmountOfDiscount = model.AmountOfDiscount,
                    TypeOfDiscount = model.TypeOfDiscount,
                    MaxNumberOfUses = model.MaxNumberOfUses,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };

                _httpClientWrapper.Put<CouponDto, CouponDto>("coupon", couponToEdit);

            }
            else
            {
                var couponToAdd = new CouponDto
                {
                    Code = model.Code,
                    AmountOfDiscount = model.AmountOfDiscount,
                    TypeOfDiscount = model.TypeOfDiscount,
                    MaxNumberOfUses = model.MaxNumberOfUses,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };

                var request = _httpClientWrapper.Post<CouponDto, CouponDto>("coupon", couponToAdd);
            }

            return RedirectToAction("Index", "Coupon");
        }
        else
        {
            return View(model);
        }
    }
}
