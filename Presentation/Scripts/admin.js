﻿$(function () {
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
    if (validationForms.length === 0) return;

    var settings = $.data(validationForms[0]).validator.settings,
        baseErrorPlacement = settings.errorPlacement,
        baseSuccess = settings.success;

    if (validationForms.hasClass('form-grid')) {
        settings.showErrors = function(errorMap, errorList) {
            $('.valid')
                .each(function(i, v) {
                    $(v).tooltip('destroy');
                    $(v.closest('td')).removeClass('has-error');
                });

            $.each(errorList,
                function(i, v) {
                    $(v.element).tooltip({ title: v.message, placement: 'top' });
                    $(v.element.closest('td')).addClass('has-error');
                });

            this.defaultShowErrors();
        };
    } else {
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
}

//#endregion

//#region Table-Rendering Functions

var adminListTable;
var dtRowKey;

function datatable_RenderList(options) {
    ko.applyBindings({
        columns: options.columns.slice(0, -1),
        customizableColumns: options.columns.filter(function(column) {
            return !!column.title;
        })
    });

    options.tableSelector = '#adminListTable';

    adminListTable = initDataTables(options);
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

function renderBoolCheckbox(data, type, row, meta) {
    return type === 'display'
        ? '<input name="[' + row[dtRowKey] + '].' + meta.settings.aoColumns[meta.col].data + '" type="checkbox"' + (data ? ' checked="checked"' : '') + ' value="true" />'
        : data;
}

function renderClinchDropdown(data, type, row) {
    if (type !== 'display' || !data) return data.ClinchChar;

    selectOption(clinchSelect, data.ClinchChar);
    return '<select name="[' + row[dtRowKey] + '].Clinch">' + clinchSelect.html() + '</select>';
}

function renderConfDiv(data, type) {
    return type === 'sort'
        ? (data.IsInLeague ? 0 : 1000) + data.Sort
        : data.Name;
}

function renderDivDropdown(data, type, row) {
    if (type === 'sort') return (data.IsInLeague ? 0 : 1000) + data.Sort;
    if (type !== 'display') return data.Name;

    selectOption(divisionSelect, data.DivisionYearId);
    return '<select name="[' + row[dtRowKey] + '].DivisionYearId">' + divisionSelect.html() + '</select>';
}

function renderDate(data, type) {
    return data
        ? type === 'display'
            ? moment(data).format(consts.tableDateFormat)
            : moment(data).unix()
        : '';
}

function renderEditLink(data, type) {
    if (type !== 'display' || !data) return null;

    return renderLink(data, 'edit', 'edit');
}

function renderEditLinkWithHiddenId(data, type, row) {
    if (type !== 'display' || !data) return null;

    var hiddenId = '<input type="hidden" name="[' + row[dtRowKey] + '].' + dtRowKey + '" value="' + row[dtRowKey] + '" />';
    var hiddenIndex = '<input type="hidden" name="Index" value="' + row[dtRowKey] + '" />';

    return renderEditLink(data, type) + hiddenId + hiddenIndex;
}

function renderGameResultsLinks(data, type, row) {
    if (type !== 'display' || !data) return null;

    var submitResultsTitle = 'submit game result' + (row.IsWaitingForMyInput
        ? ' (awaiting your input)'
        : row.IsFinalized
            ? ' (confirmed result - add notes only)'
            : '');

    var enterStatsTitle = row.NoStats
        ? 'enter stats'
        : 'modify stats';

    var needActionClasses = 'btn btn-xs btn-success';

    var submitResultsClasses = row.IsWaitingForMyInput
        ? needActionClasses
        : row.IsFinalized
            ? 'text-danger'
            : undefined;

    var enterStatsClasses = row.NoStats
        ? needActionClasses
        : undefined;

    return [
        renderLink(data.SubmitResults, submitResultsTitle, 'pencil', submitResultsClasses),
        renderLink(data.EnterStats, enterStatsTitle, 'stats', enterStatsClasses)
    ].join(' ');
}

function renderLink(data, title, icon, linkClass) {
    return data
        ? '<a href="' + data + '" title="' + title + '"' + (linkClass ? ' class="' + linkClass + '"' : '') + '><span class="glyphicon glyphicon-' + icon + '"></span></a>'
        : '';
}

function renderNewsLinks(data, type, row) {
    if (type !== 'display') return null;

    return renderLink(data.Deactivate, 'deactivate', 'remove', 'deactivate-news') + ' ' + renderEditLink(data.Edit, type);
}

function renderPartialContent(data, type) {
    return type === 'display'
        ? data && data.length
            ? '<div class="partial-content">' + data + '</div>'
            : ''
        : data;
}

function renderPlayerMerge(data, type) {
    if (type !== 'display') return null;

    return '<input type="checkbox" class="merge-player" value="' + data + '" /> ' + renderLink('#', 'merge', 'random', 'merge-link');
}

function renderPlayerTransferLink(data, type) {
    if (type !== 'display') return null;

    var transferDestination = (data.UrlForTransfer.substr(-3) === '/' + consts.playerPoolTeamId)
        ? consts.playerPoolTeamName
        : (data.UrlForTransfer.substr(-2) === '/0')
            ? 'A Team'
            : 'My Team';

    return '<a class="player-transfer" href="' + data.UrlForTransfer + '">Transfer to ' + transferDestination + '</a>';
}

function renderTransfer(data, type, row) {
    if (type !== 'display' || row.ExistsInCurrentYear) return null;

    return '<input name="teamYearIds" type="checkbox" checked="checked" value="' + row.TeamYearId + '" />';
}

function selectOption(select, value) {
    select.val(value);
    select.find('option[selected]').removeAttr('selected');
    select.find('option:selected').attr('selected', 'selected');
}

//#endregion

//#region List Table Settings Functions

$('#adminListTable').on('click', '.admin-list-settings', function () {
    $('.datatable-settings').toggle();
});

$('#adminListTable').on('click', '.column-toggle', function () {
    var index = $(this).attr('data-column'); // TODO: use .data method
    var column = adminListTable.column(index);

    column.visible(!column.visible());
});

function setupPlayerMerge(url) {
    $('#adminListTable').on('click', 'a.merge-link', function () {
        var checkedBoxes = $('.merge-player:checked');

        if (checkedBoxes.length > 1 && $(this).parent().find('.merge-player:checked').length > 0) {
            if (confirm('Are you sure you want to merge?')) {
                var data = checkedBoxes.map(function () { return 'ids=' + this.value; }).get().join("&");

                $.post(url, data, function () {
                    location.reload(true);
                });
            }
        }
    });
}

$('#adminListTable').on('click', 'a.player-transfer', function (e) {
    e.preventDefault();

    var link = $(this);
    var href = link.attr('href');
    var rowIndex = adminListTable.row(link.closest('tr')[0]).index();

    if (href.substr(-2) === '/0') {
        $('#rowIndex').val(rowIndex);
        $('#urlForTransfer').val(href.slice(0, -1));
        $('#teamPickerModal').modal();
        return;
    }

    $.post(href)
        .done(function(data) {
            adminListTable.row(rowIndex).data(data[0]).draw();
        });
});

$('#adminListTable').on('click', '.select-all', function () {
    $('.admin-list td input[type="checkbox"]').prop('checked', this.checked);
});

$('#teamPickerModal').on('click', 'button.btn-primary', function () {
    var href = $('#urlForTransfer').val() + $('#teamId').val();
    var rowIndex = $('#rowIndex').val();

    $.post(href)
        .done(function (data) {
            adminListTable.row(rowIndex).data(data[0]).draw();
            $('#teamPickerModal').modal('hide');
        });
});

//#endregion

//#region Schedule Game-Updating Functions

$('.schedule-game-cell').on('click', '.data-button', function () {
    var url = $(this).data('url');
    if (url) {
        var scheduleEdit = $(this).closest('.schedule-edit');

        $.post(url)
         .done(function () {
             scheduleEdit.find('.game-updates').children().not('a').remove();
             scheduleEdit.find('input[name$="RunsScored"]').val(0);
             scheduleEdit.find('.overlay').fadeIn(500);
         });
    }
});

//#endregion

//#region Grid Table Event Handlers

$(function () {
    if ($('.form-grid').length > 0) {
        $('.form-grid tr').not('.col-row').each(function(i) {
            var $row = $(this);
            var index = generateIndexValue();
            $row.find('[name="StatLines.index"]').val(index);
            $row.find(':input').each(function() {
                var name = $(this).attr('name');
                if (name) {
                    $(this).attr('name', name.replace('[' + (i - 1) + ']', '[' + index + ']'));
                }
                var id = $(this).attr('id');
                if (id) {
                    $(this).attr('id', id.replace('_' + (i - 1) + '_', '_' + index + '_'));
                }
            });
        });
        $.validator.unobtrusive.parseDynamicContent('.form-grid tbody');
        $('.form-grid [type="reset"],[type="submit"]').attr('disabled', 'disabled');
    }
});

function generateIndexValue() {
    return Math.floor((Math.random() * 100000) + 1);
}

$('.form-grid')
    .on('click', 'button[type="reset"]', function() {
        if (confirm('Are you sure you want to reset the form and lose any changes?')) {
            window.location.reload();
        }
    })
    .on('click', '.btn-add', function() {
        $.post($('.form-grid').data('new-row-url'), function(data) {
            var index = generateIndexValue();
            data = data.replace(/\[0\]/g, '[' + index + ']');
            data = data.replace(/_0_/g, '_' + index + '_');
            $('.form-grid tbody').append(data);
            $('.form-grid tbody tr:last').find('input[name="StatLines.index"]').val(index);
            $('.form-grid').trigger('checkform.areYouSure');
            $.validator.unobtrusive.parseDynamicContent('.form-grid tbody tr:last');
        });
    })
    .on('click', '.btn-remove', function() {
        $(this).closest('tr').remove();
        $('.form-grid').trigger('checkform.areYouSure');
    })
    .areYouSure()
    .on('dirty.areYouSure', function() {
        // Enable save button only as the form is dirty.
        $(this).find('[type="reset"],[type="submit"]').removeAttr('disabled');
    })
    .on('clean.areYouSure', function() {
        // Form is clean so nothing to save - disable the save button.
        $(this).find('[type="reset"],[type="submit"]').attr('disabled', 'disabled');
    });

$('.table-stats')
    .on('click', '.row-nav button', function () {
        var $btn = $(this);
        var $row = $btn.closest('tr');

        switch ($btn.data('direction')) {
            case 'up':
                $row.insertBefore($row.prev());
                break;
            case 'dn':
                $row.insertAfter($row.next());
                break;
            case 'top':
                $row.insertBefore($row.prevAll('tr').last());
                break;
            case 'bot':
                $row.insertAfter($row.nextAll('tr').last());
                break;
        }

        $row.closest('tbody').find('tr').each(function(i) {
            $(this).find('input[name$=".BattingOrderPosition"]').val(i + 1);
        });

        $('.form-grid').trigger('checkform.areYouSure');
    });

$('.table-form')
    .on('focusin', '.form-control', function() {
        var $this = $(this);
        var index = $this.closest('tr').find('td').index($this.closest('td'));
        $($this.closest('table').find('th')[index]).addClass('focused');
    })
    .on('focusout', '.form-control', function() {
        var $this = $(this);
        var index = $this.closest('tr').find('td').index($this.closest('td'));
        $($this.closest('table').find('th')[index]).removeClass('focused');
    });

//#endregion

//#region Game Result New Report Form Event Handlers and Validation

$('#NewReportForm').on('change', '#NewReport_IsConfirmation', function () {
    var checked = $(this).is(':checked');
    if (checked) {
        $('#NewReportForm .confirmable input').val(function () {
            return this.defaultValue;
        });
        $('#NewReportForm .confirmable option').prop('selected', function () {
            return this.defaultSelected;
        });
    }
    $('#NewReportForm .confirmable .form-control').prop('disabled', checked);
});

$('#NewReportForm').on('click', '[type="reset"]', function () {
    $('#NewReportForm .confirmable .form-control').prop('disabled', false);
});

$(function () {
    if ($.validator) {
        $.validator.addMethod('forfeit-score',
            function(value, element) {
                if (parseInt($('select[name$=".GameStatusId"').val()) !== consts.gameStatus.forfeited) return true;

                var otherValue = $('input[name$=".RunsScored"]').not('#' + element.id).val();

                // The other value is valid - make sure this one is appropriate.
                if (otherValue === '0') return value === '15';
                if (otherValue === '15') return value === '0';

                // The other value was not valid, so validate this value on its own.
                return value === '0' || value === '15';
            },
            'The score of a forfeit must be 15 - 0.');

        $.validator.addMethod('result-scheduled-with-score',
            function(value) {
                if (parseInt(value) !== consts.gameStatus.scheduled) return true;

                var nonZeroScoreValues = $('input[name$=".RunsScored"]')
                    .filter(function() {
                        return parseInt($(this).val(), 10);
                    })
                    .length;

                // If any scores were not zero, this status is invalid.
                return !nonZeroScoreValues;
            },
            'The Result of the game should be set to Final.');

        $.validator.addMethod('result-missed-with-score',
            function(value) {
                if (parseInt(value) !== consts.gameStatus.postponed &&
                    parseInt(value) !== consts.gameStatus.rainedOut) return true;

                var nonZeroScoreValues = $('input[name$=".RunsScored"]')
                    .filter(function() { return parseInt($(this).val()); })
                    .length;

                // If any scores were not zero, this status is invalid.
                return !nonZeroScoreValues;
            },
            'Results of Postponed or Rained Out are invalid for games with a score.');
    }
});

//#endregion
