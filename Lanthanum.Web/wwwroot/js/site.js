// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// menu switcher

function switchChannel(el) {
    // find all the elements in your channel list and loop over them
    Array.prototype.slice.call(document.querySelectorAll('ul[data-tag="channelList"] li')).forEach(function (element) {
        // remove the selected class
        element.classList.remove('selected');
    });
    // add the selected class to the element that was clicked
    el.classList.add('selected');
}

function switchMainArticle(el) {

    // find all the elements in your channel list and loop over them
    Array.prototype.slice.call(document.querySelectorAll('ul[data-tag="articleNumberList"] li')).forEach(function (element) {
        // remove the selected class
        element.classList.remove('activePageNumber');
    });
    // add the selected class to the element that was clicked
    el.classList.add('activePageNumber');

    var index = Number(el.textContent) - 1;

    $('.main-carousel').carousel(index);
    $('.additional-carousel').carousel(index);
    /*document.getElementById("dateCardPosition").textContent = index;
    document.getElementById("headerCardPosition").textContent = index;
    document.getElementById("articlePartTextCardPosition").textContent = index;
    document.getElementById("photoKindOfSportText").textContent = index;*/
}
