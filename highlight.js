export function highlightCode(id) {
  const target = document.getElementById(id);
  hljs.highlightElement(target);
}