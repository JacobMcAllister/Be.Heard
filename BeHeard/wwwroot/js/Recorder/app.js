//LOGGING: Disabled


//webkitURL is deprecated but nevertheless
URL = window.URL || window.webkitURL;
var gumStream;
//stream from getUserMedia() 
var rec;
//Recorder.js object 
var input;
//MediaStreamAudioSourceNode we'll be recording 
// shim for AudioContext when it's not avb. 
var AudioContext = window.AudioContext || window.webkitAudioContext;
var audioContext = new AudioContext;
//new audio context to help us record 
var recordButton = document.getElementById("recordButton");
var stopButton = document.getElementById("stopButton");
var pauseButton = document.getElementById("pauseButton");
//add events to those 3 buttons 
recordButton.addEventListener("click", startRecording);
stopButton.addEventListener("click", stopRecording);
pauseButton.addEventListener("click", pauseRecording);



// We're using the standard promise based getUserMedia()
function startRecording() {
    //console.log("recordButton clicked");
    var constraints = {
        audio: true,
        video: false
    }
    /* Disable the record button until we get a success or fail from getUserMedia() */

    recordButton.disabled = true;
    stopButton.disabled = false;
    pauseButton.disabled = false
    document.getElementById("micGif").style.display = "block";
    navigator.mediaDevices.getUserMedia(constraints).then(function (stream) {
        //console.log("getUserMedia() success, stream created, initializing Recorder.js ...");
        /* assign to gumStream for later use */
        gumStream = stream;
        /* use the stream */
        input = audioContext.createMediaStreamSource(stream);
        /* Create the Recorder object and configure to record mono sound (1 channel) Recording 2 channels will double the file size */
        rec = new Recorder(input, {
            numChannels: 1
        })
        //start the recording process 
        rec.record()
        //console.log("Recording started");
    }).catch(function (err) {
        //enable the record button if getUserMedia() fails 
        recordButton.disabled = false;
        stopButton.disabled = true;
        pauseButton.disabled = true
    });
}
function pauseRecording() {
    //console.log("pauseButton clicked rec.recording=", rec.recording);
    if (rec.recording) {
        //pause 
        rec.stop();
        pauseButton.innerHTML = "Resume";
    } else {
        //resume 
        rec.record()
        pauseButton.innerHTML = "Pause";
    }
}
function stopRecording() {
    //console.log("stopButton clicked");
    //disable the stop button, enable the record too allow for new recordings 
    stopButton.disabled = true;
    recordButton.disabled = false;
    pauseButton.disabled = true;
    document.getElementById("micGif").style.display = "none";
    //reset button just in case the recording is stopped while paused 
    pauseButton.innerHTML = "Pause";
    //tell the recorder to stop the recording 
    rec.stop(); //stop microphone access 
    gumStream.getAudioTracks()[0].stop();
    //create the wav blob and pass it on to createDownloadLink 
    rec.exportWAV(createDownloadLink);
}
function createDownloadLink(blob) {
    var url = URL.createObjectURL(blob);
    var au = document.createElement('audio');
    var li = document.createElement('li');
    var link = document.createElement('a');
    //add controls to the <audio> element 
    au.controls = true;
    au.src = url;
    //link the a element to the blob 
    link.href = url;
    link.download = 'Download Recording';
    link.innerHTML = link.download;
    //add the new audio and a elements to the li element 
    li.appendChild(au);
    li.appendChild(link);
    //add the li element to the ordered list 
    var reader = new FileReader();

    // The magic always begins after the Blob is successfully loaded
    reader.onload = function () {
        // Since it contains the Data URI, we should remove the prefix and keep only Base64 string
        var b64 = reader.result.replace(/^data:.+;base64,/, '');
        console.log(b64); //-> "V2VsY29tZSB0byA8Yj5iYXNlNjQuZ3VydTwvYj4h"

        // Decode the Base64 string and show result just to make sure that everything is OK
        // var html = atob(b64);
        // console.log(html);

        $.ajax({
            type: 'POST',
            url: 'https://localhost:44333/api/Speech/recognizer',
            contentType: 'application/json',
            data: JSON.stringify(b64),
            // dataType: 'json',
            success: function(response) {
                console.log(response);
                let responseJSON = JSON.parse(response);
                let modalBody = $('#staticBackdrop').find('.modal-body');
                modalBody.html(`<div class="mx-auto text-center"><h3>${responseJSON.text}</h3></div>`);
                modelCommunication.response_sentence = responseJSON.text;
                //Only add the recording if it was successful
                //recordingsList.appendChild(li);
                updateScore(li, modalBody);
            },
            error: function (req, status, error) {
                let modalBody = $('#staticBackdrop').find('.modal-body');
                modalBody.html(`<div class="mx-auto text-center">
                                    <h3>${"Error processing speech, most likely the server is down, or is not running locally."}</h3>
                                    <button class="btn-primary" onclick="contactAdmin()">Contact Admin</button>
                                    <img src='/images/image3.png'>
                                </div>`);
                //alert('error!');
                console.log("ML_MODEL_ERROR: response given " + error);
            }
        });
        console.log("processing..");
    };

    // Since everything is set up, let’s read the Blob and store the result as Data URI
    reader.readAsDataURL(blob);
}
