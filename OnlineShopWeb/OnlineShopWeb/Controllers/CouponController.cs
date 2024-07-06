using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Text.Json;
using OnlineShopWeb.Dtos;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWeb.Controllers;

public class CouponController : Controller
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _connectionString;
    private readonly string _connectToGetCouponList;
    public readonly string _connectToGetCouponById;
    public readonly string _connectToDeleteCoupon;
    public readonly string _connectToEditCoupon;
    public readonly string _connectToAddCoupon;

    public CouponController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ApiURL");
        _connectToGetCouponList = configuration.GetConnectionString("ApiCouponControllerGetCouponList");
        _connectToGetCouponById = configuration.GetConnectionString("ApiCouponControllerGetCouponById");
        _connectToDeleteCoupon = configuration.GetConnectionString("ApiCouponControllerDeleteCoupon");
        _connectToEditCoupon = configuration.GetConnectionString("ApiCouponControllerEditCoupon");
        _connectToAddCoupon = configuration.GetConnectionString("ApiCouponControllerAddCoupon");
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var request = await _httpClient.GetAsync(_connectionString + _connectToGetCouponList);
        var response = await request.Content.ReadAsStringAsync();

        var couponDtoList = JsonSerializer.Deserialize<List<CouponDto>>(response);
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
        var request = _httpClient.GetAsync(_connectionString + _connectToDeleteCoupon + id.ToString());
        return RedirectToAction("Index", "Coupon");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var request = await _httpClient.GetAsync(_connectionString + _connectToGetCouponById + id.ToString());
        var response = await request.Content.ReadAsStringAsync();
        var coupon = JsonSerializer.Deserialize<CouponDto>(response);

        var model = new CouponModel
        {
            CouponId = coupon.CouponId,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount,
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate,
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        var model = new CouponModel();

        if (id is not null)
        {
            var request = await _httpClient.GetAsync(_connectionString + _connectToGetCouponById + id.ToString());
            var response = await request.Content.ReadAsStringAsync();

            var coupon = JsonSerializer.Deserialize<CouponDto>(response);

            model.CouponId = coupon.CouponId;
            model.Code = coupon.Code;
            model.AmountOfDiscount = coupon.AmountOfDiscount;
            model.TypeOfDiscount = coupon.TypeOfDiscount;
            model.MaxNumberOfUses = coupon.MaxNumberOfUses;
            model.StartDate = coupon.StartDate;
            model.EndDate = coupon.EndDate;
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

                var httpBody = new StringContent(
                    JsonSerializer.Serialize(couponToEdit),
                    Encoding.UTF8,
                    Application.Json
                    );

                var request = await _httpClient.PostAsync(_connectionString + _connectToEditCoupon,
                    httpBody);
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

                var httpBody = new StringContent(
                    JsonSerializer.Serialize(couponToAdd),
                    Encoding.UTF8,
                    Application.Json
                    );

                var request = await _httpClient.PostAsync(_connectionString + _connectToAddCoupon, httpBody);
            }

            return RedirectToAction("Index", "Coupon");
        }
        else
        {
            return View(model);
        }
    }
}
