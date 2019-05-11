//[Dashboard Javascript]

//Project:	Bankio admin - Responsive Admin Template
//Primary use:   Used only for the main dashboard (index.html)

$(function () {

  'use strict';
	
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
			lineColor: '#ffffff',
			fillColor: "#00BCD4",
			spotColor: '#ffffff',
			minSpotColor: '#ffffff',
			maxSpotColor: '#ffffff',
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
			height: 355,
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
			height: 250,
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
			name: "Sale",
			data: [20, 16, 24, 28, 26, 22, 15, 5, 14, 16, 22, 29, 24, 19, 15, 10, 11, 15, 19, 23],
		  }, {
			name: "Views",
			data: [20, 16, 24, 28, 26, 22, 15, 5, 14, 16, 22, 29, 24, 19, 15, 10, 11, 15, 19, 23],
		  }],
		  labels: [15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 1, 2, 3, 4],
		  xaxis: {
			axisBorder: {
			  show: false
			},
			axisTicks: {
			  show: false
			},
			crosshairs: {
			  show: false
			},
			labels: {
			  show: false,
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

