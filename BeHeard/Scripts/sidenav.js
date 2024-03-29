$(document).ready(function(){
    // Persist user actions at bottom of viewport
    let userAction = $('#UserAction');
    let initialHeight = $('#UserAction').height();
    userAction.data('height', initialHeight);
    PersistUserActionToBottomOfParent();
    window.addEventListener('resize', PersistUserActionToBottomOfParent);
    
    // nav UX current page
    let userMenuList = $('#UserMenu').find('li');
    let specialAccessMenuList = $('#SpecialAccessMenu').find('li').not(':first-child');
    let sideNavLinksList = userMenuList.add(specialAccessMenuList);
    
    let userMenuListUrls = $('#UserMenu').find('a');
    let specialAccessMenuListUrls = $('#SpecialAccessMenu').find('a');
    let sideNavLinksUrls = userMenuListUrls.add(specialAccessMenuListUrls);
    
    sideNavLinksUrls.each((i, e) => {
        let url = window.location.origin + $(e).attr('href');
        console.log(url);
        console.log($(e).attr('href'));
        if (window.location.href.toLowerCase() === url.toLowerCase()) {
            $(sideNavLinksList[i]).css('border-right', 'solid 2px #ffd4b7');
            $(e).css({
                'font-weight': 500,
                'color': '#ffd4b7'
            });
            return false;
        }
    });
    
    // nav UX current page for mobile
    let mobileNavLinksList = $('#MobileNavBarContent').find('li');
    let mobileNavLinksUrls = $('#MobileNavBarContent').find('a');
    mobileNavLinksUrls.each((i, e) => {
        let url = window.location.origin + $(e).attr('href');
        if (window.location.href.toLowerCase() === url.toLowerCase()) {
            $(mobileNavLinksList[i]).css('border-bottom', 'solid 2px #ffd4b7');
            $(e).css({
                'font-weight': 500,
                'color': '#ffd4b7'
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

function PersistUserActionToBottomOfParent() {
    let userAction = $('#UserAction');
    userAction.height(userAction.data('height')); // reset
    let userActionPosition = $('#UserAction').position().top + $('#UserAction').height();
    let offset = window.innerHeight - userActionPosition;
    if (offset > 0) {
        userAction.height(offset);
    }
    userAction.css('opacity', 1);
}
