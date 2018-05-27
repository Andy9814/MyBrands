// Write your JavaScript code.
$(function () {

    if ($('#detailsId').val() == '') {
        var data = $('#modaltbtn' + $('#detailsId').val()).data('details');
        CopyToModal($('#detialsId').val(), data);
        $('#details_popup').modal('show');

    }

    $(function () {
        // details click to pop on catalogue
        $("a.btn-default").on("click", (e) => {
            let Id = e.target.dataset.Id;
            let data = JSON.parse(e.target.dataset.details);
            $('#results').text("");
            CopyToModal(Id, data);

        });


    }); 

    let CopyToModal = (id, data) => {
        $("#qty").val("0");
        $("#description").text(data.Description);
        $("#detailsGraphic").attr("src", data.GraphicName);
        $("#CostPrice").text(data.CostPrice);
        $("#detailsId").val(id);
    }



})