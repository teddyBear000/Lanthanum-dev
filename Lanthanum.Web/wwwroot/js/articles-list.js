search = document.querySelector(".search");
search_img = document.querySelector(".search-img");

search_img.onclick = function () {
        search.classList.toggle('active');
    }

var box = document.getElementById('.dropdown');
document.querySelectorAll("[data-link]").forEach(el => {
    el.addEventListener("click", () => window.location.href = "hello");
});

function deleteItem(form) {
$(form).closest("div.article-preview").remove();
}

function publishItem(form) {
    $(form).closest("div.article-preview").find("p.d-block").text("Published");
    $(form).closest("div.article-preview").find("img.circle").attr("src", "/images/green_circle.png");
}


function unpublishItem(form) {
    $(form).closest("div.article-preview").find("p.d-block").text("Unpublished");
    $(form).closest("div.article-preview").find("img.circle").attr("src", "/images/yellow_circle.png");
}
