const MODELO_BASE = {
    idDailyReport: 0,
    idAsset: 0,
    numberReport: 0,
    final: 0,
    closingOfDay: "",
    registerDate: "",
}


let tablaData;

$(document).ready(function () {

    fetch("/Asset/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboAsset").append(
                        $("<option>").val(item.idAsset).text(item.inter)
                    )
                })
            }
        })



    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/DailyReport/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idDailyReport", "visible": false, "searchable": false },  
            { "data": "nombreAsset" },
            { "data": "numberReport" },
            { "data": "final" },
            { "data": "closingOfDay" },
            {
                "data": "registerDate", render: function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Export Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Report Business',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7]
                }
            }, 'pageLength'
        ],
        language: {
            url: ""
        },
    });

})


function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idDailyReport)

    $("#cboAsset").val(modelo.idAsset == 0 ? $("#cboAsset option:first").val() : modelo.idAsset)
    $("#txtNumberReport").val(modelo.numberReport)
    $("#txtFinal").val(modelo.final)
    $("#txtClosingOfDay").val(modelo.closingOfDay)
    $("#txtRegisterDate").val(modelo.registerDate)

    $("#modalData").modal("show")
}

$("#btnNuevo").click(function () {
    mostrarModal()
})


$("#btnGuardar").click(function () {


    if ($("#txtClosingOfDay").val().trim() == "") {
        toastr.warning("", "You have to complete the Field : Description")
        $("#txtClosingOfDay").focus()
        return;
    }


    const modelo = structuredClone(MODELO_BASE);
    modelo["idDailyReport"] = parseInt($("#txtId").val())  
    
    modelo["idAsset"] = $("#cboAsset").val()
    modelo["numberReport"] = $("#txtNumberReport").val()
    modelo["final"] = $("#txtFinal").val()
    modelo["closingOfDay"] = $("#txtClosingOfDay").val()
    modelo["txtRegisterDate"] = $("#txtDate").val()

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idDailyReport == 0) {

        fetch("/DailyReport/Crear", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.estado) {

                    tablaData.row.add(responseJson.objeto).draw(false)
                    $("#modalData").modal("hide")
                    swal("Ready!", "The Daily Report was created", "success")
                } else {
                    swal("Sorry", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/DailyReport/Editar", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.estado) {

                    tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                    filaSeleccionada = null;
                    $("#modalData").modal("hide")
                    swal("Ready!", "The Daily Report was modified", "success")
                } else {
                    swal("Sorry", responseJson.mensaje, "error")
                }
            })

    }


})


let filaSeleccionada;
$("#tbdata tbody").on("click", ".btn-editar", function () {

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data();

    mostrarModal(data);

})



$("#tbdata tbody").on("click", ".btn-eliminar", function () {

    let fila;
    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "¿Are you sure?",
        text: `Delete the Daily Report "${data.description}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes, delete",
        cancelButtonText: "No, cancel",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (respuesta) {

            if (respuesta) {

                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/DailyReport/Eliminar?idDailyReport=${data.idDailyReport}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw()

                            swal("Ready!", "The Daily Report was deleted", "success")
                        } else {
                            swal("Sorry", responseJson.mensaje, "error")
                        }
                    })


            }
        }
    )


})