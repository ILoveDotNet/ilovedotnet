export function printSnippetFrom(id, userId, filename) {
    const target = document.getElementById(id);
    const iframe = document.createElement('iframe');
    const iframeId = `${userId}-${filename}`;
    iframe.setAttribute("id", iframeId);
    iframe.setAttribute("width", "100%");
    target.appendChild(iframe);

    let doc = iframe.document;
    if (iframe.contentDocument) doc = iframe.contentDocument;
    else if (iframe.contentWindow) doc = iframe.contentWindow.document;

    const gistScript = `<script src="https://gist.github.com/${userId}/${filename}.js"></script>`;
    const resizeScript = `onload="parent.document.getElementById('${iframeId}').style.height=document.body.scrollHeight + 'px'"`;
    const iframeHtml = `<html><body ${resizeScript}>${gistScript}</body></html>`;

    doc.open();
    doc.writeln(iframeHtml);
    doc.close();
}