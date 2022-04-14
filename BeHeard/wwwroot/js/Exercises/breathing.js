var timer = null;

document.getElementById("startButton").onclick = function () { start_timer() };

function start_timer() {
    timer = 14;

    function timeDetails() {
        if (timer > 10) {
            document.getElementById("timer").innerHTML = "Breathe In";
        }
        else if (timer <= 10 && timer > 0) {
            document.getElementById("timer").innerHTML = "Breathe Out";
        }
        else {
            clearInterval(downloadTimer);
            document.getElementById("timer").innerHTML = "Relax";
        }
        document.getElementById("time_remaining").innerHTML = timer;
        timer--;
    }

    var downloadTimer = setInterval(timeDetails, 1000);
}