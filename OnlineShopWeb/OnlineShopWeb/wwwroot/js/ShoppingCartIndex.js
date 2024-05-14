async function getCouponCode() {
    let couponCodeField = document.getElementById("couponCode");
    let couponCode = couponCodeField.value;

    let shoppingCart = JSON.parse($.cookie('ShoppingCartListModel'));

    if (shoppingCart.CouponModelList.find((element) => element.Code == couponCode) !== undefined) {
        alert('The Coupon is allready in the Cart');
        return;
    }

    const response = await fetch('/ShoppingCart/AddCoupon', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/ json'
        },
        body: JSON.stringify(couponCode)
    })


    let couponCodeData = await response.json();

    if (!couponCodeData.isValid) {
        alert(couponCodeData.validationError);
        return
    }

    let coupon = {
        Code: couponCode,
        AmountOfDiscount: couponCodeData.amountOfDiscount,
        TypeOfDiscount: couponCodeData.typeOfDiscount
    }

    shoppingCart.CouponModelList.push(coupon);

    let newShoppingCart = JSON.stringify(shoppingCart);
    $.cookie('ShoppingCartListModel', newShoppingCart);

    const responseVC = await fetch('/ShoppingCart/CouponTableVC', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/ json'
        },
        body: newShoppingCart
    })

    let viewComponentData = await responseVC.text();

    document.getElementById("couponTable").innerHTML = viewComponentData;
}

async function deleteCouponCode(couponCode) {
    var shoppingCart = JSON.parse($.cookie('ShoppingCartListModel'));
    shoppingCart.CouponModelList = shoppingCart.CouponModelList.filter((element) => element.Code != couponCode);
    let newShoppingCart = JSON.stringify(shoppingCart);
    $.cookie('ShoppingCartListModel', newShoppingCart)

    const responseVC = await fetch('/ShoppingCart/CouponTableVC', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/ json'
        },
        body: newShoppingCart
    })

    let viewComponentData = await responseVC.text();

    document.getElementById("couponTable").innerHTML = viewComponentData;
}
