const MODELO_BASE = {
    idManager: 0,
    name: "",
    workFile: "",
    description: "",
    licence: "",
    licenceExpiration: "",
    birthday: "",
    phone: "",
    emergencyContact: "",
    pictureUrl: "",
    email: "",
    active: 1,

}



let tablaData;

$(document).ready(function () {



    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Manager/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idManager", "visible": false, "searchable": false },
            {
                "data": "pictureUrl", render: function (data) {
                    return `<img style="height:50px" src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "name" },
            { "data": "workFile" },
            { "data": "description" },
            { "data": "licence" },
            {
                "data": "licenceExpiration", render: function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            {
                "data": "birthday", render: function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            { "data": "phone" },
            { "data": "emergencyContact" },
            { "data": "email" },
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
                filename: 'Report Managers',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6]
                }
            }, 'pageLength'
        ],
        language: {
            url: ""
        },
    });

})


function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idManager)
    $("#txtName").val(modelo.name)
    $("#txtWorkFile").val(modelo.workFile)
    $("#txtDescription").val(modelo.description)
    $("#txtLicence").val(modelo.licence)
    $("#txtLicenceExpiration").val(moment(modelo.licenceExpiration).format('YYYY-MM-DD'))
    $("#txtBirthday").val(moment(modelo.birthday).format('YYYY-MM-DD'))
    $("#txtPhone").val(modelo.phone)
    $("#txtEmergencyContact").val(modelo.emergencyContact)
    $("#txtEmail").val(modelo.email)
    $("#cboStatus").val(modelo.active)
    $("#txtPicture").val("")
    $("#imgUserAsset").attr("src", modelo.pictureUrl)


    $("#modalData").modal("show")
}



$("#btnNuevo").click(function () {
    mostrarModal()
})



$("#btnGuardar").click(function () {

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo : "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["idManager"] = parseInt($("#txtId").val())
    modelo["name"] = $("#txtName").val()
    modelo["workFile"] = $("#txtWorkFile").val()
    modelo["description"] = $("#txtDescription").val()
    modelo["licence"] = $("#txtLicence").val()
    modelo["licenceExpiration"] = $("#txtLicenceExpiration").val()
    modelo["birthday"] = $("#txtBirthday").val()
    modelo["phone"] = $("#txtPhone").val()
    modelo["emergencyContact"] = $("#txtEmergencyContact").val()
    modelo["email"] = $("#txtEmail").val()
    modelo["active"] = $("#cboStatus").val()

    const inputFoto = document.getElementById("txtPicture")

    const formData = new FormData();

    formData.append("imagen", inputFoto.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idManager == 0) {

        fetch("/Manager/Crear", {
            method: "POST",
            body: formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.estado) {

                    tablaData.row.add(responseJson.objeto).draw(false)
                    $("#modalData").modal("hide")
                    swal("Ready!", "The Manager was created", "success")
                } else {
                    swal("Sorry", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Manager/Editar", {
            method: "PUT",
            body: formData
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
                    swal("Ready!", "The Manager was modified", "success")
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
        title: "¿Are you Sure?",
        text: `Deleted this Manager "${data.name}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes, Delete",
        cancelButtonText: "No, Cancel",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (respuesta) {

            if (respuesta) {

                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/Manager/Eliminar?idManager=${data.idManager}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw()

                            swal("Ready!", "The Manager was deleted", "success")
                        } else {
                            swal("Los sentimos", responseJson.mensaje, "error")
                        }
                    })


            }
        }
    )


})

