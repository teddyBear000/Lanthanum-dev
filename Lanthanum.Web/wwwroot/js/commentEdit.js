var content = "";
function Edit(commentId) {
    const commentParagraph = document.getElementById("CommentContent_" + commentId);
    const editButton = document.getElementById("EditButton_" + commentId);
    const saveEditButton = document.getElementById("SaveEditButton_" + commentId);
    const discardEditButton = document.getElementById("DiscardEditButton_" + commentId);
    const deleteButton = document.getElementById("DeleteButton_" + commentId);
    commentParagraph.contentEditable = true;
    commentParagraph.style.backgroundColor = "#d5434326";
    commentParagraph.style.outline = "none";
    saveEditButton.style.display = "block";
    discardEditButton.style.display = "block";
    editButton.style.display = "none";
    content = commentParagraph.textContent;
    deleteButton.style.display = "none";
}
function SaveEdit(commentId) {
    const commentParagraph = document.getElementById("CommentContent_" + commentId);
    const editButton = document.getElementById("EditButton_" + commentId);
    const saveEditButton = document.getElementById("SaveEditButton_" + commentId);
    const discardEditButton = document.getElementById("DiscardEditButton_" + commentId);
    const deleteButton = document.getElementById("DeleteButton_" + commentId);
    commentParagraph.contentEditable = false;
    editButton.style.display = "block";
    deleteButton.style.display = "block";
    saveEditButton.style.display = "none";
    discardEditButton.style.display = "none";
    commentParagraph.style.backgroundColor = "#fff";
    document.getElementById("commentNewContent_" + commentId).value = commentParagraph.textContent;

}
function DiscardEdit(commentId) {
    const commentParagraph = document.getElementById("CommentContent_" + commentId);
    const editButton = document.getElementById("EditButton_" + commentId);
    const saveEditButton = document.getElementById("SaveEditButton_" + commentId);
    const discardEditButton = document.getElementById("DiscardEditButton_" + commentId);
    const deleteButton = document.getElementById("DeleteButton_" + commentId);
    commentParagraph.contentEditable = false;
    editButton.style.display = "block";
    saveEditButton.style.display = "none";
    deleteButton.style.display = "block";
    discardEditButton.style.display = "none";
    commentParagraph.textContent = content;
    content = "";
    commentParagraph.style.backgroundColor = "#fff";
}
function ShowMoreComments() {
    var comments = document.getElementsByClassName("clearfix");
    var ShowMoreButton = document.getElementById("show-more-button");
    for (var i = 0; i < comments.length; i++)
    {
        comments.item(i).style.display = "list-item";
    }
    ShowMoreButton.style.display = "none";

}