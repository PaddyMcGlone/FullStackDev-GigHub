var gigsController = function (attendanceService) {
    var button;

    var init = function () {
        $(".js-toogle-attendance").click(toggleAttendance);
    }

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            AttendanceService.CreateAttendance(gigId, done, fail);
        else
            AttendanceService.DeleteAttendance(gigId, done, fail);
    }

    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    }

    var fail = function () {
        alert("Something failed");
    }

    return { Init: init }
}(AttendanceService);