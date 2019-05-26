$(function () {
    $('#createButton').on('click', function () {
        $('#modalSection').load('/BudgetCategories/Index?handler=CreateNewPartial');
    });
});

$(function () {
    $('.details_button').on('click', function () {
        var modelId = $(this).data("id");
        $('#modalSection').load('BudgetCategories/Details?handler=LoadDetailsPartial&id=' + modelId);
    });
});

$(function () {
    $('.edit_button').on('click', function () {
        var modelId = $(this).data('id');
        $('#modalSection').load('BudgetCategories/Edit?handler=LoadEditPartial&id=' + modelId);
    });
});

$(function () {
    $('.delete_button').on('click', function () {
        var modelId = $(this).data('id');
        $('#modalSection').load('BudgetCategories/Delete?handler=DeletePartial&id=' + modelId);
    });
});