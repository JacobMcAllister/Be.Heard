var timer = 14;
var breatheIn = 4;
var breatheOut = 10;
var diff_level = 1;
var difficulty = null;
var diff_val = null;
var downloadTimer = null;

document.getElementById("startButton").onclick = function () { start_timer() };

function defineTime(menu_in, menu_out) {
    document.getElementById("breathin").innerHTML = menu_in;
    document.getElementById("breathout").innerHTML = menu_out;
}

function defineTimeonload() {
    document.getElementById("breathin").innerHTML = breatheIn;
    document.getElementById("breathout").innerHTML = breatheOut;
    document.getElementById("timer").innerHTML = "Relax";
    document.getElementById("time_remaining").innerHTML = '-';
}

function clock() {
    function timeDetails() {
        if (timer > breatheOut) {
            document.getElementById("timer").innerHTML = "Breathe In";
        }
        else if (timer <= breatheOut && timer > 0) {
            document.getElementById("timer").innerHTML = "Breathe Out";
        }
        else {
            clearInterval(downloadTimer);
            document.getElementById("time_remaining").innerHTML = '-';
            document.getElementById("timer").innerHTML = "Relax";
        }
        document.getElementById("time_remaining").innerHTML = timer;
        timer--;
    }

    var downloadTimer = setInterval(timeDetails, 1000);
}

function start_timer() {
    switch (diff_level) {
        case 1:
            clock();
            break;
        case 2:
            clock();
            break;
        case 3:
            clock();
            break;
        case 4:
            clock();
            break;
    }
}

function diff_alert(case_num) {
    switch (case_num) {
        case 1:
            defineTime(breatheIn, breatheOut);
            document.getElementById("diff_alert").innerHTML = "In for 4 seconds.  Out for 10 seconds.  Focus on strong core muscles.";
            break;
        case 2:
            defineTime(breatheIn, breatheOut);
            document.getElementById("diff_alert").innerHTML = "In for 6 seconds.  Out for 12 seconds.  Steady inhale and exhale are key.";
            break;
        case 3:
            defineTime(breatheIn, breatheOut);
            document.getElementById("diff_alert").innerHTML = "In for 7 seconds.  Out for 15 seconds.  Remember: on inhale, really fill your whole chest cavity and on exhale, slow and steady!.";
            break;
        case 4:
            defineTime(breatheIn, breatheOut);
            document.getElementById("diff_alert").innerHTML = "In for 10 seconds.  Out for 30 seconds.  This mode is purely a goal, do your best but if you feel lightheaded or dizzy, take a break!";
            break;
    }
}

function difficulty_dropdown() {
    difficulty = document.getElementById("diff_option");
    diff_value = difficulty.value;

    function alter_difficulty(value) {
        switch (true) {
            case (value == 'easy'):
                diff_level = 1;
                timer = 14;
                breatheIn = 4;
                breatheOut = 10;
                diff_alert(1);
                break;
            case (value == 'medium'):
                diff_level = 2;
                timer = 18;
                breatheIn = 6;
                breatheOut = 12;
                diff_alert(2);
                break;
            case (value == 'hard'):
                diff_level = 3;
                timer = 22;
                breatheIn = 7;
                breatheOut = 15;
                diff_alert(3);
                break;
            case (value == 'impossible'):
                diff_level = 4;
                timer = 40;
                breatheIn = 10;
                breatheOut = 30;
                diff_alert(4);
                break;
        }
    }

    alter_difficulty(diff_value);
}
