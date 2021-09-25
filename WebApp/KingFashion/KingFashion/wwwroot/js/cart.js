$('.js-addcart-detail').on('click', function (e) {
    e.preventDefault();
    let productId = $(this).attr('data-item');
    $.ajax({
        url: "https://localhost:44322/api/Cart/GetBysessionId?sessionId=${sessionId}"
        method: "GET",
        success: function (data) {

        }
    })
});

//click quick view for add to cart
product.save = function () {
   
    $('.js-addcart-detail').on('click', function () {
        $('.header-cart-item').addClass('show-header-cart');
    });
            $.ajax({
                url: `https://localhost:44322/api/Cart/GetBysessionId?sessionId=${sessionId}`,
                method: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(createProductObj),
                success: function (data) {
                    $('#productModel').modal('show');
                    $('input[name="ProductName"]').val(data.productName);
                    $('input[name="ProductId"]').val(data.productId);
                    $('option[name="Color"]').val(data.color);
                }
            });
        }  
    }
}

//click add to cart 
product.save = function () {
    
    $('.js - addcart - detail').on('click', function () {
        $('.header-cart-item').addClass('show-header-cart');
    });
    $.ajax({
        url: `https://localhost:44322/api/Cart/GetBysessionId?sessionId=${sessionId}`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(createProductObj),
        success: function (data) {
            $('#productModel').modal('show');
            $('.column-2').text(data.productName);
            $('.column-3').text(data.price);
            $('.btn-num-product-down').val(); 
            $('.btn-num-product-up').val();
        }
    });
}  
    }
}