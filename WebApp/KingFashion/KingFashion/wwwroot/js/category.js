var category = {};
var categoryId = 0;
category.showData = function () {
    $.ajax({
        url: "https://localhost:44368/Category/Get",
        method: "GET",
        success: function (data) {
            $('#tbCategory tbody').empty();
            $('#tbCategory thead').empty();
            $('#Ccreate').hide();
            $('#pCreate').hide();
            $('#tbCategory thead').append(
                `<tr id="trr">
                    <th>
                        #
                    </th>
                    <th>
                        Danh Mục
                    </th>
                    <th></th>
                </tr>`)
            $.each(data, function (index, item) {
                $('#tbCategory tbody').append(
                    `
                        <tr>
                            <td>${item.id}</td>
                            <td>${item.title}</td>
                            <td class='text-right'>
                                <a href='javascript:;' class='btn btn-info btn-mat btn-sm' title="Xem Danh Mục" onclick="category.showCatByParentId(${item.id})">Xem Danh Mục
                                    <i class="ti-eye"></i>
                                </a>
                            </td>
                        </tr>
                    `
                );
            });
            $.fn.dataTable.ext.errMode = 'none';
            $('#tbCategory').DataTable({
                columnDefs: [
                    { orderable: false, targets: 2 }
                ],
                order: [[0, 'desc']]
            });
            $('#tbCategory_wrapper').addClass('w-100');
        }
    });
}
category.showCatByParentId = function (id) {
    categoryId = id;
    $.ajax({
        url: `https://localhost:44368/Category/GetByParentId?parentId=${id} `,
        method: "GET",
        success: function (data) {
            $('#Ccreate').show();
            $('#pCreate').show();
            $('#tbCategory tbody').empty();
            $('#tbCategory thead').empty();
            $('#tbCategory thead').append(
                `<tr id="trr">
                    <th>
                        #
                    </th>
                    <th>
                        Danh Mục
                    </th>
                    <th>Trạng Thái</th>
                    <th></th>
                </tr>`)
            $.each(data, function (index, item) {
                $('#tbCategory tbody').append(
                    `
                        <tr>
                            <td>${item.id}</td>
                            <td>${item.title}</td>
                            <td class='btn btn-sm ${item.status ? 'btn-success' : 'btn-warning'}'>${item.status ? '  Sẵn Có ' : '  Hết Hàng '}</td>
                            <td class='text-right'>
                                  </a>
                                    <a href='javascript:;' class='btn btn-sm ${item.status ? 'btn-warning' : 'btn-success'}'
                                   title='${!item.status ? 'Sẵn Có' : 'Hết Hàng'}' onclick='category.changeStatus(${item.id}, ${item.status})'>
                                    <i class='fa ${item.status ? 'ti-lock' : 'ti-unlock'}'></i>${!item.status ? 'Sẵn Có' : 'Hết Hàng'}
                                </a>
                                <a href="/Product/Index/${item.id}" class='btn btn-info btn-mat btn-sm' title="Xem Danh Mục">Xem Danh Mục
                                    <i class="ti-eye"></i>
                                </a>
                                <a href='javascript:;' class='btn btn-sm btn-secondary' title="Modify category" onclick="category.getbyCatId(${item.id})">
                                   Sửa <i class="ti-reload"></i>
                                </a>
                            </td>
                        </tr>
                    `
                );
            });
            $.fn.dataTable.ext.errMode = 'none';
            $('#tbCategory').DataTable({
                columnDefs: [
                    { orderable: false, targets: 2 }
                ],
                order: [[0, 'desc']]
            });
            $('#tbCategory_wrapper').addClass('w-100');
        }
    });
}
category.openModel = function () {
    category.reset();
    $('#categoryModel').modal('show');
    $("#exampleModalLabel").html("Thêm mới Danh Mục");
}

category.changeStatus = function (id, status) {
    bootbox.confirm({
        title: `Danh Mục ${status ? "Sẵn Có" : "Hết Hàng"}`,
        message: `Bạn có muốn danh mục đã ${status ? "hết hàng" : "sẵn có"}?` ,
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
                changeStatusCategoryObj.id = id;
                changeStatusCategoryObj.status = status;
                $.ajax({
                    url: "https://localhost:44368/Category/ChangeStatus",
                    method: "PUT",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify(changeStatusCategoryObj),
                    success: function (data) {
                        if (data.success) {
                            category.showCatByParentId(categoryId);
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

$("#btnSave").on("click", function () {
    category.save(categoryId);
})

category.save = function (id) {
    if ($('#frmCategory').valid()) {
        let parentId = id
        let catId = parseInt($('input[name="Id"]').val());
        if (catId == 0) {
            var createCategoryObj = {};
            createCategoryObj.Title = $('input[name="Title"]').val();
            createCategoryObj.MetaTitle = $('input[name="MetaTitle"]').val();
            createCategoryObj.Slug = $('input[name="Slug"]').val();
            createCategoryObj.Content = $('textarea[name="Content"]').val();
            createCategoryObj.Status = $('input[name="Status"]').is(":checked");
            createCategoryObj.ParentId = parentId;
            $.ajax({
                url: "https://localhost:44368/Category/Create",
                method: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(createCategoryObj),
                success: function (data) {
                    if (data.success) {
                        $('#categoryModel').modal('hide');
                        $('#Ccreate').show();
                        $('#pCreate').show();
                        category.showCatByParentId(id);
                        $.notify(data.message, "success");
                    }
                    else {
                        $.notify(data.message, "error");
                    }
                }
            });
        }
        else {
            var updateCategoryObj = {};
            updateCategoryObj.Title = $('input[name="Title"]').val();
            updateCategoryObj.MetaTitle = $('input[name="MetaTitle"]').val();
            updateCategoryObj.Slug = $('input[name="Slug"]').val();
            updateCategoryObj.Content = $('textarea[name="Content"]').val();
            updateCategoryObj.Status = $('input[name="Status"]').is(":checked");
            updateCategoryObj.Id = catId;
            updateCategoryObj.ParentId = parseInt($('input[name="ParentId"]').val());
            $.ajax({
                url: `https://localhost:44368/Category/Update`,
                method: "PUT",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(updateCategoryObj),
                success: function (data) {
                    if (data.success) {
                        $('#categoryModel').modal('hide');
                        $('#Ccreate').show();
                        $('#pCreate').show();
                        category.showCatByParentId(id);
                        $.notify(data.message, "success");
                    }
                    else {
                        $.notify(data.message, "error");
                    }
                }
            });
        }
    }
}
category.reset = function () {
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
    $('input[name="CategoryId"]').val(0);
}
category.get = function (id) {
    $.ajax({
        url: `https://localhost:44368/Category/GetByParentId?parentId=${id}`,
        method: "GET",
        success: function (data) {
            $('#categoryModel').modal('show');  
            $('input[name="CategoryName"]').val(data.categoryName);
            $('input[name="CategoryId"]').val(data.categoryId);
        }
    });
}
category.getbyCatId = function (id) {
    $("#exampleModalLabel").html("Sửa danh Mục");
    $.ajax({
        url: `https://localhost:44368/Category/GetCategoryById?id=${id}`,
        method: "GET",
        success: function (data) {
            $('#categoryModel').modal('show');
            $('input[name="Title"]').val(data.title);
            $('input[name="ParentId"]').val(data.parentId);
            $('input[name="Id"]').val(data.id);
            $('input[name="MetaTitle"]').val(data.metaTitle);
            $('input[name="Slug"]').val(data.slug);
            $('textarea[name="Content"]').val(data.content);
            $('input[name="Status"]').prop('checked', data.status);
        }
    });
}
$(document).ready(function () {
    category.showData();
});