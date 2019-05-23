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