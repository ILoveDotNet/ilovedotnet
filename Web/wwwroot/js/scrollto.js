export function scrollToFragment(elementId)
{
    var topoffset = document.getElementById('header').offsetHeight;
    var element = document.getElementById(elementId);
    if (element) {
        const bodyRect = document.body.getBoundingClientRect().top;
        const elementRect = element.getBoundingClientRect().top;
        const elementPosition = elementRect - bodyRect;
        const offsetPosition = elementPosition - topoffset;

        window.scrollTo({
            top: offsetPosition,
            behavior: 'smooth'
        });
    }
}