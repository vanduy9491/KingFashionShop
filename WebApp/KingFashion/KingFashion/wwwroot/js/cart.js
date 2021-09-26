cartService = {};
cart = undefined;
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
cartService.cartPanel = function () {
    if (cart !== undefined && cart.items !== undefined && cart.items.length > 0) {
        var subTotal=0;
        $(".header-cart-wrapitem").empty();
        $.each(cart.items, function (index, cartItem) {
            subTotal+=cartItem.quantity*cartItem.price;
           var images= cartItem.product.photo.split(" ");
           $(".header-cart-wrapitem").append(`<li class="header-cart-item flex-w flex-t m-b-12">
                        <div class="header-cart-item-img">
                            <img src="../images/${images[1]}" alt="IMG">
                        </div>
                        <div class="header-cart-item-txt p-t-8">
                            <a href=${cartItem.product.slug}" class="header-cart-item-name m-b-18 hov-cl1 trans-04">
                                ${cartItem.product.title}
                            </a>
                            <span class="header-cart-item-info">
                            ${cartItem.quantity} x $${cartItem.price}.00
                            </span>
                        </div>
                    </li>`);
        });
        $(".header-cart-total").text(`Total: $${subTotal}`);
    }
    else
        location.reload();
}

cartService.get=function () {
    $.ajax({
        url: "https://localhost:44322/api/Cart/GetBySessionId",
        method: "GET",
        xhrFields: {
            withCredentials: true
        },
        success: function (data) {
            if (data===undefined)
            return;
            cart = data;
            var pCount = 0;
            $.each(data.items, function (index, cartItem) {
                pCount += cartItem.quantity;
            });
            $(".js-show-cart").attr("data-notify", pCount);
        }
    });
}
cartService.get();
//click quick view for add to cart
cartService.add = function (addCart) {
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
            cart = data;
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

cartService.removeItem = function () { }