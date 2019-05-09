// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Declare constants here to make ReSharper/Intellisense happy in views that use them. Some values will be set in _Layout.cshtml
var consts = {
    firstYear: 0,
    currentYear: 0,
    playerPoolTeamId: 0,
    playerPoolTeamName: '',
    editorDateFormat: 'MMMM D, YYYY, h:mm A',
    tableDateFormat: 'MMM D, YYYY, h:mm A',

    gameStatus: {
        scheduled: 0,
        postponed: 0,
        rainedOut: 0,
        forfeited: 0,
        final: 0
    }
}

$('.calendar-content').on('click', '.calendar-year-nav a', function (e) {
    var url = $(this).data('url');
    if (url) {
        $('.calendar-content').load(url);
    }

    e.preventDefault();
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

//#region Helpers

function initDataTables(options) {
    return $(options.tableSelector)
        .DataTable({
            'ajax': {
                'url': options.dataUrl,
                'type': 'POST',
                'dataSrc': ''
            },
            'columnDefs': options.columnDefs,
            'columns': options.columns,
            'dom': options.dom || '<"row"<"col-sm-6"l><"col-sm-6"f>><"row"<"col-sm-12"tr>><"row"<"col-sm-5"i><"col-sm-7"p>>',
            'info': options.paging || false,
            'language': options.language,
            'order': options.order,
            'ordering': options.sorting || false,
            'pageLength': options.pageLength || 25,
            'paging': options.paging || false,
            'pagingType': options.pagingType || 'full_numbers',
            'searching': options.searching || false,
            'stateDuration': 0,
            'stateSave': true
        });
}

function addDataTableHeaderCells(tableSelector, columnArray) { // TODO: this should go away by using Knockout in the stats tables
    var headerRow = $(tableSelector + '>thead>tr.datatable-headers');
    for (var i = 0; i < columnArray.length; ++i) {
        headerRow.append('<th></th>');
    }
}

function findColumnIndex(columnArray, columnFieldOrTitle) {
    var matches = columnArray.filter(function (col) { return col.data === columnFieldOrTitle || col.title === columnFieldOrTitle });

    return matches.length === 0
        ? undefined
        : columnArray.indexOf(matches[0]);
}

function yearAsRouteParameter(year) {
    return year === consts.currentYear
        ? ''
        : '/' + (year === 0
            ? 'All'
            : year);
}

//#endregion
