import("./bwip.min.js");

export function generateBarcode(barcodeText)
{
    bwipjs.toCanvas('bwipcanvas', {
        bcid: 'code128',       // Barcode type
        text: barcodeText,    // Text to encode
        scale: 3,               // 3x scaling factor
        height: 10,              // Bar height, in millimeters
        includetext: true,            // Show human-readable text
        textxalign: 'center',        // Always good to set this
    });
}