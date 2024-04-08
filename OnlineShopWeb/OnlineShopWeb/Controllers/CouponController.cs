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
    public IActionResult Edit(int id)
    {
        ViewBag.ActionName = "Edit";
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

    [HttpPost]
    public IActionResult Edit(CouponModel model)
    {
        if (ModelState.IsValid)
        {
            var coupon = _couponService.GetCoupon(model.CouponId);

            coupon.CouponId = model.CouponId;
            coupon.Code = model.Code;
            coupon.AmountOfDiscount = model.AmountOfDiscount;
            coupon.TypeOfDiscount = model.TypeOfDiscount;
            coupon.MaxNumberOfUses = model.MaxNumberOfUses;

            return RedirectToAction("Index", "Coupon");
        }
        else
        {
            return View(model);
        }

    }

    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.ActionName = "Add";
        return View("~/Views/Coupon/Edit.cshtml");
    }

    [HttpPost]
    public IActionResult Add(CouponModel model)
    {
        if (ModelState.IsValid)
        {
            _couponService.AddCoupon(model.CouponId, model.Code, model.AmountOfDiscount, model.TypeOfDiscount, model.MaxNumberOfUses);
            return RedirectToAction("Index", "Coupon");
        }
        else
        {
            return View("~/Views/Coupon/Edit.cshtml");
        }
    }
}
