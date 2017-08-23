var isCorrect = false;
var flags = [];
function validate(inputSelector, messageSelector, buttonSelector, regex, message, isRequired, isEditView) {

    flags.push({
        textBox: inputSelector,
        isValid: regex.test($(inputSelector).val())
    });

    toggleSubmit(buttonSelector);

    $(messageSelector).text(message);

    $(inputSelector).blur(check);
    $(inputSelector).on('input propertychange paste', check);

    function check() {
        var text = $(this).val();
        var self = this;
        if (regex.test(text)) {
            $(messageSelector).hide();

            flags.forEach(function (item) {
                if (item.textBox === '#' + $(self)[0].id) {
                    item.isValid = true;
                    return;
                }
            });
            toggleSubmit(buttonSelector);
            return true;
        }
        else {
            $(messageSelector).show();
            flags.forEach(function (item) {
                if (item.textBox === $(self).id) {
                    item.isValid = false;
                    return;
                }
            });
            toggleSubmit(buttonSelector);
            return false;
        }
    }
}

function toggleSubmit(buttonSelector) {
    isCorrect = true;
    flags.forEach(function (item) {
        if (item.isValid === false) {
            isCorrect = false;
        }
    });

    if (isCorrect) {
        $(buttonSelector).prop('disabled', false);
    }
    else {
        $(buttonSelector).prop('disabled', true);
    }
}

