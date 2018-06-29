$(function () {
    $('#sendBtn').click(function (e) {
        e.preventDefault();

        $.ajax({
            type: "POST",
            url: "../Default/Ajax",                            //Your Action name in the DropDownListConstroller.cs
            //data: "{'AJAXParameter1':'" + $('#inputPanel').val() + "','AJAXParameter2':'" + $('#inputPanel').val() + "'}",  //Parameter in this function, Is case sensitive and also type must be string
            data: "{'AJAXParameter1':'" + $('#inputPanel').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json"

        }).done(function (data) {
            //Successfully pass to server and get response
            if (data.result = "OK") {
                $('#chatForm').append('<div class="container" align="left">' +
                '<img src="/w3images/avatar_g2.jpg" alt="Bot" class="right">' +
                '<p>' + data.chatVal + '</p>' +
                '</div>');
                //var test = JSON.parse(data);
                alert("submit successfully."+ data.chatVal);
            }
        }).fail(function (response) {
            if (response.status != 0) {
                alert(response.status + " " + response.statusText);
            }
        });

        $.ajax()
    });

});
//function getPerson(list) {
//    $.ajax({
//        url: "../Default/AjaxGet",
//        type: 'GET',
//        dataType: 'json',
//        // we set cache: false because GET requests are often cached by browsers
//        // IE is particularly aggressive in that respect
//        cache: false,
//        data: { list: list },
//        success: function (chat) {
//            $('#UserType').val(chat.UserType);
//            $('#Text').val(chat.Text);
//        }
//    });
//}
$(function () {
    var chat = $.connection.chatEngine;
    var $message = $('#inputPanel');
    var $messages = $('#messages');
    $message.focus();
    chat.client.sendMessage = function (message) {
        //$messages.append('<li>' + message + '</li>');
        $('#chatForm').append('<div class="container darker" align="right">' +
            '<img src="/w3images/avatar_g2.jpg" alt="Müşteri" class="right">' +
            '<p>' + message + '</p>' +
            '</div>');



    };
    //$("#chatForm").stop().animate({ scrollTop: $("#chatForm")[0].scrollHeight }, 1000);

    $.connection.hub.start().done(function () {
        $('#sendBtn').click(function () {

            chat.server.send($message.val());

            $message.val('').focus();
            var scrollingElement = $('#chatForm');

            $(scrollingElement).animate({
                scrollTop: document.body.scrollHeight
            }, 500);
            //Sistem mesajı buradan yayınlayacak
            
        });
    });

});
