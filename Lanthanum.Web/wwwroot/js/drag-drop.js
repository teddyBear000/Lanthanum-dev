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
                            <div class="crop-one" style="height: 0px;" id="c-one"> </div>
                            <div class="crop-two" style="width: 0px; left: 831px;" id="c-two"> </div>
                            <div class="crop-three" style="height: 0px; top: 489px;" id="c-three"> </div>
                            <div class="crop-four" style="width: 0px;" id="c-four"> </div>
                            <img class="inserted-img" id="inserted" src="${fileReader.result}" height=459px>`;

            image = fileReader.result;
        }

        fileReader.readAsDataURL(file);
        dropArea.classList.add("active");

        makeResizableDiv('.c-resizable');
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
    document.querySelector('.view-buttons').style.zIndex = "8";

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
    document.querySelector('.p-image-container').style.filter = "none";

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
    document.querySelector('.p-image-container').style.filter = "sepia(50%)";

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
    document.querySelector('.p-image-container').style.filter = "sepia(1%) hue-rotate(190deg) saturate(90%)";

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
    document.querySelector('.p-image-container').style.filter = "grayscale(100%)";

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

    document.querySelector('.crop-one').style.height = "0px";
    document.querySelector('.crop-four').style.width = "0px";

    var elementTwo = document.querySelector('.crop-two');
    elementTwo.style.width = "0px";
    elementTwo.style.left = "831px";

    var elementThree = document.querySelector('.crop-three');
    elementThree.style.height = "0px";
    elementThree.style.top = "489px";
    var elementFill = document.getElementById("crop-container");
    elementFill.style.display = "block";
    elementFill.style.zIndex = "4";

    var elementFillTwo = document.getElementById("taken");
    elementFillTwo.style.display = "block";
    elementFillTwo.style.zIndex = "12";
}

function MakeCrop(ftop, sleft, thieght, fwidth) {
        
        var element = document.getElementById('c-one');

    element.style.height = (parseInt(ftop) - 31) + "px";

        var elementOne = document.querySelector('.crop-two');

    elementOne.style.width = (831 - (parseInt(sleft) + parseInt(fwidth))) + "px";

    elementOne.style.left = (parseInt(sleft) + parseInt(fwidth)) + "px";

        var elementTwo = document.querySelector('.crop-three');

    elementTwo.style.height = (489 - (parseInt(ftop) + parseInt(thieght))) + "px";
    elementTwo.style.top = (parseInt(ftop) + parseInt(thieght)) + "px";

        var elementThree = document.querySelector('.crop-four');

    elementThree.style.width = (parseInt(sleft) - 18) + "px";
}

function DeleteCrop() {
    document.querySelector('.crop-one').style.height = "0px";
    document.querySelector('.crop-four').style.width = "0px";

    var elementTwo = document.querySelector('.crop-two');
    elementTwo.style.width = "0px";
    elementTwo.style.left = "831px";

    var elementThree = document.querySelector('.crop-three');
    elementThree.style.height = "0px";
    elementThree.style.top = "489px";

    var elementFillTwo = document.getElementById("taken");
    elementFillTwo.style.height = "459px";
    elementFillTwo.style.width = "816px";
    elementFillTwo.style.left = "14.5px";
    elementFillTwo.style.top = "29px";

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
        var element = document.querySelector(".c-resizable");
        document.getElementById("getCrop").value = element.style.top + " " + element.style.left + " " + element.style.height + " " + element.style.width;
        MakeCrop(element.style.top, element.style.left, element.style.height, element.style.width);
        var elementFill = document.getElementById("crop-container");
        elementFill.style.display = "none";
        elementFill.style.zIndex = "-2";

        var elementFillTwo = document.getElementById("taken");
        elementFillTwo.style.display = "none";
        elementFillTwo.style.zIndex = "-2";
    }
}

function ResizeImage()
{
    var elementValue = document.getElementById("myRange").value;
    var element = document.querySelector('.inserted-img');
    var elementTwo = document.querySelector('.p-image-container');

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

function makeResizableDiv(div) {
    const element = document.querySelector(div);
    const resizers = document.querySelectorAll(div + ' .c-resizer')
    const minimum_size = 0;
    let original_width = 0;
    let original_height = 0;
    let original_x = 0;
    let original_y = 0;
    let original_mouse_x = 0;
    let original_mouse_y = 0;
    for (let i = 0; i < resizers.length; i++) {
        const currentResizer = resizers[i];
        currentResizer.addEventListener('mousedown', function (e) {
            e.preventDefault()
            original_width = parseFloat(getComputedStyle(element, null).getPropertyValue('width').replace('px', ''));
            original_height = parseFloat(getComputedStyle(element, null).getPropertyValue('height').replace('px', ''));
            original_x = element.getBoundingClientRect().left;
            original_y = element.getBoundingClientRect().top;
            original_mouse_x = e.pageX;
            original_mouse_y = e.pageY;
            window.addEventListener('mousemove', resize)
            window.addEventListener('mouseup', stopResize)
        })

        function resize(e) {
            if (currentResizer.classList.contains('c-bottom-right')) {
                const width = original_width + (e.pageX - original_mouse_x);
                const height = original_height + (e.pageY - original_mouse_y)
                if (width > minimum_size) {
                    element.style.width = width + 'px'
                }
                if (height > minimum_size) {
                    element.style.height = height + 'px'
                }
            }

        }

        function stopResize() {
            window.removeEventListener('mousemove', resize)
        }
    }
}

var movable = false;
function removeTo() {
    movable = !movable;

    if (movable) {
        $('.c-resizable').draggable({
            containment: "parent"
        });
        document.getElementById('toHide').style.display = 'none';
        document.getElementById('cButton').innerText = "Change Size";
    }
    else {
        $('.c-resizable').draggable("destroy");
        document.getElementById('toHide').style.display = 'block';
        document.getElementById('cButton').innerText = "Change Position";
    }
}