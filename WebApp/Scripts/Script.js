
var Message = {
    ShowFixed: function (message, type) {
        $('.divmsg:visible').html('<div class="alert alert-' + type + '" role="alert">' + message + '</div>');
    },
    ShowNonFixed: function (message, type) {
        $.growl({
            message: message
        }, {
            spacing: 10,
            type: type,
            placement: {
                from: "bottom",
                align: "right"
            },
            delay: 4000,
            animate: {
                enter: 'animated bounceInUp',
                exit: 'animated bounceOutDown'
            }
        });
    },
    RemoveFixed: function () {
        $('.divmsg:visible').html('');

    },

    HideFixed: function () {
        $('.divmsg').html('');
    }
}

function successo(d) {
    $('.divmsg:visible').html('');

    var msgfixed = d.messageisfixed;
    if (msgfixed != undefined) {
        if (msgfixed) {
            Message.ShowFixed(d.message, d.typemessage);
        } else {
            Message.ShowNonFixed(d.message, d.typemessage);

        }
    }   
}