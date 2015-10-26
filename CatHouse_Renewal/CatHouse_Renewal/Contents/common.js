/************************************************
   _____       _   _   _                      
  /  __ \     | | | | | |                     
  | /  \/ __ _| |_| |_| | ___  _   _ ___  ___ 
  | |    / _` | __|  _  |/ _ \| | | / __|/ _ \
  | \__/\ (_| | |_| | | | (_) | |_| \__ \  __/
   \____/\__,_|\__\_| |_/\___/ \__,_|___/\___|

  공용 스크립트
                      JoonChul Kim (jETA). 2015.
************************************************/

/*** 초기화 ***/
$(document).ready(function() {
    $("#mobileMenu").click(function() {
        if($("#gnb > ul").css("display") == "none") {
            $("#gnb > ul").slideDown();
            $("#mobileMenu").css("background-image", "url('Contents/Image/close.png')");
        } else {
            $("#gnb > ul").slideUp();
            $("#mobileMenu").css("background-image", "url('Contents/Image/menu.png')");
        }
    });
});

/*** 팝업 메시지 ***/
function showPopup(title, text, width, height) {
    var transparent = document.createElement("div");
    transparent.className = "_transparent";
    transparent.addEventListener("click", closePopup);
    
    var message = document.createElement("div");
    message.id = "popup";
    
    $("body").append(transparent);
    $("body").append(message);
    
    $("#popup").css({
        "width" : width,
        "right" : (($(window).width() - width) / 2) + "px",
        "height" : height,
        "top" : (($(window).height() - height) / 2) + "px"
    });

    $('#popup').html(
        '<img onclick="closePopup()" src="Contents/Image/close.png" />' +
        '<span class="popupTitle">' + title + '</span>' +
        '<span class="popupMemo">' + text + '</span>'
    );
    
    $("._transparent").show();
    $("#popup").show();
}

function closePopup() {
    $("#popup").remove();
    $("._transparent").remove();
}