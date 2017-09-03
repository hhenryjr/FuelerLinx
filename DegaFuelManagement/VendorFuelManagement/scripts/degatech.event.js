degatech.page.handlers.onSubmitFormClicked = function (evt) {
    evt.preventDefault();
    //validate data

    //submit confirmation message
    //send home 
    

    var targetFormId = $(this).attr("data-formId");
    var targetForm = $(targetFormId);
    var targetCommentElement = null;

    if (degatech.page.lastReplyLink) {

        targetCommentElement = $(degatech.page.lastReplyLink).closest(".comment-content");
    };

    var formGrab = degatech.page.grabDataInput(targetForm);

    degatech.page.showComments();
    degatech.page.addComment(formGrab, targetCommentElement);

    targetForm[0].reset();
    degatech.page.lastReplyLink = null;
    $("#myModal").modal("hide");
};


degatech.objects = [];

degatech.page.wireUpReplies = function (context, data) {

    var repliesFound = $(".reply", context);
    //  console.log("Found these many replies to wire up:" + repliesFound.length);
    // console.dir(repliesFound);
    console.log(repliesFound);


    degatech.objects.push(repliesFound);

    repliesFound.on("click", degatech.page.handlers.onReplyClicked);


}



degatech.page.subtmitComments = function () {
    $(".eventForm").slideDown();
}


degatech.page.goTo = function (jQueryObject) {

    var topOfComments = jQueryObject.offset().top;

    var animateOptions = {
        scrollTop: topOfComments
    };

    $('html, body').animate(animateOptions, 2000);
}
