var lel;

function stoppedTyping() {
    if ('#inputPanel'.length > 0) {
        document.getElementById('sendBtn').disabled = false;
    } else {
        document.getElementById('sendBtn').disabled = true;
    }
}
$(function () {
    var chat = $.connection.chatEngine;
    var $message = $('#inputPanel');
    var $messages = $('#messages');
    $message.focus();
    chat.client.sendMessage = function (message) {
        //$messages.append('<li>' + message + '</li>');
        $('#chatForm').append('<div class="container darker" align="right">' +
            '<img alt="Müşteri" class="right">' +
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
            document.getElementById('sendBtn').disabled = true;
            //Sistem mesajı buradan yayınlayacak

        });
    });

});
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
            if (data.result == "OK") {
                $('#chatForm').append('<div class="container" align="left">' +
                '<img alt="Bot" class="right">' +
                '<p>' + data.chatVal + '</p>' +
                '</div>');
            }
            if(data.result=="Table"){
                
                $('#chatForm').append('<div class="container" align="left">' +
                    '<img alt="Bot" class="right">' +
                    '<p>' + data.tableInfo.message + '</p>' +
                    '</div>');
                $('#chatForm').append('<div class="container" align="left">' +
                 '<table style="width:100%">' +
                  '<tr>' +
                   '<th>Karton No</th>' +
                    '<th>Karton Borç</th>' +
                  '</tr>' +
                  data.tableInfo.tableData +
                '</table>' +
                '</div>');
                $('#chatForm').append('<div class="container" align="left">' +
                    '<img alt="Bot" class="right">' +
                    '<p> Toplam borcunuz: ' + data.tableInfo.total + ' Ödeme planı yaratmak ister misiniz?</p>' +
                    '</div>');
            }
            if (data.result == "PaymentTable") {

                //$('#chatForm').append('<div class="container" align="left">' +
                //    '<img src="/w3images/avatar_g2.jpg" alt="Bot" class="right">' +
                //    '<p>' + data.tableInfo.message + '</p>' +
                //    '</div>');
                $('#chatForm').append('<div class="container" align="left">' +
                 '<table style="width:100%">' +
                  '<tr>' +
                   '<th>Ödeme Tarihleri</th>' +
                    '<th>Ödenecek Taksit Miktarı</th>' +
                  '</tr>' +
                  data.tableInfo.tableData +
                '</table>' +
                '</div>');
                $('#chatForm').append('<div class="container" align="left">' +
                    //'<img src="~/Content/images.jpg" alt="Bot" class="right">' +
                    '<p> İşlenen faiz ile ödeyeceğiniz toplam ücret: '+data.tableInfo.total+ ' TL. ' + data.tableInfo.message + '</p>' +
                    '</div>');
            }
            var scrollingElement = $('#chatForm');

            $(scrollingElement).animate({
                scrollTop: document.body.scrollHeight
            }, 500);
        }).fail(function (response) {
            if (response.status != 0) {
                alert(response.status + " " + response.statusText);
            }
        });

        $.ajax()
    });

});
//$(function(){

//});
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


window.onunload = function () {
    //'@Html.Raw(HttpUtility.JavaScriptStringEncode(TempData["StateOfChat"]))' = null;
    $.ajax({
        type: "GET",
        url: '../Default/DeleteAjax',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data) {
                // Works
            }
        }
    });
    //alert('@(TempData["StateOfChat"])');
    //alert('Sen nabürsen ya');
    //return "Dude, are you sure you want to refresh? Think of the kittens!";
}
