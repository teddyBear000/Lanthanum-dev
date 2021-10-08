const dropArea = document.querySelector('.drag-area');
const dropAreaFile = document.querySelector('.drag-area-file');
const fileTypes = ['image/jpeg', 'image/png', 'image/jpg', 'image/tif'];
input = dropAreaFile.querySelector('#getImage');
image = "/images/background_image.jpg";

input.addEventListener(
    'change', function(){
        ShowFile(this.files[0]);
    }
);

dropArea.addEventListener(
    "dragover",
    (event) =>{
        event.preventDefault();
        dropArea.classList.add("active");
    }
);

dropArea.addEventListener(
    "dragleave",
    (event) =>{
        event.preventDefault();
        dropArea.classList.remove("active");
    }
);

dropArea.addEventListener(
    "drop",
    (event) =>{
        event.preventDefault();
        dropArea.classList.add("active");
        ShowFile(event.dataTransfer.files[0]);
    }
);

function ShowFile(file){
    if (fileTypes.includes(file.type)) {
        let fileReader = new FileReader();

        fileReader.onload = () => {
            dropArea.innerHTML = `<div class="hided-icon">
                                <div class="icon icon-form" style="margin-top: 40px;">
                                    <img src="/images/PhotoIco.png" height="80px">
                                </div>
                                <div class="icon-text">
                                    <header><span class="hightlight" onclick="AddElement()">+Add picture</span> or drop it rigth there</header>
                                    <p>You can add next formats: png. jpg. jpeg. tif</p>
                                </div>
                            </div>
                            <div class="view-buttons">
                            <div class="row" style="position: absolute; left: 92% ;top: 11%; float: left; margin-bottom: 10px;">
                                <img onclick="FilterF()" class="button-form" src="/images/filter-ph.png">
                            </div>
                            <div class="row" style="position: absolute; left: 92%; top: 19%; float: left; margin-bottom: 10px;">
                                <img onclick="CropF()" class="button-form" src="/images/crop-ph.png">
                            </div>
                            <div class="row" style="position: absolute; left: 92%; top: 27%; float: left; margin-bottom: 10px;">
                                <img onclick="DeleteF()" class="button-form" src="/images/delete-ph.png">
                            </div>
                            <div class="row" style="position: absolute; left: 92%; top: 35%; float: left; margin-bottom: 10px;">
                                <img onclick="ResizeF()" class="button-form" src="/images/resize-ph.png">
                            </div>
                            </div>
                            <div class="crop-image">
                            <img class="inserted-img" id="inserted" src="${fileReader.result}" height=459px>
                            </div>` ;

            image = fileReader.result;
        }

        fileReader.readAsDataURL(file);
        dropArea.classList.add("active");
    }
    else{
        alert("It is not an acceptable image format");
        dropArea.classList.remove("active");
    }
}

function AddElement() {
    input.click();
}

function f1() {
    document.getElementById("textarea1").style.fontWeight = "header1";
}

function f2() {
    document.getElementById("textarea1").style.fontWeight = "header2";
}

function f3() {
    document.getElementById("textarea1").style.fontWeight = "paragraph";;
}

var firstClickWeight = true;

function f4() {
    if (firstClickWeight) {
        document.getElementById("textarea1").style.fontWeight = "bold";
        firstClickWeight = false;
    }
    else {
        document.getElementById("textarea1").style.fontWeight = "normal";
        firstClickWeight = true;
    }
}

var firstClickStyle = true;

function f5() {
    if (firstClickStyle) {
        document.getElementById("textarea1").style.fontStyle = "italic";
        firstClickStyle = false;
    }
    else {
        document.getElementById("textarea1").style.fontStyle = "normal";
        firstClickStyle = true;
    }
}

function f6() {
    replaceSelectedText('underline');
}

function f7() {
    document.getElementById("textarea1").style.textAlign = "left";
}

function f8() {
    document.getElementById("textarea1").style.textAlign = "center";
}

function f9() {
    document.getElementById("textarea1").style.textAlign = "right";
}

var firstClickTransform = true;

function f10() {
    if (firstClickTransform) {
        firstClickTransform = false;
    }
    else {
        firstClickTransform = true;
    }
}

function f11() {
    replaceSelectedText('color');
}

function f12() {
    document.getElementById("PrevPhoto").src = image;
    var elementFill = document.getElementById("mainContainer");
    elementFill.style.display = "none";
    elementFill.style.zIndex = "-1";

    var Header = document.getElementById('rHeader').value;
    var Headline = document.getElementById('rHeadLine').value;
    var MainText = tinymce.get("textarea1").getContent();

    document.getElementById('Header').textContent = Header;
    document.getElementById('Headline').textContent = Headline;
    document.getElementById('contextChange').innerHTML = MainText;

    for (var i = 0; i < 6; i++) {
        document.getElementById(('Header'+i)).textContent = Header;
        document.getElementById(('Headline'+i)).textContent = Headline;
    }

    if (sliderBool) {
        document.getElementById('hided2').style.display = "block";
    }
    else {
        document.getElementById('hided2').style.display = "none";
        document.getElementById('hided2').style.zIndex = "-1";
    }

    var element = document.getElementById('hided');
    element.style.display = "block";
    element.style.zIndex = "100";
}

function f13() {
    var element = document.getElementById('hided');
    element.style.display = "none";
    element.style.zIndex = "-1";

    var elementFill = document.getElementById("mainContainer");
    elementFill.style.display = "block";
    elementFill.style.zIndex = "10";
}

var sliderBool = false;

function f14() {
    if (sliderBool) {
        sliderBool = false;
    }
    else {
        sliderBool = true;
    }
}

var tempX = 0;

function f16() {
    tempX -= 70;
    var element = document.getElementById('divider');
    element.style.transform = "translateX(" + tempX + "px)";
    element.style.transition = "0.5s";
}

var tempX = 0;
function f15() {
    tempX += 70;
    if (tempX >= 0) { tempX = 0;}
    var element = document.getElementById('divider');
    element.style.transform = "translateX(" + tempX + "px)";
    element.style.transition = "0.5s";
}

function f17() {
    var element = document.getElementsByClassName('tox-statusbar')[0];
    element.style.display = "none";
    element.style.zIndex = "-2";

    var elementTwo = document.getElementById('kind_hidden');
    elementTwo.value = document.getElementById('kind_hidden_text').textContent;
}

function ShowButtons() {
    document.querySelector('.view-buttons').style.zIndex = "7";

    document.querySelector('.inserted-img').style.filter += " blur(3px)";
}

function HideButtons() {
    document.querySelector('.view-buttons').style.zIndex = "-1";
    var element = document.querySelector('.inserted-img')
    element.style.filter = element.style.filter.replaceAll("blur(3px)", "")
}

function FilterF()
{
    HideFunc(1);
    HideFunc(2);
    HideFunc(3);

    var elementFill = document.getElementById("filter-container");
    elementFill.style.display = "block";
    elementFill.style.zIndex = "4";

    var elements = document.getElementsByClassName('filter-photo');

    for (var i = 0; i < elements.length; i++)
    {
        elements.item(i).src = image;

        if (i == 1) {
            elements.item(i).style.filter = "sepia(50%)";
        }
        else if (i == 2) {
            elements.item(i).style.filter = "sepia(1%) hue-rotate(190deg) saturate(90%)";
        }
        else if (i == 3) {
            elements.item(i).style.filter = "grayscale(100%)";
        }
    }
}

function FilterOne()
{
    var elements = document.getElementsByClassName('red-select');
    document.getElementById("getFilter").value = "none";;

    document.querySelector('.inserted-img').style.filter = "none";
    document.querySelector('.ImageContainer').style.filter = "none";

    for (var i = 0; i < elements.length; i++) {
        if (i == 0) {
            elements.item(i).style.color = "rgb(215,33,48)";
        }
        else {
            elements.item(i).style.color = '#212529';
        }
    }
}

function FilterTwo()
{
    var elements = document.getElementsByClassName('red-select');
    document.getElementById("getFilter").value = "sepia(50%)";

    document.querySelector('.inserted-img').style.filter = "sepia(50%)";
    document.querySelector('.ImageContainer').style.filter = "sepia(50%)";

    for (var i = 0; i < elements.length; i++) {
        if (i == 1) {
            elements.item(i).style.color = "rgb(215,33,48)";
        }
        else {
            elements.item(i).style.color = '#212529';
        }
    }
}

function FilterThree()
{
    var elements = document.getElementsByClassName('red-select');
    document.getElementById("getFilter").value = "sepia(1%) hue-rotate(190deg) saturate(90%)";

    document.querySelector('.inserted-img').style.filter = "sepia(1%) hue-rotate(190deg) saturate(90%)";
    document.querySelector('.ImageContainer').style.filter = "sepia(1%) hue-rotate(190deg) saturate(90%)";

    for (var i = 0; i < elements.length; i++) {
        if (i == 2) {
            elements.item(i).style.color = "rgb(215,33,48)";
        }
        else {
            elements.item(i).style.color = '#212529';
        }
    }
}

function FilterFour()
{
    var elements = document.getElementsByClassName('red-select');
    document.getElementById("getFilter").value = "grayscale(100%)";

    document.querySelector('.inserted-img').style.filter = "grayscale(100%)";
    document.querySelector('.ImageContainer').style.filter = "grayscale(100%)";

    for (var i = 0; i < elements.length; i++) {
        if (i == 3) {
            elements.item(i).style.color = "rgb(215,33,48)";
        }
        else {
            elements.item(i).style.color = '#212529';
        }
    }
}

function CropF()
{
    HideFunc(1);
    HideFunc(2);
    HideFunc(3);

    var elementFill = document.getElementById("crop-container");
    elementFill.style.display = "block";
    elementFill.style.zIndex = "4";
}

function MakeCrop()
{
    var element = document.querySelector('.inserted-img');

}

function DeleteCrop() {
    var element = document.querySelector('.inserted-img');

}

function DeleteF()
{
    HideFunc(1);
    HideFunc(2);
    HideFunc(3);

    var element = document.getElementById('inserted');
    element.style.display = "none";
    element.style.zIndex = "-2";
    document.getElementById('getImage').value = null;
    image = "/images/background_image.jpg";
    document.querySelector('.hided-icon').style.display = "block";
}

function ResizeF()
{
    HideFunc(1);
    HideFunc(2);
    HideFunc(3);

    var elementFill = document.getElementById("resize-container");
    elementFill.style.display = "block";
    elementFill.style.zIndex = "4";
}

function HideFunc(id)
{
    if (id == 1)
    {
        var elementFill = document.getElementById("filter-container");
        elementFill.style.display = "none";
        elementFill.style.zIndex = "-2";
    }

    if (id == 2)
    {
        var elementFill = document.getElementById("resize-container");
        elementFill.style.display = "none";
        elementFill.style.zIndex = "-2";
    }

    if (id == 3)
    {
        var elementFill = document.getElementById("crop-container");
        elementFill.style.display = "none";
        elementFill.style.zIndex = "-2";
    }
}

function ResizeImage()
{
    var elementValue = document.getElementById("myRange").value;
    var element = document.querySelector('.inserted-img');
    var elementTwo = document.querySelector('.ImageContainer');

    element.style.height = (459 + parseInt(elementValue, 10)).toString() + "px";
    element.style.width = (816 * parseInt(element.style.height, 10) / 459).toString() + "px";

    elementTwo.style.height = (459 + parseInt(elementValue, 10)).toString() + "px";
    elementTwo.style.width = (816 * parseInt(element.style.height, 10) / 459).toString() + "px";

    document.getElementById('image-container').style.paddingTop = ((459 - parseInt(elementTwo.style.height)) / 2).toString() + "px";

    document.getElementById("getSize").value = element.style.height + " " + element.style.width;
}

function ChangeElement(valu)
{
    var elements = document.getElementsByClassName('kinds-text');

    for (var i = 0; i < elements.length; i++)
    {
        if (elements.item(i).textContent == valu)
        {
            elements.item(i).style.color = "rgb(215, 33, 48)";
            elements.item(i).style.cursor = "default";
        }
        else
        {
            elements.item(i).style.color = 'rgb(180, 180, 180)';
            elements.item(i).style.cursor = "pointer";
        }
    }
}