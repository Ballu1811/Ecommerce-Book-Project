var dataTable;  // for shorting, filteration data

$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"

        },
        "columns": [
            { "data": "name", "width": "70%" },
            { "data": "streetAddress", "width": "70%" },
            { "data": "city", "width": "70%" },
            { "data": "state", "width": "70%" },
            { "data": "phoneNumber", "width": "70%" },
            {
                "data": "isAuthorizedCompany",
                "render": function (data) {
                    if (data) {
                        return ` <input type="checkbox" disabled checked /> `;
                    }
                    else {
                        return ` <input type="checkbox" disabled /> `;
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Company/Upsert/${data}" class="btn btn-info">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a class="btn btn-danger" onclick=Delete("/Admin/Company/Delete/${data}")>
                                <i class="fas fa-trash-alt"></i>
                            </a>
                        </div>
                    `;
                }
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "Want to delete data?",
        text: "Delete Information",
        buttons: true,
        icon: "warning"
    }).then((wildelete) => {
        if (wildelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}