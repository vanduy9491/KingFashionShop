var categorydetail = {};
$.fn.dataTable.ext.errMode = 'none';
$('#tbCategory').DataTable({
    columnDefs: [
        { orderable: false, targets: 2 },
        { orderable: false, targets: 3 },
        { orderable: false, targets:4 }
    ],
    order: [[0, 'desc']]
});
$('#tbCategory_wrapper').addClass('w-100');
categorydetail.openModel = function () {
    categorydetail.reset();
    $('#categoryModel').modal('show');
    $("#exampleModalLabel").html("Thêm Mới Danh Mục");
}
categorydetail.save = function () {
    if ($('#frmCategory').valid()) {
        let catDetailsId = parseInt($('input[name="CatDetailsId"]').val());
        let categoryId = parseInt($('input[name="CategoryId"]').val());
        //create new category
        if (catDetailsId == 0) {
            var createCategoryObj = {};
            createCategoryObj.CatDetailsName = $('input[name="CatDetailsName"]').val();
            createCategoryObj.Status = $('input[name="Status"]').is(":checked");
            createCategoryObj.CategoryId = parseInt($('input[name="CategoryId"]').val());
            $.ajax({
                url: `https://localhost:44368/CategoryDetails/${categoryId}/Create`,
                method: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(createCategoryObj),
                success: function (data) {
                    if (data.success) {
                        $('#categoryModel').modal('hide');
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
            var updateCategoryObj = {};
            updateCategoryObj.catDetailsId = catDetailsId;
            updateCategoryObj.catDetailsName = $('input[name="CatDetailsName"]').val();
            updateCategoryObj.Status = $('input[name="Status"]').is(":checked");
            $.ajax({
                url: "https://localhost:44368/CategoryDetails/Update",
                method: "PUT",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(updateCategoryObj),
                success: function (data) {
                    if (data.success) {
                        $('#categoryModel').modal('hide');
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
categorydetail.reset = function () {
    //reset error messages
    $('#frmCategory').validate().resetForm();
    //reset input values
    $('#frmCategory')[0].reset();
    //reset input color
    var inputs = $('#frmCategory input');
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].classList.remove('error');
    }
    //Reset category Id = 0
    $('input[name="CatDetailsId"]').val(0);
}
categorydetail.changeStatus = function (id, status) {
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
                changeStatusCategoryObj.CatDetailsId = id;
                changeStatusCategoryObj.Status = status;
                $.ajax({
                    url: "https://localhost:44368/CategoryDetails/ChangeStatus",
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
categorydetail.changeIsDeleted = function (id, isDeleted) {
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
                changeStatusCategoryObj.CatDetailsId = id;
                changeStatusCategoryObj.IsDeleted = isDeleted;
                $.ajax({
                    url: "https://localhost:44368/CategoryDetails/ChangeIsDeleted",
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
categorydetail.get= function (id) {
    $.ajax({
        url: `https://localhost:44368/CategoryDetails/Get/${id}`,
        method: "GET",
        success: function (data) {
            $('#categoryModel').modal('show');
            $('input[name="CatDetailsName"]').val(data.catDetailsName);
            $('input[name="CatDetailsId"]').val(data.catDetailsId);
            $('input[name="Status"]').prop('checked', data.status);
        }
    });
    $("#exampleModalLabel").html("Sửa Danh Mục");
}
$(document).ready(function () {
    $('#tbCategory').DataTable();
});