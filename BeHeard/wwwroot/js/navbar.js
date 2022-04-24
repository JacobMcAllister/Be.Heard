$(document).ready(function() {
    // CreateAccountButton changes
    let w = window.matchMedia("(max-width: 991px)");
    CreateAccountButtonChange(w);
    w.addEventListener('change', w => CreateAccountButtonChange(w));
    
    // nav UX current page
    let navLinksList = $('#NavBarContent').find('li');
    let navLinksUrls = $('#NavBarContent').find('a');
    navLinksUrls.each((i, e) => {
        let url = window.location.origin + $(e).attr('href');
        if (window.location.href === url) {
            if (i === 3) { // NOTE: fix later...
                return false;
            }
            
            $(navLinksList[i]).css('border-bottom', 'solid 2px #fc6801');
            $(e).css({
                'font-weight': 500,
                'color': '#0b3954'
            });
            return false;
        }
    });
    
});

document.addEventListener('DOMContentLoaded', function () {
    $('.navbar-toggler').on('click', function () {
        $(this)
            .find('[data-fa-i2svg]')
            .toggleClass('fa-bars')
            .toggleClass('fa-times');
    });
});

function CreateAccountButtonChange(w) {
    if (w.matches) {
        $('#CreateAccountButton').removeClass();
        $('#CreateAccountButton').addClass('nav-link');
    } else {
        $('#CreateAccountButton').removeClass();
        $('#CreateAccountButton').addClass('btn btn-primary bh-btn-primary');

    }
}

