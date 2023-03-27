var array = new Array();

function reply_click(clicked_id)
{
    var check = document.getElementById(clicked_id);
    if(check.checked){
        array.push(clicked_id);
    }else{
        var index = array.indexOf(clicked_id);
        array.splice(index, 1);
    }
    
    array.forEach(element => {
        console.log(element);
    });
}