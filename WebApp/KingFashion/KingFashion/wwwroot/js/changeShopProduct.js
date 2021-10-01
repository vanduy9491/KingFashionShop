var product = {}
product.changeShop = function (id, shop) {
    bootbox.confirm({
        title: `Danh Mục ${shop ? "Đang Bán" : "Tạm Ngưng"}`,
        message: `Bạn có muốn danh mục đã ${shop ? "Tạm Ngưng" : "Đang Bán"}?`,
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
                var changeStatusProductObj = {};
                changeStatusProductObj.id = id;
                changeStatusProductObj.shop = shop;
                $.ajax({
                    url: "https://localhost:44368/Product/ChangeShop",
                    method: "PUT",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify(changeStatusProductObj),
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