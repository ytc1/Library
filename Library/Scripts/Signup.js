$(function () {
    $("#lastName-text").on("blur", function () {
        $.post("/home/checkuser", { firstName: $("#username-text").text() , lastName: $().text() }, function (valid) {
            if (!valid) {
                $("#nonvalid-user").text("Sorry not a valid user name");
            }
        })
    })
});