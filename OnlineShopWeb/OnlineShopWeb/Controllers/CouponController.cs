using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponservice)
    {
        _couponService = couponservice;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new CouponListModel
        {
            CouponList = _couponService.GetCouponList()
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _couponService.Delete(id);
        return RedirectToAction("Index", "Coupon");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var coupon = _couponService.GetCoupon(id);

        var model = new CouponModel
        {
            CouponId = coupon.CouponId,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount,
            MaxNumberOfUses = coupon.MaxNumberOfUses,
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        var model = new CouponModel();

        if (id != null)
        {
            var coupon = _couponService.GetCoupon(id.Value);

            model.CouponId = coupon.CouponId;
            model.Code = coupon.Code;
            model.AmountOfDiscount = coupon.AmountOfDiscount;
            model.TypeOfDiscount = coupon.TypeOfDiscount;
            model.MaxNumberOfUses = coupon.MaxNumberOfUses;
        }


        return View(model);
    }

    [HttpPost]
    public IActionResult Update(CouponModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.CouponId != null)
            {
                var coupon = _couponService.GetCoupon(model.CouponId.Value);

                coupon.Code = model.Code;
                coupon.AmountOfDiscount = model.AmountOfDiscount;
                coupon.TypeOfDiscount = model.TypeOfDiscount;
                coupon.MaxNumberOfUses = model.MaxNumberOfUses;

            }
            else
            {
                _couponService.AddCoupon(_couponService.GetCouponList().Count() + 1,
                    model.Code,
                    model.AmountOfDiscount,
                    model.TypeOfDiscount,
                    model.MaxNumberOfUses);
            }

            return RedirectToAction("Index", "Coupon");
        }
        else
        {
            return View(model);
        }
    }
}
