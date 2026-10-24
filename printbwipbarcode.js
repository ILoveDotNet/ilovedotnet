import("./bwip.min.js");

export function printBarcode() {
    var iframe = document.querySelector('#barcode-print') || document.createElement('iframe');
    iframe.setAttribute("id", "barcode-print");
    iframe.setAttribute("style", "display:none;");
    document.body.appendChild(iframe);
    iframe.contentWindow.document.body.innerHTML = document.querySelector('#barcode-section-to-print').innerHTML;
    var printScript = document.createElement("script");
    printScript.innerHTML = `function setPrintStyles() {
                                var css = \`@media print { @page { size: 100mm 35mm; } }\`,
        		                    head = document.head || document.getElementsByTagName('head')[0],
        		                    style = document.createElement('style');
	    	                    head.appendChild(style);
	    	                    style.type = 'text/css';
	    	                    style.appendChild(document.createTextNode(css));
                            }`;
    iframe.contentWindow.document.head.appendChild(printScript);
    var barCodeScript = document.createElement("script");
    barCodeScript.innerHTML = `function drawBarCode() {
                                document.querySelectorAll('canvas').forEach(function (barcodeCanvas) {
                                        bwipjs.toCanvas(barcodeCanvas, {
                                        bcid: "code128", // Barcode type
                                        text: barcodeCanvas.dataset.text, // Text to encode
                                        scale: 2, // 2x scaling factor
                                        height: 6, // Bar height, in millimeters
                                        includetext: false, // Show human-readable text
                                        textxalign: "center", // Always good to set this
                                    });
                                });
                            }`;
    iframe.contentWindow.document.head.appendChild(barCodeScript);
    var bodyScript = document.createElement("script");
    bodyScript.innerHTML = `fetch('${window.location.origin}/barcode-label.css')
							.then(response => response.text())
							.then(cssdata => {
								var head = document.head || document.getElementsByTagName('head')[0],
									style = document.createElement('style');
								head.appendChild(style);
								style.type = 'text/css';
								style.appendChild(document.createTextNode(cssdata));
                                
                                fetch('${window.location.origin}/bwip.min.js')
							        .then(response => response.text())
							        .then(scriptdata => {
								        var body = document.body || document.getElementsByTagName('body')[0],
									        script = document.createElement('script');
								        body.appendChild(script);
								        script.type = 'text/javascript';
								        script.appendChild(document.createTextNode(scriptdata));
                                        drawBarCode();
								        setPrintStyles();
								        window.print();
							        });
							});`;
    iframe.contentWindow.document.body.appendChild(bodyScript);
}