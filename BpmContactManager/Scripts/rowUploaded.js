var dynamicLoadContactCount = 15;
var defaultOffset = 0;
var defaultUrl = "/Home/Contacts";

function getContacts(count, offset, url) {
    url = url || defaultUrl;
    count = count || dynamicLoadContactCount;
    offset = offset || defaultOffset;
    $.getJSON(url, { count: count, offset: offset }, addRows);
}

function addRows(contacts) {
    contacts.forEach(function (contact) {
        addTableRow(contact);
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
    }));

    var actionsSection = $('<td>', {
        class: 'actions',
    })
    .append($('<a>', {
        text: 'Edit',
        href: '/Home/Edit/' + contact.ServiceId
    }))
    .append(' | ')
    .append($('<a>', {
        text: 'Delete',
        href: '/Home/Delete/' + contact.ServiceId
    }));

    row.append(actionsSection);

    $('tbody').append(row);
}