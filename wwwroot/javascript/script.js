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

    $("#logoutButton").click(function () {
        $.ajax({
            url: 'Home/Logout',
        }).done(function () {
            location.reload(true);
        });
    });

    $("#registerButton").click(function () {
        $("#filters").collapse("hide");
        $("#login").collapse("hide");
    });

    $(document).on("click", '#movieInfo', function () {
        hideMovieInfo();
    });
 
    var displayedPosterInfo = null;
    $(document).on("click", ".poster", function () {
        if ($("#movieInfo").is(":visible")) {
            $(".poster").removeClass("border-danger")
        }
        showMovieInfo(this);
    });

    $(document).on("click", '#cardLink', function () {
        $("#loginButton").click();
    });

    function showMovieInfo(poster) {
  
        $("#login").collapse("hide");
        $("#register").collapse("hide");
        $("#filters").collapse("hide");
        $("#movieInfo").show().addClass("col-sm-3");
        $("#movieBrowse").removeClass("col-sm-12").addClass("col-sm-9");
        $(poster).toggleClass("border-danger");
        displayedPosterInfo = this;
        getMovieInfo(poster.alt);
        
    }

    function hideMovieInfo() {
        $("#movieInfo").hide().removeClass("col-sm-3");
        $("#movieBrowse").removeClass("col-sm-9").addClass("col-sm-12");
        $(".border").removeClass("border-danger");
    }

    function getMovieInfo(title) {
        $.ajax({
            url: 'movie/' + title,
            data: { title: title }
        }).done(function (partialViewResult) {
            $("#movieInfo").html(partialViewResult);
        });
    }

    //Display/Hide password 
    $("#loginShowPassword").on("input",function () {
        togglePasswordDisplay("loginPassword");
    });

    $("#registerShowPassword").on("input",function () {
        togglePasswordDisplay("registerPassword");
    });

    function togglePasswordDisplay(selector) {
        var x = document.getElementById(selector);
        if (x.type === "password") {
            x.type = "text";
        } else {
            x.type = "password";
        }
    }

    //Filtering of movies
    //const filters = document.querySelectorAll("#filters input[type='checkbox']");
    const filters = document.querySelectorAll("#filters label");
    for (var i = 0; i < filters.length; i++) {
        filters[i].addEventListener("click", toggleFilter);
    }

    function toggleFilter(e) {
        $.ajax({
            url: 'movie/filterMovies',
            data: { filter: this.id }
        }).done(function (partialViewResult) {
            $("#movieBrowse").html(partialViewResult);
        });
    }

});


