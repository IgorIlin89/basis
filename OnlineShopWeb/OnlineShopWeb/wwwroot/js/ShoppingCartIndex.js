var couponList;

async function getCouponCode() {
    let couponCodeField = document.getElementById("couponCode");
    let couponCode = couponCodeField.value;

    const response = await fetch('/ShoppingCart/JsAddCoupon', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/ json'
        },
        body: JSON.stringify(couponCode)
    })


    let couponCodeData = await response.json();
    let timeNow = new Date().toJSON();

    if (couponCodeData == null) {
        alert('Coupon does not Exist');
        return;
    } else if ((couponCodeData.StartDate > timeNow) || (couponCodeData.EndDate < timeNow)) {
        alert('Coupon is expired');
        return;
    } else if (couponCodeData.MaxNumberOfUses <= 0) {
        alert('All coupons with this code are allready taken');
        return;
    }

    let shoppingCart = JSON.parse($.cookie('ShoppingCartListModel'));

    if (shoppingCart.CouponModelList.find((element) => element.Code == couponCode) !== undefined) {
        alert('The Coupon is allready in the Cart');
        return;
    }

    shoppingCart.CouponModelList.push(couponCodeData);

    let newShoppingCart = JSON.stringify(shoppingCart);
    $.cookie('ShoppingCartListModel', newShoppingCart);

    location.reload();
}

async function deleteCouponCode(couponCode) {
    var shoppingCart = JSON.parse($.cookie('ShoppingCartListModel'));
    shoppingCart.CouponModelList = shoppingCart.CouponModelList.filter((element) => element.Code != couponCode);
    let newShoppingCart = JSON.stringify(shoppingCart);
    $.cookie('ShoppingCartListModel', newShoppingCart)
    location.reload();
}
