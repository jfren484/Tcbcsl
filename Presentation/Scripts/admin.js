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

            // Call oldoriginal handler to do rest of the work
            baseSuccess(label);
        };
    }

    tinymce.init({ selector: '.html-editor' });
});

//#region Table-Rendering Functions

function datatable_RenderList(options) {
    addDataTableHeaderCells(options.tableSelector, options.columns);

    $(options.tableSelector).dataTable({
        'ajax': {
            'url': options.dataUrl,
            'type': 'POST',
            'dataSrc': ''
        },
        'info': options.paging,
        'ordering': options.sorting,
        'paging': options.paging,
        'pagingType': 'full_numbers',
        'pageLength': 25,
        'searching': false,
        'order': options.order,
        'columns': options.columns
    });
}

function renderAuditData(data, type) {
    return type === 'display'
        ? 'Created by ' + data.CreatedBy + ' on ' + data.Created + (data.Modified
            ? '<br />Last modified by ' + data.ModifiedBy + ' on ' + data.Modified
            : '')
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
