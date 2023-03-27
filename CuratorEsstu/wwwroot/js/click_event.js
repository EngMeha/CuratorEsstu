//со списком работаем через array


var button = document.getElementsByClassName("test");
var btn_close = document.getElementById("close");
var modal = document.getElementById("modal-name");

btn_close.onclick = function(){
    modal.style.display = "none";
}

var outerFunction = function(event) {
    Array.from(button).forEach(b=> {
        b.addEventListener("click", innerFunction); 
    });


    setTimeout(function(){ Array.from(button).forEach(b=> {
        b.removeEventListener("click", innerFunction); 
    }); }, 300);
};

var innerFunction = function(event) {
    modal.style.display = "block";
}


Array.from(button).forEach(b=> {
    b.addEventListener("click", outerFunction); 
});
