﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class CouponController : Controller
{
    private readonly ICouponRepository _couponRepository;

    public CouponController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new CouponListModel
        {
            CouponList = _couponRepository.GetCouponList()
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _couponRepository.DeleteCoupon(id);
        return RedirectToAction("Index", "Coupon");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var coupon = _couponRepository.GetCoupon(id);

        var model = new CouponModel
        {
            CouponId = coupon.Id,
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

        if (id is not null)
        {
            var coupon = _couponRepository.GetCoupon(id.Value);

            model.CouponId = coupon.Id;
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
            if (model.CouponId is not null)
            {
                var coupon = _couponRepository.GetCoupon(model.CouponId.Value);

                coupon.Code = model.Code;
                coupon.AmountOfDiscount = model.AmountOfDiscount;
                coupon.TypeOfDiscount = model.TypeOfDiscount;
                coupon.MaxNumberOfUses = model.MaxNumberOfUses;

            }
            else
            {
                _couponRepository.AddCoupon(
                    
                    new Coupon { 
                    Code = model.Code,
                    AmountOfDiscount = model.AmountOfDiscount,
                    TypeOfDiscount = model.TypeOfDiscount,
                    MaxNumberOfUses = model.MaxNumberOfUses
                    }
                    );
            }

            return RedirectToAction("Index", "Coupon");
        }
        else
        {
            return View(model);
        }
    }
}
