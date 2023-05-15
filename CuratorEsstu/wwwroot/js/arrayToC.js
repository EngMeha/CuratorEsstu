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

function sendArrayForEvent(parametrs) {
    const title = $('#title').val();
    const dateEvent = $('#dateEvent').val();
    const eventId = $('#eventId').val();
    $.ajax({
        
        type: 'POST',
        url: parametrs.url,
        data: { 'groups': array, 'title': title, 'dateEvent': dateEvent, 'idEvent': eventId },
        success: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });
};

function sendArrayForGroup(parametrs) {
    $.ajax({

        type: 'POST',
        url: parametrs.url,
        data: { 'group': title.id },
        
    });
};

var title;
function send_title(clicked_id) {
    title = document.getElementById(clicked_id);
}