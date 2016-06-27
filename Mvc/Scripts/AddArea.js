
document.addEventListener('DOMContentLoaded', function () {
    var createTag = function (tagText) {
        return $(`<div class="element"><span>${tagText}</span> <button class="delete-tag-button">X</button></div>`)
    };

    $('#tags-container').click(function(event) {
        event.preventDefault();
        if ($(event.target).hasClass("delete-tag-button")) {
            $(event.target).parent().fadeOut('300', function () {
                $(this).remove();
            });
        }
    });

    $('#add-tag-button').click(function (event) {
        event.preventDefault();
        var $tagNameElement = $('#tag-name');
        if ($tagNameElement.val()) {
            createTag($tagNameElement.val()).appendTo($('#tags-container'));
            $tagNameElement.val('');
        }
    });
});
