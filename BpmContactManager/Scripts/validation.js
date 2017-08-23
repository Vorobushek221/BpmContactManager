var isCorrect = false;
var flags = [];
function validate(inputSelector, messageSelector, buttonSelector, regex, message) {

    flags.push({ textBox: inputSelector, isValid: false });

    $(messageSelector).text(message);

    $(inputSelector).blur(function () {

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

            enableSubmit(buttonSelector);
        }
        else {
            $(messageSelector).show();
            flags.forEach(function (item) {
                if (item.textBox === $(self).id) {
                    item.isValid = false;
                    return;
                }
            });
            disableSubmit(buttonSelector);
        }
    });
}

function disableSubmit(buttonSelector) {
    $(buttonSelector).prop('disabled', true);
}

function enableSubmit(buttonSelector) {
    isCorrect = true;
    flags.forEach(function (item) {
        if (item.isValid === false) {
            isCorrect = false;
        }
    });

    if (isCorrect) {
        $(buttonSelector).prop('disabled', false);
    }
}

