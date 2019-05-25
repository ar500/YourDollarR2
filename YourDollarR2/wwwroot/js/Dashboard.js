am4core.ready(function () {

	// Themes begin
	am4core.useTheme(am4themes_material);
	am4core.useTheme(am4themes_animated);
	// Themes end

	// Create chart instance
	var chart = am4core.create("chartdiv", am4charts.PieChart);

	// Add and configure Series
	var pieSeries = chart.series.push(new am4charts.PieSeries());
	pieSeries.dataFields.value = "litres";
	pieSeries.dataFields.category = "country";

	// Let's cut a hole in our Pie chart the size of 30% the radius
	chart.innerRadius = am4core.percent(30);

	// Put a thick white border around each Slice
	pieSeries.slices.template.stroke = am4core.color("#fff");
	pieSeries.slices.template.strokeWidth = 2;
	pieSeries.slices.template.strokeOpacity = 1;
	pieSeries.slices.template
		// change the cursor on hover to make it apparent the object can be interacted with
		.cursorOverStyle = [
			{
				"property": "cursor",
				"value": "pointer"
			}
		];

	//pieSeries.alignLabels = false;
	//pieSeries.labels.template.bent = true;
	//pieSeries.labels.template.radius = 3;
	//pieSeries.labels.template.padding(0, 0, 0, 0);
	pieSeries.labels.template.disabled = true;

	pieSeries.ticks.template.disabled = true;

	// Create a base filter effect (as if it's not there) for the hover to return to
	var shadow = pieSeries.slices.template.filters.push(new am4core.DropShadowFilter);
	shadow.opacity = 0;

	// Create hover state
	var hoverState = pieSeries.slices.template.states.getKey("hover"); // normally we have to create the hover state, in this case it already exists

	// Slightly shift the shadow and make it more prominent on hover
	var hoverShadow = hoverState.filters.push(new am4core.DropShadowFilter);
	hoverShadow.opacity = 0.7;
	hoverShadow.blur = 5;

	chart.data = [{
		"country": "Lithuania",
		"litres": 501.9
	}, {
		"country": "Germany",
		"litres": 165.8
	}, {
		"country": "Australia",
		"litres": 139.9
	}, {
		"country": "Austria",
		"litres": 128.3
	}, {
		"country": "UK",
		"litres": 99
	}, {
		"country": "Belgium",
		"litres": 60
	}];

}); // end am4core.ready()

am4core.ready(function () {

	// Themes begin
	am4core.useTheme(am4themes_animated);
	// Themes end

	var iconPath = "M119.68,131.394c-15.109-3.942-27.591-14.454-27.591-30.876c0-15.329,13.138-26.935,27.591-29.343V53    h15.328v18.175c12.482,1.095,23.213,9.416,30.656,19.051l-10.729,9.416c-9.854-10.949-14.234-13.796-19.928-14.671v35.912    c17.736,2.627,33.284,13.358,33.284,32.847c0,18.176-16.204,30.877-33.284,31.096V203H119.68v-18.176    c-13.576-0.658-24.963-10.512-31.971-21.68l12.044-8.76c5.037,7.445,10.292,15.328,19.927,16.643V131.394z M119.68,84.97    c-7.227,1.752-12.262,9.196-12.262,16.204c0,7.226,5.255,13.139,12.262,15.547V84.97z M135.008,171.027    c9.635-0.438,17.956-7.445,17.956-17.518c0-9.635-8.759-16.424-17.956-17.736V171.027z";



	var chart = am4core.create("pictorialDiv", am4charts.SlicedChart);
	chart.hiddenState.properties.opacity = 0; // this makes initial fade in effect

	chart.data = [{
		"name": "The first",
		"value": 354
	}, {
		"name": "The second",
		"value": 245
	}, {
		"name": "The third",
		"value": 187
	}, {
		"name": "The fourth",
		"value": 123
	}, {
		"name": "The fifth",
		"value": 87
	}, {
		"name": "The sixth",
		"value": 45
	}, {
		"name": "The seventh",
		"value": 23
	}];

	var series = chart.series.push(new am4charts.PictorialStackedSeries());
	series.dataFields.value = "value";
	series.dataFields.category = "name";
	series.labels.template.disabled = true;

	series.maskSprite.path = iconPath;
	series.ticks.template.disabled = true;

	series.labelsContainer.width = 0;

}); // end am4core.ready()