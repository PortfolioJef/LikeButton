

var urlApi = 'http://localhost:29453/api/Like/';

$(document).ready(function () {
    $.get(urlApi + 'getCountLike', function (cnt) {
        $('#spcnt').text(cnt);
    });

    $('button').on("click", function (e)
    {
        $('#spcnt').attr("disabled", "disabled");
        $.ajax({
            url: urlApi + 'incrementCount',
            type: "PUT",            
            contentType: "application/json",
            success: function (cnt) {
                $('#spcnt').text(cnt);
                $('#spcnt').attr("disabled", "");
            }
        });
    });

});