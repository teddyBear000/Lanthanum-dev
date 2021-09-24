const warn = document.getElementById('warn');
const warning_container = document.getElementById('warning-container');
const close_warn = document.getElementById('close-warn');

warn.addEventListener('click', ()=>{
    warning_container.classList.add('show');
});

close_warn.addEventListener('click', ()=>{
    warning_container.classList.remove('show');
});
