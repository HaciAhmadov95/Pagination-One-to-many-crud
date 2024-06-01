
$(document).on("click", ".show-more button", function () {

    let skip = parseInt($(".blogs-area").children().length);
    let blogsCount = parseInt($(".blogs-area").attr("data-count"));
    let parentElem = $(".blogs-area");
    let parentElemContent = $(".blogs-area").html();

    $.ajax({
        url: `blog/showmore?skip=${skip}`,
        type: "GET",
        success: function (response) {
            parentElemContent += response;
            $(parentElem).html(parentElemContent);

            skip = parseInt($(".blogs-area").children().length);
            if (skip >= blogsCount) {
                $(".show-more button").addClass("d-none")
            }
        },
    });
})
});