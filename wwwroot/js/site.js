// Write your JavaScript code.
$(function () {
    // display message if modal still loaded
    if ($('#detailsId').val() && $('#detailsId').val().length > 0) {
        var data = $('#modalbtn' + $('#detailsId').val()).data('details');
        CopyToModal($('#detailsId').val(), data);
        $('#details_popup').modal('show');
    }
    if ($("#register_popup") !== undefined) {
        $('#register_popup').modal('show');
    }
    if ($("#login_popup") !== undefined) {
        $('#login_popup').modal('show');
    }
    // details click - to load popup on catalogue
    $("a.btn-default").on("click", (e) => {
        let Id = e.target.dataset.id;
        let data = JSON.parse(e.target.dataset.details); 
        $("#results").text("");
        CopyToModal(Id, data);
    });

     $('.nav-tabs a').on('show.bs.tab', function (e) {
        if ($(e.relatedTarget).text() === 'Demographic') { // tab 1
            $('#Firstname').valid()
            $('#Lastname').valid()
            $('#Age').valid()
            $('#CreditcardType').valid()
            if ($('#Firstname').valid() === false || $('#Lastname').valid() === false || $('#Age').valid() === false || $('#CreditcardType').valid() === false) {
                return false; // suppress click
            }
        }
        if ($(e.relatedTarget).text() === 'Address') { // tab 2
            $('#Address1').valid()
            $('#City').valid()
            $('#Region').valid()
            $('#Country').valid()
            $('#Mailcode').valid()


            if ($('#Address1').valid() === false || $('#City').valid() === false || $('#Region').valid() === false || $('#Country').valid() === false || $('#Mailcode').valid() === false ) {
                return false; // suppress click
            }
        }
        if ($(e.relatedTarget).text() === 'Account') { // tab 3 
            $('#Email').valid()
            $('#Password').valid()
            $('#RepeatPassword').valid()
            if ($('#Email').valid() === false || $('#Password').valid() === false || $('#RepeatPassword').valid() === false) {
                return false; // suppress click
            }
        }
    }); // show bootstrap tab
})

let CopyToModal = (id, data) => {
    $("#qty").val("0");
    $("#description").text(data.Description);
    $("#detailsGraphic").attr("src", data.GraphicName);
    $("#CostPrice").text(data.CostPrice);
    $("#detailsId").val(id);
    $("#msrp").text("$" + data.MSRP.toFixed(2));
    $("#productname").text(data.ProductName);
    $("#detailsId").val(id);
    $("#qtyOnHand").text(data.QtyOnHand);
    $("#qtyOnBackOrder").text(data.QtyOnBackOrder);

}

