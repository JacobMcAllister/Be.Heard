$(document).ready(() => {
    // style
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
        img.parent().css({
            opacity: 0,
            position: 'relative',
            top: '50px'
        });
    })
    
    let HeroContentImages = $('#HeroContent').children().find('img');
    let offset = 30;
    HeroContentImages.each((i,e) => {
       let img = $(e);
       let alternate = (i % 2 === 0) ? 1 : -1;
       img.css({
           'position': 'relative',
           'top': `${alternate*offset}px`
       });
    });
    
    
    const easeObjects = []
    HeroContentImages.each((i,e) => {
       const timing = e.getAttribute('data-timing');
        let alternate = (i % 2 === 0) ? -1 : 1;
       easeObjects.push(basicScroll.create({
           elem: e,
           from: '0',
           to: 'top-top',
           direct: true,
           props: {
               '--img-translate': {
                   from: '0',
                   to: `${alternate*offset}px`,
                   timing: timing
               }
           }
       }));
    });
    
    let GoalsSection = document.getElementById('Goals');
    easeObjects.push(basicScroll.create({
        elem: GoalsSection,
        from: 'top-middle',
        to: 'top-top',
        direct: false,
        props: {
            '--goals-sect-translate': {
                from: '0',
                to: '-200px',
                timing: GoalsSection.getAttribute('data-timing')
            },
            '--op': {
                from: '0',
                to: '5',
                timing: (t) => t * 0.1
            }
        }
    }));
    
    let TheTeamSection = document.getElementById('TheTeam');
    easeObjects.push(basicScroll.create({
        elem: GoalsSection,
        from: 'top-top',
        to: 'middle-top',
        direct: false,
        props: {
            '--team-sect-translate': {
                from: '0',
                to: '-250px',
                timing: TheTeamSection.getAttribute('data-timing')
            },
            '--team-op': {
                from: '0',
                to: '1',
                timing: TheTeamSection.getAttribute('data-timing')
            }
        }
    }));
    
    $(document).on('scroll', () => {
       if ($(document).scrollTop() >= $('section#Goals').position().top * 0.15) {
           let time = 250;
           GoalsContentImages.each((i,e) => {
               setTimeout(() => {
                   $(e).parent().animate({
                       opacity: 1,
                       top: '0px'
                   }, 800);
               }, time);
               time += 250;
           });
       }
    });
    
    easeObjects.forEach((easeObject) => easeObject.start())
});