function resizeFunc(){
    var element = document.getElementById("date-range");
    var width = window.screen.width;
    if(width <= 768){
        element.classList.add("justify-content-center");
        element.classList.remove("justify-content-start");
    }else{
        element.classList.remove("justify-content-center");
        element.classList.add("justify-content-start");
    }
}

//Отслеживание изменения размера окна. Даймаут для отложенного выполнения на 0.5 секунды чтобы не лагало
let timer = setTimeout(() => {}, 10);
window.addEventListener('resize', (e) => {
    clearTimeout(timer);
    timer = setTimeout(() => {
    resizeFunc();//Выполняемая функция
    }, 500);
});

//Запуск функции при загрузке страницы
document.addEventListener('DOMContentLoaded', resizeFunc);