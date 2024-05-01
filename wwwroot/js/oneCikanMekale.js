function ulOrtala(dropdownMenu,button) {
    var dropdownWidth = dropdownMenu.offsetWidth;
    var windowWidth = window.innerWidth;
    var center = windowWidth / 2;
    var ElementSoldanStartKonum = dropdownMenu.getBoundingClientRect().left;
    var leftPosition = center - ElementSoldanStartKonum;
    var ulYarisi = dropdownWidth / 2
    leftPosition = leftPosition - ulYarisi
    
    setTimeout(function () {
        var topPosition = getTranslateY(dropdownMenu);
        dropdownMenu.style.left = leftPosition + 'px';
        dropdownMenu.style.transform = 'translateY(' + topPosition + 'px)';
    }, 400);
}


function getTranslateY(element) {
    // Get the current transform property value
    var transform = window.getComputedStyle(element).getPropertyValue('transform');
    // If there is no transform value, return 0
    if (transform === 'none') {
        return 0;
    }
    // Parse the transform value
    var matrix = transform.match(/^matrix\(([^\(]*)\)$/);
    // If matrix format is not valid, return 0
    if (!matrix || matrix.length < 2) {
        return 0;
    }
    // Extract the translateY value from the transform matrix
    var translateY = parseFloat(matrix[1].split(', ')[5]);
    // Return the translateY value
    return translateY;
}



document.addEventListener('DOMContentLoaded', function () {
    const sectionOneCikanMakaleler=document.getElementById("OneCikanMakaleler");
    var dropdownMenus = sectionOneCikanMakaleler.querySelectorAll('.dropdown-menu');
    var dropdownButtons = sectionOneCikanMakaleler.querySelectorAll('.dropdown-toggle');

    dropdownButtons.forEach(function (button, index) {
        button.addEventListener('click', function () {
            var dropdownMenu = dropdownMenus[index];
            ulOrtala(dropdownMenu, button);
        });// button.addEventListener 'click'
    });
});
