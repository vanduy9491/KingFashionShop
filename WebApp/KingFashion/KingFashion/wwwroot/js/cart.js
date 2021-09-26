cartService = {};
cart = {};
$('.js-addcart-detail').each(function () {
    var nameProduct = $(this).parent().parent().parent().parent().find('.js-name-detail').html();
    $(this).on('click', function () {
        let pId = $(this).attr('data-item');
        var addCart = {
            productId: parseInt(pId),
            quantity: parseInt($('input[name="num-product"]').val())
        }
        cartService.add(addCart);
    });
});


cartService.get=function () {
    $.ajax({
        url: "https://localhost:44322/api/Cart/GetBySessionId",
        method: "GET",
        xhrFields: {
            withCredentials: true
        },
        success: function (data) {
            cart = data;
            var pCount = 0;
            $.each(data.items, function (index, cartItem) {
                pCount += cartItem.quantity;
            });
            $(".icon-header-noti").attr("data-notify", pCount);
        }
    });
}
cart.cartService();
//click quick view for add to cart
cart.add = function (addCart) {

    $('.js-addcart-detail').on('click', function () {
        $('.header-cart-item').addClass('show-header-cart');
    });
    $.ajax({
        url: `https://localhost:44322/api/Cart/Add`,
        method: "POST",
        xhrFields: {
            withCredentials: true
        },
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(addCart),
        success: function (data) {
            cart.items = data.items;
            var pCount = 0;
            $.each(data.items, function (index, cartItem) {
                pCount += cartItem.quantity;
            });
            $(".icon-header-noti").attr("data-notify", pCount);
            // $('#productModel').modal('show');
            // $('input[name="ProductName"]').val(data.productName);
            // $('input[name="ProductId"]').val(data.productId);
            // $('option[name="Color"]').val(data.color);
        }
    });
}

