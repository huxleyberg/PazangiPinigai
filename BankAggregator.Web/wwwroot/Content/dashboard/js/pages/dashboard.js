//[Dashboard Javascript]

//Project:	Indusui admin - Responsive Admin Template
//Primary use:   Used only for the main dashboard (index.html)

$(function () {

  'use strict';
	
		// donut chart
		$('.donut').peity('donut');
	
		// revenue-analysis           
		 var revenueanalysis = jQuery('#revenueanalysis')
			if (revenueanalysis.length > 0) {
			   var options = {
					chart: {
						height: 359,
						type: 'area',
						toolbar: {
						show: false
						},
					},
					dataLabels: {
						enabled: false
					},
					legend: {
						show: false,
					},
					colors:['#2a93d5', '#FC3468'],
					fill: {
					type: 'gradient',
						gradient: {
						  type: "vertical",
						  shadeIntensity: 0,
						  opacityFrom: 0.3,
						  opacityTo: 0.2,
						  gradientToColors: ['#fcc525'],
						  stops: [0, 90, 100]
						}
				  },
					stroke: {
						curve: 'smooth',
						width: 2
					},
					series: [{
						name: 'Total Income',
						data: [20, 30, 42, 53, 32, 50, 60, 43, 43, 32, 20, 40]
					},
					{
						name: 'Total Sale',
						data: [30, 40, 29, 33, 42, 30, 40, 52, 33, 22, 40, 50]
					}],
					tooltip: {
						x: {
							format: 'dd/MM/yy HH:mm'
						},
					},
					xaxis: {
						show: true,
						labels: {
							show: true,
						},
						 axisBorder: {
							 color: '#cccccc',
							 show: true,
						 },
						 axisTicks: {
						 color: '#cccccc',
						 },
						categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
					},
					yaxis: {
					labels: {
						show: true,
					},
					axisBorder: {
						 color: '#fff',
						 show: true,
					 },
					},
					grid: {
					show: true,
					borderColor: '#efefef',
					},
				}
			var chart = new ApexCharts(
				document.querySelector("#revenueanalysis"),
				options
			);
			chart.render();
		 }
	
		$("#linearea").sparkline([50, 90, 85, 70, 95 , 88, 82, 90, 85, 70, 95 , 88, 82, 50], {
			type: 'line',
			width: '100%',
			height: '30',
			lineColor: '#00c689',
			fillColor: '#00c689',
			lineWidth: 1,
			spotRadius: 2,
			minSpotColor: false,
    		maxSpotColor: false
		});
		$("#baralc").sparkline([50, 90, 85, 70, 95 , 88, 82, 90, 85, 70, 95 , 88, 82, 50], {
			type: 'bar',
			height: '30',
			barWidth: 6,
			barSpacing: 4,
			barColor: '#fda006',
		});		
		$("#lineIncrease").sparkline([50, 90, 85, 70, 95 , 88, 82, 90, 85, 70, 95 , 88, 82, 50], {
			type: 'line',
			width: '100%',
			height: '30',
			lineWidth: 2,
			lineColor: '#00BCD4',
			fillColor: "#ffffff",
			spotColor: '#fda006',
			minSpotColor: '#fda006',
			maxSpotColor: '#fda006',
			spotRadius: 3,
		});
	
	
		Apex.grid = {
		  padding: {
			right: 0,
			left: 0
		  }
		}

		Apex.dataLabels = {
		  enabled: false
		}

		var randomizeArray = function (arg) {
		  var array = arg.slice();
		  var currentIndex = array.length, temporaryValue, randomIndex;

		  while (0 !== currentIndex) {

			randomIndex = Math.floor(Math.random() * currentIndex);
			currentIndex -= 1;

			temporaryValue = array[currentIndex];
			array[currentIndex] = array[randomIndex];
			array[randomIndex] = temporaryValue;
		  }

		  return array;
		}

		// data for the sparklines that appear below header area
		var sparklineData = [47, 45, 54, 38, 56, 24, 65, 31, 37, 39, 62, 51, 35, 41, 35, 27, 93, 53, 61, 27, 54, 43, 19, 46];

		var spark1 = {
		  chart: {
			id: 'sparkline1',
			group: 'sparklines',
			type: 'area',
			height: 373,
			sparkline: {
			  enabled: true
			},
		  },
		  stroke: {
			curve: 'straight'
		  },
		  fill: {
			opacity: 1,
			colors: ['#405189']
		  },
		  series: [{
			name: 'Sales',
			data: randomizeArray(sparklineData)
		  }],
		  labels: [...Array(24).keys()].map(n => `2018-09-0${n+1}`),
		  yaxis: {
			min: 0
		  },
		  xaxis: {
			type: 'datetime',
		  },
		  colors: ['#405189'],

		}

		new ApexCharts(document.querySelector("#spark1"), spark1).render();
	
		
	
		var optionsBar = {
		  chart: {
			type: 'bar',
			height: 359,
			width: '100%',
			stacked: true,
			foreColor: '#999'
		  },
		  plotOptions: {
			bar: {
			  dataLabels: {
				enabled: false
			  },
			  columnWidth: '75%',
			  endingShape: 'rounded'
			}
		  },
		  colors: ["#00C5A4", '#F3F2FC'],
		  series: [{
			name: "Total Income",
			data: [20, 16, 24, 28, 26, 22, 15],
		  }, {
			name: "Total Sale",
			data: [18, 11, 14, 27, 21, 18, 10],
		  }],
		  xaxis: {
			axisBorder: {
			  show: false
			},
			categories: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
			axisTicks: {
			  show: false
			},
			crosshairs: {
			  show: false
			},
			labels: {
			  show: true,
			  style: {
				fontSize: '14px'
			  }
			},
		  },
		  grid: {
			xaxis: {
			  lines: {
				show: false
			  },
			},
			yaxis: {
			  lines: {
				show: false
			  },
			}
		  },
		  yaxis: {
			axisBorder: {
			  show: false
			},
			labels: {
			  show: false
			},
		  },
		  tooltip: {
			shared: true
		  }

		}

		var chartBar = new ApexCharts(document.querySelector('#bar'), optionsBar);
		chartBar.render();
	
		WeatherIcon.add('icon1'	, WeatherIcon.SLEET , {stroke:false , shadow:false , animated:true } );
	
		
	
}); // End of use strict

