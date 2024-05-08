
const VISTA_BUSQUEDA = {

    searchDate: () => {

        $("#txtStartDate").val("")
        $("#txtEndDate").val("")
        $("#txtNumberRequest").val("")

        $(".search-date").show()
        $(".search-request").hide()

    }, searchRequest: () => {

        $("#txtStartDate").val("")
        $("#txtEndDate").val("")
        $("#txtNumberRequest").val("")

        $(".search-date").hide()
        $(".search-request").show()
    }
}

$(document).ready(function () {
    VISTA_BUSQUEDA["searchDate"]()

    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtStartDate").datepicker({ dateFormat: "dd/mm/yy" })
    $("#txtEndDate").datepicker({ dateFormat: "dd/mm/yy" })

})

$("#cboSearchBy").change(function () {

    if ($("#cboSearchBy").val() == "date") {
        VISTA_BUSQUEDA["searchDate"]()
    } else {
        VISTA_BUSQUEDA["searchRequest"]()
    }

})


$("#btnSearch").click(function () {

    if ($("#cboSearchBy").val() == "date") {

        if ($("#txtStartDate").val().trim() == "" || $("#txtEndDate").val().trim() == "") {
            toastr.warning("", "You must enter start and end date")
            return;
        }
    } else {

        if ($("#txtNumberRequest").val().trim() == "") {
            toastr.warning("", "You must enter the Request number")
            return;
        }
    }

    let numberRequest = $("#txtNumberRequest").val()
    let fechaInicio = $("#txtStartDate").val()
    let fechaFin = $("#txtEndDate").val()


    $(".card-body").find("div.row").LoadingOverlay("show");

    fetch(`/Request/Historial?numberRequest=${numberRequest}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
        .then(response => {
            $(".card-body").find("div.row").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            $("#tbrequest tbody").html("");

            if (responseJson.length > 0) {

                responseJson.forEach((request) => {

                    $("#tbrequest tbody").append(
                        $("<tr>").append(
                            $("<td>").text(request.registerDate),
                            $("<td>").text(request.numberRequest),
                            $("<td>").text(request.assetRequestType),
                            $("<td>").text(request.description),
                            $("<td>").text(request.priority),
                            $("<td>").text(request.usuario),
                            $("<td>").text(request.odometer),
                            
                            $("<td>").append(
                                $("<button>").addClass("btn btn-info btn-sm").append(
                                    $("<i>").addClass("fas fa-eye")
                                ).data("request", request)
                            )
                        )
                    )

                })

            }

        })

})

$("#tbrequest tbody").on("click", ".btn-info", function () {

    let d = $(this).data("request")

    $("#txtRegisterDate").val(d.registerDate)
    $("#txtNumRequest").val(d.numberRequest)
    $("#txtUsuarioRegistro").val(d.usuario)
    $("#txtRequestType").val(d.assetRequestType)
    $("#txtDescription").val(d.description)
    $("#txtOdometer").val(d.odometer)
    $("#txtPriority").val(d.priority)
    


    $("#tbAsset tbody").html("");

    d.requestDetail.forEach((item) => {

        $("#tbAsset tbody").append(
            $("<tr>").append(
                $("<td>").text(item.assetInternal),
                $("<td>").text(item.assetType),
                $("<td>").text(item.makerAsset),
                $("<td>").text(item.modelAsset),
                $("<td>").text(item.fuelAsset),
                $("<td>").text(item.locationAsset),
                $("<td>").text(item.userAsset),
                $("<td>").text(item.managerAsset),
                $("<td>").text(item.yearAsset),
                $("<td>").text(item.licencePlateAsset),
                
                
            )
        )

    })

    $("#linkPrint").attr("href", `/Request/MostrarPDFRequest?numberRequest=${d.numberRequest}`)

    $("#modalData").modal("show");

})