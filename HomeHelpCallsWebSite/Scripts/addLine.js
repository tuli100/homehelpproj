$(document).ready(initialize);

var form;

function initialize() {
    var button = $('.part-insert, .workpart-insert');
    button.click(addLine.addLine);
};

addLine.addLine = function (e) {
    $.ajax({
        url: addLine.addLineUrl,
        data: { lineType: lineType },
        type: 'GET',
        success: function (data, textStatus, jqXHR) {
            if (data.message) {
                alert(data.message);
            } else {
                data = $(data);
                addLine.addRowToTable(data);
            }
        },
        error: Site.unauthorizedAjaxHandler
    });
};

addLine.removeLine = function (e) {
    var button = $(e.target);
    var tr = button.closest('tr');
    tr.remove();
};

addLine.beforeApprove = function (e) {
    var table = $('.lines tbody');
    //$.each(table.children('tr'), function (index, row) {
    //    row = $(row);
    //    var input = row.find('td:nth-child(1)').find('input');
    //    input.attr('name', 'Transactions[' + index + '].Type');
    //    input = row.find('td:nth-child(2)').find('input');
    //    $(input[0]).attr('name', 'Transactions[' + index + '].ReceiverDisplayName');
    //    $(input[1]).attr('name', 'Transactions[' + index + '].ReceiverHID');
    //    input = row.find('td:nth-child(3)').find('input');
    //    input.attr('name', 'Transactions[' + index + '].Amount');
    //    input = row.find('td:nth-child(4)').find('input');
    //    input.attr('name', 'Transactions[' + index + '].DetailsForReceiver');
    //    input = row.find('td:nth-child(5)').find('input');
    //    input.attr('name', 'Transactions[' + index + '].DetailsForSender');
    //});
    if (form.validate().valid()) {
        Site.enableLoading();
    }
    else {
        e.preventDefault();
        return false;
    }
};

addLine.rowSelected = function (cell, table) {
    cell.find('input[type=hidden]').val(table.row('.selected').data()[0]);
    cell.closest('tr').find('td:nth-child(3) input').focus();
};

addLine.cloneLine = function (e) {
    var row = $(e.target).closest('tr').clone(); //Duplicate the row
    row.find('div.tooltip').remove(); //Remove the tooltip
    addLine.addRowToTable(row); //Insert to table
};

addLine.addRowToTable = function (row) {
    row.find('.button-remove').click(budget.sendMoney.removeLine);
    row.find('.button-clone').click(budget.sendMoney.cloneLine);
    $('.transactions-table tbody').append(row);
    Site.initializeValidation(form);
    if ($('.select-button').length == 0) {
        row.find('td:nth-child(3) input').focus();
    } else {
        $('.select-button').focus();
    }
    Site.enableToolTips(row);
};