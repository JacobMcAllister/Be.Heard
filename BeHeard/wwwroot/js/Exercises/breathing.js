var timer = 14;
var anim_diff = 1;
var breatheIn = 7;
var breatheIn_sec = 7500;
var breatheOut_sec = 7000;
var breatheOut = 7;
var difficulty = null;
var diff_val = null;
var downloadTimer = null;
var breathing_timer = null;
var container = document.getElementById("breathing_anim");
var text = document.getElementById("text");
var pointer_container = document.getElementById("rotating_ball");

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

document.getElementById("startButton").onclick = function () { clock(); breathe_animation(breatheIn_sec, anim_diff); };

function defineTime(menu_in, menu_out) {
    document.getElementById("breathin").innerHTML = menu_in;
    document.getElementById("breathout").innerHTML = menu_out;
}

function defineTimeonload() {
    text.innerHTML = " - ";
    document.getElementById("breathin").innerHTML = breatheIn;
    document.getElementById("breathout").innerHTML = breatheOut;
    document.getElementById("timer").innerHTML = "Relax";
    document.getElementById("time_remaining").innerHTML = " - ";
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
            //pointer_container.className = 'pointer-container stop';
            //pointer_container.removeClass('pointer-container ');
            text.innerHTML = "Relax";
            pointer_container.className = 'pointer-container';
            clearInterval(downloadTimer);
            clearInterval(breathing_timer);
            document.getElementById("time_remaining").innerHTML = '-';
            document.getElementById("timer").innerHTML = "Relax";
        }
        document.getElementById("time_remaining").innerHTML = subTimer;
        subTimer--;
    }

    var downloadTimer = setInterval(timeDetails, 1000);
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
                anim_diff = 1;
                breatheIn = 7;
                breatheIn_sec = 7500;
                breatheOut = 7;
                diff_alert(1);
                break;
            case (value == 'Medium'):
                timer = 18;
                anim_diff = 2;
                breatheIn = 9;
                breatheIn_sec = 9500;
                breatheOut = 9;
                diff_alert(2);
                break;
            case (value == 'Hard'):
                timer = 22;
                anim_diff = 3;
                breatheIn = 11;
                breatheIn_sec = 11500;
                breatheOut = 11;
                diff_alert(3);
                break;
            case (value == 'Extreme'):
                timer = 30;
                anim_diff = 4;
                breatheIn = 15;
                breatheIn_sec = 15500;
                breatheOut = 15;
                diff_alert(4);
                break;
        }
    }

    alter_difficulty(diff_value);
}

function breathe_animation(breathing, anim) {
    console.log(breathing);
    console.log(anim);

    text.innerHTML = "Breathe In";

    switch (anim) {
        case 1:
            pointer_container.className = 'pointer-container rotate7';
            container.className = 'container grow7';
            breathing_timer = setTimeout(() => {
                text.innerHTML = "Breathe Out";
                container.className = 'container shrink7';

            }, breathing)
            break;
        case 2:
            pointer_container.className = 'pointer-container rotate9';
            container.className = 'container grow9';
            breathing_timer = setTimeout(() => {
                text.innerHTML = "Breathe Out";
                container.className = 'container shrink9';

            }, breathing)
            break;
        case 3:
            pointer_container.className = 'pointer-container rotat11';
            container.className = 'container grow11';
            breathing_timer = setTimeout(() => {
                text.innerHTML = "Breathe Out";
                container.className = 'container shrink11';

            }, breathing)
            break;
        case 4:
            pointer_container.className = 'pointer-container rotate15';
            container.className = 'container grow15';
            breathing_timer = setTimeout(() => {
                text.innerHTML = "Breathe Out";
                container.className = 'container shrink15';

            }, breathing)
            break;
    }
}

//animation: rotate 14s linear forwards infinite;
/*
 *  #rotating_ball.stop {
        position:absolute;
        top: -30px;
        left: 140px;
        width: 20px;
        height: 180px;
        transform-origin: bottom center;
        animation: rotate 15s linear forwards 0;
    }
*/