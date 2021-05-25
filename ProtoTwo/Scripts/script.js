//I haven't really been serious with this external script file. I most use inline scripting for javascript :) .

//Initializing variables
var num = 0;
var bckg = ["bck1.jpg", "bck3.jpg", "bck4.jpg"];
var funny = "";
var loadernum = 1;
var numx = 3;

//For changing background images dynamically
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

//Temporary Login Form Validation. This function is now obsolete.
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

//Useless graphic I did during debug frustration.
function Funn() {
    num += 1;
    funny += "&#187";
    if (num == 4) {
        num = 0;
        funny = "";
    }
    document.getElementById("fun").innerHTML = funny;
}

//This is for blinking red notification lights. This is just preliminary. The notification system hasn't been built yet.
function Blink() {
    var x = document.getElementsByClassName("notif");
    if (x.length > 0) {
        x = x[0];
        if (x.style.backgroundImage == "linear-gradient(red, darkred)") {
            x.style.backgroundImage = "linear-gradient(white, whitesmoke)";
        } else {
            x.style.backgroundImage = "linear-gradient(red, darkred)";
        }
    }
}
var blinky = setInterval(Blink, 500)
























