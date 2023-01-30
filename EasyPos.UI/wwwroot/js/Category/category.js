$(document).ready(function () {

    loadDataTable();
   
});


function loadDataTable()
{
    debugger
    var categories = getAllItem();
    JsonObject = JSON.parse(JSON.stringify(categories));

    debugger
    let name = JsonObject.name;

    //dataTable = $('#tbl_Category').DataTable({

    //    'autoWidth': true,
    //    'ajax': {
    //        'url': basUrl + '/Categories',
    //        'dataType': 'json',
    //        'success': function (data) {
    //            debugger
    //        }
    //        }

    //})
}

function getAllItem() {
    
    var item;
    $.ajax({
        url: "https://localhost:7173/api/Categories/GetAll",
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