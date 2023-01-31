

let baseUrl = 'https://localhost:7173/api';
let counter = 1;
var dataTable;
$(document).ready(function () {

    loadDataTable();
 
   
});


function loadDataTable()
{

    let item = getAllItem();
    
    dataTable = $('#tbl_supplier').DataTable({
        "autoWidth": true,
        "data": item,
        "columns": [
            {
                "render": function (data, type, full, meta) {
                    return counter++;
                }
            },
            { 'data': "name",},
            { 'data': "address",},
            { 'data': "phoneNumber",},
            { 'data': "openingBalance",},
            {
                "data": "id",
                "width": "40%",
                "render": function (data, type, full, meta) {
                    return '<div class="dropdown action-button mx-2">'
                        + '<button class="btn btn-success btn-sm dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false" ><i class="fa-solid fa-gears"></i> Manage</button>'
                        + '<ul class="dropdown-menu">'
                        + '<li><a  href="/Suppliers/Upsert?Id=' + data + '" class="dropdown-item text-warning"><i class="fa-solid fa-pen-to-square"></i> Edit </a></li>'
                        + '<li><a class="dropdown-item text-danger" onClick="Delete(' + data + ')" style="cursor:pointer"> <i class="fa-solid fa-trash"></i> Delete</a> </li> </ul> </div>'
                }
            },

        ]

    });
    

   
}

function getAllItem() {
    
    var item;
    $.ajax({
        url: baseUrl + "/Suppliers/GetAll",
        method: "GET",
        async: false,
        dataType: "json",
        success: function (data) {
            
            item = data; 
        },
        error: function (error) {
            console.log(error);
        }
    });
    
    return item;

}

function Delete(id) {
    
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: baseUrl +"/Suppliers/Delete?id=" + id,
                type: 'DELETE',
                success: function (data) {

                    
                    location.reload(true);
                        toastr.success(data.message);
                   
                }
            })
        }
    })
}