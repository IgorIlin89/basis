using CouponCache;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.TransferObjects.Mapping;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.Controllers;
// mapping dto to model, and other way around
public class CouponController(IGetCouponListCommandHandler couponListCommandHandler,
    ICouponDeleteCommandHandler couponDeleteCommandHandler,
    IGetCouponByIdCommandHandler getCouponByIdCommandHandler,
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
    public IActionResult Delete(int id)
    {
        var command = new CouponDeleteCommand(id.ToString());
        couponDeleteCommandHandler.Handle(command);
        return RedirectToAction("Index", "Coupon");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id,
        CancellationToken cancellationToken)
    {
        //TODO
        //GrpcTransactionAdapter, GrpcCouponAdapter usw.
        //CancellationToken

        var command = new GetCouponByIdCommand(id.ToString());
        var coupon = await getCouponByIdCommandHandler.Handle(command, cancellationToken);

        return View(coupon.MapToModel());
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id,
        CancellationToken cancellationToken)
    {
        var model = new CouponModel();

        var command = new GetCouponByIdCommand(id.ToString());
        var coupon = await getCouponByIdCommandHandler.Handle(command, cancellationToken);
        model = coupon.MapToModel();

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
