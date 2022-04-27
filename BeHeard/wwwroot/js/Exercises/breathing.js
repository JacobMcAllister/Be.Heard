var timer = 14;
var timer_sec = 14000;
var breatheIn = 7;
var breatheIn_sec = 7000;
var breatheOut_sec = 7000;
var breatheOut = 7;
var difficulty = null;
var diff_val = null;
var downloadTimer = null;
var container = document.getElementById("breathing_anim");
var text = document.getElementById("text");

// DB Fields
var SentenceSet = -1;
var Decibel = null;
var Syllable = null;
var Difficulty = null;
var Exercise = null;
var Category = null;

function UpdateDB() {

    $.ajax({
        url: "UpdateDBwResults",
        type: "POST",
        data: {
            Decibel: "NONE",
            viewSyllable: "NONE",
            viewDifficulty: diff_value,
            viewExercise: "Breathing",
            viewCategory: "NONE",
            SentenceSet: SentenceSet
        }
    })
}

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
    let subTimer = timer;
    let subcount_Out = breatheOut;

    function timeDetails() {
        if (subTimer > subcount_Out) {
            document.getElementById("timer").innerHTML = "Breathe In";
        }
        else if (subTimer <= subcount_Out && subTimer > 0) {
            document.getElementById("timer").innerHTML = "Breathe Out";
        }
        else {
            UpdateDB();
            clearInterval(downloadTimer);
            document.getElementById("time_remaining").innerHTML = '-';
            document.getElementById("timer").innerHTML = "Relax";
        }
        document.getElementById("time_remaining").innerHTML = subTimer;
        subTimer--;
    }

    var downloadTimer = setInterval(timeDetails, 1000);
}

function start_timer() {
    /*
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
    */
    clock();
}

function diff_alert(case_num) {
    switch (case_num) {
        case 1:
            defineTime(breatheIn, breatheOut);
            document.getElementById("diff_alert").innerHTML = "In for 7 seconds.  Out for 7 seconds.  Focus on strong core muscles.";
            break;
        case 2:
            defineTime(breatheIn, breatheOut);
            document.getElementById("diff_alert").innerHTML = "In for 9 seconds.  Out for 9 seconds.  Steady inhale and exhale are key.";
            break;
        case 3:
            defineTime(breatheIn, breatheOut);
            document.getElementById("diff_alert").innerHTML = "In for 11 seconds.  Out for 11 seconds.  Remember: on inhale, really fill your whole chest cavity and on exhale, slow and steady!.";
            break;
        case 4:
            defineTime(breatheIn, breatheOut);
            document.getElementById("diff_alert").innerHTML = "In for 15 seconds.  Out for 15 seconds.  This mode is purely a goal, do your best but if you feel lightheaded or dizzy, take a break!";
            break;
    }
}

function difficulty_dropdown() {
    difficulty = document.getElementById("diff_option");
    diff_value = difficulty.value;

    function alter_difficulty(value) {
        switch (true) {
            case (value == 'Easy'):
                timer = 14;
                breatheIn = 7;
                breatheOut = 7;
                diff_alert(1);
                break;
            case (value == 'Medium'):
                timer = 18;
                breatheIn = 9;
                breatheOut = 9;
                diff_alert(2);
                break;
            case (value == 'Hard'):
                timer = 22;
                breatheIn = 11;
                breatheOut = 11;
                diff_alert(3);
                break;
            case (value == 'Extreme'):
                timer = 30;
                breatheIn = 15;
                breatheOut = 15;
                diff_alert(4);
                break;
        }
    }

    alter_difficulty(diff_value);
}

breathe_animation();

function breathe_animation() {
    text.innerHTML = "Breathe In";
    container.className = 'container grow';

    setTimeout(() => {
        text.innerHTML = "Breathe Out";
        container.className = 'container shrink';

    }, breatheIn_sec)
}

setInterval(breathe_animation, timer_sec)