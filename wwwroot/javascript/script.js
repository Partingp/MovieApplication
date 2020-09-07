$(document).ready(function () {

    //Hide buttons when a button is selected
    $("#filtersButton").click(function () {
        $("#login").collapse("hide");
        $("#register").collapse("hide");
    });

    $("#loginButton").click(function () {
        $("#filters").collapse("hide");
        $("#register").collapse("hide");
    });

    $("#registerButton").click(function () {
        $("#filters").collapse("hide");
        $("#login").collapse("hide");
    });

    $("#movieInfo").click(function () {
        $("#movieInfo").hide().removeClass("col-sm-3");
        $("#movieBrowse").removeClass("col-sm-9").addClass("col-sm-12");
        $(".border").removeClass("border-danger");
    });
 
    var posters = document.querySelectorAll("#posters");
    var displayedPosterInfo = null;
    for (var i = 0; i < posters.length; i++) {
        posters[i].addEventListener("click", function () {
            if ($("#movieInfo").is(":visible")) {
                $(displayedPosterInfo).toggleClass("border-danger")
            }
            $("#movieInfo").show().addClass("col-sm-3");
            $("#movieBrowse").removeClass("col-sm-12").addClass("col-sm-9");
            $(this).toggleClass("border-danger");
            displayedPosterInfo = this;
            displayMovieInfo(this.alt);
        });
    }

    function displayMovieInfo(title) {
        $.ajax({
            url: 'movie/' + title,
            data: { title: title }
        }).done(function (partialViewResult) {
            $("#movieInfo").html(partialViewResult);
        });
    }

    //Display/Hide password 
    $("#loginShowPassword").click(function () {
        togglePasswordDisplay("#loginShowPassword", "#loginPassword");
    });

    $("#registerShowPassword").click(function () {
        togglePasswordDisplay("#registerShowPassword", "#registerPassword");
    });

    function togglePasswordDisplay(selector, passwordId) {
        $(selector).change(function () {
            if ($(selector).is(":checked")) {
                $(passwordId).attr("type", "text");
            }
            else {
                $(passwordId).attr("type", "password");
            }
        });
    }

 
});


