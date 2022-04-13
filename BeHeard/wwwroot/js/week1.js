var canvasContext = null;
var WIDTH = 500;
var HEIGHT = 50;
var drawID = null;
var arraySize = null;
var micAnalyser = null;
var average_decibel = null;
var micStreamSourceNode = null;
var isPaused = false;
var isRecording = false;
var fillVol = 0;
var timeleft = null;

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
            fillVol = volumeVal * WIDTH * 2;
            //console.log(volumeVal);
            //console.log(fillVol);
            if (fillVol > 175) {
                canvasContext.fillStyle = "#00ff00";
            }
            else {
                canvasContext.fillStyle = "#ff0000";
            }
            average_decibel += fillVol;

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

    timeleft = 10;
    fillVol = 0;
    average_decibel = 0;
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

                // Average of 10 sec
                average_decibel = average_decibel / 10;
                console.log(average_decibel);
                // Average in meter
                percent_decibel = (((average_decibel / 500) * 1.8) / 500) * 100;
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
                    alert("Wow!\n'Normal' voice volume is around 50-60 dba.\nYour volume was" + output + "dba !")
                } else {
                    alert("Great Job!\n'Normal' voice volume is around 50-60 dba.\nYour average volume was: " + output + " dba");
                }

            } else {
                document.getElementById("Timer").innerHTML = "Seconds Remaining: " + timeleft;
            }
            timeleft--;
        }
    }

    var downloadTimer = setInterval(timeDetails, 1000);
}