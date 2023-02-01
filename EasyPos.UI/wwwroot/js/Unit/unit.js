

let baseUrl = 'https://localhost:7173/api';
let counter = 1;
var dataTable;
$(document).ready(function () {

    loadDataTable();
 
   
});


function loadDataTable()
{

    let item = getAllItem();
    
    dataTable = $('#tbl_unit').DataTable({
        "autoWidth": true,
        "data": item,
        "columns": [
            {
                "render": function (data, type, full, meta) {
                    return counter++;
                }
            },
            { 'data': "name", },
            {
                "title": "Related Unit Name",
                "render": function (data, type, full, meta) {

                    var relatedUnit = full['relatedUnit'];
                    if (relatedUnit != null) {
                        var relatedUnitName = relatedUnit.name;
                        return '<span>' + relatedUnitName + '</span > '
                    }
                    else {
                        return '';
                    }




                }
            },
            { "data": "operator"},
            { "data": "relatedBy"},

            {
                "title": "Result",
                "render": function (data, type, full, meta) {
                    debugger;
                    var relatedUnit = full['relatedUnit'];
                    if (relatedUnit != null) {
                        var relatedUnitName = relatedUnit.name;
                        return '<span>' + full['name'] + ' = ' + +'1 ' + relatedUnitName + ' ' + full['operator'] + ' ' + full['relatedBy'] + '</span > '
                    }
                    else {
                        return '';
                    }



                    return 'Test';
                }
            },

            {
                "data": "id",
               
                "render": function (data, type, full, meta) {
                    return '<div class="dropdown action-button mx-2">'
                        + '<button class="btn btn-success btn-sm dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false" ><i class="fa-solid fa-gears"></i> Manage</button>'
                        + '<ul class="dropdown-menu">'
                        + '<li><a  href="/Units/Upsert?Id=' + data + '" class="dropdown-item text-warning"><i class="fa-solid fa-pen-to-square"></i> Edit </a></li>'
                        + '<li><a class="dropdown-item text-danger" onClick="Delete(' + data + ')" style="cursor:pointer"> <i class="fa-solid fa-trash"></i> Delete</a> </li> </ul> </div>'
                }
            },

        ]

    });
    

   
}

function getAllItem() {
    
    var item;
    $.ajax({
        url: baseUrl + "/Units/GetAll",
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
                url: baseUrl+"/Brands/Delete?id=" + id,
                type: 'DELETE',
                success: function (data) {

                    
                    location.reload(true);
                        toastr.success(data.message);
                   
                }
            })
        }
    })
}


$("#Unit_RelatedUnitId").change(function () {
    debugger;
    UnitCount();

});

$('#Unit_Operator').change(function () {
    debugger;
    UnitCount();
});
$('#Unit_RelatedBy').keyup(function () {
    debugger;
    UnitCount();
});
function UnitCount() {
    debugger;
    var unitName = $("#Unit_Name").val();
    var relatedBy = $("#Unit_RelatedBy").val();
    var relatedUnit = $("#Unit_RelatedUnitId option:selected").text();
    var relatedUnitId = $("#Unit_RelatedUnitId option:selected").val();
    var operator = $("#Unit_Operator option:selected").val();
    $('.unit-count label').html("1 " + unitName + "=" + (relatedUnitId != "" ? relatedUnit : "") + " " + operator + " " + (relatedBy > 0 ? relatedBy : ""));
    $('.unit-count').show();

}