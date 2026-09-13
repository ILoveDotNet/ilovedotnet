// MediaPipe Face Mesh — Blazor JS interop module (DotNetObjectReference callbacks)
// Supports three modes: 'visualize' (default), 'enroll', 'checkin'
// In enroll/checkin modes, adds blink-liveness detection and 956-float identity vector.

const mpFaceMesh = window;
const drawingUtils = window;

// Assigned inside onInit so they always point to the current page's elements
// (Blazor SPA navigation replaces DOM nodes; module-level constants would go stale)
let videoElement;
let canvasElement;
let canvasCtx;

// Stored so dispose() can stop them before re-init on navigation
let currentCamera  = null;
let currentFaceMesh = null;

/** Dynamically inject a <script> tag and wait for it to load */
function insertGlobalScript(url) {
    const element = document.createElement('script');
    element.setAttribute('src', url);
    element.setAttribute('crossorigin', 'anonymous');
    document.head.appendChild(element);
    return new Promise((resolve) => { element.onload = resolve; });
}

// ── Attendance / liveness state ──────────────────────────────────────────────
let faceMeshMode       = 'visualize'; // 'visualize' | 'enroll' | 'checkin'
let faceMeshLiveness   = 'none';      // 'none' | 'blink' | 'depth' | 'both'
let livenessConfirmed  = false;
let blinkDetected      = false;        // blink sub-flag (used by 'blink' and 'both' modes)
let depthOk            = false;        // z-depth sub-flag (used by 'depth' and 'both' modes)
let lastFaceDetected   = false;
let currentVector      = null;        // normalized 956-float landmark vector
let autoSendInterval   = null;
let lastVectorUpdateMs = 0;           // throttle for live vector display

// Blink state machine
let blinkState          = 'open';  // 'open' | 'was_closed' | 'sustained'
let wasClosedFrames     = 0;
const EAR_BLINK_THRESH  = 0.22;
const EAR_OPEN_THRESH   = 0.25;
const MAX_CLOSED_FRAMES = 5;

// ── Helpers ──────────────────────────────────────────────────────────────────
function dist2d(a, b) { return Math.hypot(a.x - b.x, a.y - b.y); }

// EAR via Face Mesh landmark indices
// Left eye:  top=159, bottom=145, innerCorner=33,  outerCorner=133
// Right eye: top=386, bottom=374, innerCorner=362, outerCorner=263
function eyeEAR(lm) {
    const l = dist2d(lm[159], lm[145]) / dist2d(lm[33],  lm[133]);
    const r = dist2d(lm[386], lm[374]) / dist2d(lm[362], lm[263]);
    return (l + r) / 2;
}

// Z-depth variance: real faces have high variance; flat photos near zero
function zDepthVariance(lm) {
    let sum = 0;
    for (let i = 0; i < lm.length; i++) sum += lm[i].z;
    const mean = sum / lm.length;
    let v = 0;
    for (let i = 0; i < lm.length; i++) { const d = lm[i].z - mean; v += d * d; }
    return v / lm.length;
}

// Normalize 478 landmarks → 956-float vector (x,y only; z excluded — too noisy)
// Centred on iris midpoint, scaled by inter-ocular distance → pose invariant
function normalizeLandmarks(lm) {
    const li = lm[468]; const ri = lm[473]; // left/right iris centres (refineLandmarks: true)
    const cx = (li.x + ri.x) / 2;
    const cy = (li.y + ri.y) / 2;
    const iod = dist2d(li, ri);
    const sc  = iod > 0.01 ? iod : 0.1;
    const out = new Array(lm.length * 2);
    for (let i = 0; i < lm.length; i++) {
        out[i * 2]     = (lm[i].x - cx) / sc;
        out[i * 2 + 1] = (lm[i].y - cy) / sc;
    }
    return out;
}

// Blink state machine — sets blinkDetected; does NOT set livenessConfirmed directly
function checkBlink(lm) {
    const ear = eyeEAR(lm);
    if (blinkState === 'open') {
        if (ear < EAR_BLINK_THRESH) { blinkState = 'was_closed'; wasClosedFrames = 1; }
    } else if (blinkState === 'was_closed') {
        if (ear >= EAR_BLINK_THRESH) { blinkState = 'open'; wasClosedFrames = 0; blinkDetected = true; }
        else if (++wasClosedFrames > MAX_CLOSED_FRAMES) { blinkState = 'sustained'; wasClosedFrames = 0; }
    } else if (blinkState === 'sustained') {
        if (ear >= EAR_OPEN_THRESH) blinkState = 'open';
    }
}

// Evaluate all liveness sub-checks and fire callbacks when state changes
function evaluateLiveness(component, lm) {
    const prevLiveness = livenessConfirmed;
    const prevDepth    = depthOk;

    if (faceMeshLiveness === 'blink') {
        checkBlink(lm);
        livenessConfirmed = blinkDetected;
    } else if (faceMeshLiveness === 'depth') {
        depthOk = zDepthVariance(lm) > 0.0005;
        livenessConfirmed = depthOk;
    } else if (faceMeshLiveness === 'both') {
        depthOk = zDepthVariance(lm) > 0.0005;
        if (depthOk) checkBlink(lm);   // only start watching for blink once depth passes
        livenessConfirmed = depthOk && blinkDetected;
    }

    if (livenessConfirmed !== prevLiveness)
        component.invokeMethodAsync('OnLivenessChanged', livenessConfirmed);
    if (depthOk !== prevDepth)
        component.invokeMethodAsync('OnDepthChanged', depthOk);
}

// ── Exports callable from C# ─────────────────────────────────────────────────
export function resetLiveness() {
    livenessConfirmed = false; blinkDetected = false; depthOk = false;
    blinkState = 'open'; wasClosedFrames = 0;
}

export function setLivenessMode(mode) {
    faceMeshLiveness = mode ?? 'none';
    livenessConfirmed = false; blinkDetected = false; depthOk = false;
    blinkState = 'open'; wasClosedFrames = 0;
}

export function dispose() {
    if (autoSendInterval) { clearInterval(autoSendInterval); autoSendInterval = null; }
    if (currentCamera)   { try { currentCamera.stop();  } catch(e) {} currentCamera   = null; }
    if (currentFaceMesh) { try { currentFaceMesh.close(); } catch(e) {} currentFaceMesh = null; }
    currentVector = null; livenessConfirmed = false; blinkDetected = false; depthOk = false;
    lastFaceDetected = false; blinkState = 'open'; wasClosedFrames = 0;
}

// Sample currentVector 5 times 200 ms apart, return element-wise average
export async function captureFeatureVector(component) {
    const samples = [];
    for (let i = 0; i < 5; i++) {
        if (currentVector) samples.push([...currentVector]);
        if (i < 4) await new Promise(r => setTimeout(r, 200));
    }
    if (samples.length === 0) {
        await component.invokeMethodAsync('OnFeatureVectorReady',
            JSON.stringify({ detected: false, vector: null, sampleCount: 0 }));
        return;
    }
    const len = samples[0].length;
    const avg = new Array(len).fill(0);
    for (const s of samples) for (let i = 0; i < len; i++) avg[i] += s[i];
    for (let i = 0; i < len; i++) avg[i] /= samples.length;
    await component.invokeMethodAsync('OnFeatureVectorReady',
        JSON.stringify({ detected: true, vector: avg, sampleCount: samples.length }));
}

export async function onInit(component, mode = 'visualize', liveness = 'none') {
    // Stop previous camera / faceMesh (handles re-init on SPA navigation)
    if (currentCamera)   { try { currentCamera.stop();  } catch(e) {} currentCamera   = null; }
    if (currentFaceMesh) { try { currentFaceMesh.close(); } catch(e) {} currentFaceMesh = null; }
    if (autoSendInterval) { clearInterval(autoSendInterval); autoSendInterval = null; }

    faceMeshMode = mode; faceMeshLiveness = liveness;
    livenessConfirmed = false; blinkDetected = false; depthOk = false;
    currentVector = null; lastFaceDetected = false; lastVectorUpdateMs = 0;
    blinkState = 'open'; wasClosedFrames = 0;

    // Re-select DOM elements — must happen after Blazor has rendered the new page
    videoElement = document.querySelector('.input_video');
    canvasElement = document.querySelector('.output_canvas');
    canvasCtx = canvasElement.getContext('2d');

    // Load MediaPipe libraries from CDN
    await insertGlobalScript('https://cdn.jsdelivr.net/npm/@mediapipe/camera_utils@latest/camera_utils.js');
    await insertGlobalScript('https://cdn.jsdelivr.net/npm/@mediapipe/drawing_utils@latest/drawing_utils.js');
    await insertGlobalScript('https://cdn.jsdelivr.net/npm/@mediapipe/face_mesh@latest/face_mesh.js');

    // Create the FaceMesh detector (stored for cleanup)
    const faceMesh = new mpFaceMesh.FaceMesh({
        locateFile: (file) => {
            return `https://cdn.jsdelivr.net/npm/@mediapipe/face_mesh@latest/${file}`;
        }
    });

    faceMesh.setOptions({
        selfieMode:             true,
        maxNumFaces:            2,
        refineLandmarks:        true,   // adds 10 iris landmarks per eye (478 total per face)
        minDetectionConfidence: 0.5,
        minTrackingConfidence:  0.5
    });

    currentFaceMesh = faceMesh;
    faceMesh.onResults(results => onResults(component, results));

    // Start the webcam feed
    const camera = new Camera(videoElement, {
        onFrame: async () => {
            await faceMesh.send({ image: videoElement });
        },
        width: 1280,
        height: 720
    });
    currentCamera = camera;
    camera.start();

    // Wire the spinner for this page's loading element
    const spinner = document.querySelector('.loading');
    if (spinner) spinner.ontransitionend = () => { spinner.style.display = 'none'; };

    // In check-in mode, auto-send feature vector every 1.5 s when liveness is confirmed
    if (faceMeshMode === 'checkin') {
        autoSendInterval = setInterval(() => {
            if (currentVector && (faceMeshLiveness === 'none' || livenessConfirmed)) {
                component.invokeMethodAsync('OnFeatureVectorReady',
                    JSON.stringify({ detected: true, vector: currentVector, sampleCount: 1 }));
            }
        }, 1500);
    }
}

function onResults(component, results) {
    document.body.classList.add('loaded');

    canvasCtx.save();
    canvasCtx.clearRect(0, 0, canvasElement.width, canvasElement.height);

    // selfieMode: true already mirrors both the image and the landmark coordinates
    canvasCtx.drawImage(results.image, 0, 0, canvasElement.width, canvasElement.height);

    if (results.multiFaceLandmarks) {
        for (const landmarks of results.multiFaceLandmarks) {
            // Full face tessellation mesh (semi-transparent grey)
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_TESSELATION,
                { color: '#C0C0C055', lineWidth: 1 });

            // Face oval
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_FACE_OVAL,
                { color: '#E0E0E0', lineWidth: 2 });

            // Right eye + eyebrow (red)
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_RIGHT_EYE,
                { color: '#FF3030', lineWidth: 2 });
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_RIGHT_EYEBROW,
                { color: '#FF3030', lineWidth: 2 });

            // Left eye + eyebrow (green)
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_LEFT_EYE,
                { color: '#30FF30', lineWidth: 2 });
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_LEFT_EYEBROW,
                { color: '#30FF30', lineWidth: 2 });

            // Iris (requires refineLandmarks: true)
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_RIGHT_IRIS,
                { color: '#FF3030', lineWidth: 2 });
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_LEFT_IRIS,
                { color: '#30FF30', lineWidth: 2 });

            // Lips (white)
            drawingUtils.drawConnectors(
                canvasCtx, landmarks, mpFaceMesh.FACEMESH_LIPS,
                { color: '#FFFFFF', lineWidth: 2 });
        }
    }

    canvasCtx.restore();

    // ── Attendance mode: liveness + feature vector ───────────────────────────
    if (faceMeshMode !== 'visualize') {
        const hasFace = !!(results.multiFaceLandmarks?.length);

        if (hasFace !== lastFaceDetected) {
            lastFaceDetected = hasFace;
            component.invokeMethodAsync('OnFaceStatusChanged', hasFace);
        }

        if (hasFace) {
            const lm = results.multiFaceLandmarks[0];
            currentVector = normalizeLandmarks(lm);

            // Throttled live vector preview — push to .NET once per second
            const now = Date.now();
            if (now - lastVectorUpdateMs > 1000) {
                lastVectorUpdateMs = now;
                component.invokeMethodAsync('OnCurrentVectorUpdated', Array.from(currentVector));
            }

            if (faceMeshLiveness !== 'none') {
                evaluateLiveness(component, lm);
            }
        } else {
            currentVector = null;
            const prevLiveness = livenessConfirmed;
            const prevDepth    = depthOk;
            livenessConfirmed = false; blinkDetected = false; depthOk = false;
            blinkState = 'open'; wasClosedFrames = 0;
            if (prevLiveness) component.invokeMethodAsync('OnLivenessChanged', false);
            if (prevDepth)    component.invokeMethodAsync('OnDepthChanged', false);
        }
        return; // skip full-landmark JSON in attendance mode — not needed by the page
    }

    // ── Visualize mode: send full landmark JSON to Blazor ────────────────────
    if (results.multiFaceLandmarks && results.multiFaceLandmarks.length > 0) {
        const faces = results.multiFaceLandmarks.map((landmarks, index) => ({
            index,
            landmarks   // array of {x, y, z} — already plain objects
        }));
        const json = JSON.stringify({ faces });
        component.invokeMethodAsync('OnResults', json);
    }
}
