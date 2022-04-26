let list = $('#MedicalProvider').find('li').not(':first-child');
let time = 350;
list.each((i,e) => {
    setTimeout(() => {
        $(e).animate({
            opacity: 1,
            top: '0px'
        }, 250);
    }, time);
    time += 150;
});