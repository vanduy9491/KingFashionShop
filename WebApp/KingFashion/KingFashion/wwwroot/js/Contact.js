var contact = {};


contact.showData = function () {
    $.ajax({
        url: "https://localhost:44368/Contact/Get",
        method: "GET",
        success: function (data) {
            $('#tbCategory tbody').empty();
            $('#tbCategory thead').empty();
            $('#tbContact thead').append(
                `<tr id="trr">
                    <th>
                        #
                    </th>
                    <th>
                        Email
                    </th>
                    <th>
                        Nội dung
                    </th>
                    <th>
                        Trạng thái
                    </th>
                    <th></th>
                </tr>`)
            $.each(data, function (index, item) {
                $('#tbContact tbody').append(
                    `
                        <tr>
                            <td>${item.id}</td>
                            <td>${item.email}</td>
                            <td>${item.content}</td>
                            <td>${item.status ? 'Đã phản hồi' : 'Chưa phản hồi'}</td>
                            <td>
                                  <a href='https://localhost:44368/Contact/ViewContact/${item.id}' class='btn btn-info btn-mat btn-sm' title="Xem phản hồi" /*onclick="contact.get(${item.id})*/">Xem chi tiết
                                    <i class="ti-eye"></i>
                                </a>
                            </td>
                        </tr>
                    `
                );
            });
            $.fn.dataTable.ext.errMode = 'none';
            $('#tbContact').DataTable({
                columnDefs: [
                    { orderable: false, targets: [2, 3] }
                ],
                order: [[0, 'desc']]
            });
            $('#tbContact_wrapper').addClass('w-100');
        }
    });
}
$(document).ready(function () {
    contact.showData();
});
//$("#btnSend").on("click", function () {
//    category.save();
//})
contact.openModel = function () {
    $('#replyModel').modal('show');
};
contact.get = function (id) {
    $.ajax({
        url: `https://localhost:44368/Contact/ViewContact/${id}`,
        method: "GET",
        success: function (data) {
            $('#replyModel').modal('show');
            /*$('input[name="EmailTo"]').val() = data.email;*/
        }
    });
}
contact.save = function (id) {
    var emailObj = {};
    emailObj.To = $('input[name="EmailTo"]').val();
    emailObj.Subject = $('input[name="Subject"]').val();
    emailObj.Body = $('textarea[name="ContentReply"]').val();
    emailObj.FromEmail = "kingstartloving@gmail.com"
    emailObj.FromPassword = 'Loveemyeu2';
    $.ajax({
        url: "https://localhost:44368/Contact/Reply",
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(emailObj),
        success: function (data) {
            if (data.success) {
                contact.changeStatus(id);
            }
            else {
                alert("Trả lời không thành công");
            }
        }
    });
}
contact.changeStatus = function (id) {
    var changeStatusContact = {};
        changeStatusContact.Id = id
    $.ajax({
        url: `https://localhost:44368/ChangeStatus`,
        method: "PUT",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(changeStatusContact),
        success: function (data) {
            if (data.success) {
                alert("Trả lời thành công");
            }
            else {
                alert("Trả lời không thành công");
            }
        }
    });
}

