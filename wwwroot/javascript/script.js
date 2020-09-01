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


    //Display/Hide password 
    $("#showPassword").change(function () {
        if ($("#showPassword").is(":checked")) {
            $("#password").attr("type", "text");
        }
        else {
            $("#password").attr("type", "password");
        }
    });
 
});


