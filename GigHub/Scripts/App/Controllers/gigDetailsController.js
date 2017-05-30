var GigDetailsController = function (followingService) {
    var button;

    var init = function() {
        $(".js-toggle-follow").click(toggleFollow);
    };

    var toggleFollow = function(e) {
        button = $(e.target);

        var followerId = button.attr("data-user-id");

        if (button.hasClass("btn-default"))            
            followingService.createFollowing(followerId, done, fail);
        else
            followingService.deleteFollowing(followerId, done, fail);        
    };

    var done = function() {
        var text = (button.text() == "Following") ? "Following?" : "Following";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var fail = function() {
        alert("Something failed");
    };

    return {
         init: init
    }

}(FollowingService);