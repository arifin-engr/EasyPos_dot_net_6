let baseUrl = 'https://localhost:7173/api';


$(document).ready(function () {

    loadInvoice();
   
});

function loadInvoice() {

}


function getCustomer()
{
    var customer;
    let customerId = $('#aioConceptName').find(":selected").val();
    $.ajax({

        URL: baseUrl+'GetById?id=',
        method: 'GET',
        success: function (data)
        {

        }


        })

}