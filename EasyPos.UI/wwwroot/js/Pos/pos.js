
//Add POS
let baseUrl = 'https://localhost:7173/api';
var counter = 0;
var index = 0;
var productPrice = 0;
var product = null;
var productVm = null;



$("#ProductList").on("change", function () {

    debugger

    var productId = $("#ProductList option:selected").val();
    var productName = $("#ProductList option:selected").text();
    counter++;
    $.ajax({

        url: baseUrl + "/Products/GetById?id=" + productId,
        method: "GET",
        type: "json",
        async: false,

        success: function (data) {
            debugger;
            productVm = data;
            product = data;
            productPrice = product.salePrice;

        }
    });
    debugger;
    var productUnitRelatedBy = product.unit.relatedBy;

    debugger
    
    //var singleUnit = '<td class="pr-3"> <div class="form-row"><label class="ml-2 mr-2">###unitname###:</label>' +
    //    '<input type = "text" value = "0" class="form-control col main_qty" name ="" data-relatedby="' + product.unit.relatedBy + '" onkeyup = "CalculateSubTotal()"></div> </td>';

    //var doubleUnit = '<td class="pr-3"> <div class="form-row"><label class="ml-2 mr-2">###unitname###:</label>' +
    //    '<input type = "text" value = "0" class="form-control col main_qty" name ="" data-relatedby="' + product.unit.relatedBy + '" onkeyup = "CalculateSubTotal()">' +
    //    '<label class="ml-2 mr-2">###subunitname###:</label><input type = "text" value = "0" class="form-control col sub_qty" name = "" onkeyup = "CalculateSubTotal()"> </div> </td>';
    //if (productVm.subUnit != null) {
    //    doubleUnit = doubleUnit.replace("###unitname###", product.unit.name).replace("###subunitname###", productVm.subUnit.name);
    //}
    //else {
    //    singleUnit = singleUnit.replace("###unitname###", product.unit.name);
    //}

    var productRow =
        '<tr> <td> <span class="productName">' + productName + '</span> <input type = "hidden" value = "' + productId + '" class="ProductId" name = "Sale.SalesItems[' + index + '].ProductId" class="" ></td>' +
        '<td class="pr-3"> <div class="form-row"><label class="ml-2 mr-2">KG:</label>' +
        '<input type = "text" value = "0" class="form-control col main_qty" name ="" data-relatedby="' + product.unit.relatedBy + '" onkeyup = "CalculateSubTotal()">' +
        '<label class="ml-2 mr-2">gm:</label><input type = "text" value = "0" class="form-control col sub_qty" name = "" onkeyup = "CalculateSubTotal()"> </div> </td>'+
       /* '<td ><div class="d-flex"> <span>KG: </span> <input type = "number" style="width: 80px;height: 26px;margin: 0 10px;"  class="form-control" > <span>gm: </span> <input type = "number" style="width: 80px;height: 26px;margin: 0 10px;" class="form-control" > </div></td > ' +*/
        '###quantityinput### <input type="hidden" class="quantity" name="Sale.SalesItems[' + index + '].Quantity" /> <input type="hidden" value="' + product.purchaseCost + '" name="Sale.SalesItems[' + index + '].PurchaseCost" />' +
        '<td ><input type="text" style="width: 55px;height: 26px;" value="' + productPrice + '" class="form-control rate" name="Sale.SalesItems[' + index + '].Price" onkeyup="CalculateSubTotal()"></td>' +

        '<td > <strong><span class="sub_total">0</span> Tk</strong> <input  type="hidden" name="Sales.SalesItems[' + index + '].SubTotal" class="subtotal_input" value="0"></td>' +
        '<td> <a onclick="RemoveProduct(this)" class="removeProduct">  <i class="fa fa-trash"></i> </a> </td>  </tr> ';

    //if (productVm.subUnit != null) {
    //    productRow = productRow.replace("###quantityinput###", doubleUnit);
    //}
    //else {
    //    productRow = productRow.replace("###quantityinput###", singleUnit);
    //}


    $("#SellProduct tbody").append(productRow);
    index++;
    $("#ProductList option:selected").remove();

});

function CalculateSubTotal() {

    var rows = $("#SellProduct tbody tr");
    rows.each(function () {

        var main_qty = 0;
        var sub_qty = 0;
        var tot_qty = 0;
        var relatedBy = 0;
        var price = parseFloat($(this).find(".rate").val()).toFixed(2);
        main_qty = parseInt($(this).find(".main_qty").val());
        relatedBy = parseInt($(this).find(".main_qty").attr("data-relatedby"));
        if ($(this).find(".sub_qty").length > 0) {
            sub_qty = parseInt($(this).find(".sub_qty").val());

            $(this).find(".subtotal_input").val(parseFloat((price * main_qty) + ((price / relatedBy) * sub_qty)).toFixed(2));
            tot_qty = ((main_qty * relatedBy) + sub_qty) / relatedBy;
        }
        else {

            $(this).find(".subtotal_input").val(price * main_qty);
            tot_qty = main_qty;
        }

        $(this).find(".quantity").val(tot_qty);

        $(this).find(".sub_total").html($(this).find(".subtotal_input").val());

    });

    CalculateGrandTotal();

}

function CalculateGrandTotal() {

    var rows = $("#SellProduct tbody tr");
    var grandTotal = 0;
    rows.each(function () {


        grandTotal += parseFloat($(this).find(".subtotal_input").val());

    });

    $("#PurchaseGrandTotal").val(grandTotal);
    $("#grandTotalTxt").html($("#PurchaseGrandTotal").val());

}
function CalculateTotalQty() {
    debugger
    var rows = $("#SellProduct tbody tr");
    var TotalQty = 0;
    rows.each(function () {
        debugger;

        TotalQty += parseFloat($(this).find(".quantity").val());

    });

    $("#sellTotalQty").val(TotalQty);
    $("#TotalQtyTxt").html($("#sellTotalQty").val());

}

function RemoveProduct(elem) {
    debugger;
    var row = $(elem).parent().parent();
    var productName = row.find(".productName").text();
    var productId = row.find(".ProductId").val();
    var data = {
        id: productId,
        text: productName
    };

    var newOption = new Option(data.text, data.id, false, false);
    row.remove();
    $("#ProductList").append(newOption);
}
$("#payment-modal").on("shown.bs.modal", function () {
    debugger;
    var dis = 0;

    var grandTotal = $("#PurchaseGrandTotal").val();
    $("#receivable").html(grandTotal);
    if ($("#discount").val() > 0) {
        $("#after_discount").val() = 10;
    }
    else {
        $("#after_discount").html(grandTotal);
    }
    $("#balance").html(0 - grandTotal);
    $("#totalPayable_input").val(grandTotal);
    $("#due_input").val(grandTotal);
})
function SetPaidAmount() {
    debugger

    var grandTotal = $("#PurchaseGrandTotal").val();
    $("#pay_amount").val(grandTotal);
    $("#balance").html("0");
    $("#balance_input").val(0);
}

$("#pay_amount").keyup(function () {
    debugger;
    var grandTotal = $("#PurchaseGrandTotal").val();
    var payAmount = $("#pay_amount").val();
    var due = parseFloat(0 - grandTotal);
    $("#balance").html(due);
    $("#balance_input").val(due);

})

$(document).ready(function () {


    getProducts();
    //Pagination JS
    //how much items per page to show
    var show_per_page = 9;
    //getting the amount of elements inside pagingBox div
    var number_of_items = $('#pagingBox').children().length;
    //calculate the number of pages we are going to have
    var number_of_pages = Math.ceil(number_of_items / show_per_page);

    //set the value of our hidden input fields
    $('#current_page').val(0);
    $('#show_per_page').val(show_per_page);

    var navigation_html = '<li class="previous_link page-item " onclick="previous()" aria-label="« Previous"><span class="page-link" aria-hidden="true" >‹</span></li >';
    var current_link = 0;
    while (number_of_pages > current_link) {

        navigation_html += '<li class="page-item page_link" onclick="go_to_page(' + current_link + ')" aria-current="page" longdesc="' + current_link + '">' +
            '<span class="page-link" > ' + (current_link + 1) + '</span ></li > ';
        current_link++;
    }

    navigation_html += '<li class="page-item" ><a class="page-link" href="javascript:next()" rel="next" aria-label="Next »" >›</a ></li >';

    $('#page_navigation').html(navigation_html);

    //add active_page class to the first page link
    $('#page_navigation .page_link:first').addClass('active_page');

    //hide all the elements inside pagingBox div
    $('#pagingBox').children().css('display', 'none');

    //and show the first n (show_per_page) elements
    $('#pagingBox').children().slice(0, show_per_page).css('display', 'block');




});



//Pagination JS

function previous() {

    new_page = parseInt($('#current_page').val()) - 1;
    //if there is an item before the current active link run the function
    if ($('.active_page').prev('.page_link').length == true) {
        go_to_page(new_page);
    }

}

function next() {
    new_page = parseInt($('#current_page').val()) + 1;
    //if there is an item after the current active link run the function
    if ($('.active_page').next('.page_link').length == true) {
        go_to_page(new_page);
    }

}
function go_to_page(page_num) {
    debugger;
    //get the number of items shown per page
    var show_per_page = parseInt($('#show_per_page').val());

    //get the element number where to start the slice from
    start_from = page_num * show_per_page;

    //get the element number where to end the slice
    end_on = start_from + show_per_page;

    //hide all children elements of pagingBox div, get specific items and show them
    $('#pagingBox').children().css('display', 'none').slice(start_from, end_on).css('display', 'block');

    /*get the page link that has longdesc attribute of the current page and add active_page class to it
    and remove that class from previously active page link*/
    $('.page_link[longdesc=' + page_num + ']').addClass('active_page').siblings('.active_page').removeClass('active_page');

    //update the current page input field
    $('#current_page').val(page_num);
}

function getProducts(searchFilter, categoryId) {
    debugger;
    $.ajax({

        url: "/Pos/GetProducts?searchFilter=" + searchFilter + "&categoryId=" + categoryId,
        method: "GET",
        type: "json",
        async: false,

        success: function (data) {
            debugger
            products = data.data;

            $("#pagingBox").html("");

            for (var i = 0; i < products.length; i++) {
                let pid = products[i].code;

                var productDiv = '<div class="col-sm-3 border col-md-3 custom-display" style="display:inline-block;"  ><div class="product text-center m-2 product selimage" data-pcode="' + pid + '" style="cursor:pointer"><input type="hidden" value="' + products[i].code + '" id="pcode"><img src="###imgurl###" class="align-self-start img-thumbnail" alt="###productnamealt###" style="width:80px;height:40px" data-pagespeed-url-hash="31082695"  ><br><span style="font-size: 12px;">###productname### - ###productcode###</span> <br> <small style="font-size: 12px;color: #125813;" class="font-weight-bold">###productprice### Tk</small> <br><small class="stock">Stock: </small> </div></div>';

                productDiv = productDiv.replace("###productsearch###", products[i].code).replace("###imgurl###", products[i].imageUrl).replace("###productnamealt###", products[i].name).replace("###productname###", products[i].name).replace("###productcode###", products[i].code).replace("###productprice###", products[i].retailPrice)
                $("#pagingBox").append(productDiv);


            }

        }


    });


}




$("#pagingBox").on('click', '.selimage', function () {
    debugger;

    var productId = $(this).data("pcode");
    $.ajax({

        url: "/Products/GetByCode/" + productId,
        method: "GET",
        type: "json",
        async: false,

        success: function (data) {

            productVm = data.data;
            product = data.data.product;
            productPrice = product.salePrice;

        }
    });

    debugger;
    var singleUnit = '<td class="pr-3"> <div class="form-row"><label class="ml-2 mr-2">###unitname###:</label>' +
        '<input type = "text" value = "0" class="form-control col main_qty" name ="" data-relatedby="' + product.unit.relatedBy + '" onkeyup = "CalculateSubTotal()"></div> </td>';

    var doubleUnit = '<td class="pr-3"> <div class="form-row"><label class="ml-2 mr-2">###unitname###:</label>' +
        '<input type = "text" value = "0" class="form-control col main_qty" name ="" data-relatedby="' + product.unit.relatedBy + '" onkeyup = "CalculateSubTotal()">' +
        '<label class="ml-2 mr-2">###subunitname###:</label><input type = "text" value = "0" class="form-control col sub_qty" name = "" onkeyup = "CalculateSubTotal()"> </div> </td>';
    if (productVm.subUnit != null) {
        doubleUnit = doubleUnit.replace("###unitname###", product.unit.name).replace("###subunitname###", productVm.subUnit.name);
    }
    else {
        singleUnit = singleUnit.replace("###unitname###", product.unit.name);
    }


    var productRow =
        '<tr> <td> <span class="productName">' + product.name + '</span> <input type = "hidden" value = "' + productId + '" class="ProductId" name = "Sales.SalesItems[' + index + '].ProductId" class="" ></td>' +
        '###quantityinput### <input type="hidden" class="quantity" name="Sales.SalesItems[' + index + '].Quantity" /> <input type="hidden" value="' + product.purchaseCost + '" name="Sales.SalesItems[' + index + '].PurchaseCost" />' +
        '<td ><input type="text" value="' + productPrice + '" class="form-control rate" name="Sales.SalesItems[' + index + '].Price" onkeyup="CalculateSubTotal()"></td>' +

        '<td width="120"> <strong><span class="sub_total">0</span> Tk</strong> <input type="hidden" name="Sales.SalesItems[' + index + '].SubTotal" class="subtotal_input" value="0"></td>' +
        '<td> <a onclick="RemoveProduct(this)" class="removeProduct">  <i class="fa fa-trash"></i> </a> </td>  </tr> ';

    if (productVm.subUnit != null) {
        productRow = productRow.replace("###quantityinput###", doubleUnit);
    }
    else {
        productRow = productRow.replace("###quantityinput###", singleUnit);
    }


    $("#SellProduct tbody").append(productRow);
    index++;
    $("#ProductList option:selected").remove();

});
function searchProduct() {
    debugger;
    var filter = $("#searchInput").val();
    getProducts(filter, null);

}

function getAllProduct() {

    var product = null;
    $.ajax({

        url: baseUrl + "/Products/GetAll",
        method: "GET",
        type: "json",
        async: false,

        success: function (data) {

            product = data;

        }

    });

    return product;

}
