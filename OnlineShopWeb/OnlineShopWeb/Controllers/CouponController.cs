using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Misc;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.TransferObjects.Models;
using OnlineShopWeb.TransferObjects.Models.ListModels;
using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Mapping;

namespace OnlineShopWeb.Controllers;
// mapping dto to model, and other way around
public class CouponController : Controller
{
    private readonly IProductCouponAdapter _productCouponAdapter;

    public CouponController(IProductCouponAdapter productCouponAdapter)
    {
        _productCouponAdapter = productCouponAdapter;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var couponDtoList = await _productCouponAdapter.GetCouponList();

        var model = new CouponListModel();

        foreach (var element in couponDtoList)
        {
            model.CouponModelList.Add(element.MapToModel());
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _productCouponAdapter.CouponDelete(id.ToString());
        return RedirectToAction("Index", "Coupon");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var couponDto = await _productCouponAdapter.GetCouponById(id.ToString());

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
            var couponDto = await _productCouponAdapter.GetCouponById(id.ToString());

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

                await _productCouponAdapter.CouponUpdate(couponToEdit);
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

                await _productCouponAdapter.CouponAdd(couponToAdd);
            }

            return RedirectToAction("Index", "Coupon");
        }
        else
        {
            return View(model);
        }
    }
}
