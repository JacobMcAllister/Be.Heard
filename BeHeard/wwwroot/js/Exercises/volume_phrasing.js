// List #30, #31, #32 from Harvard sentences
// cs.columbia.edu/~hgs/audio.harvard.html

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
var userStopped = false;
var fillVol = 0;
var timeDuration= null;
var average_over_time = null;
var average_over_meter = null;
var counter = 0;
var sentence_Choice = null;
var last_sentence = 15;
var current_sentence = null;

const sentences = [
    "The mute muffled the high tones of the horn. Slide the box into the empty space. The store walls were lined with colored frocks.",
    "The gold ring fits only a pierced ear. The plant grew large and green in the window. The peace league met to discuss their plans.",
    "The old pan was covered with hard fudge. The beam dropped down on the workmen's head. The rise to fame of a person takes luck.",
    "Watch the log float in the wide river. Pink clouds floated with the breeze. Paper is scarce so write with much care.",
    "The node on the stalk of wheat grew daily. She danced like a swan, tall and graceful. The quick fox jumped on the sleeping cat.",
    "Write fast, if you want to finish early. The tube was blown and the tire flat and useless. The nozzle of the fire hose was bright brass.",
    "His shirt was clean but one button was gone. Let's all join as we sing the last chorus. Time brings us many changes.",
    "The barrel of beer was a brew of malt and hops. The last switch cannot be turned off. The purple tie was ten years old.",
    "Tin cans are absent from store shelves. The fight will end in just six minutes. Men think and plan and sometimes act.",
    "The heap of fallen leaves was set on fire. It is late morning on the old wall clock. Scrw the round cap on as tight as needed."
];

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

            // Increase factor for phrased speech
            fillVol = volumeVal * WIDTH * 3.2;
            //console.log(volumeVal);
            //console.log(fillVol);
            if (fillVol > 50) {
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

function Stop() {
    userStopped = true;
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

function start_timer() {

    // Connect
    micStreamSourceNode.connect(micAnalyser);
    if (audioContext.state === 'suspended') {
        audioContext.resume();
    }

    timeDuration = 0;
    fillVol = 0;
    total_decibel = 0;
    average_over_time = 0;
    average_over_meter = 0;
    userStopped = false;
    isRecording = true;

    //animateVoice();

    function timeDetails() {
        animateVoice();

        if (!isPaused) {
            if (userStopped || timeDuration == 30) {
                micStreamSourceNode.disconnect(micAnalyser);
                isRecording = false;

                // Clear canvas
                canvasContext.clearRect(0, 0, WIDTH, HEIGHT);

                clearInterval(downloadTimer);

                // Average of time duration in sec
                average_over_time = total_decibel / timeDuration;

                // Average per sec of meter
                average_over_meter = average_over_time / WIDTH;

                // Percent of meter
                percent_decibel = (average_over_meter / WIDTH) * 100;

                //console.log("total time = " + timeDuration);
                //console.log("total = " + total_decibel);
                //console.log("average /" + timeDuration + " = " + average_over_time);
                //console.log("percent = " + percent_decibel);

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
                    alert("Wow!\n'Normal' voice volume is around 50-60 dba.\nYour volume was" + output + "dba!")
                } else {
                    alert("Great Job!\n'Normal' voice volume is around 50-60 dba.\nYour average volume was: " + output + " dba.");
                }
            }

            timeDuration++;
        }
    }

    var downloadTimer = setInterval(timeDetails, 1000);
}

//  Random number between 0 and 9
function random_Int( min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}

//  Get random sentence
function get_Randomsentence() {
    let loop = true;

    sentence_Choice = random_Int(0, 9);
    current_sentence = sentence_Choice;
    if (current_sentence === last_sentence) {
        while (loop) {
            sentence_Choice = random_Int(0, 9);
            current_sentence = sentence_Choice;
            if (current_sentence != last_sentence) {
                loop = false
            }
        }
    }

    last_sentence = current_sentence;
    console.log(sentence_Choice);
    document.getElementById("random_sentence").innerHTML = sentences[sentence_Choice];
}