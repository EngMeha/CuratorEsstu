function openModal(parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');

    if (id === undefined || url === undefined) {
        alert('Упссс.... что-то пошло не так')
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: { "id": id },
        success: function (response) {
            modal.find(".modal-box").html(response);
            modal.css("display",
                "block");
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};


function closeModal() {
    const modal = $('#modal');
    modal.css("display", "none");
    $('.modal-backdrop').css("display", "none");
}

/*var btn_close = document.getElementById("close");


btn_close.onclick = function(){
    modal.modal("hide");
}*/

//var outerFunction = function(event) {
//    Array.from(button).forEach(b=> {
//        b.addEventListener("click", innerFunction); 
//    });


//    setTimeout(function(){ Array.from(button).forEach(b=> {
//        b.removeEventListener("click", innerFunction); 
//    }); }, 300);
//};

//var innerFunction = function(event) {
//    modal.style.display = "block";
//}


//Array.from(button).forEach(b=> {
//    b.addEventListener("click", outerFunction); 
//});
