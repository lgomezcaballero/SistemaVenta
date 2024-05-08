let tablaData;
$(document).ready(function () {

    $.datepicker.setDefaults($.datepicker.regional["es-AR"])

    $("#txtStartDate").datepicker({ dateFormat: "dd/mm/yy" })
    $("#txtEndDate").datepicker({ dateFormat: "dd/mm/yy" })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Report/RequestReport?fechaInicio=01/01/1991&fechaFin=01/01/1991',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "registerDate" },
            { "data": "numberRequest" },
            { "data": "requestType" },
            { "data": "description" },
            { "data": "odometer" },
            { "data": "inter" },
            { "data": "assetType" },
            { "data": "modelAsset" },
            { "data": "asset" },
            
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Export Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Request Report'
            }, 'pageLength'
        ],
        language: {
            url: ""
        },
    });


})

$("#btnSearch").click(function () {

    if ($("#txtStartDate").val().trim() == "" || $("#txtEndDate").val().trim() == "") {
        toastr.warning("", "Debe ingresar fecha inicio y fin")
        return;
    }

    let fechaInicio = $("#txtStartDate").val().trim();
    let fechaFin = $("#txtEndDate").val().trim();

    let nueva_url = `/Report/RequestReport?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`;

    tablaData.ajax.url(nueva_url).load();


})