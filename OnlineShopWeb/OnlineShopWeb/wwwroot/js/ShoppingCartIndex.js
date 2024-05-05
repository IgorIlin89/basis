let couponList;
let Cookie;

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

    let couponData = await response.json();

}