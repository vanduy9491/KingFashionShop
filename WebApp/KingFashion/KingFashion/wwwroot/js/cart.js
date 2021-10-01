﻿cartService = {};
var Id = 0;
var total = 0;
cart = undefined;
/*-----------------------CART HEADER-----------------------*/
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
        cartService.updateCartHeader(cart.items);
        $('.js-panel-cart').addClass('show-header-cart');
    } else
        location.reload();
}

cartService.homeCartInit = function () {
    if (!location.pathname.startsWith("/Cart"))
        $.ajax({
            url: "https://localhost:44322/api/Cart/GetBySessionId",
            method: "GET",
            xhrFields: {
                withCredentials: true
            },
            success: function (data) {
                if (data === undefined)
                    return;
                cart = data;
                cartService.updateNotify(data.items);
            }
        });
}
cartService.homeCartInit();
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
            if (data === undefined)
                return;
            cart = data;
            cartService.updateNotify(data.items);
        }
    });
}

cartService.removeItem = function (productId) {
    $.ajax({
        url: "https://localhost:44322/api/Cart/Remove?productId=" + productId,
        method: "GET",
        xhrFields: {
            withCredentials: true
        },
        success: function (data) {
            if (data === undefined)
                return;
            cart = data;
            cartService.updateNotify(data.items);
            cartService.updateCartHeader(data.items);
            if (location.pathname.startsWith("/Cart"))
                cartService.updateCart(data.items);
        }
    });
}

cartService.updateNotify = function (items) {
    var pCount = 0;
    $.each(items, function (index, cartItem) {
        pCount += cartItem.quantity;
    });
    $(".js-show-cart").attr("data-notify", pCount);
}
cartService.updateCartHeader = function (items) {
    var subTotal = 0;
    $(".header-cart-wrapitem").empty();
    $.each(items, function (index, cartItem) {
        subTotal += cartItem.quantity * cartItem.price;
        $(".header-cart-wrapitem").append(`<li class="header-cart-item flex-w flex-t m-b-12">
                        <div class="header-cart-item-img" data-item="${cartItem.product.id}">
                            <img src="../images/${cartItem.product.mainPhoto}" alt="IMG">
                        </div>
                        <div class="header-cart-item-txt p-t-8">
                            <a href=${cartItem.product.slug}" class="header-cart-item-name m-b-18 hov-cl1 trans-04">
                                ${cartItem.product.title}
                            </a>
                            <span class="header-cart-item-info">
                            ${cartItem.quantity} x ${cartItem.price} VND
                            </span>
                        </div>
                    </li>`);
    });
    $(".header-cart-total").text(`Total: ${subTotal} VND`);
    $(".header-cart-item-img").on('click', function (e) {
        let productId = $(this).attr('data-item');
        cartService.removeItem(productId);
        $(this).parent().remove();
    });
}


/*---------------------------------------CART------------------------*/

cartService.cartInit = function () {
    if (location.pathname.startsWith("/Cart"))
        $.ajax({
            url: "https://localhost:44322/api/Cart/GetBySessionId",
            method: "GET",
            xhrFields: {
                withCredentials: true
            },
            success: function (data) {
                if (data === undefined)
                    return;
                cart = data;
                cartService.updateNotify(data.items);
                cartService.updateCart(data.items);
            }
        });

}
cartService.changeItem = function (productId, quantity) {
    $.ajax({
        url: `https://localhost:44322/api/Cart/ChangeItem?productId=${productId}&quantity=${quantity}`,
        method: "GET",
        xhrFields: {
            withCredentials: true
        },
        success: function (data) {
            if (data === undefined)
                return;
            cart = data;
            cartService.updateNotify(data.items);
            cartService.updateCartHeader(data.items);
            cartService.updateCart(data.items);
            cartService.totalCart(data.items);
        }
    });

}
var total = 0;
cartService.cartInit();
cartService.updateCart = function (items) {
    var subTotal = 0;
    $(".table-shopping-cart").empty();
    $.each(items, function (index, cartItem) {
        subTotal += cartItem.quantity * cartItem.price;
        total = subTotal;
        $(".table-shopping-cart").append(
            `<tr class="table_row">
                            <td class="column-1">
                                <div data-item="${cartItem.product.id}" class="how-itemcart1">
                                    <img src="../images/${cartItem.product.mainPhoto}" alt="IMG">
                                </div>
                            </td>
                            <td class="column-2">${cartItem.product.title}</td>
                            <td class="column-3"> ${cartItem.price} VND</td>
                            <td class="column-4">
                                <div  class="wrap-num-product flex-w m-l-auto m-r-0">
                                    <div data-item="${cartItem.product.id}" class="btn-num-product-down cl8 hov-btn3 trans-04 flex-c-m">
                                        <i class="fs-16 zmdi zmdi-minus"></i>
                                    </div>
                                    <input class="mtext-104 cl3 txt-center num-product" type="number" name="num-product1" value="${cartItem.quantity}">
                                    <div data-item="${cartItem.product.id}" class="btn-num-product-up cl8 hov-btn3 trans-04 flex-c-m">
                                        <i class="fs-16 zmdi zmdi-plus"></i>
                                    </div>
                                </div>
                            </td>
                            <td class="column-5"> ${cartItem.price * cartItem.quantity} VND</td> 
            </tr>`
        )



    });
    $(".size-209, .p-t-1").children().text(`${subTotal} VND`)
    /*==================================================================
    [ +/- num product ]*/
    $('.btn-num-product-down').on('click', function () {
        var numProduct = Number($(this).next().val());
        if (numProduct > 0) {
            numProduct -= 1;
            $(this).next().val(numProduct);
            let productId = $(this).attr('data-item');
            cartService.changeItem(productId, numProduct);

        }
    });

    $('.btn-num-product-up').on('click', function () {
        var numProduct = Number($(this).prev().val());
        numProduct += 1;
        $(this).prev().val(numProduct);
        let productId = $(this).attr('data-item');
        cartService.changeItem(productId, numProduct);
    });

    $(".how-itemcart1").on('click', function (e) {
        let productId = $(this).attr('data-item');
        cartService.removeItem(productId);
        $(this).parent().parent().remove();
    });
}




/*check out*/
$("#clickPayment").on("click", function () {
    cartService.checkout(Id);
})

cartService.checkout = function (id) {


    let firstName = $('input[name="FirstName"]').val();
    let lastName = $('input[name="LastName"]').val();
    let mobile = $('input[name="Mobile"]').val();
    let email = $('input[name="Email"]').val();
    let line1 = $('input[name="Line1"]').val();
    let city = $('input[name="City"]').val();
    let province = $('input[name="Province"]').val();

    $.ajax({
        url: location.origin + `/Checkout?firstName=${firstName}&lastName=${lastName}&mobile=${mobile}&email=${email}&line1=${line1}&city=${city}&province=${province}`,
        method: "POST",
        success: function (data) {
            if (data !== undefined || data !== null)
                return;
            swal("Checkout successful", "", "success");
            setTimeout(function () {
                location = "/";
            }, 3000)
        }
    });
}


//$("#clickProcess").on("click", function () {
//    cartService.totalCart();
//})
//cartService.totalCart = function (items) {    

//    $("#showCart").append(
//        `
//                        <li class="list-group-item d-flex justify-content-between bg-light">
//                        <small class="text-muted">${total} (VND)</small>
//                        <strong class="text-info">tổng tiền</strong>
//                    </li>
//            `
//    )   
//}