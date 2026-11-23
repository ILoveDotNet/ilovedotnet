export function highlightCode(id) {
  const target = document.getElementById(id);
  hljs.highlightElement(target);
}

export async function copyCode(id) {
  const target = document.getElementById(id);
  await navigator.clipboard.writeText(target.textContent);
}