//[Dashboard Javascript]

//Project:	Indusui admin - Responsive Admin Template
//Primary use:   Used only for the main dashboard (index.html)

$(function () {

  'use strict';
		
	
		var ctx6 = document.getElementById('earning2');
		  new Chart(ctx6, {
			type: 'line',
			data: {
			  labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
			  datasets: [{
				data: [10, 27, 11, 8, 14, 28, 12, 7, 13, 25, 18, 6],
				backgroundColor: 'rgba(33, 150, 243, 0.8)',
				borderWidth: 1,
				fill: true
			  },{
				data: [6, 5, 15, 9, 7, 8, 12, 22, 9, 7, 10, 9],
				backgroundColor: 'rgba(156, 39, 176, 0.8)',
				borderWidth: 1,
				fill: true
			  },{
				data: [30, 10, 11, 27, 12, 14, 32, 34, 13, 10, 22, 12],
				backgroundColor: 'rgba(139, 195, 74, 0.8) ',
				borderWidth: 1,
				fill: true
			  }]
			},
			options: {
			  legend: {
				display: false,
				  labels: {
					display: false
				  }
			  },
			  scales: {
				yAxes: [{
				  ticks: {
					beginAtZero:true,
					fontSize: 10,
					max: 45
				  }
				}],
				xAxes: [{
				  ticks: {
					beginAtZero:true,
					fontSize: 11
				  }
				}]
			  }
			}
		  });
	
	
		var ctx3 = document.getElementById('salesstatistics');
		  var myChart3 = new Chart(ctx3, {
			type: 'line',
			data: {
			  labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sut', 'Sun'],
			  datasets: [{
				label: 'Item',
				data: [5000, 10000, 12000, 6000, 6000, 900, 500],
				borderColor: '#00c689',
				borderWidth: 3,
				fill: false
			  }]
			},
			options: {
			  legend: {
				display: false,
				  labels: {
					display: false
				  }
			  },
			  scales: {
				yAxes: [{
				  ticks: {
					beginAtZero:true,
					fontSize: 10,
					max: 14000
				  }
				}],
				xAxes: [{
				  ticks: {
					beginAtZero:true,
					fontSize: 11
				  }
				}]
			  }
			}
		  });


		//INITIALIZE SPARKLINE CHARTS
		$(".sparkline").each(function () {
		  var $this = $(this);
		  $this.sparkline('html', $this.data());
		});

    
	
// sparkline chart
	
		$("#barchart4").sparkline([32,24,26,24,32,26,40,34,22,24], {
			type: 'bar',
			height: '100',
			width: '50%',
			barWidth: 6,
			barSpacing: 4,
			barColor: '#3da5f4',
		});
		
		// jQuery UI sortable for the todo list
		  $('.todo-list').sortable({
			placeholder         : 'sort-highlight',
			handle              : '.handle',
			forcePlaceholderSize: true,
			zIndex              : 999999
		  });	

		 /* The todo list plugin */
		  $('.todo-list').todoList({
			onCheck  : function () {
			  window.console.log($(this), 'The element has been checked');
			},
			onUnCheck: function () {
			  window.console.log($(this), 'The element has been unchecked');
			}
		  });
	
	
		WeatherIcon.add('icon1'	, WeatherIcon.SLEET , {stroke:false , shadow:false , animated:true } );
	
		
	
}); // End of use strict

