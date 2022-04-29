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
var option = null;
var option_Choice = null;
var target_fillVol = 50;
var difficulty = null;
var diff_value = null;
var option_val = null;

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
            Decibel: volume,
            viewSyllable: "NONE",
            viewDifficulty: diff_value,
            viewExercise: "Rote",
            viewCategory: Category,
            SentenceSet: SentenceSet
        }
    })
}


const Cities = [
    "I live in Reno, Nevada.\nBy car, Reno is about 2 hours away from Sacramento.\nBy plane, Reno is about 1 hour away from Las Vegas.",
    "The White House is in Washington D.C.\nThe Queen of England lives in London.\nThe Chancellor of Germany resides in Berlin.",
    "The capital of Japan is Tokyo.\nI have always wanted to visit Kyoto.\nI have heard that Osaka is also a nice place to visit.",
    "San Francisco is home to the baseball Giants.\nNew York is home to the football Giants.\nThere is also a Giants baseball team in Tokyo.",
    "Kingman is a small town in Arizona.\nElko is an even smaller town in Nevada.\nNew Harmony is much smaller than both in Utah."
];
const Directions = [
    "To get to school, I must turn left down Main street, then take a right on Broadway.\nIf I have gotten to twenty third street, then I know I have gone too far.",
    "The nice woman said the bus stop is three blocks down seventh avenue and then turn right on C street.\nI hope I do not forget C street.",
    "If I am traveling down Glendale boulevard, eventually I will need to turn right at the movie theater.\nThe movie theater is on John Wayne street, that is funny.",
    "To get to the airport, I must get on the freeway at Keystone avenue.\nThen I must take the freeway past Wells boulevard.\nIf I miss the turn onto the three ninety five highway, I'll have to circle around.",
    "My house is on Barbara drive.\nTo get there from here, I need to go down Pennsylvania drive and turn left on Montana drive.\nBarbara is just on the right."
]
const PhoneNumbers = [
    "My phone number is one two three, four five six, seven eight nine zero.\nWhat a mouthful.\nBut remember, one two three, four five six, seven eight nine zero.",
    "The cab company phone number is easy to remember.\nThree three three, thirty three thirty three.\nThe owner must like the number three.",
    "Always remember, in an emergency, call nine one one.\nIt is only three digits, nine one one.\nYou cannot forget, nine one one.",
    "I wrote down Dave's number and I think it was four five five, six eight nine one.\nI called and he did not answer.\nI hope it is nine one and not one nine.",
    "Did you write down the number for the soccer coach?\nYes I did, seven seven five, eight three nine, four five one two.\nRemember, seven seven five for the area code."
]
const CommonRequests = [
    "Hey, could you please open the window?\nIt is getting really hot in the car.\nSure, no problem.",
    "Please pass the ketchup.\nI sure do love putting ketchup on my french fries.\nI heard that Pat Mahomes puts ketchup on everything.",
    "Will that be paper or plastic for your groceries?\nPaper please, I use the bags to cover my text books.",
    "Can you watch my dog while I am away?\nOh, I am sorry.\nI am also going out of town for vacation then.",
    "Excuse me!\nI just need to go past, my seat for the movie is on the other side of you.\nOh, sure thing."
]
const MealOrders = [
    "I will have the cheeseburger for dinner.\nFor my side, I will have a salad with caesar dressing.\nOh, and I would like a coca-cola to drink.",
    "For breakfast, I will have two eggs, over easy, bacon, and a side of sourdough toast.\nNice and simple breakfast for me.",
    "Nothing is better for lunch on a busy day than a sub sandwich.\n  I like my sandwish with salami, turkey, and ham.\nOh, and do not forget the mayo and mustard please.",
    "My favorite thing to order at a baseball game is a hot dog with relish, mustard, ketchup, and onions.\nThen, I go to the popcorn stand and order a large buttery popcorn!",
    "When I am at a fancy restaurant, I order the steak, medium rare, with vegetables and a baked potato.\nActually, my favorite thing is to order dessert, one giant hot fudge sundae."
]

const options = ["Cities", "Directions", "PhoneNumbers", "CommonRequests", "MealOrders"]

//  Random number between min and max
function random_Int(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}

//  Get random sentence
function get_Randomsentence(option_array) {
    let loop = true;

    sentence_Choice = random_Int(0, 5);
    current_sentence = sentence_Choice;
    if (current_sentence === last_sentence) {
        while (loop) {
            sentence_Choice = random_Int(0, 5);
            current_sentence = sentence_Choice;
            if (current_sentence != last_sentence) {
                loop = false
            }
        }
    }

    last_sentence = current_sentence;
    console.log(sentence_Choice);
    SentenceSet = sentence_Choice;
    document.getElementById("random_sentence").innerHTML = option_array[sentence_Choice];
}

function getSentence() {
    console.log("Clicked");
    option = document.getElementById("option");
    option_val = option.value;
    option_Choice = options[option_val];
    Category = option_Choice;

    switch (option_Choice) {
        case "Cities":
            get_Randomsentence(Cities);
            break;
        case "Directions":
            get_Randomsentence(Directions);
            break;
        case "PhoneNumbers":
            get_Randomsentence(PhoneNumbers);
            break;
        case "CommonRequests":
            get_Randomsentence(CommonRequests);
            break;
        case "MealOrders":
            get_Randomsentence(MealOrders);
            break;
    }
}

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
            fillVol = volumeVal * WIDTH * 3;
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
                        output = ">100";
                        break;
                }

                if (loud) {
                    UpdateDB(output);
                    alert("Wow!\n'Normal' voice volume is around 50-60 dba.\nYour volume was: " + output + " dba!")
                } else {
                    UpdateDB(output);
                    alert("Great Job!\n'Normal' voice volume is around 50-60 dba.\nYour average volume was: " + output + " dba.");
                }
            }

            timeDuration++;
        }
    }

    var downloadTimer = setInterval(timeDetails, 1000);
}

function diff_alert(case_num) {
    switch (case_num) {
        case 1:
            document.getElementById("diff_alert").innerHTML = "Go for a standard volume target, perhaps your normal inside voice volume level.";
            break;
        case 2:
            document.getElementById("diff_alert").innerHTML = "Increased volume target, perhaps to a friend outside or far away.  Develope vocal control!";
            break;
        case 3:
            document.getElementById("diff_alert").innerHTML = "Much higher volume target, focus on volume control and clarity.  Do your best!";
            break;
        case 4:
            document.getElementById("diff_alert").innerHTML = "Really try to max out the volume bar.  Utilize the pause and resume buttons.  Give it all you've got!";
            break;
    }
}

function difficulty_dropdown() {
    difficulty = document.getElementById("diff_option");
    diff_value = difficulty.value;

    function alter_difficulty(value) {
        switch (true) {
            case (value == 'Easy'):
                target_fillVol = 50
                diff_alert(1);
                break;
            case (value == 'Medium'):
                target_fillVol = 100;
                diff_alert(2);
                break;
            case (value == 'Hard'):
                target_fillVol = 200;
                diff_alert(3);
                break;
            case (value == 'Extreme'):
                target_fillVol = WIDTH;
                diff_alert(4);
                break;
        }
    }

    alter_difficulty(diff_value);
}