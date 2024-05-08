$(document).ready(function () {


    fetch("/Request/ListaAssetRequestType")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboAssetRequestType").append(
                        $("<option>").val(item.idAssetRequestType).text(item.description)
                    )
                })
            }
            //Agregar el texto predeterminado al dropdown cboAssetRequestType
            //$("#cboAssetRequestType").append('<option value="" disabled selected>Select Request Type</option>');

        })

      

    fetch("/Request/ListaPriority")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboPriority").append(
                        $("<option>").val(item.idPriority).text(item.description)
                    )
                })
            }
            
        })

    

    $("#cboBuscarAsset").select2({
        ajax: {
            url: "/Request/ObtenerAsset",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return {
                    busqueda: params.term
                };
            },
            processResults: function (data,) {

                return {
                    results: data.map((item) => (
                        {
                            id: item.idAsset,
                            text: item.model,

                            make: item.make,
                            assetType: item.nombreAssetType,
                            pictureUrl: item.pictureUrl,
                            location: item.nombreLocation,
                            inter: item.inter,
                            year:item.year,
                            licencePlate:item.licencePlate,
                            fuel: item.nombreFuel,
                            licencePlate: item.licencePlate,                                                     
                            userAsset: item.nombreUserAsset,
                            manager: item.nombreManager

                            
                            // Damos los valores de request y detail request, nombre:item.el valor de obtenerasset)
                            // 
                            
                        }
                    ))
                };
            }
        },
        language: "",
        placeholder: 'Search Asset...',
        minimumInputLength: 1,
        templateResult: formatoResultados
    });


})

function formatoResultados(data) {

    //esto es por defecto, ya que muestra el "buscando..."
    if (data.loading)
        return data.text;


    var contenedor = $(
        `<table width="100%">
            <tr>
                <td style="width:60px">
                    <img style="height:70px;width:70px;margin-right:10px" src="${data.pictureUrl}"/>
                </td>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.inter}</p>
                     <p style="font-weight: bolder;margin:2px">${data.assetType}</p>               
                </td>
                <td>                 
                    <p style="font-weight: bolder;margin:2px">${data.make}</p>
                     <p style="font-weight: bolder;margin:2px">${data.text}</p>               
                </td>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.fuel}</p>
                     <p style="font-weight: bolder;margin:2px">${data.location}</p>               
                </td>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.licencePlate}</p>
                    <p style="font-weight: bolder;margin:2px">${data.year}</p>
                                 
                </td>
                
            </tr>
         </table>`
    );

    return contenedor;
}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let AssetParaRequest = [];
$("#cboBuscarAsset").on("select2:select", function (e) {
    const data = e.params.data;

    let asset_encontrado = AssetParaRequest.filter(p => p.idAsset == data.id)
    if (asset_encontrado.length > 0) {
        $("#cboBuscarAsset").val("").trigger("change")
        toastr.warning("", "El ASSET ya fue agregado")
        return false
    }

    swal({
        title: data.assetType + ' ' +data.inter,
        text: data.make + ' ' + data.text,

        imageUrl: data.pictureUrl,
      
        showCancelButton: true,
        closeOnConfirm: false
    },
        function (valor) {

           

            let asset = {
                idAsset: data.id,
                makerAsset: data.make,
                modelAsset: data.text,
                assetType: data.assetType,
                assetInternal: data.inter,
                fuelAsset: data.fuel,
                locationAsset: data.location, 
                userAsset: data.userAsset,
                managerAsset: data.manager,
                yearAsset: data.year,
                licencePlateAsset: data.licencePlate,

                //el nombre del la lista VMRequestDetail: data.el nombre que se le dió en obtenerAsset. 
            }

            AssetParaRequest.push(asset)

            mostrarAsset_ForRequest();
            $("#cboBuscarAsset").val("").trigger("change")
            swal.close()
        }
    )

})

function mostrarAsset_ForRequest() {

    $("#tbAsset tbody").html("")

    AssetParaRequest.forEach((item) => {

        $("#tbAsset tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<i>").addClass("fas fa-trash-alt")
                    ).data("idAsset", item.idAsset)
                ),
                $("<td>").text(item.makerAsset),
                $("<td>").text(item.modelAsset),
                $("<td>").text(item.locationAsset),
                $("<td>").text(item.userAsset),
                $("<td>").text(item.managerAsset)
              
            )
        )
    })

    
    


}

$(document).on("click", "button.btn-eliminar", function () {

    const _idasset = $(this).data("idAsset")

    AssetParaRequest = AssetParaRequest.filter(p => p.idAsset != _idasset);

    mostrarAsset_ForRequest();
})


$("#btnTerminarRequest").click(function () {

    if (AssetParaRequest.length < 1) {
        toastr.warning("", "Debe ingresar un Asset")
        return;
    }

    const description = $("#txtDescription").val().trim();
    const odometer = $("#txtOdometer").val().trim();

    

    if (!description) {
        toastr.warning("", "Debe ingresar una Descripción");
        return;
    }

    if (!odometer) {
        toastr.warning("", "Debe ingresar el Odómetro");
        return;
    }


    const vmRequestDetail = AssetParaRequest;

    const request = {
        idAssetRequestType: $("#cboAssetRequestType").val(),
        description: $("#txtDescription").val(),
        odometer: $("#txtOdometer").val(),
        idPriority: $("#cboPriority").val(),
        RequestDetail: vmRequestDetail
    }

    $("#btnTerminarRequest").LoadingOverlay("show");

    fetch("/Request/RegistrarRequest", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(request)
    })
        .then(response => {
            $("#btnTerminarRequest").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {
                AssetParaRequest = [];
                mostrarAsset_ForRequest();

                $("#txtDescription").val("")
                $("#txtOdometer").val("")
                $("#cboAssetRequestType").val($("#cboAssetRequestType option:first").val())
                $("#cboPriority").val($("#cboPriority option:first").val())

                swal("Registrado!", `Numero Request : ${responseJson.objeto.numberRequest}`, "success")
            } else {
                swal("Lo sentimos!", "No se pudo registrar el Request", "error")
            }
        })

})