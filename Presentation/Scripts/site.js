// Declare constants here to make ReSharper/Intellisense happy in views that use them. Values will be set in _Layout.cshtml
var consts = {
    firstYear: 0,
    currentYear: 0
}

$('#calendarContent').on('click', '.calendar-year-nav a', function (e) {
    e.stopPropagation();

    var year = $(this).data('year');
    if (year >= consts.firstYear && year <= consts.currentYear) {
        $('#calendarContent').load('/Schedule/YearCalendar/' + year);
    }

    e.preventDefault();
});

$('#calendarContent').click(function (e) {
    e.stopPropagation();
});
