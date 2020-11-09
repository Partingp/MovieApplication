$(document).ready(function () {

    //Hide buttons when a button is selected
    $("#filtersButton").click(function () {
        $("#login").collapse("hide");
        $("#register").collapse("hide");
    });

    $("#loginButton").click(function () {
        togglePanels("#loginPanel");
    });

    $("#logoutButton").click(function () {
        $.ajax({
            url: 'Home/Logout',
        }).done(function () {
            location.reload(true);
        });
    });

    $("#registerButton").click(function () {
        togglePanels("#registerPanel");
    });

    $(document).on("click", '#movieInfo', function () {
        hideMovieInfo();
    });
 
    var displayedPosterInfo = null;
    $(document).on("click", ".poster", function () {
        togglePanels("#movieInfo");
        $(this).toggleClass("border-danger");
        displayedPosterInfo = this;
        getMovieInfo(this.alt);
    });

    $(document).on("click", '#cardLink', function () {
        $("#loginButton").click();
    });

    function togglePanels(id) {
        //$("#filters").collapse("hide");
        $(".poster").removeClass("border-danger")
        $(".panel").hide().removeClass("col-sm-3");
        var selector = id + ".panel";
        $(selector).show().addClass("col-sm-3");
        $("#movieBrowse").removeClass("col-sm-12").addClass("col-sm-9");
    }

    function hideMovieInfo() {
        $("#movieInfo").hide().removeClass("col-sm-3");
        $("#movieBrowse").removeClass("col-sm-9").addClass("col-sm-12");
        $(".border").removeClass("border-danger");
    }

    function getMovieInfo(title) {
        $.ajax({
            url: 'movie/' + title,
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
    const filters = document.querySelectorAll("#filters input");
    for (var i = 0; i < filters.length; i++) {
        filters[i].addEventListener("click", toggleFilter);
    }

    function toggleFilter(e) {

        var id = $(e.currentTarget).parent("label").attr("id");

        var selectedfilters = $("#filters .btn.active").map(function () {
            return this.id;
        })
            .get()
            .join();


        if (selectedfilters === '') {
            selectedfilters = id;
        }
        else {
            selectedfilters = selectedfilters.concat(","+id);
        }

        $.ajax({
            url: 'movie/filterMovies',
            data: { filters: selectedfilters }
        }).done(function (partialViewResult) {
            $("#movieBrowse").html(partialViewResult);
        });
    }

});


