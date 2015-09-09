// Declare constants here to make ReSharper/Intellisense happy in views that use them. Values for years will be set in _Layout.cshtml
var consts = {
    firstYear: 0,
    currentYear: 0,
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

function addDataTableHeaderCells(tableSelector, columnArray) {
    var headerRow = $(tableSelector + '>thead>tr.datatable-headers');
    for (var i = 0; i < columnArray.length; ++i) {
        headerRow.append('<th></th>');
    }
}
