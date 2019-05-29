$(function () {
	$('#createButton').on('click', function () {
		$('#modalSection').load('/Expenses/Index?handler=CreateNewPartial');
	});
});

$(function () {
	$('.details_button').on('click', function () {
		var modelId = $(this).data("id");
		$('#modalSection').load('/Expenses/Details?handler=LoadDetailsPartial&id=' + modelId);
	});
});

$(function () {
	$('.edit_button').on('click', function () {
		var modelId = $(this).data('id');
		$('#modalSection').load('/Expenses/Edit?handler=LoadEditPartial&id=' + modelId);
	});
});

$(function () {
	$('.delete_button').on('click', function () {
		var modelId = $(this).data('id');
		$('#modalSection').load('/Expenses/Delete?handler=DeletePartial&id=' + modelId);
	});
});

$(function () {
	$('.pay_in_full').on('click', function () {
		var expenseId = $(this).data('id');
		var amountPaid = $(this).data('amountpaid');
		var patch = [{ "op": "replace", "path": "/amountSpent", "value": amountPaid }];
		$.ajax({
			async: true,
			url: '/Api/Expenses/' + expenseId,
			type: 'PATCH',
			dataType: 'json',
			contentType: "application/json-patch+json; charset=utf-8",
			data: JSON.stringify(patch),
			success: function (data, textStatus, xhr) {
				location.reload();
				console.log(data);
			},
			error: function (xhr, textStatus, errorThrown) {
				console.log('Error in Operation');
			}
		});
	});
});

$('#tab-list a').on('click',
	function (e) {
		e.preventDefault();
		$(this).Tab('show');
	});

