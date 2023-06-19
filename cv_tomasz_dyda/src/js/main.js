const menuElement = document.getElementById('menu');
const menuToggleElement = document.getElementById('toggle-menu');

menuToggleElement.addEventListener('click', function() {
    menuElement.classList.toggle('visible');
});

const laptopBreakpoint = window.matchMedia('(min-width: 768px)');
laptopBreakpoint.addEventListener('change', function() {
    if (laptopBreakpoint.matches) {
        menuElement.classList.remove('visible');
    }
});
