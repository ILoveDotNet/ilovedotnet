export async function LoadImageThroughStream(imageElement, imageStream) {
    const arrayBuffer = await imageStream.arrayBuffer();
    const blob = new Blob([arrayBuffer], { type: 'image/webp' });
    const url = URL.createObjectURL(blob);

    if (imageElement) {
        imageElement.onload = () => {
            URL.revokeObjectURL(url);
        }
        imageElement.src = url;
    }
}