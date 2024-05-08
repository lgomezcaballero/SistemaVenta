const MODELO_BASE = {
    idAsset: 0,
    inter: "",
    make: "",
    model: "",
    idAssetType: 0,
    year: "",
    licencePlate: "",
    vin: "",
    engine: "",
    idFuel: 0,
    purchasedPrice: 0,
    purchasedDate: "",
    idInsurance: 0,
    idInsurancePT: 0,
    insuranceExpiration: "",
    idSectionalRegistration: 0,
    idLocation: 0,
    idUserAsset: 0,
    idBusiness: 0,
    businessType: "",
    idManager: 0,
    idStatus: 0,
    board: "",
    pictureUrl: "",

}



let tablaData;

$(document).ready(function () {

    fetch("/AssetType/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboType").append(
                        $("<option>").val(item.idAssetType).text(item.description)
                    )
                })
            }
        })

    fetch("/Fuel/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboFuel").append(
                        $("<option>").val(item.idFuel).text(item.description)
                    )
                })
            }
        })

    fetch("/Insurance/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboInsurance").append(
                        $("<option>").val(item.idInsurance).text(item.description)
                    )
                })
            }
        })

    fetch("/InsurancePT/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboInsurancePT").append(
                        $("<option>").val(item.idInsurancePT).text(item.description)
                    )
                })
            }
        })

    fetch("/SectionalRegistration/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboSectionalRegistration").append(
                        $("<option>").val(item.idSectionalRegistration).text(item.description)
                    )
                })
            }
        })

    fetch("/Location/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboLocation").append(
                        $("<option>").val(item.idLocation).text(item.description)
                    )
                })
            }
        })

    fetch("/UserAsset/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboUserAsset").append(
                        $("<option>").val(item.idUserAsset).text(item.name)
                    )
                })
            }
        })

    fetch("/Business/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboBusiness").append(
                        $("<option>").val(item.idBusiness).text(item.description)
                    )
                })
            }
        })

    fetch("/Manager/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboManager").append(
                        $("<option>").val(item.idManager).text(item.name)
                    )
                })
            }
        })

    fetch("/Status/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboStatus").append(
                        $("<option>").val(item.idStatus).text(item.description)
                    )
                })
            }
        })




    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Asset/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idAsset", "visible": false, "searchable": false },
            { "data": "inter" },
            {
                "data": "pictureUrl", render: function (data) {
                    return `<img style="height:50px"  src=${data} class="rounded mx-auto d-block"/>`
                }
            },
            { "data": "make" },
            { "data": "model" },
            { "data": "nombreAssetType" },
            { "data": "year" },
            { "data": "licencePlate" },
            { "data": "vin" },
            { "data": "engine" },
            { "data": "nombreFuel" },
            { "data": "purchasedPrice" },
            {
                "data": "purchasedDate", render: function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            { "data": "nombreInsurance" },
            { "data": "nombreInsurancePT" },
            {
                "data": "insuranceExpiration", render: function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            { "data": "nombreSectionalRegistration" },
            { "data": "nombreLocation" },
            { "data": "nombreUserAsset" },
            { "data": "nombreBusiness" },
            { "data": "businessType" },
            { "data": "nombreManager" },
            { "data": "nombreStatus" },
            { "data": "board" },
            
            {
                "defaultContent":'<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-warning btn-documentos btn-sm mr-2"><i class="fas fa-file-alt"></i></button>' +
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
                filename: 'Report Assets',
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
    $("#txtId").val(modelo.idAsset)
    $("#txtInter").val(modelo.inter)
    $("#txtMake").val(modelo.make)
    $("#txtModel").val(modelo.model)
    $("#cboType").val(modelo.idAssetType == 0 ? $("#cboType option:first").val() : modelo.idAssetType)
    $("#txtYear").val(modelo.year)
    $("#txtLicencePlate").val(modelo.licencePlate)
    $("#txtVin").val(modelo.vin)
    $("#txtEngine").val(modelo.engine)
    $("#cboFuel").val(modelo.idFuel == 0 ? $("#cboFuel option:first").val() : modelo.idFuel)
    $("#txtPurchasedPrice").val(modelo.purchasedPrice)
    $("#txtPurchasedDate").val(moment(modelo.purchasedDate).format('YYYY-MM-DD'))
    $("#cboInsurance").val(modelo.idInsurance == 0 ? $("#cboInsurance option:first").val() : modelo.idInsurance)
    $("#cboInsurancePT").val(modelo.idInsurancePT == 0 ? $("#cboInsurancePT option:first").val() : modelo.idInsurancePT)
    $("#txtInsuranceExpiration").val(moment(modelo.insuranceExpiration).format('YYYY-MM-DD'))
    $("#cboSectionalRegistration").val(modelo.idSectionalRegistration == 0 ? $("#cboSectionalRegistration option:first").val() : modelo.idSectionalRegistration)
    $("#cboLocation").val(modelo.idLocation == 0 ? $("#cboLocation option:first").val() : modelo.idLocation)
    $("#cboUserAsset").val(modelo.idUserAsset == 0 ? $("#cboUserAsset option:first").val() : modelo.idUserAsset)
    $("#cboBusiness").val(modelo.idBusiness == 0 ? $("#cboBusiness option:first").val() : modelo.idBusiness)
    $("#txtBusinessType").val(modelo.businessType)
    $("#cboManager").val(modelo.idManager == 0 ? $("#cboManager option:first").val() : modelo.idManager)
    $("#cboStatus").val(modelo.idStatus == 0 ? $("#cboStatus option:first").val() : modelo.idStatus)
    $("#txtBoard").val(modelo.board)
    $("#txtPicture").val("")
    $("#imgAsset").attr("src", modelo.pictureUrl)


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
    modelo["idAsset"] = parseInt($("#txtId").val())
    modelo["inter"] = $("#txtInter").val()
    modelo["make"] = $("#txtMake").val()
    modelo["model"] = $("#txtModel").val()
    modelo["idAssetType"] = $("#cboType").val()
    modelo["year"] = $("#txtYear").val()
    modelo["licencePlate"] = $("#txtLicencePlate").val()
    modelo["vin"] = $("#txtVin").val()
    modelo["engine"] = $("#txtEngine").val()
    modelo["idFuel"] = $("#cboFuel").val()
    modelo["purchasedPrice"] = $("#txtPurchasedPrice").val()
    modelo["purchasedDate"] = $("#txtPurchasedDate").val()
    modelo["idInsurance"] = $("#cboInsurance").val()
    modelo["idInsurancePT"] = $("#cboInsurancePT").val()
    modelo["insuranceExpiration"] = $("#txtInsuranceExpiration").val()
    modelo["idSectionalRegistration"] = $("#cboSectionalRegistration").val()
    modelo["idLocation"] = $("#cboLocation").val()
    modelo["idUserAsset"] = $("#cboUserAsset").val()
    modelo["idBusiness"] = $("#cboBusiness").val()
    modelo["businessType"] = $("#txtBusinessType").val()
    modelo["emergencyContact"] = $("#txtEmergencyContact").val()
    modelo["idManager"] = $("#cboManager").val()
    modelo["idStatus"] = $("#cboStatus").val()
    modelo["board"] = $("#txtBoard").val()


    const inputFoto = document.getElementById("txtPicture")

    const formData = new FormData();

    formData.append("imagen", inputFoto.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idAsset == 0) {

        fetch("/Asset/Crear", {
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
                    swal("Ready!", "The Asset was created", "success")
                } else {
                    swal("Sorry", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Asset/Editar", {
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
                    swal("Ready!", "The Asset was modified", "success")
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


////////////////////////////////////////////// funcionamiento modal docs



function abrirModalDocumentos() {
    // Aquí seleccionamos el modal de documentos por su ID y lo mostramos
    $("#documentModal").modal("show");
}
$(document).ready(function () {
    // Resto de tu código...

    $("#tbdata tbody").on("click", ".btn-documentos", function () {
        abrirModalDocumentos();
    });
});



////////////////////////////////////////////// funcion doc

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
        text: `Deleted this Asset "${data.licencePlate}"`,
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

                fetch(`/Asset/Eliminar?idAsset=${data.idAsset}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw()

                            swal("Ready!", "The Asset was deleted", "success")
                        } else {
                            swal("Los sentimos", responseJson.mensaje, "error")
                        }
                    })


            }
        }
    )


})

