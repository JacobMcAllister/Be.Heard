var canvasContext = null;
var WIDTH = 300;
var HEIGHT = 50;
var drawID = null;
var arraySize = null;
var micAnalyser = null;
var total_decibel = null;
var micStreamSourceNode = null;
var isPaused = false;
var isRecording = false;
var fillVol = 0;
var timeleft = 10;
var average_over_time = null;
var average_over_meter = null;
var counter = 0;
var difficulty = null;
var diff_value = null;
var scaling_factor = 2.2;
var target_fillVol = 100;
var total_time = 0;
var syllable1 = null;
var syllablechoice = null;

// DB Fields
var SentenceSet = -1;
var Decibel = null;
var Syllable = null;
var Difficulty = null;
var Exercise = null;
var Category = null;

function UpdateDB(volume) {

    $.ajax({
        url: "UpdateDBwResults",
        type: "POST",
        data: {
            /*
            Decibel: volume,
            viewSyllable: syllablechoice,
            viewDifficulty: diff_value,
            viewExercise: "VolumeChasing",
            viewCategory: "NONE",
            SentenceSet: SentenceSet
            */
        }
    })
}


// Grab Syllable
syllable1 = document.getElementById("dropdown");
syllablechoice = syllable1.value;

// Grab our canvas
canvasContext = document.getElementById("meter").getContext("2d");

// fork getUserMedia for multiple browser versions, for those
// that need prefixes
navigator.getUserMedia = (navigator.getUserMedia ||
    navigator.webkitGetUserMedia ||
    navigator.mozGetUserMedia ||
    navigator.msGetUserMedia);

// Get user mic
// const inputMicStream = navigator.mediaDevices.getUserMedia({ audio: true, video: false });

// Audio  context for processing sound
var audioContext = new AudioContext();

if (navigator.mediaDevices) {
    console.log('getUserMedia supported.');
    navigator.getUserMedia(
        // constraints - only audio needed for this app
        {
            audio: true
        },
        // Success callback
        function (stream) {
            // Create mic stream node
            micStreamSourceNode = audioContext.createMediaStreamSource(stream);
            // Create audio analyser
            micAnalyser = audioContext.createAnalyser();
            // Connect
            //micStreamSourceNode.connect(micAnalyser);

            //animateVoice();

        },
        // Error callback
        function (err) {
            console.log('The following gUM error occured: ' + err);
        }
    );
} else {
    console.log('getUserMedia not supported on your browser!');
}

function animateVoice() {

    // Create array of mic input data
    const dataArray = new Float32Array(micAnalyser.fftSize);

    // Clear canvas
    canvasContext.clearRect(0, 0, WIDTH, HEIGHT);

    function drawVolume() {
        if (isRecording) {

            // Clear canvas
            canvasContext.clearRect(0, 0, WIDTH, HEIGHT);

            // Fill array
            micAnalyser.getFloatTimeDomainData(dataArray);

            let sumSquares = 0.0;
            for (const amplitude of dataArray) { sumSquares += amplitude * amplitude; }
            volumeVal = Math.sqrt(sumSquares / dataArray.length);
            fillVol = volumeVal * WIDTH * scaling_factor;
            //console.log(volumeVal);
            //console.log(fillVol);
            if (fillVol > target_fillVol) {
                canvasContext.fillStyle = "#00ff00";
            }
            else {
                canvasContext.fillStyle = "#ff0000";
            }
            total_decibel += fillVol;
            counter += 1;

            canvasContext.fillRect(0, 0, fillVol, HEIGHT);

            drawID = requestAnimationFrame(drawVolume);
        }
    }

    drawVolume();
}

function Pause() {
    isPaused = true;
    isRecording = false;
    if (audioContext.state === 'running') {
        audioContext.suspend();
    }
}

function Resume() {
    isPaused = false;
    isRecording = true;
    if (audioContext.state === 'suspended') {
        audioContext.resume();
    }
}

/*
document.getElementById("pauseButton").onclick = function () { pause_Play() };
function pause_Play() {
    if (audioContext.state === 'running') {
        audioContext.suspend().then(function () {
            document.getElementById("pauseButton").innerHTML = 'Resume';
        });
    } else if (audioContext.state === 'suspended') {
        audioContext.resume().then(function () {
            document.getElementById("pauseButton").innerHTML = 'Pause';
        });
    }
}
*/

document.getElementById("startButton").onclick = function () { start_timer() };

function start_timer() {

    // Connect
    micStreamSourceNode.connect(micAnalyser);
    if (audioContext.state === 'suspended') {
        audioContext.resume();
    }

    total_time = timeleft;
    fillVol = 0;
    total_decibel = 0;
    isRecording = true;

    //animateVoice();

    function timeDetails() {
        animateVoice();

        if (!isPaused) {
            if (timeleft < 0) {
                micStreamSourceNode.disconnect(micAnalyser);
                isRecording = false;

                // Clear canvas
                canvasContext.clearRect(0, 0, WIDTH, HEIGHT);

                clearInterval(downloadTimer);
                document.getElementById("Timer").innerHTML = "Finished";

                // Average over duration of seconds
                average_over_time = total_decibel / total_time;

                // Average per sec of meter
                average_over_meter = average_over_time / WIDTH;

                //console.log("total hits = " + counter);
                //console.log("total = " + total_decibel);
                //console.log("average / 10 = " + average_over_time);

                // Percent of meter
                percent_decibel = (average_over_meter / WIDTH) * 100;
                let output;
                let loud = false;

                switch (true) {
                    case (percent_decibel > 0 && percent_decibel < 20.0):
                        output = "~20";
                        break;
                    case (percent_decibel > 20.1 && percent_decibel < 25.0):
                        output = "~30";
                        break;
                    case (percent_decibel > 25.1 && percent_decibel < 35.0):
                        output = "~40";
                        break;
                    case (percent_decibel > 35.1 && percent_decibel < 40.0):
                        output = "~50";
                        break;
                    case (percent_decibel > 40.1 && percent_decibel < 45.0):
                        output = "~60";
                        break;
                    case (percent_decibel > 45.1 && percent_decibel < 50.0):
                        output = "~70";
                        break;
                    case (percent_decibel > 50.1 && percent_decibel < 55.0):
                        output = "~80";
                        break;
                    case (percent_decibel > 55.1 && percent_decibel < 60.0):
                        output = "~90";
                        break;
                    case (percent_decibel > 60.1 && percent_decibel < 65.0):
                        output = "~100";
                        break;
                    case (percent_decibel > 65.1):
                        loud = true;
                        output = " greater then 100";
                        break;
                }

                if (loud) {
                    updateDB(loud);
                    alert("Wow!\n'Normal' voice volume is around 50-60 dba.\nYour volume was" + output + "dba!")
                } else {
                    UpdateDB(loud);
                    alert("Great Job!\n'Normal' voice volume is around 50-60 dba.\nYour average volume was: " + output + " dba.");
                }

            } else {
                document.getElementById("Timer").innerHTML = "Seconds Remaining: " + timeleft;
            }
            timeleft--;
        }
    }

    var downloadTimer = setInterval(timeDetails, 1000);
}

function diff_alert(case_num) {
    switch (case_num) {
        case 1:
            document.getElementById("diff_alert").innerHTML = "Standard time, standard volume target.  Aim for volume!";
            break;
        case 2:
            document.getElementById("diff_alert").innerHTML = "Increased time, increased volume target.  Develope vocal control!";
            break;
        case 3:
            document.getElementById("diff_alert").innerHTML = "Time increase to 15 seconds, volume target greatly increased.  Utilize the pause and resume buttons!  Do your best!";
            break;
        case 4:
            document.getElementById("diff_alert").innerHTML = "Time increased to 20 seconds, max out the volume bar.  Utilize the pause and resume buttons.  Give it all you've got!";
            break;
    }
}

function difficulty_dropdown() {
    difficulty = document.getElementById("diff_option");
    diff_value = difficulty.value;

    function alter_difficulty(value) {
        switch (true) {
            case (value == 'Easy'):
                target_fillVol = 100;
                timeleft = 10;
                document.getElementById("diff_span").innerHTML = timeleft;
                diff_alert(1);
                break;
            case (value == 'Medium'):
                target_fillVol = 125;
                timeleft = 12;
                document.getElementById("diff_span").innerHTML = timeleft;
                diff_alert(2);
                break;
            case (value == 'Hard'):
                target_fillVol = 200;
                timeleft = 15;
                document.getElementById("diff_span").innerHTML = timeleft;
                diff_alert(3);
                break;
            case (value == 'Impossible'):
                target_fillVol = WIDTH;
                timeleft = 20;
                document.getElementById("diff_span").innerHTML = timeleft;
                diff_alert(4);
                break;
        }
    }

    alter_difficulty(diff_value);
}
