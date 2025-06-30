jQuery(window).on("load", function () {
    "use strict";
    ventic.load(),
        setTimeout(function () {
            ventic.handleMenuPosition();
        }, 3000);
});

var ventic = function () {
    "use strict";
    return {
        load: function () {
            $("#preloader").fadeOut(3000), // Fades out the preloader after 1.5 seconds
                setTimeout(function () {
                    $("#main-wrapper").addClass("show"); // Adds 'show' class to main wrapper after 1.5 seconds
                }, 3000);
        }
    }
}();
document.addEventListener("DOMContentLoaded", function () {
    const menuToggle = document.querySelector(".menu-toggle");
    const sideMenu = document.querySelector(".side-menu");

    menuToggle.addEventListener("click", function () {
        sideMenu.classList.toggle("open");
    });
});
function myFunction() {
    document.getElementsByClassName("header")[0].classList.toggle("responsive");
}
function togglePasswordVisibility() {
    var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
    var eyeIcon = document.getElementById('eyeIcon');

    if (passwordField.type === "password") {
        passwordField.type = "text";
        eyeIcon.classList.remove("fa-eye-slash");
        eyeIcon.classList.add("fa-eye");
    } else {
        passwordField.type = "password";
        eyeIcon.classList.remove("fa-eye");
        eyeIcon.classList.add("fa-eye-slash");
    }
}