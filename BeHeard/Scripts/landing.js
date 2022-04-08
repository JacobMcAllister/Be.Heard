$(document).ready(() => {
    let GoalsContentImages = $('#GoalsContent').children().find('img');
    let height = 9999;
    GoalsContentImages.each((i,e) => {
        let img = $(e);
        if (height > img.height()) {
            height = img.height();
        }
    });
    GoalsContentImages.each((i,e) => {
        let img = $(e);
        img.height(height);
    })
});