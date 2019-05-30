$(function () {
	$('#createButton').on('click', function () {
		$('#modalSection').load('/Bills/Index?handler=CreateNewPartial');
	});
});

$(function () {
	$('.details_button').on('click', function () {
		var modelId = $(this).data("id");
		$('#modalSection').load('/Bills/Details?handler=LoadDetailsPartial&id=' + modelId);
	});
});

$(function () {
	$('.edit_button').on('click', function () {
		var modelId = $(this).data('id');
		$('#modalSection').load('/Bills/Edit?handler=LoadEditPartial&id=' + modelId);
	});
});

$(function () {
	$('.delete_button').on('click', function () {
		var modelId = $(this).data('id');
		$('#modalSection').load('/Bills/Delete?handler=DeletePartial&id=' + modelId);
	});
});



$('#tab-list a').on('click',
	function (e) {
		e.preventDefault();
		$(this).Tab('show');
	});

