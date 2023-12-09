﻿export function printSnippetFrom(id, userId, filename) {
    const darkMode = document.documentElement.classList.contains('dark');
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
    const darkStyle = 'body .gist .highlight {background: #272822;}body .gist .blob-num, body .gist .blob-code-inner, body .gist .pl-s2, body .gist .pl-stj {color: #f8f8f2;}body .gist .pl-c1 {color: #ae81ff;}body .gist .pl-enti {color: #a6e22e;font-weight: 700;}body .gist .pl-st {color: #66d9ef;}body .gist .pl-mdr {color: #66d9ef;font-weight: 400;}body .gist .pl-ms1 {background: #fd971f;}body .gist .pl-c, body .gist .pl-c span, body .gist .pl-pdc {color: #75715e;font-style: italic;}body .gist .pl-cce, body .gist .pl-cn, body .gist .pl-coc, body .gist .pl-enc, body .gist .pl-ens, body .gist .pl-kos, body .gist .pl-kou, body .gist .pl-mh .pl-pdh, body .gist .pl-mp, body .gist .pl-mp1 .pl-sf, body .gist .pl-mq, body .gist .pl-pde, body .gist .pl-pse, body .gist .pl-pse .pl-s2, body .gist .pl-mp .pl-s3, body .gist .pl-smi, body .gist .pl-stp, body .gist .pl-sv, body .gist .pl-v, body .gist .pl-vi, body .gist .pl-vpf, body .gist .pl-mri, body .gist .pl-va, body .gist .pl-vpu {color: #66d9ef;}body .gist .pl-cos, body .gist .pl-ml, body .gist .pl-pds, body .gist .pl-s, body .gist .pl-s1, body .gist .pl-sol {color: #e6db74;}body .gist .pl-e, body .gist .pl-ef, body .gist .pl-en, body .gist .pl-enf, body .gist .pl-enm, body .gist .pl-entc, body .gist .pl-entm, body .gist .pl-eoac, body .gist .pl-eoac .pl-pde, body .gist .pl-eoi, body .gist .pl-mai .pl-sf, body .gist .pl-mm, body .gist .pl-pdv, body .gist .pl-som, body .gist .pl-sr, body .gist .pl-vo {color: #a6e22e;}body .gist .pl-ent, body .gist .pl-eoa, body .gist .pl-eoai, body .gist .pl-eoai .pl-pde, body .gist .pl-k, body .gist .pl-ko, body .gist .pl-kolp, body .gist .pl-mc, body .gist .pl-mr, body .gist .pl-ms, body .gist .pl-s3, body .gist .pl-smc, body .gist .pl-smp, body .gist .pl-sok, body .gist .pl-sra, body .gist .pl-src, body .gist .pl-sre {color: #f92672;}body .gist .pl-mb, body .gist .pl-pdb {color: #e6db74;font-weight: 700;}body .gist .pl-mi, body .gist .pl-pdi {color: #f92672;font-style: italic;}body .gist .pl-pdc1, body .gist .pl-scp {color: #ae81ff;}body .gist .pl-sc, body .gist .pl-sf, body .gist .pl-mo, body .gist .pl-entl {color: #fd971f;}body .gist .pl-mi1, body .gist .pl-mdht {color: #a6e22e;background: rgba(0, 64, 0, .5);}body .gist .pl-md, body .gist .pl-mdhf {color: #f92672;background: rgba(64, 0, 0, .5);}body .gist .pl-mdh, body .gist .pl-mdi {color: #a6e22e;font-weight: 400;}body .gist .pl-ib, body .gist .pl-id, body .gist .pl-ii, body .gist .pl-iu {background: #a6e22e;color: #272822;}';
    const resizeScript = `onload="parent.document.getElementById('${iframeId}').style.height=document.body.scrollHeight + 24 + 'px'"`;
    const iframeHtml = `<!DOCTYPE html><html lang="en"><head><style>${darkMode ? darkStyle : null}</style></head><body ${resizeScript}>${gistScript}</body></html>`;

    doc.open();
    doc.writeln(iframeHtml);
    doc.close();
}