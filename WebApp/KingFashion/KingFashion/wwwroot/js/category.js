var category = {};
category.showData = function () {
    $.ajax({
        url: "https://localhost:44368/Category/Get",
        method: "GET",
        success: function (data) {
            $('#tbCategory tbody').empty();
            $('#tbCategory thead').empty();
            $('#back').empty();
            $('#create').empty();
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
    let pId = id;
    $.ajax({
        url: `https://localhost:44368/Category/Get/${id} `,
        method: "GET",
        success: function (data) {
            $('#create').empty();
            $('#create').append(
                `<p id="pCreate">
                    <a id="create" href="javascript:;" class="btn btn-out-dashed btn-success btn-square" onclick="category.openModel()"><i class="ti-plus"></i>Thêm Mới</a>
                </p>
                <p id="pCreate">
                    <a id="back" href="javascript:;" class="btn btn-out-dashed btn-info btn-square" onclick="category.showData()"><i class="ti-plus"></i>Quay lại</a>
                </p>
                <div class="modal fade" id="categoryModel" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Thêm mới danh mục</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <form id="frmCategory">
                                    <div class="form-group">
                                        <label>Tên danh mục</label>
                                        <input type="text" name="Title" class="form-control" data-rule-required="true" />
                                    </div>
                                    <div class="form-group">
                                        <label>Tiêu đề meta</label>
                                        <input type="text" name="MetaTitle" class="form-control"  />
                                    </div>
                                    <div class="form-group">
                                        <label>Slug</label>
                                        <input type="text" name="Slug" class="form-control" data-rule-required="true" />
                                    </div>
                                        <div class="form-group">
                                        <label>Nội dung</label>
                                        <textarea id="Content" type="text" name="Content" class="form-control"  ></textarea>
                                        <script>CKEDITOR.replace("Content");</script>
                                    </div>
                                </form>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                    <i class="ti-close"></i> Đóng
                                </button>
                                <button type="button" class="btn btn-primary" onclick="category.save(${pId})">
                                    <i class="ti-check"></i> Lưu
                                </button>
                            </div>
                        </div>
                    </div>
                </div>`);

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
                            <td class='text-right btn btn-sm ${item.status ? 'btn-success' : 'btn-warning'}'>${item.status ? '  Sẵn Có ' : '  Hết Hàng '}</td>
                            <td class='text-right'>
                                  </a>
                                    <a href='javascript:;' class='btn btn-sm ${item.status ? 'btn-warning' : 'btn-success'}'
                                   title='${!item.status ? 'Sẵn Có' : 'Hết Hàng'}' onclick='category.changeStatus(${item.id}, ${item.status})'>
                                    <i class='fa ${item.status ? 'ti-lock' : 'ti-unlock'}'></i>${!item.status ? 'Sẵn Có' : 'Hết Hàng'}
                                </a>
                                <a href="/Product/Index/${item.id}" class='btn btn-info btn-mat btn-sm' title="Xem Danh Mục">Xem Danh Mục
                                    <i class="ti-eye"></i>
                                </a>
                                <a href='javascript:;' class='btn btn-sm btn-secondary' title="Modify category" onclick="category.get(${item.id})">
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

category.save = function (id) {
    if ($('#frmCategory').valid()) {
        /*let Id = parseInt($('input[name="CategoryId"]').val());*/
        //create new category
        /*if (Id == 0) {*/
        var createCategoryObj = {};
        createCategoryObj.Title = $('input[name="Title"]').val();
        createCategoryObj.MetaTitle = $('input[name="MetaTitle"]').val();
        createCategoryObj.Slug = $('input[name="Slug"]').val();
        createCategoryObj.Content = $('textarea[name="Content"]').val();
        createCategoryObj.ParentId = id;
        $.ajax({
            url: "https://localhost:44368/Category/Create",
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(createCategoryObj),
            success: function (data) {
                if (data.success) {
                    $('#categoryModel').modal('hide');
                    $('#pCreate').empty();
                    category.showCatByParentId(id);
                    $.notify(data.message, "success");
                }
                else {
                    $.notify(data.message, "error");
                }
            }
        });
        /* }*/
        //update category
        //else {
        //    var updateCategoryObj = {};
        //    updateCategoryObj.CategoryId = categoryId;
        //    updateCategoryObj.CategoryName = $('input[name="CategoryName"]').val();
        //    updateCategoryObj.Status = $('input[name="Status"]').is(":checked");
        //    $.ajax({
        //        url: "https://localhost:44368/Category/Update",
        //        method: "PUT",
        //        dataType: "json",
        //        contentType: "application/json",
        //        data: JSON.stringify(updateCategoryObj),
        //        success: function (data) {
        //            if (data.success) {
        //                $('#categoryModel').modal('hide');
        //                category.showData();
        //                $.notify(data.message, "success");
        //            }
        //            else {
        //                $.notify(data.message, "error");
        //            }
        //        }
        //    });
        //}
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
    $("#exampleModalLabel").html("Sửa Danh Mục");
    $.ajax({
        url: `https://localhost:44368/Category/Get/${id}`,
        method: "GET",
        success: function (data) {
            $('#categoryModel').modal('show');  
            $('input[name="CategoryName"]').val(data.categoryName);
            $('input[name="CategoryId"]').val(data.categoryId);
        }
    });

}
$(document).ready(function () {
    category.showData();
});