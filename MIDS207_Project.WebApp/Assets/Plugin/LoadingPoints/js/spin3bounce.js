$(document).ready(function () {
 
});

var removeInitialLoading = function () {

    $('#initialLoading').addClass('animated bounceOutUp');

    setTimeout(function () {
        $("#initialLoading").remove();
    }, 3000);
};

function createUUID() {
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
    s[8] = s[13] = s[18] = s[23] = "-";

    var uuid = s.join("");
    return uuid;
}

$.fn.extend({
    showLoader: function () {
        var loader =
                 '<div class="cx-ajax-loader">' +
                    '<div class="bg"></div>' +
'                    <div class="spinner3bounce"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>' +
                 '</div>';

        $(this).append(loader);
        $(this).find('.cx-ajax-loader').fadeIn(0);
    },

    hideLoader: function () {
        $(this).find('.cx-ajax-loader').fadeOut(0, function () {
            $(this).remove();
        });
    },

    hideLoaderWithSuccessMessage: function (message) {
        var panel = this;
        $(this).find('.cx-ajax-loader').fadeOut(400, function () {
            $(this).remove();

            //swal
        });
    },

    hideLoaderWithErrorMessage: function (message) {
        var panel = this;
        $(this).find('.cx-ajax-loader').fadeOut(400, function () {
            $(this).remove();

            //swal
        });
    }
});


var showSuccess = function (message, timeout) {
    $('body').pgNotification({
        style: 'flip',
        message: message,
        position: 'top-right',
        type: 'success',
        timeout: timeout,
        onShown: function () { }
    }).show();
}

var showError = function (message, timeout) {
    $('body').pgNotification({
        style: 'flip',
        message: message,
        position: 'top-right',
        type: 'danger',
        timeout: timeout,
        onShown: function () { }
    }).show();
}

var showAjaxLoader = function (targetID) {
    var newID = createUUID();
    return newID;
};

var hideAjaxLoader = function () {

};