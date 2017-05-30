var FollowingService = function() {

    var createFollowing = function(followerId, done, fail) {
        $.post("/api/following", { followerId: followerId })
            .done(done)
            .fail(fail);
    }

    var deleteFollowing = function(followerId, done, fail) {
        $.ajax({
                url: '/api/following/' + followerId,
                method: 'DELETE'
            })
            .done(done)
            .fail(fail);
    }

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}();