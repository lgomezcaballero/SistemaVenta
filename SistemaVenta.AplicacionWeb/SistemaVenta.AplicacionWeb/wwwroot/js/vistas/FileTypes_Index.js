const MODELO_BASE = {
    idFileTypes: 0,
    description: "",
    active: 1,
}


let tablaData;

$(document).ready(function () {


    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/FileTypes/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idFileTypes", "visible": false, "searchable": false },
            { "data": "description" },
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
                filename: 'Report File Type',
                exportOptions: {
                    columns: [1, 2]
                }
            }, 'pageLength'
        ],
        language: {
            url: ""
        },
    });

})


function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idFileTypes)
    $("#txtDescription").val(modelo.description)
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
    modelo["idFileTypes"] = parseInt($("#txtId").val())
    modelo["description"] = $("#txtDescription").val()
    modelo["active"] = $("#cboStatus").val()

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idFileTypes == 0) {

        fetch("/FileTypes/Crear", {
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
                    swal("Ready!", "The File Type was created", "success")
                } else {
                    swal("Sorry", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/FileTypes/Editar", {
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
                    swal("Ready!", "The File Type was modified", "success")
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
        text: `Delete the FileType "${data.description}"`,
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

                fetch(`/FileTypes/Eliminar?idFileTypes=${data.idFileTypes}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw()

                            swal("Ready!", "The File Type was deleted", "success")
                        } else {
                            swal("Sorry", responseJson.mensaje, "error")
                        }
                    })


            }
        }
    )


})