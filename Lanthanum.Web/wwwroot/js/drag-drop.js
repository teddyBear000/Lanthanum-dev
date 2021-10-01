const dropArea = document.querySelector('.drag-area');
const dropAreaFile = document.querySelector('.drag-area-file');
const fileTypes = ['image/jpeg', 'image/png', 'image/jpg', 'image/tif'];
input = dropAreaFile.querySelector('#getImage');

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
            dropArea.innerHTML = `<img id="inserted" src="${fileReader.result}" height=459px>`;
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
    var Header = document.getElementById('rHeader').value;
    var Headline = document.getElementById('rHeadLine').value;
    var MainText = tinymce.get("textarea1").getContent();

    document.getElementById('Header').textContent = Header;
    document.getElementById('Headline').textContent = Headline;
    document.getElementById('MainText').textContent = MainText;

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
