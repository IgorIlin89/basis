using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.TransferObjects.Mapping;
using OnlineShopWeb.TransferObjects.Models;
using OnlineShopWeb.TransferObjects.Models.ListModels;

namespace OnlineShopWeb.Controllers;
// mapping dto to model, and other way around
public class CouponController(IGetCouponListCommandHandler couponListCommandHandler,
    ICouponDeleteCommandHandler couponDeleteCommandHandler,
    IGetCouponByIdCommandHandler getCouponByIdCommandHandler,
    ICouponUpdateCommandHandler couponUpdateCommandHandler,
    ICouponAddCommandHandler couponAddCommandHandler) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var command = new GetCouponListCommand();
        var couponList = await couponListCommandHandler.Handle(command);

        var model = new CouponListModel();

        foreach (var element in couponList)
        {
            model.CouponModelList.Add(element.MapToModel());
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var command = new CouponDeleteCommand(id.ToString());
        couponDeleteCommandHandler.Handle(command);
        return RedirectToAction("Index", "Coupon");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var command = new GetCouponByIdCommand(id.ToString());
        var coupon = await getCouponByIdCommandHandler.Handle(command);

        return View(coupon.MapToModel());
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        var model = new CouponModel();

        if (id is not null)
        {
            var command = new GetCouponByIdCommand(id.ToString());
            var coupon = await getCouponByIdCommandHandler.Handle(command);
            model = coupon.MapToModel();
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
                var command = new CouponUpdateCommand(model.CouponId.Value, model.Code,
                    model.AmountOfDiscount, model.TypeOfDiscount.MapToDto(), model.MaxNumberOfUses,
                    model.StartDate, model.EndDate);

                var coupon = couponUpdateCommandHandler.Handle(command);
            }
            else
            {
                var command = new CouponAddCommand(model.Code,
                    model.AmountOfDiscount, model.TypeOfDiscount.MapToDto(), model.MaxNumberOfUses,
                    model.StartDate, model.EndDate);

                var coupon = couponAddCommandHandler.Handle(command);
            }

            return RedirectToAction("Index", "Coupon");
        }
        else
        {
            return View(model);
        }
    }
}
