$(function () {
    var validationForms = $('.form-with-validation');
    if (validationForms.length > 0) {
        var settings = $.data(validationForms[0]).validator.settings,
            baseErrorPlacement = settings.errorPlacement,
            baseSuccess = settings.success;

        settings.errorPlacement = function(label, element) {
            // Call original handler so it can update the HTML
            baseErrorPlacement(label, element);

            // Add Bootstrap classes to newly added elements
            label.parents('.form-group').addClass('has-error');
            label.addClass('text-danger');
        };

        settings.success = function(label) {
            // Remove error class from <div class="form-group">, but don't worry about
            // validation error messages as the plugin is going to remove it anyway
            label.parents('.form-group').removeClass('has-error');

            // Call original handler to do rest of the work
            baseSuccess(label);
        };
    }

    tinymce.init({ selector: '.html-editor' });
});

//#region Table-Rendering Functions

var settingsLink = '<a class="admin-list-settings"><span class="glyphicon glyphicon-cog"></span></a>';

function datatable_RenderList(options) {
    addDataTableHeaderCells(options.tableSelector, options.columns);

    datatable_AddTableSettingsColumns(options.columns);

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

function datatable_AddTableSettingsColumns(columns) {
    $('.datatable-settings th').attr('colspan', columns.length);
    var list = $('.datatable-settings th div.btn-group');
    for (var i = 0; i < columns.length - 1; ++i) {
        var label = $('<label class="btn btn-primary column-toggle">').appendTo(list);
        label.attr('data-column', i);

        var input = $('<input type="checkbox" autocomplete="off">').appendTo(label);
        label.append(columns[i].title);

        if (columns[i].visible !== false) {
            label.addClass('active');
            input.prop('checked', true);
        }
    }
}

function renderActiveCell(data, type) {
    return type === 'display'
        ? '<span class="glyphicon glyphicon-' + (data ? 'ok' : 'remove') + '"></span>'
        : data;
}

function renderEditLink(data, type) {
    return type === 'display'
        ? '<a href="' + data + '" title="edit"><span class="glyphicon glyphicon-edit"></span></a>'
        : data;
}

function renderPartialContent(data, type) {
    return type === 'display'
        ? '<div class="partial-content">' + data + '</div>'
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
