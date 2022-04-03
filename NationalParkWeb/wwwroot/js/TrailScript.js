var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Trail/GetAllTrail",
            "datatype": "json",
            "type": "GET"
        },
        "columns": [
            { "data": "nationalPark.name", "width": "20%" },
            { "data": "name", "width": "20%" },
            { "data": "distance", "width": "10%" },
            { "data": "elevation", "width": "10%" },
            {
                "data": "id", "render": function (data) {
                    return `<div class="text-center">
                            <a href="/Trail/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            Edit
                            </a>
                            &nbsp;&nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;' onclick=Delete('/Trail/Delete?id='+${data}) >
                            Delete
                            </a>
                            <\div>`;
                }, "width": "40%"
            }
        ]

    });
}

function Delete(url) {
    swal({
        title: "Are you sure ?",
        icon: "warning",
        text: "Once deleted, you will not be able to recover.",
        dangerMode: true,
        buttons: true

    }).then((willDelete) => {
        if (willDelete) {
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

            });
        }
    });
}