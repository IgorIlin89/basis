$(document).ready(function () {
    $('#couponButton').click(function () {
        fetch('/ShoppingCart/JsAddCoupon', {
            method: 'POST',
            body: "test123"
        })
            .then(response => response.json())
            .then(() => {
                getItems();
                addNameTextbox.value = '';
            })
            .catch(error => console.error('Unable to add item.', error));
    }


    //$.get("/ControllerName/Index", { year: @DateTime.Now.Year, month: @DateTime.Now.Month, groupId: $('#groupSelect').val(), forback: "for" }).done(function (data) {
    //    if (data.result == 'Redirect') {
    //        //redirecting to page
    //        window.location = data.url;
    //    }
    //});
});