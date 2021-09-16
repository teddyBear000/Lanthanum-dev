const dropArea = document.querySelector('.drag-area');
const fileTypes = ['image/jpeg', 'image/png', 'image/jpg', 'image/tif'];
input = dropArea.querySelector('#getImage');

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
    if(fileTypes.includes(file.type)){
        console.log("if begin: value = " + input.value)
        let fileReader = new FileReader();
        fileReader.onload = () => {
            dropArea.innerHTML= `<img id="inserted" src="${fileReader.result}" height=459px>`;
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
