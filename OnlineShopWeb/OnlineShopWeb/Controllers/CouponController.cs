using CouponCache;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Models;
using OnlineShopWeb.Models.Mapping;

namespace OnlineShopWeb.Controllers;
// mapping dto to model, and other way around
public class CouponController(IGetCouponListCommandHandler couponListCommandHandler,
    ICouponDeleteCommandHandler couponDeleteCommandHandler,
    IGetCouponByIdCommandHandler getCouponByIdCommandHandler,
    IGetCouponByCodeCommandHandler getCouponByCodeCommandHandler,
    ICouponUpdateCommandHandler couponUpdateCommandHandler,
    ICouponAddCommandHandler couponAddCommandHandler,
    ICache cache,
    IAuthenticationService authenticationService) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var token = await authenticationService.GetTokenAsync(HttpContext,
            OpenIdConnectDefaults.AuthenticationScheme,
            "access_token");
        //var couponList = await couponListCommandHandler.Handle();

        var couponList = await cache.GetCoupons();

        return View(couponList.MapToModelList());
    }

    [HttpGet]
    public IActionResult Delete(string code)
    {
        var command = new CouponDeleteCommand(code);
        couponDeleteCommandHandler.Handle(command);
        return RedirectToAction("Index", "Coupon");
    }

    [HttpGet]
    public async Task<ActionResult> Details(string code,
        CancellationToken cancellationToken)
    {
        var command = new GetCouponByCodeCommand(code);
        var coupon = await getCouponByCodeCommandHandler.Handle(command, cancellationToken);

        return View(coupon.MapToModel());
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id,
        CancellationToken cancellationToken)
    {
        var model = new CouponModel();

        var command = new GetCouponByIdCommand(id);
        var coupon = await getCouponByIdCommandHandler.Handle(command, cancellationToken);

        if (coupon.Code is not null)
        {
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
