var product = {};
$.fn.dataTable.ext.errMode = 'none';
$('#tbProduct').DataTable({
    columnDefs: [
        { orderable: false, targets:11 }
    ],
    order: [[0, 'desc']]
});
$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $('.no-photo').attr('src', window.URL.createObjectURL($(this)[0].files[0]));
    $(".custom-file-label").addClass("selected").html(fileName);
});
product.changeStatus = function (id, status) {
    bootbox.confirm({
        title: `Danh Mục ${status ? "Sẵn Có" : "Hết Hàng"}`,
        message: `Bạn có muốn danh mục đã ${status ? "hết hàng" : "sẵn có"}?`,
        buttons: {
            cancel: {
                label: '<i class="ti-close"></i> Trở Về'
            },
            confirm: {
                label: '<i class="ti-check"></i> Đồng ý'
            }
        },
        callback: function (result) {
            if (result) {
                var changeStatusCategoryObj = {};
                changeStatusCategoryObj.ProductId = id;
                changeStatusCategoryObj.Status = status;
                $.ajax({
                    url: "https://localhost:44368/Product/ChangeStatus",
                    method: "PUT",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify(changeStatusCategoryObj),
                    success: function (data) {
                        if (data.success) {
                            location.reload();
                            $.notify(data.message, "success");
                        }
                        else {
                            $.notify(data.message, "error");
                        }
                    }
                });
            }
        }
    });
}
product.openModel = function () {
    product.reset();
    $('#productModel').modal('show');
    $("#exampleModalLabel").html("Thêm Mới Sản Phẩm");
}
product.reset = function () {
    //reset error messages
    $('#frmProduct').validate().resetForm();
    //reset input values
    $('#frmProduct')[0].reset();
    //reset input color
    var inputs = $('#frmProduct input');
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].classList.remove('error');
    }
    //Reset category Id = 0
    $('input[name="ProductId"]').val(0);
}
product.changeIsDeleted = function (id, isDeleted) {
    bootbox.confirm({
        title: `Danh Mục ${isDeleted ? "Đã Xóa" : "Hoạt Động"}`,
        message: `Bạn có muốn ${isDeleted ? "hoạt động" : "xóa"} ngay bây giời không?`,
        buttons: {
            cancel: {
                label: '<i class="ti-close"></i> Trở Về'
            },
            confirm: {
                label: '<i class="ti-check"></i> Đồng ý'
            }
        },
        callback: function (result) {
            if (result) {
                var changeStatusCategoryObj = {};
                changeStatusCategoryObj.ProductId = id;
                changeStatusCategoryObj.IsDeleted = isDeleted;
                $.ajax({
                    url: "https://localhost:44368/Product/ChangeIsDeleted",
                    method: "PUT",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify(changeStatusCategoryObj),
                    success: function (data) {
                        if (data.success) {
                            location.reload();
                            $.notify(data.message, "success");
                        }
                        else {
                            $.notify(data.message, "error");
                        }
                    }
                });
            }
        }
    });
}
product.save = function () {
    let url = URL.createObjectURL($('.no-photo').attr('src', window.URL.createObjectURL($(this)[0].files[0])));
    imgElem.setAttribute('src', url);
    if ($('#frmProduct').valid()) {
        let catDetailsId = parseInt($('input[name="CatDetailsId"]').val());
        let productId = parseInt($('input[name="ProductId"]').val());
        //create new category
        if (productId == 0) {
            var createProductObj = {
                ProductName: $('input[name="ProductName"]').val(),
                Price: parseInt($('input[name="Price"]').val()),
                Quantity: parseInt($('input[name="Quantity"]').val())
            };
            createProductObj.Status = $('input[name="Status"]').is(":checked");
            createProductObj.Color = $('option[name="Color"]').val();
            createProductObj.Photo = $('input[name="Photo"]').val();
            createProductObj.ProductCode = $('input[name="ProductCode"]').val();
            createProductObj.Description = $('input[name="Description"]').val();
            createProductObj.Size = $('option[name="Size"]').val();
            createProductObj.CatDetailsId = parseInt($('input[name="CatDetailsId"]').val());
            $.ajax({
                url: `https://localhost:44368/Product/${catDetailsId}/Create`,
                method: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(createProductObj),
                success: function (data) {
                    if (data.success) {
                        $('#productModel').modal('hide');
                        /*$.notify(data.message, "success");*/
                        location.reload();
                    }
                    else {
                        $.notify(data.message, "error");
                    }
                }
            });
        }
        //update category
        else {
            var updateProductObj = {};
            updateProductObj.productId = productId;
            updateProductObj.productName = $('input[name="ProductName"]').val();
            updateProductObj.status = $('input[name="Status"]').is(":checked");
            updateProductObj.color = $('option[name="Color"]').val();
            updateProductObj.price = parseInt($('input[name="Price"]').val());
            updateProductObj.photo = $('input[name="Photo"]').val();
            updateProductObj.quantity = parseInt($('input[name="Quantity"]').val());
            updateProductObj.productCode = $('input[name="ProductCode"]').val();
            updateProductObj.description = $('input[name="Description"]').val();
            updateProductObj.size = $('option[name="Size"]').val();
            $.ajax({
                url: "https://localhost:44368/Product/Update",
                method: "PUT",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(updateProductObj),
                success: function (data) {
                    if (data.success) {
                        $('#productModel').modal('hide');
                        /*$.notify(data.message, "success");*/
                        location.reload();
                    }
                    else {
                        $.notify(data.message, "error");
                    }
                }
            });
        }
    }
}
product.get = function (id) {
    $.ajax({
        url: `https://localhost:44368/Product/Get/${id}`,
        method: "GET",
        success: function (data) {
            $('#productModel').modal('show');
            $('input[name="ProductName"]').val(data.productName);
            $('input[name="ProductId"]').val(data.productId);
            $('input[name="Status"]').prop('checked', data.status);
            $('option[name="Color"]').val(data.color);
            $('input[name="Price"]').val(data.price);
            $('input[name="Photo"]').val(data.photo);
            $('input[name="Quantity"]').val(data.quantity);
            $('input[name="ProductCode"]').val(data.productCode);
            $('input[name="Description"]').val(data.description);
            $('option[name="Size"]').val(data.size);
        }
    });
    $("#exampleModalLabel").html("Sửa Sản Phẩm");
}
$(document).ready(function () {
    $('#tbProduct').DataTable();
});