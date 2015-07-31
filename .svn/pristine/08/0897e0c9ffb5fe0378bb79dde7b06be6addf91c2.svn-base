function InitHome() {
    $('#RegisterDiv').dialog(
     {
         modal: true,
         dialogClass: "no-close",
         autoOpen: false,
     }
     );

    $('#LoginDiv').dialog(
    {
        modal: true,
        dialogClass: "no-close",
        autoOpen: false
    }
    );
}

//function RunAdvertise() {
//    var speed = 3000;
//    var pics = 'Images/Home/advertise.jpg|Images/Home/003.jpg|Images/Home/004.jpg|Images/Home/005.jpg|Images/Home/advertise.jpg|Images/Home/003.jpg|Images/Home/004.jpg|Images/Home/005.jpg|Images/Home/004.jpg|Images/Home/005.jpg';
//    var mylinks = '#|#|#|#|#|#|#|#|#|#';
//    var texts = '就爱我吧|就爱我吧|就爱我吧|就爱我吧|就爱我吧|就爱我吧|就爱我吧|就爱我吧|就爱我吧|就爱我吧';
//    var sohuFlash2 = new sohuFlash("Scripts/focus0414.swf", "flashcontent01", "695", "340", "8", "#ffffff");

//    sohuFlash2.addParam("quality", "medium");
//    sohuFlash2.addParam("wmode", "opaque");
//    sohuFlash2.addVariable("speed", speed);
//    sohuFlash2.addVariable("p", pics);
//    sohuFlash2.addVariable("l", mylinks);
//    sohuFlash2.addVariable("icon", texts);
//    sohuFlash2.write("flashcontent01");
//}

var marquee_demo;
var zzjs;
var zzjs_net;

function RunShowGirl() {
    var speed = 30;
    marquee_demo = document.getElementById("marquee_demo");
    zzjs = document.getElementById("zzjs");
    zzjs_net = document.getElementById("zzjs_net");
    zzjs_net.innerHTML = zzjs.innerHTML;
    var MyMar = setInterval(Marquee, speed);
    marquee_demo.onmouseover = function () { clearInterval(MyMar) };
    marquee_demo.onmouseout = function () { MyMar = setInterval(Marquee, speed) };
}

function Marquee() {
    if (marquee_demo.scrollLeft >= zzjs.scrollWidth) {
        marquee_demo.scrollLeft = 0;
    }
    else {
        marquee_demo.scrollLeft++;
    }
}

//function SwithToLogin() {
//    var loginDiv = $("#LoginDiv");
//    var registerDiv = $("#RegisterDiv");
//    loginDiv.css({ "display": "inline" });
//    registerDiv.css({ "display": "none" });
//}

//function SwitchToRegister() {
//    var loginDiv = $("#LoginDiv");
//    var registerDiv = $("#RegisterDiv");
//    registerDiv.css({ "display": "inline" });
//    loginDiv.css({ "display": "none" });
//}

function onLogin() {
    $('#LoginDiv').dialog('open');
}

function onRegist() {
    $('#RegisterDiv').dialog('open');
}

function onClose() {
    $('#LoginDiv').dialog('close');
    $('#RegisterDiv').dialog('close');
}

