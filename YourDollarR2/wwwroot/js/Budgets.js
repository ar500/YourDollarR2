$(function () {
    $('#createButton').on('click', function () {
        $('#modalSection').load('/BudgetsR2/Index?handler=CreateNewPartial');
    });
});

$(function () {
    $('.edit_button').on('click', function () {
        var modelId = $(this).data('id');
        $('#modalSection').load('/BudgetsR2/Edit?handler=LoadEditPartial&id=' + modelId);
    });
});

$(function () {
    $('.delete_button').on('click', function () {
        var modelId = $(this).data('id');
        $('#modalSection').load('/BudgetsR2/Delete?handler=DeletePartial&id=' + modelId);
    });
});

$(function () {
    $('#CycleStartDate').datepicker({
        uiLibrary: 'bootstrap4'
    });
});
$(function () {
    $('#CycleEndDate').datepicker({
        uiLibrary: 'bootstrap4'
    });
});

// Multi-select plugin
$(function () {
    $('#expenses-select').multiselect({
        enableFiltering: true,
        enableCaseInsensitiveFiltering: true,
        maxHeight: 400,
        buttonClass: 'btn btn-outline-info',
        nonSelectedText: 'Add your bills',
        numberDisplayed: 10,
        includeSelectAllOption: true
    });
});