function reply_click(clicked_id)
{
    var button = document.getElementById(clicked_id);
    button.addEventListener("click", createEvent);

    setTimeout(function(){ 
        button.removeEventListener("click", createEvent); 
     }, 300);
}

var createEvent = function(e){
    var element = document.elementFromPoint(e.clientX, e.clientY);
    alert(element.id);
}