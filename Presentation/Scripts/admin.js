$(function () {
    fixValidationClassesForBootstrap();

    $('.datetime-picker').datetimepicker({ sideBySide: true, format: consts.editorDateFormat });

    $('.multi-select').multiselect({ buttonWidth: '100%' });

    $('input[type="tel"]').mask('(999) 999-9999');

    tinymce.init({
        plugins: 'autolink autosave image imagetools link lists table',
        selector: '.html-editor'
    });
});

//#region Handle Bootstrap classes for form controls during validation

function fixValidationClassesForBootstrap() {
    var validationForms = $('.form-with-validation');

    if (validationForms.length > 0) {
        var settings = $.data(validationForms[0]).validator.settings,
            baseErrorPlacement = settings.errorPlacement,
            baseSuccess = settings.success;

        settings.errorPlacement = function (label, element) {
            // Call original handler so it can update the HTML
            baseErrorPlacement(label, element);

            // Add Bootstrap classes to newly added elements
            label.parents('.form-group').addClass('has-error');
            label.addClass('text-danger');
        };

        settings.success = function (label) {
            // Remove error class from <div class="form-group">, but don't worry about
            // validation error messages as the plugin is going to remove it anyway
            label.parents('.form-group').removeClass('has-error');

            // Call original handler to do rest of the work
            baseSuccess(label);
        };
    }
}

//#endregion

//#region Table-Rendering Functions

function datatable_RenderList(options) {
    ko.applyBindings({
        columns: options.columns.slice(0, -1)
    });

    $(options.tableSelector).DataTable({
        'ajax': {
            'url': options.dataUrl,
            'type': 'POST',
            'dataSrc': ''
        },
        'columns': options.columns,
        'info': options.paging,
        'order': options.order,
        'orderCellsTop': false,
        'ordering': options.sorting,
        'pageLength': 25,
        'paging': options.paging,
        'pagingType': 'full_numbers',
        'searching': false
    });
}

function renderActiveCell(data, type) {
    return type === 'display'
        ? '<span class="glyphicon glyphicon-' + (data ? 'ok' : 'remove') + '"></span>'
        : data;
}

function renderBool(data, type) {
    return type === 'display'
        ? (data ? 'Yes' : 'No')
        : data;
}

function renderConfDiv(data, type) {
    return type === 'sort'
        ? (data.IsInLeague ? 0 : 1000) + data.Sort
        : data.Name;
}

function renderDate(data, type) {
    return data
        ? type === 'display'
            ? moment(data).format(consts.tableDateFormat)
            : moment(data).unix()
        : '';
}

function renderEditLink(data, type) {
    return type === 'display'
        ? '<a href="' + data + '" title="edit"><span class="glyphicon glyphicon-edit"></span></a>'
        : data;
}

function renderPartialContent(data, type) {
    return type === 'display'
        ? data && data.length
            ? '<div class="partial-content">' + data + '</div>'
            : null
        : data;
}

//#endregion

//#region List Table Settings Functions

$('.admin-list').on('click', '.admin-list-settings', function () {
    $('.datatable-settings').toggle();
});

$('.admin-list').on('click', '.column-toggle', function () {
    var index = $(this).attr('data-column');
    var column = $('.admin-list').DataTable().column(index);

    column.visible(!column.visible());
});

//#endregion
