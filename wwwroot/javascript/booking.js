$(document).ready(function () {


    //#region seatSVGFunctionality

    var takenSeats;
    
    $.ajax({
        url: 'getTakenSeats'
    }).done(function (seats) {
        takenSeats = seats.split(',');
        disableBookedSeats();
    });

    //Each entry in groups is a row
    //SVG event preperation
    var groups = document.getElementsByClassName("cinemaRow");
    var letter = "A";
    for (var i = 0; i < groups.length; i++) {
        setSeatsId(groups.item(i), letter)
        incrementLetter(letter);
    }
    

    function setSeatsId(group, groupLetter) {
        var seats = group.getElementsByClassName("seat");
        var seatNumber = 1;
        Array.from(seats).map(seat => seat.setAttribute("id", letter + (seatNumber++)))
    }

    function incrementLetter() {
        letter = String.fromCharCode(letter.charCodeAt(0) + 1);
    }

    $("rect").each(function () {
        $(this).attr("fill","transparent");
    });

    function disableBookedSeats() {
        takenSeats.forEach(seat => $("#" + seat).addClass("unavailable").attr("tabindex",-1));
    }


    //Seat interaction
    var selectedSeats = new Set();

    $(".seat").on("click", function (e) {

        if (selectedSeats.has(e.delegateTarget.id)) {
            selectedSeats.delete(e.delegateTarget.id);
            $(e.delegateTarget).find("path").attr("fill","#1a1a1a");
        }
        else if (selectedSeats.size == 10) {
            return
        }
        else {
            selectedSeats.add(e.delegateTarget.id);
            $(e.delegateTarget).find("path").attr("fill", "red");
        }
        var selectedSeatString = [...selectedSeats].join(',');
        $("#selectedSeats").text(selectedSeatString);
        checkSeatCount();
    });

    //#endregion

    //#region seatsSubmission

    $("#submitSeats").on("click", function (e) {
        $.ajax({
            url: 'submitSeats',
            data: { seatsParam: $("#selectedSeats").text() }
        }).done(function (result) {
            //Popup modal with booking details
            $("#confirmation").modal('show');
        })
        .fail(function (result) {
             //Popup modal with error details e.g. seats taken, request timed out
        });
    });

    function checkSeatCount() {
        if (selectedSeats.size == 0) {
            $("#submitSeats").prop("disabled",true);
        }
        else {
            $("#submitSeats").prop("disabled", false);
        }
    }


    $('#confirmation').on('hidden.bs.modal', function (e) {
        location.href = "/";
    });
    //#endregion

});