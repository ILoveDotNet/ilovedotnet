//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

const e=()=>(async()=>{try{return new WebAssembly.Module(Uint8Array.from(atob("AGFzbQEAAAABBAFgAAADAgEAChABDgACaR9AAQMAAAsACxoL"),e=>e.codePointAt(0))),!0}catch(e){return!1}})(),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),n=Symbol.for("wasm promise_control");function r(e,o){let t=null;const r=new Promise(function(n,r){t={isDone:!1,promise:null,resolve:o=>{t.isDone||(t.isDone=!0,n(o),e&&e())},reject:e=>{t.isDone||(t.isDone=!0,r(e),o&&o())}}});t.promise=r;const s=r;return s[n]=t,{promise:s,promise_control:t}}function s(e){return e[n]}function i(e){e&&function(e){return void 0!==e[n]}(e)||We(!1,"Promise is not controllable")}const a="__mono_message__",l=["debug","log","trace","warn","info","error"],c="MONO_WASM: ";let d,u,f,m,g,p;function h(e){m=e}function b(e){if(ke.diagnosticTracing){const o="function"==typeof e?e():e;console.debug(c+o)}}function w(e,...o){console.info(c+e,...o)}function y(e,...o){console.info(e,...o)}function v(e,...o){console.warn(c+e,...o)}function _(e,...o){if(o&&o.length>0&&o[0]&&"object"==typeof o[0]){if(o[0].silent)return;if(o[0].toString)return void console.error(c+e,o[0].toString())}console.error(c+e,...o)}function A(e,o,t){return function(...n){try{let r=n[0];if(void 0===r)r="undefined";else if(null===r)r="null";else if("function"==typeof r)r=r.toString();else if("string"!=typeof r)try{r=JSON.stringify(r)}catch(e){r=r.toString()}o(t?JSON.stringify({method:e,payload:r,arguments:n.slice(1)}):[e+r,...n.slice(1)])}catch(e){f.error(`proxyConsole failed: ${e}`)}}}function x(e,o,t){u=o,m=e,f={...o};const n=`${t}/console`.replace("https://","wss://").replace("http://","ws://");d=new WebSocket(n),d.addEventListener("error",E),d.addEventListener("close",j),function(){for(const e of l)u[e]=A(`console.${e}`,R,!0)}()}function T(e){let o=30;const t=()=>{d?0==d.bufferedAmount||0==o?(e&&y(e),function(){for(const e of l)u[e]=A(`console.${e}`,f.log,!1)}(),d.removeEventListener("error",E),d.removeEventListener("close",j),d.close(1e3,e),d=void 0):(o--,globalThis.setTimeout(t,100)):e&&f&&f.log(e)};t()}function R(e){d&&d.readyState===WebSocket.OPEN?d.send(e):f.log(e)}function E(e){f.error(`[${m}] proxy console websocket error: ${e}`,e)}function j(e){f.debug(`[${m}] proxy console websocket closed: ${e}`,e)}function D(){ke.preferredIcuAsset=C(ke.config);let e="invariant"==ke.config.globalizationMode;if(!e)if(ke.preferredIcuAsset)ke.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===ke.config.globalizationMode||"all"===ke.config.globalizationMode||"sharded"===ke.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}ke.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,ke.preferredIcuAsset=null}const o="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",t=ke.config.environmentVariables;if(void 0===t[o]&&e&&(t[o]="1"),void 0===t.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(t.TZ=e)}catch(e){w("failed to detect timezone, will fallback to UTC")}}function C(e){var o;if((null===(o=e.resources)||void 0===o?void 0:o.icu)&&"invariant"!=e.globalizationMode){const o=e.applicationCulture||(Me?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale);e.applicationCulture||(e.applicationCulture=o);const t=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(t.length>=1)return t[0].name}else o&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const o=e.split("-")[0];return"en"===o||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(o)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(o)):n="icudt.dat";if(n)for(let e=0;e<t.length;e++){const o=t[e];if(o.virtualPath===n)return o.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const M=class{constructor(e){this.url=e}toString(){return this.url}};async function S(e){if(Se&&"function"!=typeof globalThis.atob){const e="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";globalThis.atob=o=>{const t=String(o).replace(/=+$/,"");let n="";for(let o=0,r=0,s=0;s<t.length;s++){const i=e.indexOf(t.charAt(s));-1!==i&&(r=o%4?64*r+i:i,o++%4&&(n+=String.fromCharCode(255&r>>(-2*o&6))))}return n}}if(Ee){const e=await import(/*! webpackIgnore: true */"process"),o=14;if(e.versions.node.split(".")[0]<o)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${o}.`)}const o=/*! webpackIgnore: true */import.meta.url,t=o.indexOf("?");var n;if(t>0&&(ke.modulesUniqueQuery=o.substring(t)),ke.scriptUrl=o.replace(/\\/g,"/").replace(/[?#].*/,""),ke.scriptDirectory=(n=ke.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",ke.locateFile=e=>"URL"in globalThis&&globalThis.URL!==M?new URL(e,ke.scriptDirectory).toString():I(e)?e:ke.scriptDirectory+e,ke.fetch_like=U,ke.out=console.log,ke.err=console.error,ke.onDownloadResourceProgress=e.onDownloadResourceProgress,Me&&globalThis.navigator){const e=globalThis.navigator,o=e.userAgentData&&e.userAgentData.brands;o&&o.length>0?ke.isChromium=o.some(e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand):e.userAgent&&(ke.isChromium=e.userAgent.includes("Chrome"),ke.isFirefox=e.userAgent.includes("Firefox"))}void 0===globalThis.URL&&(globalThis.URL=M)}async function U(e,o){try{const t="function"==typeof globalThis.fetch;if(Ee){const n=e.startsWith("file://");if(!n&&t)return globalThis.fetch(e,o||{credentials:"same-origin"});g||(p=await import(/*! webpackIgnore: true */"url"),g=await import(/*! webpackIgnore: true */"fs")),n&&(e=p.fileURLToPath(e));const r=await g.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>r,json:()=>JSON.parse(r),text:()=>{throw new Error("NotImplementedException")}}}if(t)return globalThis.fetch(e,o||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(o){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+o,arrayBuffer:()=>{throw o},json:()=>{throw o},text:()=>{throw o}}}throw new Error("No fetch implementation available")}const P=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,k=/[a-zA-Z]:[\\/]/;function I(e){return Ee||Se?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||k.test(e):P.test(e)}let O,$=0;const L=[],N=[],z=new Map,F={"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},W={...F,"js-module-library-initializer":!0},V={...F,dotnetwasm:!0,heap:!0,manifest:!0},B={...W,manifest:!0},J={...W,dotnetwasm:!0},H={dotnetwasm:!0,symbols:!0},Q={...W,dotnetwasm:!0,symbols:!0},q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=ke.preferredIcuAsset)}function Z(e,o,t){null!=o||(o=[]),We(1==o.length,`Expect to have one ${t} asset in resources`);const n=o[0];return n.behavior=t,K(n),e.push(n),n}function K(e){V[e.behavior]&&z.set(e.behavior,e)}function X(e){We(V[e],`Unknown single asset behavior ${e}`);const o=z.get(e);if(o&&!o.resolvedUrl)if(o.resolvedUrl=ke.locateFile(o.name),F[o.behavior]){const e=me(o);e?("string"!=typeof e&&We(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),o.resolvedUrl=e):o.resolvedUrl=le(o.resolvedUrl,o.behavior)}else if("dotnetwasm"!==o.behavior)throw new Error(`Unknown single asset behavior ${e}`);return o}function Y(e){const o=X(e);return We(o,`Single asset for ${e} not found`),o}let ee=!1;async function oe(){if(!ee){ee=!0,ke.diagnosticTracing&&b("mono_download_assets");try{const e=[],o=[],t=(e,o)=>{!Q[e.behavior]&&G(e)&&ke.expected_instantiated_assets_count++,!J[e.behavior]&&G(e)&&(ke.expected_downloaded_assets_count++,o.push(se(e)))};for(const o of L)t(o,e);for(const e of N)t(e,o);ke.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...o]).then(()=>{ke.allDownloadsFinished.promise_control.resolve()}).catch(e=>{throw ke.err("Error in mono_download_assets: "+e),Xe(1,e),e}),await ke.runtimeModuleLoaded.promise;const n=async e=>{const o=await e;if(H[o.behavior])return"symbols"===o.behavior&&(await Pe.instantiate_symbols_asset(o),ge(o)),void++ke.actual_downloaded_assets_count;if(o.buffer){if(!Q[o.behavior]){o.buffer&&"object"==typeof o.buffer||We(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof o.resolvedUrl&&We(!1,"resolvedUrl must be string");const e=o.resolvedUrl,t=await o.buffer,n=new Uint8Array(t);ge(o),await Pe.beforeOnRuntimeInitialized.promise,await Pe.afterInstantiateWasm.promise,Pe.instantiate_asset(o,e,n)}}else o.isOptional||We(!1,"Expected asset to have the downloaded buffer"),!J[o.behavior]&&G(o)&&ke.expected_downloaded_assets_count--,!Q[o.behavior]&&G(o)&&ke.expected_instantiated_assets_count--},r=[],s=[];for(const o of e)r.push(n(o));for(const e of o)s.push(n(e));Promise.all(r).then(()=>{Ce||Pe.coreAssetsInMemory.promise_control.resolve()}).catch(e=>{throw ke.err("Error in mono_download_assets: "+e),Xe(1,e),e}),Promise.all(s).then(async()=>{Ce||(await Pe.coreAssetsInMemory.promise,Pe.allAssetsInMemory.promise_control.resolve())}).catch(e=>{throw ke.err("Error in mono_download_assets: "+e),Xe(1,e),e})}catch(e){throw ke.err("Error in mono_download_assets: "+e),e}}}let te=!1;function ne(){if(te)return;te=!0;const e=ke.config,o=[];if(e.assets)for(const o of e.assets)"object"!=typeof o&&We(!1,`asset must be object, it was ${typeof o} : ${o}`),"string"!=typeof o.behavior&&We(!1,"asset behavior must be known string"),"string"!=typeof o.name&&We(!1,"asset name must be string"),o.resolvedUrl&&"string"!=typeof o.resolvedUrl&&We(!1,"asset resolvedUrl could be string"),o.hash&&"string"!=typeof o.hash&&We(!1,"asset resolvedUrl could be string"),o.pendingDownload&&"object"!=typeof o.pendingDownload&&We(!1,"asset pendingDownload could be object"),o.isCore?L.push(o):N.push(o),K(o);else if(e.resources){const t=e.resources;t.wasmNative||We(!1,"resources.wasmNative must be defined"),t.jsModuleNative||We(!1,"resources.jsModuleNative must be defined"),t.jsModuleRuntime||We(!1,"resources.jsModuleRuntime must be defined"),Z(N,t.wasmNative,"dotnetwasm"),Z(o,t.jsModuleNative,"js-module-native"),Z(o,t.jsModuleRuntime,"js-module-runtime"),t.jsModuleDiagnostics&&Z(o,t.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,o,t)=>{const n=e;n.behavior=o,t?(n.isCore=!0,L.push(n)):N.push(n)};if(t.coreAssembly)for(let e=0;e<t.coreAssembly.length;e++)n(t.coreAssembly[e],"assembly",!0);if(t.assembly)for(let e=0;e<t.assembly.length;e++)n(t.assembly[e],"assembly",!t.coreAssembly);if(0!=e.debugLevel&&ke.isDebuggingSupported()){if(t.corePdb)for(let e=0;e<t.corePdb.length;e++)n(t.corePdb[e],"pdb",!0);if(t.pdb)for(let e=0;e<t.pdb.length;e++)n(t.pdb[e],"pdb",!t.corePdb)}if(e.loadAllSatelliteResources&&t.satelliteResources)for(const e in t.satelliteResources)for(let o=0;o<t.satelliteResources[e].length;o++){const r=t.satelliteResources[e][o];r.culture=e,n(r,"resource",!t.coreAssembly)}if(t.coreVfs)for(let e=0;e<t.coreVfs.length;e++)n(t.coreVfs[e],"vfs",!0);if(t.vfs)for(let e=0;e<t.vfs.length;e++)n(t.vfs[e],"vfs",!t.coreVfs);const r=C(e);if(r&&t.icu)for(let e=0;e<t.icu.length;e++){const o=t.icu[e];o.name===r&&n(o,"icu",!1)}if(t.wasmSymbols)for(let e=0;e<t.wasmSymbols.length;e++)n(t.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let o=0;o<e.appsettings.length;o++){const t=e.appsettings[o],n=pe(t);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||N.push({name:t,behavior:"vfs",cache:"no-cache",useCredentials:!0})}e.assets=[...L,...N,...o]}async function re(e){const o=await se(e);return await o.pendingDownloadInternal.response,o.buffer}async function se(e){try{return await ie(e)}catch(o){if(!ke.enableDownloadRetry)throw o;if(Se||Ee)throw o;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw o;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw o;if(o&&404==o.status)throw o;e.pendingDownloadInternal=void 0,await ke.allDownloadsQueued.promise;try{return ke.diagnosticTracing&&b(`Retrying download '${e.name}'`),await ie(e)}catch(o){return e.pendingDownloadInternal=void 0,await new Promise(e=>globalThis.setTimeout(e,100)),ke.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await ie(e)}}}async function ie(e){for(;O;)await O.promise;try{++$,$==ke.maxParallelDownloads&&(ke.diagnosticTracing&&b("Throttling further parallel downloads"),O=r());const o=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const o=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>o,json:()=>JSON.parse(new TextDecoder("utf-8").decode(o)),text:()=>new TextDecoder("utf-8").decode(o),headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const o=e.loadRemote&&ke.config.remoteSources?ke.config.remoteSources:[""];let t;for(let n of o){n=n.trim(),"./"===n&&(n="");const o=ae(e,n);e.name===o?ke.diagnosticTracing&&b(`Attempting to download '${o}'`):ke.diagnosticTracing&&b(`Attempting to download '${o}' for ${e.name}`);try{e.resolvedUrl=o;const n=ue(e);if(e.pendingDownloadInternal=n,t=await n.response,!t||!t.ok)continue;return t}catch(e){t||(t={ok:!1,url:o,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&ke.config.ignorePdbLoadErrors;if(t||We(!1,`Response undefined ${e.name}`),!n){const o=new Error(`download '${t.url}' for ${e.name} failed ${t.status} ${t.statusText}`);throw o.status=t.status,o}w(`optional download '${t.url}' for ${e.name} failed ${t.status} ${t.statusText}`)}(e);return o?(H[e.behavior]||(e.buffer=await o.arrayBuffer(),++ke.actual_downloaded_assets_count),e):e}finally{if(--$,O&&$==ke.maxParallelDownloads-1){ke.diagnosticTracing&&b("Resuming more parallel downloads");const e=O;O=void 0,e.promise_control.resolve()}}}function ae(e,o){let t;return null==o&&We(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?t=e.resolvedUrl:(t=""===o?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:o+e.name,t=le(ke.locateFile(t),e.behavior)),t&&"string"==typeof t||We(!1,"attemptUrl need to be path or url string"),t}function le(e,o){return ke.modulesUniqueQuery&&B[o]&&(e+=ke.modulesUniqueQuery),e}let ce=0;const de=new Set;function ue(e){try{e.resolvedUrl||We(!1,"Request's resolvedUrl must be set");const o=function(e){let o=e.resolvedUrl;if(ke.loadBootResource){const t=me(e);if(t instanceof Promise)return t;"string"==typeof t&&(o=t)}const t={};return e.cache?t.cache=e.cache:ke.config.disableNoCacheFetch||(t.cache="no-cache"),e.useCredentials?t.credentials="include":!ke.config.disableIntegrityCheck&&e.hash&&(t.integrity=e.hash),ke.fetch_like(o,t)}(e),t={name:e.name,url:e.resolvedUrl,response:o};return de.add(e.name),t.response.then(()=>{"assembly"==e.behavior&&ke.loadedAssemblies.push(e.name),ce++,ke.onDownloadResourceProgress&&ke.onDownloadResourceProgress(ce,de.size)}),t}catch(o){const t={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+o,arrayBuffer:()=>{throw o},json:()=>{throw o}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(t)}}}const fe={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs"};function me(e){var o;if(ke.loadBootResource){const t=null!==(o=e.hash)&&void 0!==o?o:"",n=e.resolvedUrl,r=fe[e.behavior];if(r){const o=ke.loadBootResource(r,e.name,n,t,e.behavior);return"string"==typeof o?function(e){return"string"!=typeof e&&We(!1,"url must be a string"),!I(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}(o):o}}}function ge(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function pe(e){let o=e.lastIndexOf("/");return o>=0&&o++,e.substring(o)}async function he(e){e&&await Promise.all((null!=e?e:[]).map(e=>async function(e){try{const o=e.name;if(!e.moduleExports){const t=le(ke.locateFile(o),"js-module-library-initializer");ke.diagnosticTracing&&b(`Attempting to import '${t}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */t)}ke.libraryInitializers.push({scriptName:o,exports:e.moduleExports})}catch(o){v(`Failed to import library initializer '${e}': ${o}`)}}(e)))}async function be(e,o){if(!ke.libraryInitializers)return;const t=[];for(let n=0;n<ke.libraryInitializers.length;n++){const r=ke.libraryInitializers[n];r.exports[e]&&t.push(we(r.scriptName,e,()=>r.exports[e](...o)))}await Promise.all(t)}async function we(e,o,t){try{await t()}catch(t){throw v(`Failed to invoke '${o}' on library initializer '${e}': ${t}`),Xe(1,t),t}}function ye(e,o){if(e===o)return e;const t={...o};return void 0!==t.assets&&t.assets!==e.assets&&(t.assets=[...e.assets||[],...t.assets||[]]),void 0!==t.resources&&(t.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},t.resources)),void 0!==t.environmentVariables&&(t.environmentVariables={...e.environmentVariables||{},...t.environmentVariables||{}}),void 0!==t.runtimeOptions&&t.runtimeOptions!==e.runtimeOptions&&(t.runtimeOptions=[...e.runtimeOptions||[],...t.runtimeOptions||[]]),Object.assign(e,t)}function ve(e,o){if(e===o)return e;const t={...o};return t.config&&(e.config||(e.config={}),t.config=ye(e.config,t.config)),Object.assign(e,t)}function _e(e,o){if(e===o)return e;const t={...o};return void 0!==t.coreAssembly&&(t.coreAssembly=[...e.coreAssembly||[],...t.coreAssembly||[]]),void 0!==t.assembly&&(t.assembly=[...e.assembly||[],...t.assembly||[]]),void 0!==t.lazyAssembly&&(t.lazyAssembly=[...e.lazyAssembly||[],...t.lazyAssembly||[]]),void 0!==t.corePdb&&(t.corePdb=[...e.corePdb||[],...t.corePdb||[]]),void 0!==t.pdb&&(t.pdb=[...e.pdb||[],...t.pdb||[]]),void 0!==t.jsModuleNative&&(t.jsModuleNative=[...e.jsModuleNative||[],...t.jsModuleNative||[]]),void 0!==t.jsModuleDiagnostics&&(t.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...t.jsModuleDiagnostics||[]]),void 0!==t.jsModuleRuntime&&(t.jsModuleRuntime=[...e.jsModuleRuntime||[],...t.jsModuleRuntime||[]]),void 0!==t.wasmSymbols&&(t.wasmSymbols=[...e.wasmSymbols||[],...t.wasmSymbols||[]]),void 0!==t.wasmNative&&(t.wasmNative=[...e.wasmNative||[],...t.wasmNative||[]]),void 0!==t.icu&&(t.icu=[...e.icu||[],...t.icu||[]]),void 0!==t.satelliteResources&&(t.satelliteResources=function(e,o){if(e===o)return e;for(const t in o)e[t]=[...e[t]||[],...o[t]||[]];return e}(e.satelliteResources||{},t.satelliteResources||{})),void 0!==t.modulesAfterConfigLoaded&&(t.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...t.modulesAfterConfigLoaded||[]]),void 0!==t.modulesAfterRuntimeReady&&(t.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...t.modulesAfterRuntimeReady||[]]),void 0!==t.extensions&&(t.extensions={...e.extensions||{},...t.extensions||{}}),void 0!==t.vfs&&(t.vfs=[...e.vfs||[],...t.vfs||[]]),Object.assign(e,t)}function Ae(){const e=ke.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){ke.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const o of e.assets){const t={};switch(o.behavior){case"assembly":t.assembly=[o];break;case"pdb":t.pdb=[o];break;case"resource":t.satelliteResources={},t.satelliteResources[o.culture]=[o];break;case"icu":t.icu=[o];break;case"symbols":t.wasmSymbols=[o];break;case"vfs":t.vfs=[o];break;case"dotnetwasm":t.wasmNative=[o];break;case"js-module-runtime":t.jsModuleRuntime=[o];break;case"js-module-native":t.jsModuleNative=[o];break;case"js-module-diagnostics":t.jsModuleDiagnostics=[o];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${o.behavior} of asset ${o.name}`)}_e(e.resources,t)}}e.debugLevel,void 0===e.virtualWorkingDirectory&&(e.virtualWorkingDirectory=Ue),e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Pe.diagnosticTracing=ke.diagnosticTracing=!!e.diagnosticTracing,Pe.waitForDebugger=e.waitForDebugger,ke.maxParallelDownloads=e.maxParallelDownloads||ke.maxParallelDownloads,ke.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:ke.enableDownloadRetry}let xe=!1;async function Te(e){var o;if(xe)await ke.afterConfigLoaded.promise;else try{if(xe=!0,Ae(),await he(null===(o=ke.config.resources)||void 0===o?void 0:o.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[ke.config]),e.onConfigLoaded)try{await e.onConfigLoaded(ke.config,Oe),Ae()}catch(e){throw _("onConfigLoaded() failed",e),e}Ae(),ke.afterConfigLoaded.promise_control.resolve(ke.config)}catch(o){const t=`Failed to initialize config ${o} ${null==o?void 0:o.stack}`;throw ke.config=e.config=Object.assign(ke.config,{message:t,error:o,isError:!0}),Xe(1,new Error(t)),o}}function Re(){return!!globalThis.navigator&&(ke.isChromium||ke.isFirefox)}"function"==typeof importScripts&&(globalThis.dotnetSidecar=!0);const Ee="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,je="function"==typeof importScripts,De=je&&"undefined"!=typeof dotnetSidecar,Ce=je&&!De,Me="object"==typeof window||je&&!Ee,Se=!Me&&!Ee,Ue="/";let Pe={},ke={},Ie={},Oe={},$e={},Le=!1;const Ne={},ze={config:Ne},Fe={mono:{},binding:{},internal:$e,module:ze,loaderHelpers:ke,runtimeHelpers:Pe,diagnosticHelpers:Ie,api:Oe};function We(e,o){if(e)return;const t="Assert failed: "+("function"==typeof o?o():o),n=new Error(t);_(t,n),Pe.nativeAbort(n)}function Ve(){return void 0!==ke.exitCode}function Be(){return Pe.runtimeReady&&!Ve()}function Je(){Ve()&&We(!1,`.NET runtime already exited with ${ke.exitCode} ${ke.exitReason}. You can use dotnet.runMain() which doesn't exit the runtime.`),Pe.runtimeReady||We(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function He(){Me&&(globalThis.addEventListener("unhandledrejection",eo),globalThis.addEventListener("error",oo))}let Qe,qe;function Ge(){Qe=ze.onAbort,qe=ze.onExit,ze.onAbort=Ke,ze.onExit=Ze}function Ze(e){qe&&qe(e),Xe(e,ke.exitReason)}function Ke(e){Qe&&Qe(e||ke.exitReason),Xe(1,e||ke.exitReason)}function Xe(e,o){var t;const n=o&&"object"==typeof o;e=n&&"number"==typeof o.status?o.status:void 0===e?-1:e;const r=n&&"string"==typeof o.message?o.message:""+o;(o=n?o:Pe.ExitStatus?function(e,o){const t=new Pe.ExitStatus(e);return t.message=o,t.toString=()=>o,t}(e,r):new Error("Exit with code "+e+" "+r)).status=e,o.message||(o.message=r);const s=""+(o.stack||(new Error).stack);try{Object.defineProperty(o,"stack",{get:()=>s})}catch(e){}const i=!!o.silent;if(o.silent=!0,Ve())ke.diagnosticTracing&&b("mono_exit called after exit");else{try{ze.onAbort==Ke&&(ze.onAbort=Qe),ze.onExit==Ze&&(ze.onExit=qe),Me&&(globalThis.removeEventListener("unhandledrejection",eo),globalThis.removeEventListener("error",oo)),Pe.runtimeReady?(Pe.jiterpreter_dump_stats&&Pe.jiterpreter_dump_stats(!1),0===e&&(null===(t=ke.config)||void 0===t?void 0:t.interopCleanupOnExit)&&Pe.forceDisposeProxies(!0,!0)):(ke.diagnosticTracing&&b(`abort_startup, reason: ${o}`),function(e){ke.allDownloadsQueued.promise_control.reject(e),ke.allDownloadsFinished.promise_control.reject(e),ke.afterConfigLoaded.promise_control.reject(e),ke.wasmCompilePromise.promise_control.reject(e),ke.runtimeModuleLoaded.promise_control.reject(e),Pe.dotnetReady&&(Pe.dotnetReady.promise_control.reject(e),Pe.afterInstantiateWasm.promise_control.reject(e),Pe.afterPreRun.promise_control.reject(e),Pe.beforeOnRuntimeInitialized.promise_control.reject(e),Pe.afterOnRuntimeInitialized.promise_control.reject(e),Pe.afterPostRun.promise_control.reject(e))}(o))}catch(e){v("mono_exit A failed",e)}try{i||(function(e,o){if(0!==e&&o){const e=Pe.ExitStatus&&o instanceof Pe.ExitStatus?b:_;"string"==typeof o?e(o):(void 0===o.stack&&(o.stack=(new Error).stack+""),o.message?e(Pe.stringify_as_error_with_stack?Pe.stringify_as_error_with_stack(o.message+"\n"+o.stack):o.message+"\n"+o.stack):e(JSON.stringify(o)))}!Ce&&ke.config&&(ke.config.logExitCode?ke.config.forwardConsole?T("WASM EXIT "+e):y("WASM EXIT "+e):ke.config.forwardConsole&&T())}(e,o),function(e){if(Me&&!Ce&&ke.config&&ke.config.appendElementOnExit&&document){const o=document.createElement("label");o.id="tests_done",0!==e&&(o.style.background="red"),o.innerHTML=""+e,document.body.appendChild(o)}}(e))}catch(e){v("mono_exit B failed",e)}ke.exitCode=e,ke.exitReason||(ke.exitReason=o),!Ce&&Pe.runtimeReady&&ze.runtimeKeepalivePop()}if(ke.config&&ke.config.asyncFlushOnExit&&0===e)throw(async()=>{try{await async function(){if(Ee)try{const e=await import(/*! webpackIgnore: true */"process"),o=e=>new Promise((o,t)=>{e.on("error",t),e.end("","utf8",o)}),t=o(e.stderr),n=o(e.stdout);let r;const s=new Promise(e=>{r=setTimeout(()=>e("timeout"),1e3)});await Promise.race([Promise.all([n,t]),s]),clearTimeout(r)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Ye(e,o)}})(),o;Ye(e,o)}function Ye(e,o){if(Pe.runtimeReady&&Pe.nativeExit)try{Pe.nativeExit(e)}catch(e){!Pe.ExitStatus||e instanceof Pe.ExitStatus||v("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!Me)throw Ee?process.exit(e):Pe.quit&&Pe.quit(e,o),o}function eo(e){to(e,e.reason,"rejection")}function oo(e){to(e,e.error,"error")}function to(e,o,t){e.preventDefault();try{o||(o=new Error("Unhandled "+t)),void 0===o.stack&&(o.stack=(new Error).stack),o.stack=o.stack+"",o.silent||(_("Unhandled error:",o),Xe(1,o))}catch(e){}}!function(n){if(Le)throw new Error("Loader module already loaded");Le=!0,Pe=n.runtimeHelpers,ke=n.loaderHelpers,Ie=n.diagnosticHelpers,Oe=n.api,$e=n.internal,Object.assign(Oe,{INTERNAL:$e,invokeLibraryInitializers:be}),Object.assign(n.module,{config:ye(Ne,{environmentVariables:{}})});const a={mono_wasm_bindings_is_ready:!1,config:n.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"297acf930fb0f5c07fd4e51d78e521b4ad2e5d1f",config:n.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:r(),allDownloadsQueued:r(),allDownloadsFinished:r(),wasmCompilePromise:r(),runtimeModuleLoaded:r(),loadingWorkers:r(),is_exited:Ve,is_runtime_running:Be,assert_runtime_running:Je,mono_exit:Xe,createPromiseController:r,getPromiseController:s,assertIsControllablePromise:i,mono_download_assets:oe,resolve_single_asset_path:Y,setup_proxy_console:x,set_thread_prefix:h,installUnhandledErrorHandler:He,retrieve_asset_download:re,invokeLibraryInitializers:be,isDebuggingSupported:Re,exceptionsFinal:e,simd:t,relaxedSimd:o};Object.assign(Pe,a),Object.assign(ke,l)}(Fe);let no,ro,so,io=!1,ao=!1;async function lo(e){if(!ao){if(ao=!0,Me&&ke.config.forwardConsole&&void 0!==globalThis.WebSocket&&x("main",globalThis.console,globalThis.location.origin),ze||We(!1,"Null moduleConfig"),ke.config||We(!1,"Null moduleConfig.config"),"function"==typeof e){const o=e(Fe.api);if(o.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(ze,o),ve(ze,o)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");ve(ze,e)}await S(ze)}}async function co(e){return await lo(e),ke.config.exitOnUnhandledError&&He(),Ge(),async function(){var e;await Te(ze),ne();const o=uo();(async function(){try{const e=Y("dotnetwasm");await se(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||We(!1,"Can't load dotnet.native.wasm");const o=await e.pendingDownloadInternal.response,t=o.headers&&o.headers.get?o.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===t)n=await WebAssembly.compileStreaming(o);else{Me&&"application/wasm"!==t&&v('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await o.arrayBuffer();ke.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Se?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,ke.wasmCompilePromise.promise_control.resolve(n)}catch(e){ke.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout(async()=>{try{D(),await oe()}catch(e){Xe(1,e)}},0);const t=await Promise.all(o);return await fo(t),await Pe.dotnetReady.promise,await he(null===(e=ke.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[Fe.api]),Oe}()}function uo(){const e=Y("js-module-runtime"),o=Y("js-module-native");if(no&&ro)return[no,ro,so];"object"==typeof e.moduleExports?no=e.moduleExports:(ke.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),no=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof o.moduleExports?ro=o.moduleExports:(ke.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),ro=import(/*! webpackIgnore: true */o.resolvedUrl));const t=X("js-module-diagnostics");return t&&("object"==typeof t.moduleExports?so=t.moduleExports:(ke.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),so=import(/*! webpackIgnore: true */t.resolvedUrl))),[no,ro,so]}async function fo(e){const{initializeExports:o,initializeReplacements:t,configureRuntimeStartup:n,configureEmscriptenStartup:r,configureWorkerStartup:s,setRuntimeGlobals:i,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];i(Fe),o(Fe),c&&c.setRuntimeGlobals(Fe),await n(ze),ke.runtimeModuleLoaded.promise_control.resolve(),l(()=>(Object.assign(ze,{__dotnet_runtime:{initializeReplacements:t,configureEmscriptenStartup:r,configureWorkerStartup:s,passEmscriptenInternals:a}}),ze)).catch(e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize.");throw e})}Ce&&async function(){(function(){const e=new MessageChannel,o=e.port1,t=e.port2;o.addEventListener("message",e=>{!function(e){const o=JSON.parse(e.config),t=JSON.parse(e.monoThreadInfo);ze.config=o,ze.wasmModule=e.wasmModule,ze.wasmMemory=e.wasmMemory,ze.handlers=e.handlers,io?ke.diagnosticTracing&&b("mono config already received"):(ye(ke.config,o),Pe.monoThreadInfo=t,Ae(),ke.diagnosticTracing&&b("mono config received"),io=!0,ke.afterConfigLoaded.promise_control.resolve(ke.config),Me&&o.forwardConsole&&void 0!==globalThis.WebSocket&&ke.setup_proxy_console("worker-idle",console,globalThis.location.origin))}(e.data),o.close(),t.close()},{once:!0}),o.start(),self.postMessage({[a]:{monoCmd:"preload",port:t}},[t])})(),await ke.afterConfigLoaded.promise,function(){const e=ke.config;e.assets||We(!1,"config.assets must be defined");for(const o of e.assets)K(o),q[o.behavior]&&N.push(o)}();const e=uo(),o=await Promise.all(e);return globalThis.name="em-pthread",await fo(o),ke.config.exitOnUnhandledError&&He(),Ge(),Me&&ke.config.forwardConsole&&void 0!==globalThis.WebSocket&&x("main",globalThis.console,globalThis.location.origin),await S(ze),await oe(),self.dispatchEvent(new MessageEvent("message",{data:{cmd:1,handlers:ze.handlers,wasmMemory:ze.wasmMemory,wasmModule:ze.wasmModule}})),ze}().catch(e=>Xe(1,e));const mo=new class{withModuleConfig(e){try{return ve(ze,e),this}catch(e){throw Xe(1,e),e}}withInterpreterPgo(e,o){try{return ye(Ne,{interpreterPgo:e,interpreterPgoSaveDelay:o}),Ne.runtimeOptions?Ne.runtimeOptions.push("--interp-pgo-recording"):Ne.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Xe(1,e),e}}withConfig(e){try{return ye(Ne,e),this}catch(e){throw Xe(1,e),e}}withConfigSrc(e){return this}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||We(!1,"must be directory path"),ye(Ne,{virtualWorkingDirectory:e}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariable(e,o){try{const t={};return t[e]=o,ye(Ne,{environmentVariables:t}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||We(!1,"must be dictionary object"),ye(Ne,{environmentVariables:e}),this}catch(e){throw Xe(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&We(!1,"must be boolean"),ye(Ne,{diagnosticTracing:e}),this}catch(e){throw Xe(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||We(!1,"must be number"),ye(Ne,{debugLevel:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||We(!1,"must be array of strings"),ye(Ne,{applicationArguments:e}),this}catch(e){throw Xe(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||We(!1,"must be array of strings"),Ne.runtimeOptions?Ne.runtimeOptions.push(...e):Ne.runtimeOptions=e,this}catch(e){throw Xe(1,e),e}}withMainAssembly(e){try{return ye(Ne,{mainAssemblyName:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Xe(1,e),e}}withApplicationEnvironment(e){try{return ye(Ne,{applicationEnvironment:e}),this}catch(e){throw Xe(1,e),e}}withApplicationCulture(e){try{return ye(Ne,{applicationCulture:e}),this}catch(e){throw Xe(1,e),e}}withResourceLoader(e){try{return ke.loadBootResource=e,this}catch(e){throw Xe(1,e),e}}async download(){try{await async function(){lo(ze),await Te(ze),ne(),D(),oe(),await ke.allDownloadsFinished.promise}()}catch(e){throw Xe(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await co(ze),Fe.api}()),this.instance}catch(e){throw Xe(1,e),e}}run(){return this.runMainAndExit()}async runMainAndExit(){try{return ze.config||We(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Xe(1,e),e}}async runMain(){try{return ze.config||We(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMain()}catch(e){throw Xe(1,e),e}}},go=Xe,po=co;Se||"function"==typeof globalThis.URL||We(!1,"This browser/engine doesn't support URL API. Please use a modern version."),"function"!=typeof globalThis.BigInt64Array&&We(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://learn.microsoft.com/aspnet/core/blazor/supported-platforms"),globalThis.performance&&"function"==typeof globalThis.performance.now||We(!1,"This browser/engine doesn't support performance.now. Please use a modern version."),Se||globalThis.crypto&&"object"==typeof globalThis.crypto.subtle||We(!1,"This engine doesn't support crypto.subtle. Please use a modern version."),Se||globalThis.crypto&&"function"==typeof globalThis.crypto.getRandomValues||We(!1,"This engine doesn't support crypto.getRandomValues. Please use a modern version."),Ee&&"function"!=typeof process.exit&&We(!1,"This engine doesn't support process.exit. Please use a modern version."),mo.withConfig(/*json-start*/{
  "mainAssemblyName": "Web",
  "resources": {
    "hash": "sha256-BJ4gA4cq/A0CGcEVUFAL8tWSHh/yY9Pbxf2VnJNOtgw=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.6m3kuuo45t.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.uyfhwbfd13.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.uk690jndwb.wasm",
        "hash": "sha256-jmVginf64KdZ8Y+MjzFweNdGBYfIrgnpuFrVTsiOR2U=",
        "cache": "force-cache"
      }
    ],
    "icu": [
      {
        "virtualPath": "icudt_CJK.dat",
        "name": "icudt_CJK.5lgyv9xn0b.dat",
        "hash": "sha256-eZuX0pntrUwNrAmFCMwpxJjFA3/Myi/rW2x9mEZ+Mbg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_EFIGS.dat",
        "name": "icudt_EFIGS.xyuimhy3ww.dat",
        "hash": "sha256-SQcxb+bdx2UXUCU9tFdOWCr4Ctk64xghCnr0JGLWWKQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_no_CJK.dat",
        "name": "icudt_no_CJK.h0en30vv0c.dat",
        "hash": "sha256-T8YllylpxyWp9Aq4AiF+BMAxKXqYyzWB9RA5RqY19vs=",
        "cache": "force-cache"
      }
    ],
    "coreAssembly": [
      {
        "payloadSize": 48720,
        "virtualPath": "System.Runtime.InteropServices.JavaScript.wasm",
        "name": "System.Runtime.InteropServices.JavaScript.ln0sbv5zfu.wasm",
        "hash": "sha256-rzbDI4jPZ8OzoC0AA+xoHEMRw/z91sKY+8aoFqonN1o=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 2754640,
        "virtualPath": "System.Private.CoreLib.wasm",
        "name": "System.Private.CoreLib.fdcb1qkq3e.wasm",
        "hash": "sha256-YxyRlPSnAOFpsNHZ5JS9nz42TkVspVq82EataiSjAnk=",
        "cache": "force-cache"
      }
    ],
    "assembly": [
      {
        "payloadSize": 17488,
        "virtualPath": "Blazor-Analytics.wasm",
        "name": "Blazor-Analytics.t7fbmpcpt9.wasm",
        "hash": "sha256-NwQw9KDydquLYv64on80IgWlTiiSdVhowk0+t3oRIlY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 1691728,
        "virtualPath": "ClosedXML.wasm",
        "name": "ClosedXML.465gtol4a1.wasm",
        "hash": "sha256-LGv108lAwr4OIj3AIMoT1Qao70wNft7XBlH/tulWmvU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 191568,
        "virtualPath": "ClosedXML.Parser.wasm",
        "name": "ClosedXML.Parser.8nvf6d98nf.wasm",
        "hash": "sha256-DAjEfDA5TxTQhreAiGB6tZ/AcEgYmlc1u9IQDY5VPf4=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 130640,
        "virtualPath": "ClosedXML.Report.wasm",
        "name": "ClosedXML.Report.tyl74lcgui.wasm",
        "hash": "sha256-9SKKzRSEybQgIrofQKW3NqLkS9bYodsXc5peUbeQzIE=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4187216,
        "virtualPath": "DocumentFormat.OpenXml.wasm",
        "name": "DocumentFormat.OpenXml.en8gdx7anw.wasm",
        "hash": "sha256-Ni/ZN0waScmu7khjjpKQL0pLnH4At12aty6I/OAQjM8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 367696,
        "virtualPath": "DocumentFormat.OpenXml.Framework.wasm",
        "name": "DocumentFormat.OpenXml.Framework.ksly1jwarx.wasm",
        "hash": "sha256-LBDULEe8sDy4/UbB6p8gmSWpLUaPZHO1MgcwZa1cRhw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 30288,
        "virtualPath": "ExcelNumberFormat.wasm",
        "name": "ExcelNumberFormat.2uwnvs0ix9.wasm",
        "hash": "sha256-BgtlzHk2zBHKIzFujP2j/DxKTiRcdSHn8TL2cYyS/kE=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 549456,
        "virtualPath": "FluentValidation.wasm",
        "name": "FluentValidation.j3pxrxf6hc.wasm",
        "hash": "sha256-T4zD/mF1CwRer6IpH5HixU45bPfuU/5DUOACdyVYKUc=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 402000,
        "virtualPath": "Humanizer.wasm",
        "name": "Humanizer.bj64lcwkxq.wasm",
        "hash": "sha256-6SDrt/nWQhys2JfSfQrpUJ14O7MeA6VSTEdCW68VeiQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 31312,
        "virtualPath": "Microsoft.AspNetCore.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Authorization.qc0j2ao4fw.wasm",
        "hash": "sha256-irLcZQ1fvEMuanD46PGBo2K36nVN70jfLj95UjMYsdk=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 319056,
        "virtualPath": "Microsoft.AspNetCore.Components.wasm",
        "name": "Microsoft.AspNetCore.Components.n3rrcz5g87.wasm",
        "hash": "sha256-8wnfK+4HQe4EIaawS9u8uMD/mOTafg7aeUvVmfAuvWw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 15440,
        "virtualPath": "Microsoft.AspNetCore.Components.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Components.Authorization.6gnlasnjdf.wasm",
        "hash": "sha256-9tPb6E59CCoSSNad/jU2F931kn7Ix9RJD4RlhPH1C8Y=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 42576,
        "virtualPath": "Microsoft.AspNetCore.Components.Forms.wasm",
        "name": "Microsoft.AspNetCore.Components.Forms.0jud7a15qt.wasm",
        "hash": "sha256-zPpbt5cK1kAA1J1Ks3/UjFbipI9zsR/sRKNl5qKfepM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 153168,
        "virtualPath": "Microsoft.AspNetCore.Components.Web.wasm",
        "name": "Microsoft.AspNetCore.Components.Web.dyw3vw14ti.wasm",
        "hash": "sha256-ayfBp0drF2eWIFtIRpLTk+hqGC9ZihuCi6xtp3cf/Ng=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 146000,
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.ikp0bvouu1.wasm",
        "hash": "sha256-ckiLP0A/4aVhys5+4FI/T7ATCr1i8VTkOA5Mq65LSxo=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 79952,
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.i19laq1qda.wasm",
        "hash": "sha256-+lwl+E5gJUPdx3xQUn2L7TgqWxBPz3ScKaNP7cZK1KY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 5200,
        "virtualPath": "Microsoft.AspNetCore.Metadata.wasm",
        "name": "Microsoft.AspNetCore.Metadata.chpof8fnqb.wasm",
        "hash": "sha256-ACo5DKaMffXY0RyKGuGrAZX6Km7+d6WKvhNbo9bsy+U=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 13392,
        "virtualPath": "Microsoft.Extensions.Configuration.wasm",
        "name": "Microsoft.Extensions.Configuration.33otscxss0.wasm",
        "hash": "sha256-L3teSuppaG421feGxyxrCA0Of2dCbx2AxjlZ9T6dAFo=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 23632,
        "virtualPath": "Microsoft.Extensions.Configuration.Binder.wasm",
        "name": "Microsoft.Extensions.Configuration.Binder.jgevjgkz8i.wasm",
        "hash": "sha256-8433TnzY/RdHMSFHzLh+KfCB5nyf+mc0W/IfYmvAvA0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 12880,
        "virtualPath": "Microsoft.Extensions.Configuration.EnvironmentVariables.wasm",
        "name": "Microsoft.Extensions.Configuration.EnvironmentVariables.vaqacjgd1g.wasm",
        "hash": "sha256-WWSvU1JDlu+bkUC9QASXcgI6FBxn6nqhoi+goYlADAM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 9808,
        "virtualPath": "Microsoft.Extensions.Configuration.Json.wasm",
        "name": "Microsoft.Extensions.Configuration.Json.q81lkglnox.wasm",
        "hash": "sha256-it2ZhBMdA01nWq87yJoqpIh2zzD4VlW5qVKw+RKqPzw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 55376,
        "virtualPath": "Microsoft.Extensions.DependencyInjection.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.3z688wg7ki.wasm",
        "hash": "sha256-seKHGSZYg7rbuxH3lTVhWMxHspKjoLJKX+PgGNv359E=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 38480,
        "virtualPath": "Microsoft.Extensions.DependencyModel.wasm",
        "name": "Microsoft.Extensions.DependencyModel.8b6nxlih7z.wasm",
        "hash": "sha256-TOudjT7zOL+V/grkbsVPF8mQnT/ZsdzhASELVL7Wge0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 14928,
        "virtualPath": "Microsoft.Extensions.Diagnostics.wasm",
        "name": "Microsoft.Extensions.Diagnostics.es1aeztafo.wasm",
        "hash": "sha256-jrG0mdpst0h6GpNSYnKL6CGIpoqljOAai75Nx5vueow=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 45136,
        "virtualPath": "Microsoft.Extensions.Http.wasm",
        "name": "Microsoft.Extensions.Http.d0flbmsqzq.wasm",
        "hash": "sha256-PYKWNfb+oLpRwpG7VBvyh+1fTe47kvaQ0KA/sgU8nf0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 5712,
        "virtualPath": "Microsoft.Extensions.Localization.Abstractions.wasm",
        "name": "Microsoft.Extensions.Localization.Abstractions.b2zmeqgqub.wasm",
        "hash": "sha256-/a5GuIgvHJ3nPQg/M+CY+r95mEIuE76UqesU3pTN7TE=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 26192,
        "virtualPath": "Microsoft.Extensions.Logging.wasm",
        "name": "Microsoft.Extensions.Logging.2x8vwseajr.wasm",
        "hash": "sha256-5q7EHM4/AB0n8xIpb35zWoQzR3uYY17zBK3i6dzIGiI=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 8272,
        "virtualPath": "Microsoft.Extensions.Validation.wasm",
        "name": "Microsoft.Extensions.Validation.ldu2yz4khd.wasm",
        "hash": "sha256-SRTMKPr3Z5FePw1q6Wp9LtpCKZ5t4kA7iSPq1TUBwJg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 48208,
        "virtualPath": "Microsoft.JSInterop.wasm",
        "name": "Microsoft.JSInterop.ah4rx9bgw0.wasm",
        "hash": "sha256-knslvzpKnA7X38Z6YX/RyBsGc5Tq/pySXFFzQ0KfZoA=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 11856,
        "virtualPath": "Microsoft.JSInterop.WebAssembly.wasm",
        "name": "Microsoft.JSInterop.WebAssembly.kthmj8ue8z.wasm",
        "hash": "sha256-uHbb5b7vnE0GwS6Qws3ipDRp2E0UWNZ9G2JJrz8Au4U=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 411728,
        "virtualPath": "Microsoft.ML.Core.wasm",
        "name": "Microsoft.ML.Core.rik6aqyl8c.wasm",
        "hash": "sha256-Y/+cs7QybxxAxspNX7zjMI0THIXEuDnGKuHn9nr5EYY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 1557584,
        "virtualPath": "Microsoft.ML.Data.wasm",
        "name": "Microsoft.ML.Data.4xd0ddn5dm.wasm",
        "hash": "sha256-iIjhg/gd1rfDZzy8VaaMVKicoNjxhr883az+EfqmATk=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 45648,
        "virtualPath": "Microsoft.ML.KMeansClustering.wasm",
        "name": "Microsoft.ML.KMeansClustering.wj0jqpolmz.wasm",
        "hash": "sha256-jKdYB8zZwl8MgeDTn6Xc9cht/19/fmyD/WIW4GpXTZ0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 42576,
        "virtualPath": "Microsoft.ML.PCA.wasm",
        "name": "Microsoft.ML.PCA.ebvkx1afjq.wasm",
        "hash": "sha256-iqBut7iLK5Fwk/PjOpPrujvsz1MFMc8IwPeZyV7pXF0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 316496,
        "virtualPath": "Microsoft.ML.StandardTrainers.wasm",
        "name": "Microsoft.ML.StandardTrainers.5u18q59w0u.wasm",
        "hash": "sha256-syYTYj9Ei8+1RZDf3z+ypHtCe6p1pz4PezpZFHih3ro=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 762448,
        "virtualPath": "Microsoft.ML.Transforms.wasm",
        "name": "Microsoft.ML.Transforms.vopg9yjimk.wasm",
        "hash": "sha256-8fiu18RE+d/DuCDHcrt18FgmI4/6Mgi5WB/z/HRRWYY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 79440,
        "virtualPath": "Microsoft.ML.CpuMath.wasm",
        "name": "Microsoft.ML.CpuMath.mphffu33vg.wasm",
        "hash": "sha256-lmHOrCvCIRqBMM1XxzZGxw5bFWSxzkf1vUonIS7Q1ZU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 37456,
        "virtualPath": "Microsoft.ML.DataView.wasm",
        "name": "Microsoft.ML.DataView.wp3rva5w9t.wasm",
        "hash": "sha256-lIJIEoPoMqV2PD8/ncwj1q2D/YT1agWSLQCxxYk/Frg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 6224,
        "virtualPath": "MoreLinq.wasm",
        "name": "MoreLinq.5lcd857jk1.wasm",
        "hash": "sha256-BxME7pq9+ZK+avWFegENe3sn4hAxL88ULTcxkcB1WnU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 699984,
        "virtualPath": "Newtonsoft.Json.wasm",
        "name": "Newtonsoft.Json.w7fgkhfses.wasm",
        "hash": "sha256-9ObPT1MawLOgzvdYvCLPAQ7yzLoUZ1WPE5w5BPo2pRk=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 303184,
        "virtualPath": "QuestPDF.wasm",
        "name": "QuestPDF.xasqyvtp2q.wasm",
        "hash": "sha256-yV/XJam7S+RTw/7saRYV/sADq6wPwiNnJ9VTCPbL8jY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 34384,
        "virtualPath": "RBush.wasm",
        "name": "RBush.tuoy6pi7g8.wasm",
        "hash": "sha256-q6MgO4b7Lzus+LAAyXLvA7a8u3/EPEbNo9Rn/Ix2hys=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 75344,
        "virtualPath": "Serilog.wasm",
        "name": "Serilog.742ctfhz65.wasm",
        "hash": "sha256-tz78gJ98iFVgIOGwe7ydK9s+9R/iHSceJ7GocRztgVU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 28240,
        "virtualPath": "Serilog.Extensions.Logging.wasm",
        "name": "Serilog.Extensions.Logging.szx2utty1s.wasm",
        "hash": "sha256-mNpR27PltK5RcIdzWH+061ACgosjl4/rw2B/ue7+CPk=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 8272,
        "virtualPath": "Serilog.Formatting.Compact.wasm",
        "name": "Serilog.Formatting.Compact.878z0c11e7.wasm",
        "hash": "sha256-RBTQIvZK5hl4dhHNk68i0dt2Si4KYKR5sunxAq6DFOc=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 71248,
        "virtualPath": "Serilog.Settings.Configuration.wasm",
        "name": "Serilog.Settings.Configuration.yztjm1nugf.wasm",
        "hash": "sha256-YnfxIDIuLHCe9Rx49QlSsmZF5zpZqxLsW+XuSchWF8o=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 19024,
        "virtualPath": "Serilog.Sinks.BrowserConsole.wasm",
        "name": "Serilog.Sinks.BrowserConsole.iiuaqwf4yh.wasm",
        "hash": "sha256-Bn2IwUzg31Db4zoFC2hOfNegwvwxnJiZ+ryzxkRyky4=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 12368,
        "virtualPath": "Serilog.Sinks.BrowserHttp.wasm",
        "name": "Serilog.Sinks.BrowserHttp.rxgvkwpmhf.wasm",
        "hash": "sha256-gXQFxKSzKZHEygaC0m5ksZL56gtP8aCnu0WYylMNfQI=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 13904,
        "virtualPath": "Serilog.Sinks.PeriodicBatching.wasm",
        "name": "Serilog.Sinks.PeriodicBatching.zm0r30gxjb.wasm",
        "hash": "sha256-+17GTZL0GYQU3GH/krKWVOsDf780x88tOFwRZfVx5dQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 1135184,
        "virtualPath": "SixLabors.Fonts.wasm",
        "name": "SixLabors.Fonts.iz63g7x2l2.wasm",
        "hash": "sha256-HmLhPukFj5F8bm1AIkORPaRFFPIIZaKmaXY52rtb1Ds=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 61008,
        "virtualPath": "SkiaSharp.wasm",
        "name": "SkiaSharp.d00a0vyoix.wasm",
        "hash": "sha256-jRrdn0s7TpdpXB8wD00NOVEIbKLuhG9Tpe55qXY8AE0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 36432,
        "virtualPath": "SkiaSharp.Views.Blazor.wasm",
        "name": "SkiaSharp.Views.Blazor.fb7xtnemcm.wasm",
        "hash": "sha256-S/zzQMHuT4lOw33sTtC5qbWKXKBW/u1/V6kdrBNziyU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 174160,
        "virtualPath": "System.CodeDom.wasm",
        "name": "System.CodeDom.z3g0ytruc4.wasm",
        "hash": "sha256-cYj1MDQY70XbjEz648T5KBw/3dC1STK3G1kyhfpuepc=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 76368,
        "virtualPath": "System.IO.Packaging.wasm",
        "name": "System.IO.Packaging.5oqffcroxz.wasm",
        "hash": "sha256-xQP1+lgrTkMJGXekxHAb4LprCOPx6dIIUE7Wv6xm/vw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 247376,
        "virtualPath": "System.Linq.Dynamic.Core.wasm",
        "name": "System.Linq.Dynamic.Core.hhsvivqloy.wasm",
        "hash": "sha256-C1CP17yev2rIvnfo/+4/NwsOrr2Ejg3d3i5xmdltfPU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 63568,
        "virtualPath": "System.Numerics.Tensors.wasm",
        "name": "System.Numerics.Tensors.k73pbh0qqs.wasm",
        "hash": "sha256-gCs9w7ELqAkyaBap+WvAzELqllaxqj991F059ezAtx0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 12880,
        "virtualPath": "Toolbelt.Blazor.HeadElement.wasm",
        "name": "Toolbelt.Blazor.HeadElement.br03s9xy4p.wasm",
        "hash": "sha256-lxbtVpLaeMNLHV4P9mDtIQA2WmH6zp5srhRMl44WbUA=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 11344,
        "virtualPath": "Toolbelt.Blazor.HeadElement.Abstractions.wasm",
        "name": "Toolbelt.Blazor.HeadElement.Abstractions.0lpqxlq4k2.wasm",
        "hash": "sha256-BNlcuWnx5B5iTPhQIP6LUULAexfpi4ZAw8QUSV/Sxyo=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 31312,
        "virtualPath": "Toolbelt.Blazor.HeadElement.Services.wasm",
        "name": "Toolbelt.Blazor.HeadElement.Services.fsh6s84ggz.wasm",
        "hash": "sha256-cWTcYKpeIbyITyYekLJHa5zDO5nb1CVwEARApNBSFDQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 33872,
        "virtualPath": "Toolbelt.Blazor.HotKeys2.wasm",
        "name": "Toolbelt.Blazor.HotKeys2.023s2llz15.wasm",
        "hash": "sha256-eMQKJ303LBwKmXze4MB2b971LxKe7H9zIqB6aS98WG8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 255568,
        "virtualPath": "Microsoft.CSharp.wasm",
        "name": "Microsoft.CSharp.36emv3zny7.wasm",
        "hash": "sha256-O0zv1zY4iXUrq6VQhgGrxh/QXFKaJCh2u5fm6qC2HSY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 7248,
        "virtualPath": "Microsoft.Extensions.Configuration.Abstractions.wasm",
        "name": "Microsoft.Extensions.Configuration.Abstractions.i5azdi621g.wasm",
        "hash": "sha256-JrPNf/DwVXxksMDGUJCBoSOVJVekrSVULnVh2qiETGo=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 30800,
        "virtualPath": "Microsoft.Extensions.DependencyInjection.Abstractions.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.Abstractions.dydclq461e.wasm",
        "hash": "sha256-pW6SflY3p04i0PNHQm1ZzmFSWiaAk/56yJnVnCDWpj0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 8272,
        "virtualPath": "Microsoft.Extensions.Diagnostics.Abstractions.wasm",
        "name": "Microsoft.Extensions.Diagnostics.Abstractions.qz20gv1zfo.wasm",
        "hash": "sha256-2jQ7jiew9q+5Ke8i4fl+RClN1kk9u0ejlFTCHyIWg4Q=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 5200,
        "virtualPath": "Microsoft.Extensions.Hosting.Abstractions.wasm",
        "name": "Microsoft.Extensions.Hosting.Abstractions.wgrd77yf8o.wasm",
        "hash": "sha256-UI/VpVCSQq1k6851I2nIRybFHWT0BJkz8pnAhtB+6fs=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 32336,
        "virtualPath": "Microsoft.Extensions.Logging.Abstractions.wasm",
        "name": "Microsoft.Extensions.Logging.Abstractions.xxtvm8osmr.wasm",
        "hash": "sha256-q+AlFvP0/1TF4IEZEI+8HI643Rbu858v+8wy+0j9AAw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 27216,
        "virtualPath": "Microsoft.Extensions.Options.wasm",
        "name": "Microsoft.Extensions.Options.ogydisgv3j.wasm",
        "hash": "sha256-SA7FX0mwle7iS09YUz07/cy+nIOkJGhxEhyOE1hw3Bg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 11344,
        "virtualPath": "Microsoft.Extensions.Primitives.wasm",
        "name": "Microsoft.Extensions.Primitives.1cw8f4prw0.wasm",
        "hash": "sha256-T1vljupCAO//4Au7oNib235dVE8HSLpLlZVTjVV5Gtg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4176,
        "virtualPath": "System.Buffers.wasm",
        "name": "System.Buffers.xpkgrof6q3.wasm",
        "hash": "sha256-G2hDV+aMcb2IPAi4V7SMFKiyZsv4Nb6PdGQn0+bWxq8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 26192,
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.dx455stzji.wasm",
        "hash": "sha256-MU1AcsoAFco92L3s2+f3L18bNCYtdmYx4UjveRrsv/0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 95312,
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.4b6e6l57kz.wasm",
        "hash": "sha256-kJ5dlV+MdHrtxUd0IzFhKHKY8TUgZ+J/kwreCMBph3A=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 16464,
        "virtualPath": "System.Collections.NonGeneric.wasm",
        "name": "System.Collections.NonGeneric.ep8sn0ly65.wasm",
        "hash": "sha256-EmLcPId+yj4RxV+Kn8epnmCaqHcfZJFFFiVzZQsmhS8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 18000,
        "virtualPath": "System.Collections.Specialized.wasm",
        "name": "System.Collections.Specialized.679qca5ikz.wasm",
        "hash": "sha256-EnVs9ZzonfZoKiXg6jYgNeWcov2aWT0IqQcbXIcUgDg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 55376,
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.csyvous51v.wasm",
        "hash": "sha256-JO5ocAuCc9+I6Agp+cq1MsHPz+Qufw3qMTirt92tK4c=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 37968,
        "virtualPath": "System.ComponentModel.Annotations.wasm",
        "name": "System.ComponentModel.Annotations.undkdavwjt.wasm",
        "hash": "sha256-1vjnAw5NWCPbRpFqUJB/ROKwUq7LtdFDaPH2deolm9Y=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 13392,
        "virtualPath": "System.ComponentModel.Primitives.wasm",
        "name": "System.ComponentModel.Primitives.03zvfgvoqa.wasm",
        "hash": "sha256-pJ2zRETLYyXZSYyusrvvx8qJIB2dd9hzlV63b4PG/uU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 143440,
        "virtualPath": "System.ComponentModel.TypeConverter.wasm",
        "name": "System.ComponentModel.TypeConverter.l24x4dofze.wasm",
        "hash": "sha256-3aoyRUL3il04fycT1siXoSOiiIV5s6bQ/27VxccaPp0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.e3b3u6pg6u.wasm",
        "hash": "sha256-6qCtpNJZ6jgI3gK1bcEk+vfYlmy0b+MKD4VdS2Czv84=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 15952,
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.vvfbmjsvsi.wasm",
        "hash": "sha256-K6eZSqd0UMM+/Z84yegmlJgHTTxm7JwXuGUKV4rhUlw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4176,
        "virtualPath": "System.Core.wasm",
        "name": "System.Core.t6tnvkigcb.wasm",
        "hash": "sha256-e53EUPgKw2vUiPP4k9m6w1MDzGSla5436cpr+BcdORQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 481360,
        "virtualPath": "System.Data.Common.wasm",
        "name": "System.Data.Common.spuhor20km.wasm",
        "hash": "sha256-wDnNxzIbMBIDBkyv1G47Dr115HU9J3kPsnCa/cQoCu8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Diagnostics.Debug.wasm",
        "name": "System.Diagnostics.Debug.jkb0e5zjep.wasm",
        "hash": "sha256-bdPDOnY69S3g3+twXlQqod17CiJdTXZYvwsIraIAloM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 155216,
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.6pffd4x8db.wasm",
        "hash": "sha256-OsrO0vve3qwLKHJKPFF+zxQ0SfxB2LP2NVaFQrEfKhM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 6736,
        "virtualPath": "System.Diagnostics.Process.wasm",
        "name": "System.Diagnostics.Process.qez3zwu48r.wasm",
        "hash": "sha256-/sbWJ1KBxS/MMj/HeDywawNdWK7smvlB4PlrGMcbanA=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Diagnostics.Tools.wasm",
        "name": "System.Diagnostics.Tools.x10a2axpom.wasm",
        "hash": "sha256-xRiaRYAd9/d1DhPeTFtHdyTp1yp6H3LVFf13Fbnx7OI=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 25680,
        "virtualPath": "System.Diagnostics.TraceSource.wasm",
        "name": "System.Diagnostics.TraceSource.mh4cwwmwvz.wasm",
        "hash": "sha256-uqHfjVqLx0FjMNos42FKK8hBpoeuAcVnpvnFb+PxZ6k=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 37456,
        "virtualPath": "System.Drawing.Primitives.wasm",
        "name": "System.Drawing.Primitives.2gbkdy1n4y.wasm",
        "hash": "sha256-8jtIC95heQlLm7YPaQx+VFmqifFCUFmpymE68ZZtptA=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Drawing.wasm",
        "name": "System.Drawing.v57mcc1hsz.wasm",
        "hash": "sha256-PabjD65L8n7SF1ZriVDRC6ty3AlK4AXSExdLyfUItxY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 6224,
        "virtualPath": "System.IO.Compression.Brotli.wasm",
        "name": "System.IO.Compression.Brotli.letl4ah30e.wasm",
        "hash": "sha256-47vs6YsmfcqHlJmDOn2NvuAxuWtxKPuhhIg/PgP4Jr4=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 9808,
        "virtualPath": "System.IO.Compression.ZipFile.wasm",
        "name": "System.IO.Compression.ZipFile.qw8m12w7g7.wasm",
        "hash": "sha256-ZoC/I6n6MzWInuYu1WqD8GwwiUwKf/Ans2eKR1hJDk4=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 139344,
        "virtualPath": "System.IO.Compression.wasm",
        "name": "System.IO.Compression.h649sgf4ao.wasm",
        "hash": "sha256-l7aMZBRvYfj2vwxfpEArEf26xu0Xaez9vjkpnvJ857E=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.IO.FileSystem.wasm",
        "name": "System.IO.FileSystem.hmsopzo2ge.wasm",
        "hash": "sha256-6csU3lr+QC1Wmi25a8iga+2Fi+u3sMkn7oF/eJDAE8E=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 24656,
        "virtualPath": "System.IO.MemoryMappedFiles.wasm",
        "name": "System.IO.MemoryMappedFiles.7yhbkd8tdl.wasm",
        "hash": "sha256-YSjGJR7KNhe9mwEde6CgtsXy+sI/+3MIiLv5m11FDOM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 5712,
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.yeihgeljvz.wasm",
        "hash": "sha256-hnDOQNTIH/aa1TzCygGWtOIjPNT7her4x77kuTUV3To=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 446544,
        "virtualPath": "System.Linq.Expressions.wasm",
        "name": "System.Linq.Expressions.rleairlcxg.wasm",
        "hash": "sha256-kamjpoppizXV+67LcKgex6I93SpKCo5neRzN6v5V/Y8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 7248,
        "virtualPath": "System.Linq.Parallel.wasm",
        "name": "System.Linq.Parallel.cwsr8zcrni.wasm",
        "hash": "sha256-9ECmSs1qB6Som+oBb1gwxN5O1kK5M3CjAf3sO2WsxJc=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 56912,
        "virtualPath": "System.Linq.Queryable.wasm",
        "name": "System.Linq.Queryable.sygcjpelfc.wasm",
        "hash": "sha256-5PfmmhKtivs443wM3LGt06KqPf3JVzAG85IePEsR2cM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 173648,
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.0k0m5pwjbk.wasm",
        "hash": "sha256-sGparnMfqpfLHvTYdW8ZuZBWuaOXtezgtUSZG14Qwgg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 14928,
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.7mkzp1u5jp.wasm",
        "hash": "sha256-4S85cxVIi764fQXDEq22fPCONhG/BN9Tzd9AEzwS2Hs=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 185936,
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.usa8x0gi1e.wasm",
        "hash": "sha256-ril+iA054H0EYEhPducN5mz5+qvOtWxPJ0x8S1xEnXM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 7760,
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.9nhozrclla.wasm",
        "hash": "sha256-q0Excp5XSHLdxmNT3CcrXcpIEBjhkpgkgH8bkEuFitY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 6224,
        "virtualPath": "System.Net.Requests.wasm",
        "name": "System.Net.Requests.gu66tsyftf.wasm",
        "hash": "sha256-2IqbRoFm3zLzhGe3QNcXUMg7GE2lbC0BLrDbFFuqXuk=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Net.WebHeaderCollection.wasm",
        "name": "System.Net.WebHeaderCollection.qvzmckkb5w.wasm",
        "hash": "sha256-2kRos+3AkwzDsSpuH9DL2hr9HPEEs8j9lebaqvRGw4w=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Numerics.Vectors.wasm",
        "name": "System.Numerics.Vectors.x92n761s8l.wasm",
        "hash": "sha256-u0Wy1mZTNlLNB0APmEXoQNN8cBpWzc3ocLXLNVf7Vyc=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 16464,
        "virtualPath": "System.ObjectModel.wasm",
        "name": "System.ObjectModel.avb04lhdw5.wasm",
        "hash": "sha256-FoTDjQ9FPK9ozF7R68m+/01zrZ97pqG5nyf9JvfXH+U=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Private.DataContractSerialization.wasm",
        "name": "System.Private.DataContractSerialization.tfeybvnlbh.wasm",
        "hash": "sha256-mfTggEgA7z0hM/iwZzTiKvgw2gGLFJRZxkmXY6vzZy4=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 72784,
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.wytelht1jt.wasm",
        "hash": "sha256-OLiTs6ack05ij4pqGJlOCaZ+MCqa+NyDw54S8aYoQE8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 45648,
        "virtualPath": "System.Private.Xml.Linq.wasm",
        "name": "System.Private.Xml.Linq.z99larnqhz.wasm",
        "hash": "sha256-N6iIlk39BHRgoWURKU2a/pmzc+iFuCq4UT0aAxcJWqQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 1327696,
        "virtualPath": "System.Private.Xml.wasm",
        "name": "System.Private.Xml.kh9jm9gapg.wasm",
        "hash": "sha256-yNC5bJSZZDEACssVOZ8GM+pnpvSLaRkUQqVABopL8XA=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Reflection.Emit.ILGeneration.wasm",
        "name": "System.Reflection.Emit.ILGeneration.yg0n28vngf.wasm",
        "hash": "sha256-InR/3W9Vq5ww6et+c9fItesWAkCnmuk5rle4xKwtd9o=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Reflection.Emit.Lightweight.wasm",
        "name": "System.Reflection.Emit.Lightweight.dkwd2x01ka.wasm",
        "hash": "sha256-HYwWqZloNnJBl7ApuvpYkLNf3Ow+LQ3GbVzF5OJvxn0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 5200,
        "virtualPath": "System.Reflection.Emit.wasm",
        "name": "System.Reflection.Emit.6ejrimubht.wasm",
        "hash": "sha256-Eluvu6znuoPcNTgFgRvvWfcoBgruXf0L3kPwEb3JAcQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 71248,
        "virtualPath": "System.Reflection.Metadata.wasm",
        "name": "System.Reflection.Metadata.28jqwoazhm.wasm",
        "hash": "sha256-Ljxnmo3oqw7+HKdZP7B1SJHqbd3N4pwW+uxIZokasCQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Reflection.Primitives.wasm",
        "name": "System.Reflection.Primitives.sr39afldzp.wasm",
        "hash": "sha256-WPHCRvHZZa0dZ20iiUINLcLT41K+zNqqX5IG6yC9KLw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Resources.ResourceManager.wasm",
        "name": "System.Resources.ResourceManager.mnfhtsq9ra.wasm",
        "hash": "sha256-fokHw+uEY3rt60aYSbqOyfu1UPqqs0XpAY5iRgFSx20=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Runtime.CompilerServices.Unsafe.wasm",
        "name": "System.Runtime.CompilerServices.Unsafe.hivufs773i.wasm",
        "hash": "sha256-CYqYj5R5y3wp43wJMQhAbTf3Ecl3iCe+2TCSvB6LYlQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Runtime.Extensions.wasm",
        "name": "System.Runtime.Extensions.dxwfp61vbc.wasm",
        "hash": "sha256-j0ogaWb5QPjzP8Ktt6ZaxaYqtxXCMHEdPlGOwX38aoQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Runtime.InteropServices.RuntimeInformation.wasm",
        "name": "System.Runtime.InteropServices.RuntimeInformation.s4lfg6ytlz.wasm",
        "hash": "sha256-RJ/MMQ6Nm3WSOah+LLmpwk4bz3sgAOG6szhhAmhm784=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Runtime.InteropServices.wasm",
        "name": "System.Runtime.InteropServices.djknf8zwog.wasm",
        "hash": "sha256-uYblw1dKeBgndmfyxjqEamA37CTsGA7pTGw5knSBuRI=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Runtime.Intrinsics.wasm",
        "name": "System.Runtime.Intrinsics.mdfqnc6itt.wasm",
        "hash": "sha256-piiE/1ZzjVn+Hs/o1zNT2YDCcW0tqH+qW2R8zBW2Q8Y=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 129104,
        "virtualPath": "System.Runtime.Numerics.wasm",
        "name": "System.Runtime.Numerics.c60cektce2.wasm",
        "hash": "sha256-HAaGbURiZRTKL9frWlr3kgqeSbW6hafkjRthjlpWRIE=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 7760,
        "virtualPath": "System.Runtime.Serialization.Formatters.wasm",
        "name": "System.Runtime.Serialization.Formatters.w5m1xxc6pa.wasm",
        "hash": "sha256-jLTW8gUVY2bZHEQNOSCy6n5l5HCgS76crb/lDplozzU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 5712,
        "virtualPath": "System.Runtime.Serialization.Primitives.wasm",
        "name": "System.Runtime.Serialization.Primitives.tliay60gne.wasm",
        "hash": "sha256-T0mu6hFzVIXUemUxDJs8Qu09sYui2odYmiblzwpxii8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Runtime.Serialization.Xml.wasm",
        "name": "System.Runtime.Serialization.Xml.kf1lpe8cs9.wasm",
        "hash": "sha256-gNTXU8f+3PqOd15gcoacq+88LbBXHStCRJtneGS0cgY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 15440,
        "virtualPath": "System.Runtime.wasm",
        "name": "System.Runtime.whbh5dvqua.wasm",
        "hash": "sha256-dD0y68/fI3UjcL3SkEaobAG5CLlxYmUJJ/egUDXdAns=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 11344,
        "virtualPath": "System.Security.Claims.wasm",
        "name": "System.Security.Claims.7x5vxiszwk.wasm",
        "hash": "sha256-RQe0y1sNqIXV8TKQNlhThKGRl6kyddrioE9cZ31nzuM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 32848,
        "virtualPath": "System.Security.Cryptography.wasm",
        "name": "System.Security.Cryptography.0jxzr1u844.wasm",
        "hash": "sha256-fBOSd+ZeVeEI4DP2RT5m5CjzMLyBhjx8lyk4d2aeLqk=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Text.Encoding.Extensions.wasm",
        "name": "System.Text.Encoding.Extensions.bun8gqptxl.wasm",
        "hash": "sha256-s7LPtG643q5Q6zfHk8sRvgIHjgWc0fBYayJZutqgmnE=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 30800,
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.ijf0asmnsf.wasm",
        "hash": "sha256-rqjFLrnk9t9yR/6PlZ/0P/bjFQvI5AI4n7JLCuBOsgM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 461392,
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.uxrzqzb34w.wasm",
        "hash": "sha256-zlHT/c7nrFuniTx3IdM/H27odqT/Hy10/J8IESD9/xE=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 267856,
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.oicci92g14.wasm",
        "hash": "sha256-oP+m44SH7qiqwGrteSsKRxH8xv9z0xESf9Qwn0hZcTw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 25168,
        "virtualPath": "System.Threading.Channels.wasm",
        "name": "System.Threading.Channels.zys4sywszk.wasm",
        "hash": "sha256-q8+uy4IQDzRjQcHN0bhNPWx7f/irNx6MJ8/MhuLERN8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Threading.Tasks.Extensions.wasm",
        "name": "System.Threading.Tasks.Extensions.dd8odn78a9.wasm",
        "hash": "sha256-YZnMowtiSFxEaGcPrwnUa3+sgYfaJJTeNEpWeHrXDl0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 27216,
        "virtualPath": "System.Threading.Tasks.Parallel.wasm",
        "name": "System.Threading.Tasks.Parallel.x8n424quar.wasm",
        "hash": "sha256-T79FatQLzIXQWqHy15SvMJinSAzTdAUAbmZ8FMUZj5E=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Threading.Tasks.wasm",
        "name": "System.Threading.Tasks.1w26d3v35a.wasm",
        "hash": "sha256-3+JTZ7o0BLyNJJzEoSmoFshCN3iuCnjKBZeeCdFEYxQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Threading.Timer.wasm",
        "name": "System.Threading.Timer.ga6jfq15v1.wasm",
        "hash": "sha256-mA0sxiTxlt9yMQxvB5e9Q4B8m/MQ8dB0dYGtLEFOLJk=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Threading.wasm",
        "name": "System.Threading.6dd60vn9my.wasm",
        "hash": "sha256-1uqo0M0AqGCXHEoU+5xMvQ42MBnTmKNr32/lQogr/Cg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4176,
        "virtualPath": "System.Xml.Linq.wasm",
        "name": "System.Xml.Linq.6jrwceho2h.wasm",
        "hash": "sha256-Yue7a3j8L0A300dI+FSbE9aWjzz0C7n1n3cYfwSb/as=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 5200,
        "virtualPath": "System.Xml.ReaderWriter.wasm",
        "name": "System.Xml.ReaderWriter.rvncuw4fvp.wasm",
        "hash": "sha256-52FjiUgLdE1LQh2qjiffDOyIZXRjMCKuSVL0wExMz0s=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.Xml.XDocument.wasm",
        "name": "System.Xml.XDocument.z7qs0r0jjq.wasm",
        "hash": "sha256-QYDSPHiFkh+KNzBVkEy7ZwTGt0X51TQJPxbEQkxyhrw=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 4688,
        "virtualPath": "System.wasm",
        "name": "System.n1lgwvdeih.wasm",
        "hash": "sha256-deV0DUTbY0DlBTfQncbI/Sg+Eb7dON2nXbyolKeyvTM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 17488,
        "virtualPath": "netstandard.wasm",
        "name": "netstandard.3gdhtlnv17.wasm",
        "hash": "sha256-rcAL/WyPnnzMuyufwZyYV+8LcALjitwuCKAd/lI4gYk=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 527440,
        "virtualPath": "CommonComponents.wasm",
        "name": "CommonComponents.pczvycdlxg.wasm",
        "hash": "sha256-ueYtnAj2Vx9UW9gemE97BlIsUtjIgmw9UaLXuY1Oio4=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 23632,
        "virtualPath": "SharedComponents.wasm",
        "name": "SharedComponents.1c898j7gkv.wasm",
        "hash": "sha256-cTYb63kPrq0KmY986vtLaiqYZ0W/A7n2/JIy/Gw30sQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 298064,
        "virtualPath": "SharedModels.wasm",
        "name": "SharedModels.u0w5qmqehb.wasm",
        "hash": "sha256-XsOEAa0nW8wr1MbqSbhzU60Cq574J6klXnEtl+RoDOI=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 20560,
        "virtualPath": "Web.wasm",
        "name": "Web.193jnwqd2t.wasm",
        "hash": "sha256-NW4W9+MYW8wiUKCMyEvstXgL520yZt1LD+5tJWts/Uc=",
        "cache": "force-cache"
      }
    ],
    "lazyAssembly": [
      {
        "payloadSize": 460368,
        "virtualPath": "AIDemoComponents.wasm",
        "name": "AIDemoComponents.0568oq9gah.wasm",
        "hash": "sha256-7N7UANBsZCfxNhl80o4BzgOQLuHHJWWvXvmsYmXlZec=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 61008,
        "virtualPath": "BaseComponents.wasm",
        "name": "BaseComponents.mxmqb59zwt.wasm",
        "hash": "sha256-ZpqfyOEWnXHF/K6MbJBdjOOrw96pk2d+PfcUI4LoZ9k=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 1188432,
        "virtualPath": "BlazorDemoComponents.wasm",
        "name": "BlazorDemoComponents.fxd43scatj.wasm",
        "hash": "sha256-gzK2Px+L64d4UMi5ZMmCD2bNY5LUCr3t0rQ8AHvVxHY=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 120912,
        "virtualPath": "CachingDemoComponents.wasm",
        "name": "CachingDemoComponents.43s3sdcsc9.wasm",
        "hash": "sha256-8GA+syFLUJEGcVUFjHGPFycHnXrGfVYv6ZgHLcY2aIc=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 76368,
        "virtualPath": "DatabaseDemoComponents.wasm",
        "name": "DatabaseDemoComponents.xjx1m9bvog.wasm",
        "hash": "sha256-45XzRtpBBKuuepf2ZlEJWlL9plKmEBALK0QV41VH2WQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 932944,
        "virtualPath": "DDDDemoComponents.wasm",
        "name": "DDDDemoComponents.hu9k7q3tn2.wasm",
        "hash": "sha256-2e1vCbWGZRwlB2QmAgkl3XRMlV7qA5Den6/kH7zvRAM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 241744,
        "virtualPath": "DependencyInjectionDemoComponents.wasm",
        "name": "DependencyInjectionDemoComponents.ehxgs6lmr9.wasm",
        "hash": "sha256-glHO2FsKhdpRYPHg1oIUPb7J3YMTGM5tW98OXVY/OII=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 331344,
        "virtualPath": "DesignPatternDemoComponents.wasm",
        "name": "DesignPatternDemoComponents.u0549wna8f.wasm",
        "hash": "sha256-kvSURlQLIJ/B8Qwc5odplqeNY7me4zcvrH361OYskgU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 86096,
        "virtualPath": "EFCoreDemoComponents.wasm",
        "name": "EFCoreDemoComponents.t7zuzebmwz.wasm",
        "hash": "sha256-N8cI7hASJkaY+kJkMK5xfNk+ceokceEz4xqxKWDMEgQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 141904,
        "virtualPath": "HTTPClientDemoComponents.wasm",
        "name": "HTTPClientDemoComponents.bmk8sp4f2v.wasm",
        "hash": "sha256-nUVya+0i+qzFjwFL+GWa7YR4UD9Zs7yV1kshhYn60qg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 150096,
        "virtualPath": "JSONDemoComponents.wasm",
        "name": "JSONDemoComponents.lwme093jab.wasm",
        "hash": "sha256-i+BTYFMHDgnDN/RsJssrSgEDc36WoWRBMmBipGs+Mok=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 547920,
        "virtualPath": "LINQDemoComponents.wasm",
        "name": "LINQDemoComponents.91h9474qd7.wasm",
        "hash": "sha256-fX4vMJLTt7EjKxrAAohaBEjpCxfuJjaBTkTe9Wb6UjA=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 318544,
        "virtualPath": "LoggingDemoComponents.wasm",
        "name": "LoggingDemoComponents.cqwnruzfw7.wasm",
        "hash": "sha256-jNdal9yN8ZSnpWkmPgcV4R+agWqEmtECJIJPdKmmBE8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 33360,
        "virtualPath": "MAUIDemoComponents.wasm",
        "name": "MAUIDemoComponents.9xajf09l3r.wasm",
        "hash": "sha256-Df0Pl5JDW1NpThNo3A3AkJmhUtrBxz70TeMtRW2VdhM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 55376,
        "virtualPath": "MCPDemoComponents.wasm",
        "name": "MCPDemoComponents.i3kyxsjwm2.wasm",
        "hash": "sha256-woQ7iZOg3oG0szV4aAf6sqVdbo92SEb7cj5o8Wf1c+Q=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 204880,
        "virtualPath": "MemoryDemoComponents.wasm",
        "name": "MemoryDemoComponents.wvoy0fosz6.wasm",
        "hash": "sha256-hDFE+BM1OmPIyP52QfKo92+8AKzCGhxhTRAxp3ptjVQ=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 38480,
        "virtualPath": "MiddlewareDemoComponents.wasm",
        "name": "MiddlewareDemoComponents.ykfpn33dag.wasm",
        "hash": "sha256-3hEAD7CZ94f9xAxaOjFgCs7tHq0ArV3HBW/zkbrC51w=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 305744,
        "virtualPath": "MLNETDemoComponents.wasm",
        "name": "MLNETDemoComponents.ejanzpgg0b.wasm",
        "hash": "sha256-GpyspEvk4/8fJK0v32izYrn4AfvilmYXX6e4La0Rnk0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 30288,
        "virtualPath": "MSBuildDemoComponents.wasm",
        "name": "MSBuildDemoComponents.s18znn5lp2.wasm",
        "hash": "sha256-gCnd5/xBQwmiAGY06v6qNIoTaIcR6igYI0ACF1Z0GaU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 335440,
        "virtualPath": "NugetDemoComponents.wasm",
        "name": "NugetDemoComponents.wsjo4zauxy.wasm",
        "hash": "sha256-wHWomeL+7juw1sxFH2mscFiGTHEw84lpfSXNxqK8lVg=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 52304,
        "virtualPath": "OOPSDemoComponents.wasm",
        "name": "OOPSDemoComponents.e5po9uwcoh.wasm",
        "hash": "sha256-rolv/RK28mDZV+6YitbrlGKXQPoTFLbPhT3N6GdXFP8=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 46160,
        "virtualPath": "OWASPDemoComponents.wasm",
        "name": "OWASPDemoComponents.8xppwzyepq.wasm",
        "hash": "sha256-fHvjYNmsIuVz0f7Wahpt2/izZxyrZPBAPnevYuK3NNs=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 40528,
        "virtualPath": "PythonDemoComponents.wasm",
        "name": "PythonDemoComponents.s0pui8u9gw.wasm",
        "hash": "sha256-1JPzUBrvtlXHMVn/hGgESYKiZ2lqP0SNqflWI2nBiNU=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 35408,
        "virtualPath": "RegexDemoComponents.wasm",
        "name": "RegexDemoComponents.twesq1at0y.wasm",
        "hash": "sha256-fRgVulGDE9hOB3ok4LQafbkzOWF0Ms2sL4CZZB0v99c=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 532048,
        "virtualPath": "ReportDemoComponents.wasm",
        "name": "ReportDemoComponents.hsrm3bfywb.wasm",
        "hash": "sha256-z2orarKSLNL8AGgqkuMhIBap5yQ/YokoqDUACiW5+K4=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 344656,
        "virtualPath": "SecurityDemoComponents.wasm",
        "name": "SecurityDemoComponents.9kanspqs7x.wasm",
        "hash": "sha256-xypG34m1yWfJCIScXMX3LJNEJz1b1vniKuG3cRzRt8A=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 153680,
        "virtualPath": "SignalRDemoComponents.wasm",
        "name": "SignalRDemoComponents.6gk5u6zdre.wasm",
        "hash": "sha256-OuPPcdNx9t6nONkpZblYGtSxhv/jsQVN2MTjUrRDoIM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 127568,
        "virtualPath": "SOLIDDemoComponents.wasm",
        "name": "SOLIDDemoComponents.qrmzw2jjbg.wasm",
        "hash": "sha256-BraaguoyX/BaouaKeH19VlJqKKs6kpclf1aRbVJ5rYM=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 9808,
        "virtualPath": "TalkDemoComponents.wasm",
        "name": "TalkDemoComponents.0fihkf0ys0.wasm",
        "hash": "sha256-fCctgmWD+jhIp3MLXVqjx9iXTxO2GzAYgxPZTzOI2G0=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 93264,
        "virtualPath": "TDDDemoComponents.wasm",
        "name": "TDDDemoComponents.vppf9mpwl3.wasm",
        "hash": "sha256-84At5rridcoKNTFdAo8Qc6gYk+8pnEnBzzHCsZL4SgI=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 77904,
        "virtualPath": "TestingDemoComponents.wasm",
        "name": "TestingDemoComponents.xbmf9n2c6c.wasm",
        "hash": "sha256-M1wK/K+ZlAi3eA2927xeVLKKDH+/dyX0W2pkq2zzhCo=",
        "cache": "force-cache"
      },
      {
        "payloadSize": 1158224,
        "virtualPath": "WebAPIDemoComponents.wasm",
        "name": "WebAPIDemoComponents.bxf2ugiqvl.wasm",
        "hash": "sha256-4YROS5T+N5+d/5ahu4UaOivMy5iVgg3BTgFpntVxC5Y=",
        "cache": "force-cache"
      }
    ],
    "libraryInitializers": [
      {
        "name": "BlazorWasmPreRendering.Build.9609fvaqjm.lib.module.js"
      }
    ],
    "modulesAfterConfigLoaded": [
      {
        "name": "../BlazorWasmPreRendering.Build.9609fvaqjm.lib.module.js"
      }
    ]
  },
  "debugLevel": 0,
  "appsettings": [
    "../appsettings.json"
  ],
  "globalizationMode": "sharded",
  "extensions": {
    "blazor": {}
  },
  "runtimeConfig": {
    "runtimeOptions": {
      "configProperties": {
        "Microsoft.AspNetCore.Components.Routing.RegexConstraintSupport": false,
        "Toolbelt.Blazor.HotKeys2.OptimizeForWasm": true,
        "Toolbelt.Blazor.HotKeys2.JavaScriptCacheBusting": true,
        "Microsoft.Extensions.DependencyInjection.VerifyOpenGenericServiceTrimmability": true,
        "System.ComponentModel.DefaultValueAttribute.IsSupported": false,
        "System.ComponentModel.Design.IDesignerHost.IsSupported": false,
        "System.ComponentModel.TypeConverter.EnableUnsafeBinaryFormatterInDesigntimeLicenseContextSerialization": false,
        "System.ComponentModel.TypeDescriptor.IsComObjectDescriptorSupported": false,
        "System.Data.DataSet.XmlSerializationIsSupported": false,
        "System.Diagnostics.Debugger.IsSupported": false,
        "System.Diagnostics.Metrics.Meter.IsSupported": true,
        "System.Diagnostics.Tracing.EventSource.IsSupported": true,
        "System.GC.Server": true,
        "System.Globalization.Invariant": false,
        "System.TimeZoneInfo.Invariant": true,
        "System.Linq.Enumerable.IsSizeOptimized": true,
        "System.Net.Http.EnableActivityPropagation": true,
        "System.Net.Http.WasmEnableStreamingResponse": true,
        "System.Net.SocketsHttpHandler.Http3Support": false,
        "System.Reflection.Metadata.MetadataUpdater.IsSupported": false,
        "System.Resources.ResourceManager.AllowCustomResourceTypes": false,
        "System.Resources.UseSystemResourceKeys": true,
        "System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeSupported": true,
        "System.Runtime.InteropServices.BuiltInComInterop.IsSupported": false,
        "System.Runtime.InteropServices.EnableConsumingManagedCodeFromNativeHosting": false,
        "System.Runtime.InteropServices.EnableCppCLIHostActivation": false,
        "System.Runtime.InteropServices.Marshalling.EnableGeneratedComInterfaceComImportInterop": false,
        "System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization": false,
        "System.StartupHookProvider.IsSupported": false,
        "System.Text.Encoding.EnableUnsafeUTF7Encoding": false,
        "System.Text.Json.JsonSerializer.IsReflectionEnabledByDefault": true,
        "System.Threading.Thread.EnableAutoreleasePool": false,
        "Microsoft.AspNetCore.Components.Endpoints.NavigationManager.DisableThrowNavigationException": false,
        "System.Diagnostics.StackTrace.IsLineNumberSupported": false,
        "System.Runtime.CompilerServices.RuntimeFeature.IsMultithreadingSupported": false
      }
    }
  }
}/*json-end*/);export{po as default,mo as dotnet,go as exit};
