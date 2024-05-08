

$(document).ready(function () {


    $("div.container-fluid").LoadingOverlay("show");

    fetch("/DashBoard/ObtenerResumen")
        .then(response => {
            $("div.container-fluid").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {


                //mostrar datos para las tarjetas
                let d = responseJson.objeto

                $("#totalRequest").text(d.totalRequest)
                $("#totalLocations").text(d.totalLocations)
                $("#totalManagers").text(d.totalManagers)
                $("#totalUserAssets").text(d.totalUserAssets)
                $("#totalAssets").text(d.totalAssets)
                $("#totalBusiness").text(d.totalBusiness)
                

                //obtener textos y valores para nuestro grafico de barras
                let barchart_labels;
                let barchart_data;

                if (d.requestUltimaSemana.length > 0) {
                    barchart_labels = d.requestUltimaSemana.map((item) => { return item.date })
                    barchart_data = d.requestUltimaSemana.map((item) => { return item.total })
                } else {
                    barchart_labels = ["sin resultados"]
                    barchart_data = [0]
                }


                //obtener textos y valores para nuestro grafico de pie
                let assetStatusLabels;
                let assetStatusValues;

                if (d.assetVsStatus.length > 0) {
                    assetStatusLabels = d.assetVsStatus.map((item) => { return item.status });
                    assetStatusValues = d.assetVsStatus.map((item) => { return item.cantidad });
                } else {
                    assetStatusLabels = ["sin resultados"];
                    assetStatusValues = [0];
                }
                const assetStatusColors = {

                    "Operating": "#7aa3d9",
                    "Available": "#8ce08c",
                    "In Maintenance": "#f4e883",
                    "Out of service": "#e97366",
                    "Reserved": "#fc935b",
                    "In transit": "#c491c1",
                    "Awaiting assignment": "#c7cccc",
                    "Retired from the fleet": "#848484"
                };

                const backgroundColors = assetStatusLabels.map((label) => assetStatusColors[label]);

   
                // Bar Chart Example
                let controlRequest = document.getElementById("chartRequest");
                let myBarChart = new Chart(controlRequest, {
                    type: 'bar',
                    data: {
                        labels: barchart_labels,
                        datasets: [{
                            label: "Amount",
                            backgroundColor: "#2b2b2b",
                            hoverBackgroundColor: "#dbc134",
                            borderColor: "#4e73df",
                            data: barchart_data,
                        }],
                    },
                    options: {
                        maintainAspectRatio: false,
                        legend: {
                            display: false
                        },
                        scales: {
                            xAxes: [{
                                gridLines: {
                                    display: false,
                                    drawBorder: false
                                },
                                maxBarThickness: 50,
                            }],
                            yAxes: [{
                                ticks: {
                                    min: 0,
                                    maxTicksLimit: 5
                                }
                            }],
                        },
                    }
                });

                // Pie Chart Example

                let controlAssetStatus = document.getElementById("chartAssetStatus");
        

                let assetStatusChart = new Chart(controlAssetStatus, {
                    type: 'doughnut',
                    data: {
                        labels: assetStatusLabels, // Personaliza las etiquetas según tus estados de activos
                        datasets: [{
                            data: assetStatusValues, // Reemplaza con los datos reales de tu backend
                            backgroundColor: backgroundColors,
                            hoverBackgroundColor: backgroundColors,
                            hoverBorderColor: "rgba(234, 236, 244, 1)",
                        }],
                    },
                    options: {
                        maintainAspectRatio: false,
                        tooltips: {
                            backgroundColor: "rgb(255,255,255)",
                            bodyFontColor: "#858796",
                            borderColor: '#dddfeb',
                            borderWidth: 1,
                            xPadding: 15,
                            yPadding: 15,
                            displayColors: false,
                            caretPadding: 10,
                        },
                        legend: {
                            display: true
                        },
                        cutoutPercentage: 80,
                    },
                });

                //let myPieChart = new Chart(controlProducto, {
                //    type: 'doughnut',
                //    data: {
                //        labels: piechar_labels,
                //        datasets: [{
                //            data: piechart_data ,
                //            backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', "#FF785B"],
                //            hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', "#FF5733"],
                //            hoverBorderColor: "rgba(234, 236, 244, 1)",
                //        }],
                //    },
                //    options: {
                //        maintainAspectRatio: false,
                //        tooltips: {
                //            backgroundColor: "rgb(255,255,255)",
                //            bodyFontColor: "#858796",
                //            borderColor: '#dddfeb',
                //            borderWidth: 1,
                //            xPadding: 15,
                //            yPadding: 15,
                //            displayColors: false,
                //            caretPadding: 10,
                //        },
                //        legend: {
                //            display: true
                //        },
                //        cutoutPercentage: 80,
                //    },
                //});






            }



        })


})
