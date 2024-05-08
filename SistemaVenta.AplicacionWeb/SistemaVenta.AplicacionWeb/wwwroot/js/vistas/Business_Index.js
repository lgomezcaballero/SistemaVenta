const MODELO_BASE = {
    idBusiness: 0,
    description: "",
    ein: "",
    type: "",
    country: "",
    state: "",
    address: "",
    active: 1,
}


let tablaData;

$(document).ready(function () {


    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Business/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idBusiness", "visible": false, "searchable": false },
            { "data": "description" },
            { "data": "ein" },
            { "data": "type" },
            { "data": "country" },
            { "data": "state" },
            { "data": "address" },
            {
                "data": "active", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">Active</span>';
                    else
                        return '<span class="badge badge-danger">Inactive</span>';
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
    $("#txtId").val(modelo.idBusiness)
    $("#txtDescription").val(modelo.description)
    $("#txtEin").val(modelo.ein)
    $("#txtType").val(modelo.type)
    $("#txtCountry").val(modelo.country)
    $("#txtState").val(modelo.state)
    $("#txtAddress").val(modelo.address)
    $("#cboStatus").val(modelo.active)

    $("#modalData").modal("show")
}

$("#btnNuevo").click(function () {
    mostrarModal()
})


$("#btnGuardar").click(function () {


    if ($("#txtDescription").val().trim() == "") {
        toastr.warning("", "You have to complete the Field : Description")
        $("#txtDescription").focus()
        return;
    }


    const modelo = structuredClone(MODELO_BASE);
    modelo["idBusiness"] = parseInt($("#txtId").val())
    modelo["description"] = $("#txtDescription").val()
    modelo["ein"] = $("#txtEin").val()
    modelo["type"] = $("#txtType").val()
    modelo["country"] = $("#txtCountry").val()
    modelo["state"] = $("#txtState").val()
    modelo["address"] = $("#txtAddress").val()
    modelo["active"] = $("#cboStatus").val()

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idBusiness == 0) {

        fetch("/Business/Crear", {
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
                    swal("Ready!", "The Business was created", "success")
                } else {
                    swal("Sorry", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Business/Editar", {
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
                    swal("Ready!", "The Business was modified", "success")
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
        text: `Delete the Business "${data.description}"`,
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

                fetch(`/Business/Eliminar?idBusiness=${data.idBusiness}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw()

                            swal("Ready!", "The Business was deleted", "success")
                        } else {
                            swal("Sorry", responseJson.mensaje, "error")
                        }
                    })


            }
        }
    )


})