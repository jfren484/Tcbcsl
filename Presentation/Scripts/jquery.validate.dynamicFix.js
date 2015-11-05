(function($) {
    $.validator.unobtrusive.parseDynamicContent = function(selector) {
        //use the normal unobstrusive.parse method
        $.validator.unobtrusive.parse(selector);

        //get the relevant form
        var form = $(selector).first().closest('form');

        //get the collections of unobstrusive validators, and jquery validators
        //and compare the two
        var unobtrusiveValidation = form.data('unobtrusiveValidation');
        var validator = form.validate();

        $.each(unobtrusiveValidation.options.rules, function(elname, elrules) {
            if (validator.settings.rules[elname] == undefined) {
                var args = {};
                $.extend(args, elrules);
                args.messages = unobtrusiveValidation.options.messages[elname];
                $('[name="' + elname + '"]').rules('add', args);
            } else {
                $.each(elrules, function(rulename, data) {
                    if (validator.settings.rules[elname][rulename] == undefined) {
                        var args2 = {};
                        args2[rulename] = data;
                        args2.messages = unobtrusiveValidation.options.messages[elname][rulename];
                        $('[name="' + elname + '"]').rules('add', args2);
                    }
                });
            }
        });
    }
})($);
