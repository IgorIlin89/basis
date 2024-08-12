using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;
using ApiCouponProduct.Domain.Exceptions;

namespace ApiCouponProduct.Database;

internal class CouponRepository : ICouponRepository
{
    private readonly ApiCouponProductContext _context;

    public CouponRepository(ApiCouponProductContext context)
    {
        _context = context;
    }

    public Coupon AddCoupon(Coupon coupon)
    {
        var existingCoupon = _context.Coupon.FirstOrDefault(o => o.Code == coupon.Code);

        if (existingCoupon is not null)
        {
            throw new CouponExistsException($"A coupon with the code '{coupon.Code}' " +
                $"allready exists and can not be added");
        }

        var response = _context.Coupon.Add(coupon);

        return response.Entity;
    }

    public void Delete(int id)
    {
        var couponToDelete = _context.Coupon.FirstOrDefault(o => o.Id == id);

        if (couponToDelete is null)
        {
            throw new NotFoundException($"Coupon with the id '{id}' does not exist and could not be deleted");
        }

        _context.Coupon.Remove(couponToDelete);
    }

    public void Delete(string code)
    {
        var couponToDelete = _context.Coupon.FirstOrDefault(o => o.Code == code);

        if (couponToDelete is null)
        {
            throw new NotFoundException($"Coupon with the code '{code}' does not exist and could not be deleted");
        }

        _context.Coupon.Remove(couponToDelete);
    }

    public Coupon GetCouponById(int id)
    {
        var coupon = _context.Coupon.FirstOrDefault(o => o.Id == id);

        if (coupon is null)
        {
            throw new NotFoundException($"Coupon with the id '{id}' does not exist");
        }

        return coupon;
    }

    public Coupon GetCouponByCode(string code)
    {
        var coupon = _context.Coupon.FirstOrDefault(o => o.Code == code);

        if (coupon is null)
        {
            throw new NotFoundException($"Coupon with the code '{code}' does not exist");
        }

        return coupon;
    }

    public List<Coupon> GetCouponList()
    {
        return _context.Coupon.ToList();
    }

    public Coupon Update(Coupon coupon)
    {
        var couponToUpdate = _context.Coupon.FirstOrDefault(o => o.Id == coupon.Id);

        if (couponToUpdate is null)
        {
            throw new NotFoundException($"Coupon with the id '{coupon.Id}' does not exist and could not be updated");
        }

        couponToUpdate.Code = coupon.Code;
        couponToUpdate.AmountOfDiscount = coupon.AmountOfDiscount;
        couponToUpdate.TypeOfDiscount = coupon.TypeOfDiscount;
        couponToUpdate.MaxNumberOfUses = coupon.MaxNumberOfUses;
        couponToUpdate.StartDate = coupon.StartDate;
        couponToUpdate.EndDate = coupon.EndDate;

        return couponToUpdate;
    }
}
