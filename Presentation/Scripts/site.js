$('#yearSelector').change(function () {
    window.location.href = $('#yearSelector option:selected').data('url');
});
