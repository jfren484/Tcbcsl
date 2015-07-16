// Declare constants here to make ReSharper/Intellisense happy in views that use them. Values will be set in _Layout.cshtml
var consts = {
    firstYear: 0,
    currentYear: 0
}

$('.calendar-content').on('click', '.calendar-year-nav a', function (e) {
    //e.stopPropagation();

    var year = $(this).data('year');
    if (year >= consts.firstYear && year <= consts.currentYear) {
        $('.calendar-content').load('/Schedule/YearCalendar/' + year);
    }

    //e.preventDefault();
});

//$('.calendar-content').click(function (e) {
//    e.stopPropagation();
//});
