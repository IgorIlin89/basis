using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Mapping;
using OnlineShopWeb.TransferObjects.Models;
using OnlineShopWeb.TransferObjects.Models.ListModels;

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

        // TODO convert model to domain through command and handlers in application layer
        //give domain object to adapter

        // model to command
        // command to handler, handler creates domain object
        // handler to adapter, he gives them domain object
        // adapter creates DTO  and sends to api // gehts back in controller here
        // adapter gets DTO from api, converts to domain
        // adapter gives domain to controller, controller convers to MODEL

        // API
        // DTO to command
        // command to handler, handler makes CREATE on domain class and gets object, all creates go into handler
        // handler to adapter //i am missing it
        // dto2(different to first DTO, own namespace) in adapter to domain // i am missing it
        // domain gehts back in controller here
        // domain to DTO

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
