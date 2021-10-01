var users = {}
users.showData = function () {
    $.ajax({
        url: "https://localhost:44368/User/Get",
        method: "GET",
        success: function (data) {
            $('#tbUser tbody').empty();
            $('#tbUser thead').empty();
            $('#tbUser thead').append(
                `<tr id="trr">
                    <th>
                        #
                    </th>
                    <th>
                        Tên Thành Viên
                    </th>
                    <th>Email</th>
                     <th>Bị Cấm</th>
                    <th></th>
                </tr>`)
            $.each(data, function (index, item) {
                $('#tbUser tbody').append(
                    `
                        <tr>
                            <td>${item.id}</td>
                            <td>${item.userName}</td>
                            <td>${item.email}</td>
                            <td><a class="btn btn-success btn-sm">${item.isDeleted ? "Bị Cấm"  : "Hoạt Động"}</a></td>
                            <td class='text-right'>
                                <a href='javascript:;' class='btn btn-danger btn-mat btn-sm' title="Cấm" onclick="users.changeIsDelted('${item.id.toString()}',${item.isDeleted})"><i class="ti ti-close"></i>
                                    
                                </a>
                            </td>
                        </tr>
                    `
                );
            });
            $.fn.dataTable.ext.errMode = 'none';
            $('#tbUser').DataTable({
                columnDefs: [
                    { orderable: false, targets: 4 }
                ],
                order: [[0, 'desc']]
            });
            $('#tbUser').addClass('w-100');

        }
    });
}
users.changeIsDelted = function (id, isDeleted) {
    bootbox.confirm({
        title: `Thành viên ${isDeleted ? "Hoạt động" : "Bị Cấm"}`,
        message: `Bạn có muốn thành viên đã ${isDeleted ? "bị cấm" : "hoạt động"}?`,
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
                var changeIsDeletedUserObj = {};
                changeIsDeletedUserObj.id = id;
                changeIsDeletedUserObj.isDeleted = isDeleted;
                $.ajax({
                    url: "https://localhost:44368/User/ChangeIsDeleted",
                    method: "PUT",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify(changeIsDeletedUserObj),
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
$(document).ready(function () {
    users.showData();
});