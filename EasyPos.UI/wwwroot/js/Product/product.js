

let baseUrl = 'https://localhost:7173/api';
let counter = 1;
var dataTable;
var index = 0;
var productPrice = 0;
var product = null;
var productVm = null;


$(document).ready(function () {

   
    loadDataTable();
   
});


$('#unitId').change(function () {
    debugger
    let id = $(this).val();

    $.ajax({
        url: baseUrl + "/Units/GetById?id=" + id,
        type: 'GET',
        success: function (data) {
            debugger
            var obj = data;
            if (obj.relatedUnitId == null) {
                $("#Product_SubUnitId").find("option:not(:first)").remove();
                $(".defaultSubUnitOption").html("No related unit found!");
                $("#Product_SubUnitId").val(0).change();
            }
            else {

                var option = '<option selected value="' + obj.relatedUnitId + '">pc </option>';
             
                $("#Product_SubUnitId").append(option);
                $(".defaultSubUnitOption").html("--Select Unit--");
                $("#Product_SubUnitId").val(0).change();

            }

            
         
        }
    })

})

function loadDataTable()
{

    let item = getAllItem();
    
    dataTable = $('#tbl_product').DataTable({
        "autoWidth": true,
        "data": item,
        "columns": [
            {
                "render": function (data, type, full, meta) {
                    return counter++;
                }
            },
            {
                'data': "imageUrl",
                'render': function (data, type, full, meta) {
                    return '<img src="' + data + '" style="width:40px;height:30px"/>'
                }
            },
            { 'data': "code"},
            { 'data': "name"},
            { 'data': "category.name"},
            { 'data': "brand.name"},
            {
                'data': "salePrice",
                'render': function (data, type, full, meta)
                {
                    return data + " " + 'Tk';
                }
            },
            {
                'data': "purchaseCost",
                
                'render': function (data, type, full, meta) {
                    return data + " " + 'Tk';
                }
            },
            {
                "data": "id",
                "width": "40%",
                "render": function (data, type, full, meta) {
                    return '<div class="dropdown action-button mx-2">'
                        + '<button class="btn btn-success btn-sm dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false" ><i class="fa-solid fa-gears"></i> Manage</button>'
                        + '<ul class="dropdown-menu">'
                        + '<li><a  href="/Products/Upsert?Id=' + data + '" class="dropdown-item text-warning"><i class="fa-solid fa-pen-to-square"></i> Edit </a></li>'
                        + '<li><a class="dropdown-item text-danger" onClick="Delete(' + data + ')" style="cursor:pointer"> <i class="fa-solid fa-trash"></i> Delete</a> </li> </ul> </div>'
                }
            },

        ]

    });
    

   
}

function getAllItem() {
    
    var item;
    $.ajax({
        url: baseUrl + "/Products/GetAll",
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
                url: baseUrl +"/Products/Delete?id=" + id,
                type: 'DELETE',
                success: function (data) {

                    
                    location.reload(true);
                        toastr.success(data.message);
                   
                }
            })
        }
    })
}


function SaveCategory() {

    
    let name = $("#categoryName").val();
    let description = $("#categoryDescription").val();
    

    if ($("#CaetgoryForm").valid()) {

        var formData = $("#CaetgoryForm").serialize();
        $.ajax({
            url: "/Categories/Upsert",
            type: "POST",
            data: formData,
            success: function (response) {
                
                location.reload(true);
            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });



    }

}

function SaveBrand() {
    
    let id = $("#brandId").val();
    let name = $("#brandName").val();
   
    if ($("#BrandForm").valid()) {

        
        var formData = new FormData();

        formData.append("Brand.Id", id);
        formData.append("Brand.Name", name);
        formData.append("file", $("#brandLogo")[0].files[0]);

        $.ajax({
            url: "/Brands/Upsert",
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                
                location.reload(true);
            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });



    }

}
