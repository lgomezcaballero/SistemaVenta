const MODELO_BASE = {
    idFiles: 0,
    idAsset: 0,
    idFileTypes: 0,
    filex: "",
    extension:"",
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
                    $("#cboBuscarAsset").append(
                        $("<option>").val(item.idAsset).text(item.inter)
                    )
                })
            }
        })

    fetch("/FileTypes/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboFileTypes").append(
                        $("<option>").val(item.idFileTypes).text(item.description)
                    )
                })
            }
        })
    



tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Files/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idFiles", "visible": false, "searchable": false },
            { "data": "nombreAsset" },
            { "data": "nombreFileTypes" },
            {
                "data": "filex",
                "render": function (data, type, row) {
                    // Si el tipo de renderizado es para la tabla, mostrar un botón para descargar
                    if (type === 'display') {
                        return '<a href="data:image/jpeg;base64,' + data + '" download="file.jpg"><button class="btn btn-primary btn-view-image btn-sm">View Image</button></a>';
                    }
                    // Si el tipo de renderizado es otro, devolver los datos tal como están
                    return data;
                }
            },
            { "data": "extension" },
            
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
                filename: 'Report Files',
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
    $("#txtId").val(modelo.idFiles)
    $("#cboBuscarAsset").val(modelo.idAsset == 0 ? $("#cboBuscarAsset option:first").val() : modelo.idAsset)
    $("#cboFileTypes").val(modelo.idFileTypes == 0 ? $("#cboFileTypes option:first").val() : modelo.idFileTypes)
    $("#txtFilex").val(modelo.filex)
    $("#txtExtension").val(modelo.extension)

    $("#modalData").modal("show")
}

$("#btnNuevo").click(function () {
    mostrarModal()
})


$("#btnGuardar").click(function () {


    if ($("#cboBuscarAsset").val().trim() == "") {
        toastr.warning("", "You have to complete the Field : Asset")
        $("#cboBuscarAsset").focus()
        return;
    }


    const modelo = structuredClone(MODELO_BASE);
    modelo["idFiles"] = parseInt($("#txtId").val())
    modelo["idAsset"] = $("#cboBuscarAsset").val()
    modelo["idFileTypes"] = $("#cboFileTypes").val()
    modelo["filex"] = $("#txtFilex").val()
    modelo["extension"] = $("#txtExtension").val()

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idFileTypes == 0) {

        fetch("/Files/Crear", {
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
                    swal("Ready!", "The File was created", "success")
                } else {
                    swal("Sorry", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Files/Editar", {
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
                    swal("Ready!", "The File was modified", "success")
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
        text: `Delete the File "${data.filex}"`,
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

                fetch(`/Files/Eliminar?idFilex=${data.idFilex}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw()

                            swal("Ready!", "The File was deleted", "success")
                        } else {
                            swal("Sorry", responseJson.mensaje, "error")
                        }
                    })


            }
        }
    )


})