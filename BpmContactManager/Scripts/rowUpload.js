var dynamicLoadContactCount = 15;
var defaultUrl = "/Home/Contacts";

function getContacts(count, offset, url) {
    toggleLoading(true);
    url = url || defaultUrl;
    count = count || dynamicLoadContactCount;
    offset = offset || $('.info-row').length;

    $.getJSON(url, { count: count, offset: offset }, addRows);
}

function addRows(contacts) {
    contacts.forEach(function (contact) {
        addTableRow(contact);
        toggleLoading(false);
    });
}

function addTableRow(contact) {
    var row = $('<tr>', {
        class: 'info-row'
    })
    .append($('<td>', {
        class: 'name',
        text: contact.Name
    }))
    .append($('<td>', {
        class: 'dear',
        text: contact.Dear
    }))
    .append($('<td>', {
        class: 'mobile-phone',
        text: contact.MobilePhone
    }))
    .append($('<td>', {
        class: 'job-title',
        text: contact.JobTitle
    }))
    .append($('<td>', {
        class: 'birth-date',
        text: contact.BirthDate
    }))
    .attr('data-serviceid', contact.ServiceId)
    .click(infoRowClicked);

    var actionsSection = $('<td>', {
        class: 'actions',
    });
    var removeButton = $('<input>', {
        class: 'btn btn-delete',
        type: 'button',
        value: 'Remove'
    }).click(removeBtnClicked);

    actionsSection.append(removeButton);
    row.append(actionsSection);

    $('tbody').append(row);
}

function removeBtnClicked(e) {
    var contactRow = $(this).parent().parent();
    var contactServiceId = contactRow.data('serviceid');
    var contactName = contactRow.children('.name').text();
    var contactDear = contactRow.children('.dear').text();
    var contactMobilePhone = contactRow.children('.mobile-phone').text();
    var contactJobTitle = contactRow.children('.job-title').text();
    var contactBirthDate = contactRow.children('.birth-date').text();

    var message = 'Remove this contact?\n' +
        '\nName: ' + contactName +
        '\nDear: ' + contactDear +
        '\nMobile phone: ' + contactMobilePhone +
        '\nJob title: ' + contactJobTitle +
        '\nBirth date: ' + contactBirthDate;

    if (confirm(message)) {
        location.href = '/Home/Delete/' + contactServiceId;
        return false;
    }
    return false;
}

function infoRowClicked() {
    var contactRow = $(this);
    var contactServiceId = contactRow.data('serviceid');
    console.log(contactServiceId);
    location.href = '/Home/Edit/' + contactServiceId;
    return false;
}

function toggleLoading(isVisible) {
    if (isVisible) {
        $('.loading-row').show();
    }
    else {
        $('.loading-row').hide();
    }
}