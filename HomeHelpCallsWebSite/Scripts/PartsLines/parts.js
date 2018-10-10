$(document).ready(initialize);

function initialize(){
    $('#parts-modal').modal('hide');
    $('#searchPart').click('selectPart');
}

PartsLines.selectPart = function (e) {
    var cell = $(e.target).closest('td');
    Site.enableLoading();
    $.ajax({
        type: 'POST',
        url: PartsLines.parts.getPartsUrl,
        //data: { transactionType: transactionType },
        success: function (data, textStatus, jqXHR) {
            var modalBody = $('#parts-modal .modal-body');
            modalBody.empty();
            data = $(data);
            modalBody.append(data);
            var table = data.DataTable({
                columnDefs: [{ targets: [0, 1], visible: false, searchable: true }],
                autoWidth: false,
                order: [[1, 'desc'], [2, 'asc']],
                language: {
                    url: "//cdn.datatables.net/plug-ins/1.10.11/i18n/Hebrew.json"
                }
            });
            data.on('click', 'tr', function () {
                $(this).addClass('selected');
                PartsLines.parts.rowSelected(cell, table);
            });
            $('#parts-modal').modal('show');
            setTimeout(function () {//The input doesn't exist yet, waiting for it to be created.
                var input = modalBody.find('input[type=search]');
                input.focus().keypress(function (e) {
                    if (e.which === 13) { //Is the key Enter?
                        var rows = table.$('tr', { "page": "current" }); //Get all rows
                        if (rows.length === 1) { //Is there only one row?
                            $(rows[0]).addClass('selected'); //Select the row
                            PartsLines.parts.rowSelected(cell, table);
                        }
                    };
                });
            }, 500);
        },
        error: Site.unauthorizedAjaxHandler,
        complete: function () {
            Site.disableLoading();
        }
    });
};

PartsLines.rowSelected = function (cell, table) {
    cell.find('input[type=hidden]').val(table.row('.selected').data()[0]);
    cell.find('span.part-name-display').text(table.row('.selected').data()[2]);
    cell.find('input[name=ReceiverDisplayName]').val(table.row('.selected').data()[2]);
    $('#parts-modal').modal('hide');
    cell.closest('tr').find('td:nth-child(3) input').focus();
};
