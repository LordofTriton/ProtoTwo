var num = 0;
var bckg = ["bck1.jpg", "bck3.jpg", "bck4.jpg"];
var funny = "";

var loadernum = 1;
var numx = 3;

function Load() {
    loadernum += 0.1;
    var loader = document.getElementById("loader");
    loader.style.width = String(loadernum) + "%";
}

function Slide() {
    loadernum = 1;
    if (num == 2) {
        num = 0;
    } else {
        num += 1;
    }
    //num = 0;
    document.body.style.backgroundImage = "url(" + bckg[num] + ")";
}

var time = setInterval(Funn, 100);
//var load = setInterval(Load, 8);

function Verify() {
    var userid = document.getElementById("userid").value;
    var pass = document.getElementById("pass").value;
    if (userid == "root" && pass == "root") {
        return true;
    } else {
        document.getElementById("login_error").style.display = "block";
        return false;
    }
}

function Funn() {
    num += 1;
    funny += "&#187";
    if (num == 4) {
        num = 0;
        funny = "";
    }
    document.getElementById("fun").innerHTML = funny;
}

d = new Date;
document.getElementById("main_date").innerHTML = d.toDateString();

function Blink() {
    var x = document.getElementsByClassName("notif");
    if (x.length > 0) {
        x = x[0];
        if (x.style.backgroundImage == "linear-gradient(red, darkred)") {
            x.style.backgroundImage = "linear-gradient(gray, dimgray)";
        } else {
            x.style.backgroundImage = "linear-gradient(red, darkred)";
        }
    }
}

var blinky = setInterval(Blink, 500)
























