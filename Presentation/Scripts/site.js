// Declare constants here to make ReSharper/Intellisense happy in views that use them. Some values will be set in _Layout.cshtml
var consts = {
    firstYear: 0,
    currentYear: 0,
    editorDateFormat: 'MMMM D, YYYY, h:mm A',
    tableDateFormat: 'MMM D, YYYY, h:mm A'
}

$('.calendar-content').on('click', '.calendar-year-nav a', function (e) {
    var year = $(this).data('year');
    if (year >= consts.firstYear && year <= consts.currentYear) {
        $('.calendar-content').load('/Schedule/YearCalendar/' + year + '/' + $(this).data('active-date'));
    }

    e.preventDefault();
});

$(function() {
    $('[data-toggle="tooltip"]').tooltip();
});

//#region Helpers

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
