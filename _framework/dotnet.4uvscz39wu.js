//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

var e=!1;const t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,4,1,96,0,0,3,2,1,0,10,8,1,6,0,6,64,25,11,11])),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),n=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),r=Symbol.for("wasm promise_control");function i(e,t){let o=null;const n=new Promise((function(n,r){o={isDone:!1,promise:null,resolve:t=>{o.isDone||(o.isDone=!0,n(t),e&&e())},reject:e=>{o.isDone||(o.isDone=!0,r(e),t&&t())}}}));o.promise=n;const i=n;return i[r]=o,{promise:i,promise_control:o}}function s(e){return e[r]}function a(e){e&&function(e){return void 0!==e[r]}(e)||Be(!1,"Promise is not controllable")}const l="__mono_message__",c=["debug","log","trace","warn","info","error"],d="MONO_WASM: ";let u,f,m,g,p,h;function w(e){g=e}function b(e){if(Pe.diagnosticTracing){const t="function"==typeof e?e():e;console.debug(d+t)}}function y(e,...t){console.info(d+e,...t)}function v(e,...t){console.info(e,...t)}function E(e,...t){console.warn(d+e,...t)}function _(e,...t){if(t&&t.length>0&&t[0]&&"object"==typeof t[0]){if(t[0].silent)return;if(t[0].toString)return void console.error(d+e,t[0].toString())}console.error(d+e,...t)}function x(e,t,o){return function(...n){try{let r=n[0];if(void 0===r)r="undefined";else if(null===r)r="null";else if("function"==typeof r)r=r.toString();else if("string"!=typeof r)try{r=JSON.stringify(r)}catch(e){r=r.toString()}t(o?JSON.stringify({method:e,payload:r,arguments:n.slice(1)}):[e+r,...n.slice(1)])}catch(e){m.error(`proxyConsole failed: ${e}`)}}}function j(e,t,o){f=t,g=e,m={...t};const n=`${o}/console`.replace("https://","wss://").replace("http://","ws://");u=new WebSocket(n),u.addEventListener("error",A),u.addEventListener("close",S),function(){for(const e of c)f[e]=x(`console.${e}`,T,!0)}()}function R(e){let t=30;const o=()=>{u?0==u.bufferedAmount||0==t?(e&&v(e),function(){for(const e of c)f[e]=x(`console.${e}`,m.log,!1)}(),u.removeEventListener("error",A),u.removeEventListener("close",S),u.close(1e3,e),u=void 0):(t--,globalThis.setTimeout(o,100)):e&&m&&m.log(e)};o()}function T(e){u&&u.readyState===WebSocket.OPEN?u.send(e):m.log(e)}function A(e){m.error(`[${g}] proxy console websocket error: ${e}`,e)}function S(e){m.debug(`[${g}] proxy console websocket closed: ${e}`,e)}function D(){Pe.preferredIcuAsset=O(Pe.config);let e="invariant"==Pe.config.globalizationMode;if(!e)if(Pe.preferredIcuAsset)Pe.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===Pe.config.globalizationMode||"all"===Pe.config.globalizationMode||"sharded"===Pe.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}Pe.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,Pe.preferredIcuAsset=null}const t="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",o=Pe.config.environmentVariables;if(void 0===o[t]&&e&&(o[t]="1"),void 0===o.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(o.TZ=e)}catch(e){y("failed to detect timezone, will fallback to UTC")}}function O(e){var t;if((null===(t=e.resources)||void 0===t?void 0:t.icu)&&"invariant"!=e.globalizationMode){const t=e.applicationCulture||(ke?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale),o=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(o.length>=1)return o[0].name}else t&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const t=e.split("-")[0];return"en"===t||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(t)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(t)):n="icudt.dat";if(n)for(let e=0;e<o.length;e++){const t=o[e];if(t.virtualPath===n)return t.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const C=class{constructor(e){this.url=e}toString(){return this.url}};async function k(e,t){try{const o="function"==typeof globalThis.fetch;if(Se){const n=e.startsWith("file://");if(!n&&o)return globalThis.fetch(e,t||{credentials:"same-origin"});p||(h=Ne.require("url"),p=Ne.require("fs")),n&&(e=h.fileURLToPath(e));const r=await p.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>r,json:()=>JSON.parse(r),text:()=>{throw new Error("NotImplementedException")}}}if(o)return globalThis.fetch(e,t||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(t){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t},text:()=>{throw t}}}throw new Error("No fetch implementation available")}function I(e){return"string"!=typeof e&&Be(!1,"url must be a string"),!M(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}const U=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,P=/[a-zA-Z]:[\\/]/;function M(e){return Se||Ie?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||P.test(e):U.test(e)}let L,N=0;const $=[],z=[],W=new Map,F={"js-module-threads":!0,"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},B={...F,"js-module-library-initializer":!0},V={...F,dotnetwasm:!0,heap:!0,manifest:!0},q={...B,manifest:!0},H={...B,dotnetwasm:!0},J={dotnetwasm:!0,symbols:!0},Z={...B,dotnetwasm:!0,symbols:!0},Q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=Pe.preferredIcuAsset)}function K(e,t,o){null!=t||(t=[]),Be(1==t.length,`Expect to have one ${o} asset in resources`);const n=t[0];return n.behavior=o,X(n),e.push(n),n}function X(e){V[e.behavior]&&W.set(e.behavior,e)}function Y(e){Be(V[e],`Unknown single asset behavior ${e}`);const t=W.get(e);if(t&&!t.resolvedUrl)if(t.resolvedUrl=Pe.locateFile(t.name),F[t.behavior]){const e=ge(t);e?("string"!=typeof e&&Be(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),t.resolvedUrl=e):t.resolvedUrl=ce(t.resolvedUrl,t.behavior)}else if("dotnetwasm"!==t.behavior)throw new Error(`Unknown single asset behavior ${e}`);return t}function ee(e){const t=Y(e);return Be(t,`Single asset for ${e} not found`),t}let te=!1;async function oe(){if(!te){te=!0,Pe.diagnosticTracing&&b("mono_download_assets");try{const e=[],t=[],o=(e,t)=>{!Z[e.behavior]&&G(e)&&Pe.expected_instantiated_assets_count++,!H[e.behavior]&&G(e)&&(Pe.expected_downloaded_assets_count++,t.push(se(e)))};for(const t of $)o(t,e);for(const e of z)o(e,t);Pe.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...t]).then((()=>{Pe.allDownloadsFinished.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),await Pe.runtimeModuleLoaded.promise;const n=async e=>{const t=await e;if(t.buffer){if(!Z[t.behavior]){t.buffer&&"object"==typeof t.buffer||Be(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof t.resolvedUrl&&Be(!1,"resolvedUrl must be string");const e=t.resolvedUrl,o=await t.buffer,n=new Uint8Array(o);pe(t),await Ue.beforeOnRuntimeInitialized.promise,Ue.instantiate_asset(t,e,n)}}else J[t.behavior]?("symbols"===t.behavior&&(await Ue.instantiate_symbols_asset(t),pe(t)),J[t.behavior]&&++Pe.actual_downloaded_assets_count):(t.isOptional||Be(!1,"Expected asset to have the downloaded buffer"),!H[t.behavior]&&G(t)&&Pe.expected_downloaded_assets_count--,!Z[t.behavior]&&G(t)&&Pe.expected_instantiated_assets_count--)},r=[],i=[];for(const t of e)r.push(n(t));for(const e of t)i.push(n(e));Promise.all(r).then((()=>{Ce||Ue.coreAssetsInMemory.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),Promise.all(i).then((async()=>{Ce||(await Ue.coreAssetsInMemory.promise,Ue.allAssetsInMemory.promise_control.resolve())})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e}))}catch(e){throw Pe.err("Error in mono_download_assets: "+e),e}}}let ne=!1;function re(){if(ne)return;ne=!0;const e=Pe.config,t=[];if(e.assets)for(const t of e.assets)"object"!=typeof t&&Be(!1,`asset must be object, it was ${typeof t} : ${t}`),"string"!=typeof t.behavior&&Be(!1,"asset behavior must be known string"),"string"!=typeof t.name&&Be(!1,"asset name must be string"),t.resolvedUrl&&"string"!=typeof t.resolvedUrl&&Be(!1,"asset resolvedUrl could be string"),t.hash&&"string"!=typeof t.hash&&Be(!1,"asset resolvedUrl could be string"),t.pendingDownload&&"object"!=typeof t.pendingDownload&&Be(!1,"asset pendingDownload could be object"),t.isCore?$.push(t):z.push(t),X(t);else if(e.resources){const o=e.resources;o.wasmNative||Be(!1,"resources.wasmNative must be defined"),o.jsModuleNative||Be(!1,"resources.jsModuleNative must be defined"),o.jsModuleRuntime||Be(!1,"resources.jsModuleRuntime must be defined"),K(z,o.wasmNative,"dotnetwasm"),K(t,o.jsModuleNative,"js-module-native"),K(t,o.jsModuleRuntime,"js-module-runtime"),o.jsModuleDiagnostics&&K(t,o.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,t,o)=>{const n=e;n.behavior=t,o?(n.isCore=!0,$.push(n)):z.push(n)};if(o.coreAssembly)for(let e=0;e<o.coreAssembly.length;e++)n(o.coreAssembly[e],"assembly",!0);if(o.assembly)for(let e=0;e<o.assembly.length;e++)n(o.assembly[e],"assembly",!o.coreAssembly);if(0!=e.debugLevel&&Pe.isDebuggingSupported()){if(o.corePdb)for(let e=0;e<o.corePdb.length;e++)n(o.corePdb[e],"pdb",!0);if(o.pdb)for(let e=0;e<o.pdb.length;e++)n(o.pdb[e],"pdb",!o.corePdb)}if(e.loadAllSatelliteResources&&o.satelliteResources)for(const e in o.satelliteResources)for(let t=0;t<o.satelliteResources[e].length;t++){const r=o.satelliteResources[e][t];r.culture=e,n(r,"resource",!o.coreAssembly)}if(o.coreVfs)for(let e=0;e<o.coreVfs.length;e++)n(o.coreVfs[e],"vfs",!0);if(o.vfs)for(let e=0;e<o.vfs.length;e++)n(o.vfs[e],"vfs",!o.coreVfs);const r=O(e);if(r&&o.icu)for(let e=0;e<o.icu.length;e++){const t=o.icu[e];t.name===r&&n(t,"icu",!1)}if(o.wasmSymbols)for(let e=0;e<o.wasmSymbols.length;e++)n(o.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let t=0;t<e.appsettings.length;t++){const o=e.appsettings[t],n=he(o);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||z.push({name:o,behavior:"vfs",cache:"no-cache",useCredentials:!0})}e.assets=[...$,...z,...t]}async function ie(e){const t=await se(e);return await t.pendingDownloadInternal.response,t.buffer}async function se(e){try{return await ae(e)}catch(t){if(!Pe.enableDownloadRetry)throw t;if(Ie||Se)throw t;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw t;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw t;if(t&&404==t.status)throw t;e.pendingDownloadInternal=void 0,await Pe.allDownloadsQueued.promise;try{return Pe.diagnosticTracing&&b(`Retrying download '${e.name}'`),await ae(e)}catch(t){return e.pendingDownloadInternal=void 0,await new Promise((e=>globalThis.setTimeout(e,100))),Pe.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await ae(e)}}}async function ae(e){for(;L;)await L.promise;try{++N,N==Pe.maxParallelDownloads&&(Pe.diagnosticTracing&&b("Throttling further parallel downloads"),L=i());const t=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const t=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>t,json:()=>JSON.parse(new TextDecoder("utf-8").decode(t)),text:()=>{throw new Error("NotImplementedException")},headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const t=e.loadRemote&&Pe.config.remoteSources?Pe.config.remoteSources:[""];let o;for(let n of t){n=n.trim(),"./"===n&&(n="");const t=le(e,n);e.name===t?Pe.diagnosticTracing&&b(`Attempting to download '${t}'`):Pe.diagnosticTracing&&b(`Attempting to download '${t}' for ${e.name}`);try{e.resolvedUrl=t;const n=fe(e);if(e.pendingDownloadInternal=n,o=await n.response,!o||!o.ok)continue;return o}catch(e){o||(o={ok:!1,url:t,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&Pe.config.ignorePdbLoadErrors;if(o||Be(!1,`Response undefined ${e.name}`),!n){const t=new Error(`download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`);throw t.status=o.status,t}y(`optional download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`)}(e);return t?(J[e.behavior]||(e.buffer=await t.arrayBuffer(),++Pe.actual_downloaded_assets_count),e):e}finally{if(--N,L&&N==Pe.maxParallelDownloads-1){Pe.diagnosticTracing&&b("Resuming more parallel downloads");const e=L;L=void 0,e.promise_control.resolve()}}}function le(e,t){let o;return null==t&&Be(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?o=e.resolvedUrl:(o=""===t?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:t+e.name,o=ce(Pe.locateFile(o),e.behavior)),o&&"string"==typeof o||Be(!1,"attemptUrl need to be path or url string"),o}function ce(e,t){return Pe.modulesUniqueQuery&&q[t]&&(e+=Pe.modulesUniqueQuery),e}let de=0;const ue=new Set;function fe(e){try{e.resolvedUrl||Be(!1,"Request's resolvedUrl must be set");const t=function(e){let t=e.resolvedUrl;if(Pe.loadBootResource){const o=ge(e);if(o instanceof Promise)return o;"string"==typeof o&&(t=o)}const o={};return e.cache?o.cache=e.cache:Pe.config.disableNoCacheFetch||(o.cache="no-cache"),e.useCredentials?o.credentials="include":!Pe.config.disableIntegrityCheck&&e.hash&&(o.integrity=e.hash),Pe.fetch_like(t,o)}(e),o={name:e.name,url:e.resolvedUrl,response:t};return ue.add(e.name),o.response.then((()=>{"assembly"==e.behavior&&Pe.loadedAssemblies.push(e.name),de++,Pe.onDownloadResourceProgress&&Pe.onDownloadResourceProgress(de,ue.size)})),o}catch(t){const o={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(o)}}}const me={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs","js-module-threads":"dotnetjs"};function ge(e){var t;if(Pe.loadBootResource){const o=null!==(t=e.hash)&&void 0!==t?t:"",n=e.resolvedUrl,r=me[e.behavior];if(r){const t=Pe.loadBootResource(r,e.name,n,o,e.behavior);return"string"==typeof t?I(t):t}}}function pe(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function he(e){let t=e.lastIndexOf("/");return t>=0&&t++,e.substring(t)}async function we(e){e&&await Promise.all((null!=e?e:[]).map((e=>async function(e){try{const t=e.name;if(!e.moduleExports){const o=ce(Pe.locateFile(t),"js-module-library-initializer");Pe.diagnosticTracing&&b(`Attempting to import '${o}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */o)}Pe.libraryInitializers.push({scriptName:t,exports:e.moduleExports})}catch(t){E(`Failed to import library initializer '${e}': ${t}`)}}(e))))}async function be(e,t){if(!Pe.libraryInitializers)return;const o=[];for(let n=0;n<Pe.libraryInitializers.length;n++){const r=Pe.libraryInitializers[n];r.exports[e]&&o.push(ye(r.scriptName,e,(()=>r.exports[e](...t))))}await Promise.all(o)}async function ye(e,t,o){try{await o()}catch(o){throw E(`Failed to invoke '${t}' on library initializer '${e}': ${o}`),Xe(1,o),o}}function ve(e,t){if(e===t)return e;const o={...t};return void 0!==o.assets&&o.assets!==e.assets&&(o.assets=[...e.assets||[],...o.assets||[]]),void 0!==o.resources&&(o.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},o.resources)),void 0!==o.environmentVariables&&(o.environmentVariables={...e.environmentVariables||{},...o.environmentVariables||{}}),void 0!==o.runtimeOptions&&o.runtimeOptions!==e.runtimeOptions&&(o.runtimeOptions=[...e.runtimeOptions||[],...o.runtimeOptions||[]]),Object.assign(e,o)}function Ee(e,t){if(e===t)return e;const o={...t};return o.config&&(e.config||(e.config={}),o.config=ve(e.config,o.config)),Object.assign(e,o)}function _e(e,t){if(e===t)return e;const o={...t};return void 0!==o.coreAssembly&&(o.coreAssembly=[...e.coreAssembly||[],...o.coreAssembly||[]]),void 0!==o.assembly&&(o.assembly=[...e.assembly||[],...o.assembly||[]]),void 0!==o.lazyAssembly&&(o.lazyAssembly=[...e.lazyAssembly||[],...o.lazyAssembly||[]]),void 0!==o.corePdb&&(o.corePdb=[...e.corePdb||[],...o.corePdb||[]]),void 0!==o.pdb&&(o.pdb=[...e.pdb||[],...o.pdb||[]]),void 0!==o.jsModuleWorker&&(o.jsModuleWorker=[...e.jsModuleWorker||[],...o.jsModuleWorker||[]]),void 0!==o.jsModuleNative&&(o.jsModuleNative=[...e.jsModuleNative||[],...o.jsModuleNative||[]]),void 0!==o.jsModuleDiagnostics&&(o.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...o.jsModuleDiagnostics||[]]),void 0!==o.jsModuleRuntime&&(o.jsModuleRuntime=[...e.jsModuleRuntime||[],...o.jsModuleRuntime||[]]),void 0!==o.wasmSymbols&&(o.wasmSymbols=[...e.wasmSymbols||[],...o.wasmSymbols||[]]),void 0!==o.wasmNative&&(o.wasmNative=[...e.wasmNative||[],...o.wasmNative||[]]),void 0!==o.icu&&(o.icu=[...e.icu||[],...o.icu||[]]),void 0!==o.satelliteResources&&(o.satelliteResources=function(e,t){if(e===t)return e;for(const o in t)e[o]=[...e[o]||[],...t[o]||[]];return e}(e.satelliteResources||{},o.satelliteResources||{})),void 0!==o.modulesAfterConfigLoaded&&(o.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...o.modulesAfterConfigLoaded||[]]),void 0!==o.modulesAfterRuntimeReady&&(o.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...o.modulesAfterRuntimeReady||[]]),void 0!==o.extensions&&(o.extensions={...e.extensions||{},...o.extensions||{}}),void 0!==o.vfs&&(o.vfs=[...e.vfs||[],...o.vfs||[]]),Object.assign(e,o)}function xe(){const e=Pe.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleWorker:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){Pe.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const t of e.assets){const o={};switch(t.behavior){case"assembly":o.assembly=[t];break;case"pdb":o.pdb=[t];break;case"resource":o.satelliteResources={},o.satelliteResources[t.culture]=[t];break;case"icu":o.icu=[t];break;case"symbols":o.wasmSymbols=[t];break;case"vfs":o.vfs=[t];break;case"dotnetwasm":o.wasmNative=[t];break;case"js-module-threads":o.jsModuleWorker=[t];break;case"js-module-runtime":o.jsModuleRuntime=[t];break;case"js-module-native":o.jsModuleNative=[t];break;case"js-module-diagnostics":o.jsModuleDiagnostics=[t];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${t.behavior} of asset ${t.name}`)}_e(e.resources,o)}}e.debugLevel,e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Ue.diagnosticTracing=Pe.diagnosticTracing=!!e.diagnosticTracing,Ue.waitForDebugger=e.waitForDebugger,Pe.maxParallelDownloads=e.maxParallelDownloads||Pe.maxParallelDownloads,Pe.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:Pe.enableDownloadRetry}let je=!1;async function Re(e){var t;if(je)return void await Pe.afterConfigLoaded.promise;let o;try{if(e.configSrc||Pe.config&&0!==Object.keys(Pe.config).length&&(Pe.config.assets||Pe.config.resources)||(e.configSrc="dotnet.boot.js"),o=e.configSrc,je=!0,o&&(Pe.diagnosticTracing&&b("mono_wasm_load_config"),await async function(e){const t=e.configSrc,o=Pe.locateFile(t);let n=null;void 0!==Pe.loadBootResource&&(n=Pe.loadBootResource("manifest",t,o,"","manifest"));let r,i=null;if(n)if("string"==typeof n)n.includes(".json")?(i=await s(I(n)),r=await Ae(i)):r=(await import(I(n))).config;else{const e=await n;"function"==typeof e.json?(i=e,r=await Ae(i)):r=e.config}else o.includes(".json")?(i=await s(ce(o,"manifest")),r=await Ae(i)):r=(await import(ce(o,"manifest"))).config;function s(e){return Pe.fetch_like(e,{method:"GET",credentials:"include",cache:"no-cache"})}Pe.config.applicationEnvironment&&(r.applicationEnvironment=Pe.config.applicationEnvironment),ve(Pe.config,r)}(e)),xe(),await we(null===(t=Pe.config.resources)||void 0===t?void 0:t.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[Pe.config]),e.onConfigLoaded)try{await e.onConfigLoaded(Pe.config,Le),xe()}catch(e){throw _("onConfigLoaded() failed",e),e}xe(),Pe.afterConfigLoaded.promise_control.resolve(Pe.config)}catch(t){const n=`Failed to load config file ${o} ${t} ${null==t?void 0:t.stack}`;throw Pe.config=e.config=Object.assign(Pe.config,{message:n,error:t,isError:!0}),Xe(1,new Error(n)),t}}function Te(){return!!globalThis.navigator&&(Pe.isChromium||Pe.isFirefox)}async function Ae(e){const t=Pe.config,o=await e.json();t.applicationEnvironment||o.applicationEnvironment||(o.applicationEnvironment=e.headers.get("Blazor-Environment")||e.headers.get("DotNet-Environment")||void 0),o.environmentVariables||(o.environmentVariables={});const n=e.headers.get("DOTNET-MODIFIABLE-ASSEMBLIES");n&&(o.environmentVariables.DOTNET_MODIFIABLE_ASSEMBLIES=n);const r=e.headers.get("ASPNETCORE-BROWSER-TOOLS");return r&&(o.environmentVariables.__ASPNETCORE_BROWSER_TOOLS=r),o}"function"!=typeof importScripts||globalThis.onmessage||(globalThis.dotnetSidecar=!0);const Se="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,De="function"==typeof importScripts,Oe=De&&"undefined"!=typeof dotnetSidecar,Ce=De&&!Oe,ke="object"==typeof window||De&&!Se,Ie=!ke&&!Se;let Ue={},Pe={},Me={},Le={},Ne={},$e=!1;const ze={},We={config:ze},Fe={mono:{},binding:{},internal:Ne,module:We,loaderHelpers:Pe,runtimeHelpers:Ue,diagnosticHelpers:Me,api:Le};function Be(e,t){if(e)return;const o="Assert failed: "+("function"==typeof t?t():t),n=new Error(o);_(o,n),Ue.nativeAbort(n)}function Ve(){return void 0!==Pe.exitCode}function qe(){return Ue.runtimeReady&&!Ve()}function He(){Ve()&&Be(!1,`.NET runtime already exited with ${Pe.exitCode} ${Pe.exitReason}. You can use runtime.runMain() which doesn't exit the runtime.`),Ue.runtimeReady||Be(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function Je(){ke&&(globalThis.addEventListener("unhandledrejection",et),globalThis.addEventListener("error",tt))}let Ze,Qe;function Ge(e){Qe&&Qe(e),Xe(e,Pe.exitReason)}function Ke(e){Ze&&Ze(e||Pe.exitReason),Xe(1,e||Pe.exitReason)}function Xe(t,o){var n,r;const i=o&&"object"==typeof o;t=i&&"number"==typeof o.status?o.status:void 0===t?-1:t;const s=i&&"string"==typeof o.message?o.message:""+o;(o=i?o:Ue.ExitStatus?function(e,t){const o=new Ue.ExitStatus(e);return o.message=t,o.toString=()=>t,o}(t,s):new Error("Exit with code "+t+" "+s)).status=t,o.message||(o.message=s);const a=""+(o.stack||(new Error).stack);try{Object.defineProperty(o,"stack",{get:()=>a})}catch(e){}const l=!!o.silent;if(o.silent=!0,Ve())Pe.diagnosticTracing&&b("mono_exit called after exit");else{try{We.onAbort==Ke&&(We.onAbort=Ze),We.onExit==Ge&&(We.onExit=Qe),ke&&(globalThis.removeEventListener("unhandledrejection",et),globalThis.removeEventListener("error",tt)),Ue.runtimeReady?(Ue.jiterpreter_dump_stats&&Ue.jiterpreter_dump_stats(!1),0===t&&(null===(n=Pe.config)||void 0===n?void 0:n.interopCleanupOnExit)&&Ue.forceDisposeProxies(!0,!0),e&&0!==t&&(null===(r=Pe.config)||void 0===r||r.dumpThreadsOnNonZeroExit)):(Pe.diagnosticTracing&&b(`abort_startup, reason: ${o}`),function(e){Pe.allDownloadsQueued.promise_control.reject(e),Pe.allDownloadsFinished.promise_control.reject(e),Pe.afterConfigLoaded.promise_control.reject(e),Pe.wasmCompilePromise.promise_control.reject(e),Pe.runtimeModuleLoaded.promise_control.reject(e),Ue.dotnetReady&&(Ue.dotnetReady.promise_control.reject(e),Ue.afterInstantiateWasm.promise_control.reject(e),Ue.beforePreInit.promise_control.reject(e),Ue.afterPreInit.promise_control.reject(e),Ue.afterPreRun.promise_control.reject(e),Ue.beforeOnRuntimeInitialized.promise_control.reject(e),Ue.afterOnRuntimeInitialized.promise_control.reject(e),Ue.afterPostRun.promise_control.reject(e))}(o))}catch(e){E("mono_exit A failed",e)}try{l||(function(e,t){if(0!==e&&t){const e=Ue.ExitStatus&&t instanceof Ue.ExitStatus?b:_;"string"==typeof t?e(t):(void 0===t.stack&&(t.stack=(new Error).stack+""),t.message?e(Ue.stringify_as_error_with_stack?Ue.stringify_as_error_with_stack(t.message+"\n"+t.stack):t.message+"\n"+t.stack):e(JSON.stringify(t)))}!Ce&&Pe.config&&(Pe.config.logExitCode?Pe.config.forwardConsoleLogsToWS?R("WASM EXIT "+e):v("WASM EXIT "+e):Pe.config.forwardConsoleLogsToWS&&R())}(t,o),function(e){if(ke&&!Ce&&Pe.config&&Pe.config.appendElementOnExit&&document){const t=document.createElement("label");t.id="tests_done",0!==e&&(t.style.background="red"),t.innerHTML=""+e,document.body.appendChild(t)}}(t))}catch(e){E("mono_exit B failed",e)}Pe.exitCode=t,Pe.exitReason||(Pe.exitReason=o),!Ce&&Ue.runtimeReady&&We.runtimeKeepalivePop()}if(Pe.config&&Pe.config.asyncFlushOnExit&&0===t)throw(async()=>{try{await async function(){try{const e=await import(/*! webpackIgnore: true */"process"),t=e=>new Promise(((t,o)=>{e.on("error",o),e.end("","utf8",t)})),o=t(e.stderr),n=t(e.stdout);let r;const i=new Promise((e=>{r=setTimeout((()=>e("timeout")),1e3)}));await Promise.race([Promise.all([n,o]),i]),clearTimeout(r)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Ye(t,o)}})(),o;Ye(t,o)}function Ye(e,t){if(Ue.runtimeReady&&Ue.nativeExit)try{Ue.nativeExit(e)}catch(e){!Ue.ExitStatus||e instanceof Ue.ExitStatus||E("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!ke)throw Se&&Ne.process?Ne.process.exit(e):Ue.quit&&Ue.quit(e,t),t}function et(e){ot(e,e.reason,"rejection")}function tt(e){ot(e,e.error,"error")}function ot(e,t,o){e.preventDefault();try{t||(t=new Error("Unhandled "+o)),void 0===t.stack&&(t.stack=(new Error).stack),t.stack=t.stack+"",t.silent||(_("Unhandled error:",t),Xe(1,t))}catch(e){}}!function(e){if($e)throw new Error("Loader module already loaded");$e=!0,Ue=e.runtimeHelpers,Pe=e.loaderHelpers,Me=e.diagnosticHelpers,Le=e.api,Ne=e.internal,Object.assign(Le,{INTERNAL:Ne,invokeLibraryInitializers:be}),Object.assign(e.module,{config:ve(ze,{environmentVariables:{}})});const r={mono_wasm_bindings_is_ready:!1,config:e.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"b16286c2284fecf303dbc12a0bb152476d662e44",config:e.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:i(),allDownloadsQueued:i(),allDownloadsFinished:i(),wasmCompilePromise:i(),runtimeModuleLoaded:i(),loadingWorkers:i(),is_exited:Ve,is_runtime_running:qe,assert_runtime_running:He,mono_exit:Xe,createPromiseController:i,getPromiseController:s,assertIsControllablePromise:a,mono_download_assets:oe,resolve_single_asset_path:ee,setup_proxy_console:j,set_thread_prefix:w,installUnhandledErrorHandler:Je,retrieve_asset_download:ie,invokeLibraryInitializers:be,isDebuggingSupported:Te,exceptions:t,simd:n,relaxedSimd:o};Object.assign(Ue,r),Object.assign(Pe,l)}(Fe);let nt,rt,it,st=!1,at=!1;async function lt(e){if(!at){if(at=!0,ke&&Pe.config.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&j("main",globalThis.console,globalThis.location.origin),We||Be(!1,"Null moduleConfig"),Pe.config||Be(!1,"Null moduleConfig.config"),"function"==typeof e){const t=e(Fe.api);if(t.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(We,t),Ee(We,t)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");Ee(We,e)}await async function(e){if(Se){const e=await import(/*! webpackIgnore: true */"process"),t=14;if(e.versions.node.split(".")[0]<t)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${t}. See also https://aka.ms/dotnet-wasm-features`)}const t=/*! webpackIgnore: true */import.meta.url,o=t.indexOf("?");var n;if(o>0&&(Pe.modulesUniqueQuery=t.substring(o)),Pe.scriptUrl=t.replace(/\\/g,"/").replace(/[?#].*/,""),Pe.scriptDirectory=(n=Pe.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",Pe.locateFile=e=>"URL"in globalThis&&globalThis.URL!==C?new URL(e,Pe.scriptDirectory).toString():M(e)?e:Pe.scriptDirectory+e,Pe.fetch_like=k,Pe.out=console.log,Pe.err=console.error,Pe.onDownloadResourceProgress=e.onDownloadResourceProgress,ke&&globalThis.navigator){const e=globalThis.navigator,t=e.userAgentData&&e.userAgentData.brands;t&&t.length>0?Pe.isChromium=t.some((e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand)):e.userAgent&&(Pe.isChromium=e.userAgent.includes("Chrome"),Pe.isFirefox=e.userAgent.includes("Firefox"))}Ne.require=Se?await import(/*! webpackIgnore: true */"module").then((e=>e.createRequire(/*! webpackIgnore: true */import.meta.url))):Promise.resolve((()=>{throw new Error("require not supported")})),void 0===globalThis.URL&&(globalThis.URL=C)}(We)}}async function ct(e){return await lt(e),Ze=We.onAbort,Qe=We.onExit,We.onAbort=Ke,We.onExit=Ge,We.ENVIRONMENT_IS_PTHREAD?async function(){(function(){const e=new MessageChannel,t=e.port1,o=e.port2;t.addEventListener("message",(e=>{var n,r;n=JSON.parse(e.data.config),r=JSON.parse(e.data.monoThreadInfo),st?Pe.diagnosticTracing&&b("mono config already received"):(ve(Pe.config,n),Ue.monoThreadInfo=r,xe(),Pe.diagnosticTracing&&b("mono config received"),st=!0,Pe.afterConfigLoaded.promise_control.resolve(Pe.config),ke&&n.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&Pe.setup_proxy_console("worker-idle",console,globalThis.location.origin)),t.close(),o.close()}),{once:!0}),t.start(),self.postMessage({[l]:{monoCmd:"preload",port:o}},[o])})(),await Pe.afterConfigLoaded.promise,function(){const e=Pe.config;e.assets||Be(!1,"config.assets must be defined");for(const t of e.assets)X(t),Q[t.behavior]&&z.push(t)}(),setTimeout((async()=>{try{await oe()}catch(e){Xe(1,e)}}),0);const e=dt(),t=await Promise.all(e);return await ut(t),We}():async function(){var e;await Re(We),re();const t=dt();(async function(){try{const e=ee("dotnetwasm");await se(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||Be(!1,"Can't load dotnet.native.wasm");const t=await e.pendingDownloadInternal.response,o=t.headers&&t.headers.get?t.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===o)n=await WebAssembly.compileStreaming(t);else{ke&&"application/wasm"!==o&&E('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await t.arrayBuffer();Pe.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Ie?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,Pe.wasmCompilePromise.promise_control.resolve(n)}catch(e){Pe.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout((async()=>{try{D(),await oe()}catch(e){Xe(1,e)}}),0);const o=await Promise.all(t);return await ut(o),await Ue.dotnetReady.promise,await we(null===(e=Pe.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[Fe.api]),Le}()}function dt(){const e=ee("js-module-runtime"),t=ee("js-module-native");if(nt&&rt)return[nt,rt,it];"object"==typeof e.moduleExports?nt=e.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),nt=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof t.moduleExports?rt=t.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),rt=import(/*! webpackIgnore: true */t.resolvedUrl));const o=Y("js-module-diagnostics");return o&&("object"==typeof o.moduleExports?it=o.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),it=import(/*! webpackIgnore: true */o.resolvedUrl))),[nt,rt,it]}async function ut(e){const{initializeExports:t,initializeReplacements:o,configureRuntimeStartup:n,configureEmscriptenStartup:r,configureWorkerStartup:i,setRuntimeGlobals:s,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];s(Fe),t(Fe),c&&c.setRuntimeGlobals(Fe),await n(We),Pe.runtimeModuleLoaded.promise_control.resolve(),l((e=>(Object.assign(We,{ready:e.ready,__dotnet_runtime:{initializeReplacements:o,configureEmscriptenStartup:r,configureWorkerStartup:i,passEmscriptenInternals:a}}),We))).catch((e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize. See also https://aka.ms/dotnet-wasm-features");throw e}))}const ft=new class{withModuleConfig(e){try{return Ee(We,e),this}catch(e){throw Xe(1,e),e}}withOnConfigLoaded(e){try{return Ee(We,{onConfigLoaded:e}),this}catch(e){throw Xe(1,e),e}}withConsoleForwarding(){try{return ve(ze,{forwardConsoleLogsToWS:!0}),this}catch(e){throw Xe(1,e),e}}withExitOnUnhandledError(){try{return ve(ze,{exitOnUnhandledError:!0}),Je(),this}catch(e){throw Xe(1,e),e}}withAsyncFlushOnExit(){try{return ve(ze,{asyncFlushOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withExitCodeLogging(){try{return ve(ze,{logExitCode:!0}),this}catch(e){throw Xe(1,e),e}}withElementOnExit(){try{return ve(ze,{appendElementOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withInteropCleanupOnExit(){try{return ve(ze,{interopCleanupOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withDumpThreadsOnNonZeroExit(){try{return ve(ze,{dumpThreadsOnNonZeroExit:!0}),this}catch(e){throw Xe(1,e),e}}withWaitingForDebugger(e){try{return ve(ze,{waitForDebugger:e}),this}catch(e){throw Xe(1,e),e}}withInterpreterPgo(e,t){try{return ve(ze,{interpreterPgo:e,interpreterPgoSaveDelay:t}),ze.runtimeOptions?ze.runtimeOptions.push("--interp-pgo-recording"):ze.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Xe(1,e),e}}withConfig(e){try{return ve(ze,e),this}catch(e){throw Xe(1,e),e}}withConfigSrc(e){try{return e&&"string"==typeof e||Be(!1,"must be file path or URL"),Ee(We,{configSrc:e}),this}catch(e){throw Xe(1,e),e}}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||Be(!1,"must be directory path"),ve(ze,{virtualWorkingDirectory:e}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariable(e,t){try{const o={};return o[e]=t,ve(ze,{environmentVariables:o}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||Be(!1,"must be dictionary object"),ve(ze,{environmentVariables:e}),this}catch(e){throw Xe(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&Be(!1,"must be boolean"),ve(ze,{diagnosticTracing:e}),this}catch(e){throw Xe(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||Be(!1,"must be number"),ve(ze,{debugLevel:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ve(ze,{applicationArguments:e}),this}catch(e){throw Xe(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ze.runtimeOptions?ze.runtimeOptions.push(...e):ze.runtimeOptions=e,this}catch(e){throw Xe(1,e),e}}withMainAssembly(e){try{return ve(ze,{mainAssemblyName:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Xe(1,e),e}}withApplicationEnvironment(e){try{return ve(ze,{applicationEnvironment:e}),this}catch(e){throw Xe(1,e),e}}withApplicationCulture(e){try{return ve(ze,{applicationCulture:e}),this}catch(e){throw Xe(1,e),e}}withResourceLoader(e){try{return Pe.loadBootResource=e,this}catch(e){throw Xe(1,e),e}}async download(){try{await async function(){lt(We),await Re(We),re(),D(),oe(),await Pe.allDownloadsFinished.promise}()}catch(e){throw Xe(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await ct(We),Fe.api}()),this.instance}catch(e){throw Xe(1,e),e}}async run(){try{return We.config||Be(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Xe(1,e),e}}},mt=Xe,gt=ct;Ie||"function"==typeof globalThis.URL||Be(!1,"This browser/engine doesn't support URL API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),"function"!=typeof globalThis.BigInt64Array&&Be(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),ft.withConfig(/*json-start*/{
  "mainAssemblyName": "Web",
  "resources": {
    "hash": "sha256-8JubN1daMDylr03yHnV/0P/tIdpCUr4Z0yLcOOpC8JA=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.qyr6mqji56.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.2zl32tp6ah.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.cm13qmjkdk.wasm",
        "integrity": "sha256-DDMRK0BWnr7ElFfUCLvs5+NLmG2ei5RaCGLjmMO1Ths=",
        "cache": "force-cache"
      }
    ],
    "icu": [
      {
        "virtualPath": "icudt_CJK.dat",
        "name": "icudt_CJK.tjcz0u77k5.dat",
        "integrity": "sha256-SZLtQnRc0JkwqHab0VUVP7T3uBPSeYzxzDnpxPpUnHk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_EFIGS.dat",
        "name": "icudt_EFIGS.tptq2av103.dat",
        "integrity": "sha256-8fItetYY8kQ0ww6oxwTLiT3oXlBwHKumbeP2pRF4yTc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_no_CJK.dat",
        "name": "icudt_no_CJK.lfu7j35m59.dat",
        "integrity": "sha256-L7sV7NEYP37/Qr2FPCePo5cJqRgTXRwGHuwF5Q+0Nfs=",
        "cache": "force-cache"
      }
    ],
    "coreAssembly": [
      {
        "virtualPath": "System.Runtime.InteropServices.JavaScript.wasm",
        "name": "System.Runtime.InteropServices.JavaScript.uewvbup70t.wasm",
        "integrity": "sha256-1iiw4kXAtZprYY/IOu7BsDdEB9+H5H/JpUwq7Lxak+8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.CoreLib.wasm",
        "name": "System.Private.CoreLib.m8u87mrshd.wasm",
        "integrity": "sha256-6oWRlqTJldiulUYs+oblTeByqlAfV7LO5QdRTgg4H4M=",
        "cache": "force-cache"
      }
    ],
    "assembly": [
      {
        "virtualPath": "Blazor-Analytics.wasm",
        "name": "Blazor-Analytics.4ensyheg7d.wasm",
        "integrity": "sha256-bIvCtkn88pVsOm5DqLGKrhR3kj1VwhhG8KdF1Hk8yLE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ClosedXML.wasm",
        "name": "ClosedXML.6d8ybop3kc.wasm",
        "integrity": "sha256-Jga3CUuNfwvpwnsHZxpWURvivrlrEqnUo9XR5EOLYGc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ClosedXML.Parser.wasm",
        "name": "ClosedXML.Parser.kcd8ka7nog.wasm",
        "integrity": "sha256-bN619zkNwVyqGT2tFn5NMXPDIo3OUVaRYMG/tXRtn+A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ClosedXML.Report.wasm",
        "name": "ClosedXML.Report.fcz87vijt6.wasm",
        "integrity": "sha256-74fGlfMz4HhXehO/K3vaVnzZ53lEmUNw68kwmSzDXVA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DocumentFormat.OpenXml.wasm",
        "name": "DocumentFormat.OpenXml.0gr15g35pw.wasm",
        "integrity": "sha256-MBbZzfjPOSIquvoCx+NxtLYAI63vk0mstqecqcKULfc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DocumentFormat.OpenXml.Framework.wasm",
        "name": "DocumentFormat.OpenXml.Framework.hhtgzs2q6p.wasm",
        "integrity": "sha256-u3MueHh2gEf2RLS+UmJWjOGBzFZ5Gl0WU5GjsX7xBxs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ExcelNumberFormat.wasm",
        "name": "ExcelNumberFormat.zkc9yronjy.wasm",
        "integrity": "sha256-DWrWJf+NGbjj12iqpO+Ufl4YS0bkW1yz6JwjocU15wY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "FluentValidation.wasm",
        "name": "FluentValidation.yuptatg8bv.wasm",
        "integrity": "sha256-1vkL1fNCyvLkWYtavCvKdUZS0BPY392ec7LutoPTps4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "HarfBuzzSharp.wasm",
        "name": "HarfBuzzSharp.4hlg18yscd.wasm",
        "integrity": "sha256-uLEJA5ByBiQq38q3IGoYXLmTF16imsrHhqzqIXEa9Ec=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Humanizer.wasm",
        "name": "Humanizer.0y075xbmmv.wasm",
        "integrity": "sha256-iHkFsQ2zqQtfhrtiRjN28v45pdzEtFoGqoyvxXjE4ik=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Authorization.vwu4jxs9t1.wasm",
        "integrity": "sha256-gXOf4+Ouq1aMaLCXnAZKFD/FYKoBG/TXdjrKApTm5PA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.wasm",
        "name": "Microsoft.AspNetCore.Components.fdqhmn4gzs.wasm",
        "integrity": "sha256-w3H4Pw4XwxYn2jB6K/WgN/aFcPRibIl4D9fRBVC4Mzs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Components.Authorization.rg7hwzv9mg.wasm",
        "integrity": "sha256-9QNSUd5Pby7vi9zx4lFAmLA2TwJcbmVJO9pOK3cPYO4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Forms.wasm",
        "name": "Microsoft.AspNetCore.Components.Forms.aff0lrfe84.wasm",
        "integrity": "sha256-Fj9bglpuaeUZA4JLtqwphjXd2cdY11izQZGZ4zDxfEc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Web.wasm",
        "name": "Microsoft.AspNetCore.Components.Web.js0v0qp6pf.wasm",
        "integrity": "sha256-PTKShvB5TGKIeHNOt8cnDzY69/a6/1VACRsjwg4DUNw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.k5rmmft3ai.wasm",
        "integrity": "sha256-+DR1wrdMWsgPA6TmwelWGY92BgFU7TBmuV4a+oIgXJw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.ifw7d8uph0.wasm",
        "integrity": "sha256-EvN+mjLv9JuVHXj2dEFYgOo4rr1blzK3dLW7z1Ivu7Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Metadata.wasm",
        "name": "Microsoft.AspNetCore.Metadata.2eq98n7xp1.wasm",
        "integrity": "sha256-Jp4amCMCrjiruTde7mc7O7eCnEV/kmufNH4uw4l/ODc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.DotNet.HotReload.WebAssembly.Browser.wasm",
        "name": "Microsoft.DotNet.HotReload.WebAssembly.Browser.v5huyml5sc.wasm",
        "integrity": "sha256-nUOp/kC3zVe2D4ay6RHxrSXcFPp/ckiVhdpJ76khAVE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.wasm",
        "name": "Microsoft.Extensions.Configuration.k9wnvalxec.wasm",
        "integrity": "sha256-GHr2soZguoF1TC3TiwwPZ+Nkyc8zNznWY/UmcLVYUMQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Abstractions.wasm",
        "name": "Microsoft.Extensions.Configuration.Abstractions.9673pk44n5.wasm",
        "integrity": "sha256-7++32afIAoDziFIp0P4uxGU/nSALf8wgXadrzl1bC4U=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Binder.wasm",
        "name": "Microsoft.Extensions.Configuration.Binder.tktgekyl8v.wasm",
        "integrity": "sha256-vfc2bGFGs7MQOKYqiqMkGoarDxgY2MWibxzQY98iF0o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Json.wasm",
        "name": "Microsoft.Extensions.Configuration.Json.q0m0kr40cg.wasm",
        "integrity": "sha256-eo/oa3JT4Cc/LE0otD/LWr4fQN9Vz7QtpfNt8oeccXM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.93q8hlqx98.wasm",
        "integrity": "sha256-68SvqMXFVOSWN1JRqtVRisAmUktQaLjalHIUy4kszEI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.Abstractions.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.Abstractions.31bkbeiutr.wasm",
        "integrity": "sha256-KQifYO/5yvT1e7wVZ2oP51tXgDF0dMLs8QD6HKHCH8A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyModel.wasm",
        "name": "Microsoft.Extensions.DependencyModel.pkiabcs8hj.wasm",
        "integrity": "sha256-dyjqOZrh0fL9qXtviAqzKuApsnl/SKAIMcg5Eusesac=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.wasm",
        "name": "Microsoft.Extensions.Diagnostics.otzyit3keh.wasm",
        "integrity": "sha256-JACJQA/PTsCLH8900torHpUGseUlSx7zmYGihDYYDIg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.Abstractions.wasm",
        "name": "Microsoft.Extensions.Diagnostics.Abstractions.aq4efrgfvw.wasm",
        "integrity": "sha256-jnUANINJX5ej9QFMDcnAYzmPZRJelDzKgzNrIPzaZBo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Http.wasm",
        "name": "Microsoft.Extensions.Http.yokhq3sh2d.wasm",
        "integrity": "sha256-AskUmLuyalReZxaIwGLE6kSjfJcPzD/D06UOjW5F55I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.wasm",
        "name": "Microsoft.Extensions.Logging.kbp3tjzw2l.wasm",
        "integrity": "sha256-EMTeKXuoV5iPM7tDRwy6Zt+osd+YlwZoeL0vV8jgtu0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.Abstractions.wasm",
        "name": "Microsoft.Extensions.Logging.Abstractions.8y3obv3q5r.wasm",
        "integrity": "sha256-AKE1scv/ZpNH81N5Dd4xAoI9aQrXkwNisLFTQJN+3Ds=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Options.wasm",
        "name": "Microsoft.Extensions.Options.9ny166j3vf.wasm",
        "integrity": "sha256-WwxRNZAh8zD9VrqTOj3ReYC10hP5oy++kDqCiyOnUH4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Primitives.wasm",
        "name": "Microsoft.Extensions.Primitives.m84zq78fb1.wasm",
        "integrity": "sha256-yumksFeLjkCb3oD27DyYQPGSrliFAjLg4s8CaDv3WyA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Validation.wasm",
        "name": "Microsoft.Extensions.Validation.o5kzzozb1w.wasm",
        "integrity": "sha256-zKNWe/zGp/E5ibBoimP2IH/do0w9653nb3NKFc7lFnk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.JSInterop.wasm",
        "name": "Microsoft.JSInterop.ci2rb2gklc.wasm",
        "integrity": "sha256-dHgVlWQFAw4nPi5XtQ+x6sDpwjH+LBlFuLtBsPLQHWM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.JSInterop.WebAssembly.wasm",
        "name": "Microsoft.JSInterop.WebAssembly.kivdlry06d.wasm",
        "integrity": "sha256-zIGd0h8/AoN3niSEUSSn1zX71vJ4hqGJZ3UwI1g+UD0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Core.wasm",
        "name": "Microsoft.ML.Core.bxjtgvvl3m.wasm",
        "integrity": "sha256-HyZST47FO+wUJSHgSeHBC2RS1CJEuaEnqzP3Y8DwrHE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Data.wasm",
        "name": "Microsoft.ML.Data.ox7wm0gfo5.wasm",
        "integrity": "sha256-1HkCliT+BpD5WDDfdo402a4qwu2lXcM8Kz1zbZ6N5xU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.KMeansClustering.wasm",
        "name": "Microsoft.ML.KMeansClustering.6vabkiwx18.wasm",
        "integrity": "sha256-PpieiRwzMSrDcGs+ztUYAcDRCl6VW0LTTCj4TEeqVmE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.PCA.wasm",
        "name": "Microsoft.ML.PCA.xl08hf2lwq.wasm",
        "integrity": "sha256-FWJaxOd7G/Bj5EABuaqKHK4pvoWVEGb9/YIBx4L/jrg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.StandardTrainers.wasm",
        "name": "Microsoft.ML.StandardTrainers.9t28s6ey65.wasm",
        "integrity": "sha256-w2vrFkWFeiK5MkIZVSY9+8V7sGKYgriV6woC+26wJsA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Transforms.wasm",
        "name": "Microsoft.ML.Transforms.ckk36w4c50.wasm",
        "integrity": "sha256-JO8iJi00DIvJJ+3uRV/tnFjkX6SPCF1frku+URJXAVU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.CpuMath.wasm",
        "name": "Microsoft.ML.CpuMath.75p1cvfdjn.wasm",
        "integrity": "sha256-i80Ipsg5RG0DMfpPpt4jr77JDgECkptfxVsLNYUa8sw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.DataView.wasm",
        "name": "Microsoft.ML.DataView.75um0vs3tz.wasm",
        "integrity": "sha256-5WG8HDaTA9yHrhowktgauIqri1ZFw2VwAP5QaV2SUZ0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MoreLinq.wasm",
        "name": "MoreLinq.t9qndd4vav.wasm",
        "integrity": "sha256-P5qbdkn5DK+SW/6U7HrwZaXF92oMBCozV8hlIQDDy/0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Newtonsoft.Json.wasm",
        "name": "Newtonsoft.Json.qkbufwhni2.wasm",
        "integrity": "sha256-GlXMWKvDs45M2pACoR3Y4Qh8mcrOZGljqmvJY+6JZ5s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "QuestPDF.wasm",
        "name": "QuestPDF.urfme4jgvb.wasm",
        "integrity": "sha256-pnix/kZGAJXSMR5FZKT+njhgafD1XFfExT7rA0Py5XU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "RBush.wasm",
        "name": "RBush.048ali807d.wasm",
        "integrity": "sha256-cHsLmPL/ZYfNtaqW793aQui9cR4N7JXjHm+xggcoz7w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.wasm",
        "name": "Serilog.gyu309shhb.wasm",
        "integrity": "sha256-eINX94J6ml442Owg464gw2C0HWnn4gb6U4urj876uoE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Extensions.Logging.wasm",
        "name": "Serilog.Extensions.Logging.4aarm1ia2h.wasm",
        "integrity": "sha256-NHk6xRS3dIv7nLWuBUZvce3EXz7kvxiBsrjE7KvSMcI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Formatting.Compact.wasm",
        "name": "Serilog.Formatting.Compact.4jmpy34ji2.wasm",
        "integrity": "sha256-UIyeQvWYOcthNH9MPe2VOkn3DuLVkXXwUkQ7siKIUno=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Settings.Configuration.wasm",
        "name": "Serilog.Settings.Configuration.aio2pf68g1.wasm",
        "integrity": "sha256-kp2SCXz4fdsDb+fR+WFGpxe7oRlXLYbU5tSruqQUGHw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Sinks.BrowserConsole.wasm",
        "name": "Serilog.Sinks.BrowserConsole.x47anjwz3v.wasm",
        "integrity": "sha256-oaUyoVo2a89AwFRLViIDIdBnhpi17mAo03uxs1QLd2g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Sinks.BrowserHttp.wasm",
        "name": "Serilog.Sinks.BrowserHttp.yuh16li3u5.wasm",
        "integrity": "sha256-qipma+046bRHJU4HS+vEXCZCvxb1llPL3nDMRRDlALk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Sinks.PeriodicBatching.wasm",
        "name": "Serilog.Sinks.PeriodicBatching.bkp0ftramr.wasm",
        "integrity": "sha256-a7SsUJRYu0ocL5GIOv+h90XsR4Ge1a45cP4pvKiiQrE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SixLabors.Fonts.wasm",
        "name": "SixLabors.Fonts.1ntl3xbxha.wasm",
        "integrity": "sha256-TYAwAMga8iYHgcD5+CMUwDAmCjtNUbzJJP6/e+Xtl5w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SkiaSharp.wasm",
        "name": "SkiaSharp.iakbbik212.wasm",
        "integrity": "sha256-hpFLToPv5vC7bKDp6nwcxxt9GFSw39EA06RNSLMaDtE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SkiaSharp.HarfBuzz.wasm",
        "name": "SkiaSharp.HarfBuzz.7puwmda4yf.wasm",
        "integrity": "sha256-ay0z9UhyLxhPHwJnyVJBy2zKfj0Rk5bU3N23ZEhXX3g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SkiaSharp.Views.Blazor.wasm",
        "name": "SkiaSharp.Views.Blazor.qjg6ki2nzu.wasm",
        "integrity": "sha256-umr6dKSZOsGrK9hxqMGie0lAZITPnt0QReCANkp86+8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.CodeDom.wasm",
        "name": "System.CodeDom.yx1nyu4kqy.wasm",
        "integrity": "sha256-lkNyquW9zP8bN5VGHdTOgbzfceKVzlYMq3eVPRmhT6Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Packaging.wasm",
        "name": "System.IO.Packaging.ig3wndboch.wasm",
        "integrity": "sha256-fqsFQ1qc3dTVq0FGMdKgnnlf8TUxqyCy7KI6iB7F8ao=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Dynamic.Core.wasm",
        "name": "System.Linq.Dynamic.Core.4vazbp99me.wasm",
        "integrity": "sha256-QB/t+uYL9UedVTW4zO/5HG8oL5paKwdKTZW68+tSTrA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.Tensors.wasm",
        "name": "System.Numerics.Tensors.wa44xt9pq0.wasm",
        "integrity": "sha256-OOwE74r8Tvv7VD1EAagYTCkS9/3GRpY8Lo2veul0oiw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.wasm",
        "name": "Toolbelt.Blazor.HeadElement.3owkc4sscm.wasm",
        "integrity": "sha256-Y75VwEf7HtcK38P94hintQ/mS9jYJIMHjHBeLlbbuMI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.Abstractions.wasm",
        "name": "Toolbelt.Blazor.HeadElement.Abstractions.x5jlech633.wasm",
        "integrity": "sha256-u501aGqqctPbubaom/I8a06VWX49+BPMkFdMD9DZ6Ns=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.Services.wasm",
        "name": "Toolbelt.Blazor.HeadElement.Services.d2s2vfnjr9.wasm",
        "integrity": "sha256-lUUkjiF9VItGEXZ1LJzjXzsABjxlCf4xn7aj66M3xQQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HotKeys2.wasm",
        "name": "Toolbelt.Blazor.HotKeys2.kla3w3gszb.wasm",
        "integrity": "sha256-mJv65i9iDpSWZkXqdHIebNlB3kKZyTDFr1+XlUlH3Zw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.CSharp.wasm",
        "name": "Microsoft.CSharp.gevtfxfi7x.wasm",
        "integrity": "sha256-yLhNQSu3w9SM3IjvZWU46iAkOA7Yd9p65aAsBAt/Yz0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Buffers.wasm",
        "name": "System.Buffers.2vj5deyuq7.wasm",
        "integrity": "sha256-MkfYwK1VomPf2cBvKksVVZmWrP4G8sQvqRMvII5o86o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.7p2we4tpf8.wasm",
        "integrity": "sha256-q0OgzKAYAURk6uQG/X0wGk2e6ClbbCBvgc5L/UJ+fW8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.7i8e58ucpe.wasm",
        "integrity": "sha256-j0fPLRdfBR1JDn39CZg3oYaV1X/Z76+3wJR/QQ4dxJA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.NonGeneric.wasm",
        "name": "System.Collections.NonGeneric.7kxdldkpjh.wasm",
        "integrity": "sha256-J/QG81Ch/j5S9kFIguoLHe+XOBkaZOjkCPh+CA80ppI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Specialized.wasm",
        "name": "System.Collections.Specialized.v4g2c5e6c0.wasm",
        "integrity": "sha256-0SEREpPXgKGzmuzLonIrSeS4njZZGU9vluMrrRRtMWY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.0kjvi4uq1c.wasm",
        "integrity": "sha256-wLZVxjjLMhl9YJN2SZkZYcKERY6m19TvpE3fIyw26vo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Annotations.wasm",
        "name": "System.ComponentModel.Annotations.yrgfal4113.wasm",
        "integrity": "sha256-Ur/tFS7oan/jLjoi4eCJwb+SP0I96V9FdwEpKfuNUso=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Primitives.wasm",
        "name": "System.ComponentModel.Primitives.lke6v54avf.wasm",
        "integrity": "sha256-RQIKSSkjW+8fc5uzNL0wYjOW8xbGNZpPppCkHuqyB6k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.TypeConverter.wasm",
        "name": "System.ComponentModel.TypeConverter.ym1i5v5gdk.wasm",
        "integrity": "sha256-NtbsMjwOxGzrwGhyaxqCxyTXEW3s2fmwl2qATDLEBd4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.p4db1ray47.wasm",
        "integrity": "sha256-x9uzqwDn1crs2MmVVIsjOBhwLttNcKWvXYy050JChWo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.22hctbmlav.wasm",
        "integrity": "sha256-WsoB1HaIHKQgKsNpIkU/LCe/D+MrkDNliH68nQZjygE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Core.wasm",
        "name": "System.Core.jq9mib523o.wasm",
        "integrity": "sha256-NffiQSq52Jy7hHgCuP0tSE0YYYpMWw8kM9NJhQjNxQE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.Common.wasm",
        "name": "System.Data.Common.bcdqryh47j.wasm",
        "integrity": "sha256-9WZTqoeykIKxPIqOsP3WWtbB+CED48SLmg/JPtNh4nI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Debug.wasm",
        "name": "System.Diagnostics.Debug.tdr7juqlz6.wasm",
        "integrity": "sha256-YZFJrLPp63hNML++lZHcqKAoeytPumG1qj1tlafEKEs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.of0xeu1lk7.wasm",
        "integrity": "sha256-dNYH7vM+uLoQN9chgJq3F57wq80uFL2IDeAnzmpZMlc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Process.wasm",
        "name": "System.Diagnostics.Process.nprjha8pt0.wasm",
        "integrity": "sha256-9ROaFt/TphCMDypbLguht7GnuYV3NHdsGY6Vt5Ipd2c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Tools.wasm",
        "name": "System.Diagnostics.Tools.ouw58shzoy.wasm",
        "integrity": "sha256-cALiP+EU2bP3ytynSndpRwouKYIggncOpNWfWi/3PeU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.TraceSource.wasm",
        "name": "System.Diagnostics.TraceSource.8bor6giqzg.wasm",
        "integrity": "sha256-1Fk8karu48MrxxoxZKStwn+0zzO02xvC3tqadrsRga4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.Primitives.wasm",
        "name": "System.Drawing.Primitives.5u5bodf6dk.wasm",
        "integrity": "sha256-Tc4de00xRURIW+HJX2X5bunh43xGhRNCxPvC3R0BAvA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.wasm",
        "name": "System.Drawing.d8abg920y6.wasm",
        "integrity": "sha256-cwPGI68ueVqohpGFUXeVYVJ2KzT1QCSlQ5Rwnj1ngQ0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.Brotli.wasm",
        "name": "System.IO.Compression.Brotli.s5dt5jfywa.wasm",
        "integrity": "sha256-4KgtKQ268FtYxdP4k771fD/eO0zaOTOKE+FMtm6so0o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.ZipFile.wasm",
        "name": "System.IO.Compression.ZipFile.avsgxju5o8.wasm",
        "integrity": "sha256-JgN3+A3XgUFi9zmdS8a9E7pSgJIqrVwDr1RHs4+Qtes=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.wasm",
        "name": "System.IO.Compression.02oqg3qrtp.wasm",
        "integrity": "sha256-SFh21Rsot9dGreB29L/eviFgyaDNlNeKpciqt7LoOjc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.wasm",
        "name": "System.IO.FileSystem.jwaeo9ozrz.wasm",
        "integrity": "sha256-TbV7sravnVLwG4ZfQ2rYAn1I+Uyd+mm+/Y8sT5M7pW8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.MemoryMappedFiles.wasm",
        "name": "System.IO.MemoryMappedFiles.x03lc6awuw.wasm",
        "integrity": "sha256-b/kA6GT1ULCkjOqM+isX4ikoRkwSs40t5U1ywmQvHWE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.pwfw0zwx98.wasm",
        "integrity": "sha256-icS8WxKQ4KAWcutc7rR8xAIOabkTt6riZJOoYooT3/k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Expressions.wasm",
        "name": "System.Linq.Expressions.lt0voatxvb.wasm",
        "integrity": "sha256-F1piQvbmOzm/dnBj/gZU3uJa7dQzkGSB7TnYKS87qOg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Parallel.wasm",
        "name": "System.Linq.Parallel.s6r8yhb5b8.wasm",
        "integrity": "sha256-XI51SH0nhozVnhojgAKRD2ITaGhLYZhTtsMglbE7ITE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Queryable.wasm",
        "name": "System.Linq.Queryable.ont0390k0h.wasm",
        "integrity": "sha256-JD0eki1domQIGB3SxZKTLcq88lyFoG4lE1+OVwpLUNs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.72fs4r2m62.wasm",
        "integrity": "sha256-wVSQbHYgF0uKxO26KPR7eXmxW2w2gg6UMClz2SJO1q8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.hrcjjghtzh.wasm",
        "integrity": "sha256-o7VsbTNtFoyOh7EbEpHyPdb6NeImJoqEqA8WyPDISC0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.Json.wasm",
        "name": "System.Net.Http.Json.9j32s2wlz2.wasm",
        "integrity": "sha256-m13TjmzOni6pPDudDyUc6HRwG24ix90xOxavVKIv2l0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.69vb02fy1v.wasm",
        "integrity": "sha256-+ioR+a7kezIK+1C8VMFlMJg03UV4xmGtqA97XBMru6Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.yl1uus6nzv.wasm",
        "integrity": "sha256-plHxgH09JKpwQgM6VKoolDp+/W1iDi95M2dJMferqKE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Requests.wasm",
        "name": "System.Net.Requests.jtmnh6p5vn.wasm",
        "integrity": "sha256-Ryx/tPZAPuMkmqY5AgqOb8WLjjm97XPS6z5978FS3Fk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebHeaderCollection.wasm",
        "name": "System.Net.WebHeaderCollection.dqvyfpkch8.wasm",
        "integrity": "sha256-23eTwdJzQyjDT2eE9v/kHgLPW/gYqcLV+JwIGpy6490=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.Vectors.wasm",
        "name": "System.Numerics.Vectors.bfusl0pbil.wasm",
        "integrity": "sha256-+fHlrGI36vCn6qE7ac+3cumwtYAqR9lpAqhj34MFfiw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ObjectModel.wasm",
        "name": "System.ObjectModel.7i98zoktdl.wasm",
        "integrity": "sha256-4Tp+TnCV5SepyAGCERXSTZZ9YHDJCyEdus3JXKA580I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.j5x4jqxnaz.wasm",
        "integrity": "sha256-1+s9FPU0CzVR15UUiKK+PELwNTl5If7uldsv0Uz15ks=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.Linq.wasm",
        "name": "System.Private.Xml.Linq.35vf0ye4cj.wasm",
        "integrity": "sha256-mQx3RqoMqcbxtaadz4sy1v1aaSIXWNFIrbuLQPjcs/Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.wasm",
        "name": "System.Private.Xml.iakkdgqkd6.wasm",
        "integrity": "sha256-urAfmnT2WNFdL4szCF8NRHOZzoSee+d2aITnOOPSd4k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.ILGeneration.wasm",
        "name": "System.Reflection.Emit.ILGeneration.dzv3ojfe0l.wasm",
        "integrity": "sha256-V4cO2L79HqD66qYmWg7u6HU1ranXCT3q4gsp8uY7Jyk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.Lightweight.wasm",
        "name": "System.Reflection.Emit.Lightweight.gl1dwy0h33.wasm",
        "integrity": "sha256-rI6HA6b1lY+uWF1zaH/bEm9LtB7KV5Cq2T8t1Lv2nV8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.wasm",
        "name": "System.Reflection.Emit.5svchj7o1r.wasm",
        "integrity": "sha256-G4c8q/l/+TWqqjO6ySsJVZ+r1/KuxhnonFPB6DdL4Uw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Metadata.wasm",
        "name": "System.Reflection.Metadata.nm7o1gwvqm.wasm",
        "integrity": "sha256-YSJtBwcza7fBgq+6C9QbumYhtu8+xGc8NmLg+bmBz0w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Primitives.wasm",
        "name": "System.Reflection.Primitives.soa2aswi66.wasm",
        "integrity": "sha256-ZKNzk4yT8RabjrGQkBswkfw6d0D7CWkIQcrUAmuwAzk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.ResourceManager.wasm",
        "name": "System.Resources.ResourceManager.c9m6w72wf6.wasm",
        "integrity": "sha256-MHLwSrQQTLldUK0pMHpv1XRK89nQRmBTndbEMzPRhPo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.CompilerServices.Unsafe.wasm",
        "name": "System.Runtime.CompilerServices.Unsafe.g7ixw9tmgt.wasm",
        "integrity": "sha256-lPFxVHwvjDTyYPLCtmYKaDQ7qeyVTtiPD6WkHaHc8uA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Extensions.wasm",
        "name": "System.Runtime.Extensions.zf7oz2k8bg.wasm",
        "integrity": "sha256-r8YR3OCjIuI16/QRIu86kAQlGwwi2AH7/cwF43T43D4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.RuntimeInformation.wasm",
        "name": "System.Runtime.InteropServices.RuntimeInformation.9khdqvijas.wasm",
        "integrity": "sha256-KbClFiMvdWxdWxLJ4A7NgnyexUqyGItEkOSNDZSSwxQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.wasm",
        "name": "System.Runtime.InteropServices.6rnqbzo887.wasm",
        "integrity": "sha256-Pi4PKqu0vS3iodadtYBTaxgPYKgzQyhnoZ3ejsi4Dv8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Intrinsics.wasm",
        "name": "System.Runtime.Intrinsics.n87py7pcr7.wasm",
        "integrity": "sha256-517enG/ltcFbcCUc0U6cdrEjw/ooIctV/XU/a6iK3lU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Loader.wasm",
        "name": "System.Runtime.Loader.kvpmcvenag.wasm",
        "integrity": "sha256-gMutmXG7FGm3gE1aebS207NZJU32oqwkUDGe6P6KYYo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Numerics.wasm",
        "name": "System.Runtime.Numerics.xd3las3l8w.wasm",
        "integrity": "sha256-m1HGOEau8s/eQHnurkyNkS5GUdIpLbqBB7CEqHlSgSA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Formatters.wasm",
        "name": "System.Runtime.Serialization.Formatters.75gyazxdtp.wasm",
        "integrity": "sha256-xXOHmYJ0M/aKWwGPe3pyGDZOlg91HwGx9xWEesJPzFg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Primitives.wasm",
        "name": "System.Runtime.Serialization.Primitives.zzwqj1m5v2.wasm",
        "integrity": "sha256-cZ8xq5ldTGfAeisBJoFnyzIw0vWyjE+kc6/yJSswu5k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.wasm",
        "name": "System.Runtime.og9pqu37qr.wasm",
        "integrity": "sha256-KH7yBJm1C40XUNqgQ4HkdAjPtKTVfUsiC3ibm7Br/9o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Claims.wasm",
        "name": "System.Security.Claims.rpjsqqf1co.wasm",
        "integrity": "sha256-z4RPKXc2ob97CMy2kz/GcClgABDDZXfsDbhRQWV68BA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.wasm",
        "name": "System.Security.Cryptography.adjj8porq1.wasm",
        "integrity": "sha256-MstyvAikpM3XxcYg3o/KD/d9btDE1FYeIvRfOSO043Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.Extensions.wasm",
        "name": "System.Text.Encoding.Extensions.0w3al6sujs.wasm",
        "integrity": "sha256-0ICK1urofcWmz7vV1/rHhXYnpHG221DbVVUfWMinzcw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.nrlwe3nrlo.wasm",
        "integrity": "sha256-MyAO765XzYoxMH8f5cEfKjUxPzY2BEcrdN2mmFFBeSk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.ch2egkxgwl.wasm",
        "integrity": "sha256-EPSxeT5eH/Rq2C3OotaVxgjKPTpQAaaaMx8J4wch6+g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.eti2k9i4cq.wasm",
        "integrity": "sha256-PrwB9J7ZmpeQzgrrPUIxkuJSMi/khOIiga1oEV3hekU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Channels.wasm",
        "name": "System.Threading.Channels.7kr6oxf6bj.wasm",
        "integrity": "sha256-B32JcQ4EWosIPJCLlZe1RP0h0ND9P/DcM/H6V+I3cq0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Extensions.wasm",
        "name": "System.Threading.Tasks.Extensions.1l021qjvkk.wasm",
        "integrity": "sha256-dairxDie3w8H5Lhj8N2KI0pDsUoPsWbJKtk4eHcsZ34=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Parallel.wasm",
        "name": "System.Threading.Tasks.Parallel.y42svpg8ua.wasm",
        "integrity": "sha256-LoN+840oW8q3NM92h40zPD496VX1K3bO3PxmluQoxEw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.wasm",
        "name": "System.Threading.Tasks.xb4m97uqps.wasm",
        "integrity": "sha256-k7sPUt4+ZgXHjq/g+d73aejd57WldDQQJzFUfH6qRaI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Timer.wasm",
        "name": "System.Threading.Timer.7grcwti1qp.wasm",
        "integrity": "sha256-9ylNTkpuzTsxvWCLByAvbtp6yqGlfkI4VPBCBxd3W2o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.wasm",
        "name": "System.Threading.3yqszfgin3.wasm",
        "integrity": "sha256-a5b5y78WujMj9McwXPD3JbeO5zvDuGaFbEHWESItGKI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Web.HttpUtility.wasm",
        "name": "System.Web.HttpUtility.wyhbkgruz0.wasm",
        "integrity": "sha256-SMzXq4AJLj3s+oDCkj25H2l8XtgKkTCvXagXtsqPV9o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.Linq.wasm",
        "name": "System.Xml.Linq.1t1f7abjkk.wasm",
        "integrity": "sha256-5fWQqFGllSkAU/RIKtlSWTiQ6yo6HqKLGnlWX4e/sPk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.ReaderWriter.wasm",
        "name": "System.Xml.ReaderWriter.z22nmtp0zi.wasm",
        "integrity": "sha256-S2R82kxm3W56tla2HUKeATCRKM3n9ylP2gavTqDqANY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XDocument.wasm",
        "name": "System.Xml.XDocument.85rg0gj23d.wasm",
        "integrity": "sha256-bN03C7w8/mIYSie/Mu1TLkn4vu+RsAgKm6wxwW7PvlM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.wasm",
        "name": "System.0x1wb7tfot.wasm",
        "integrity": "sha256-tMixsUF4GSgbd7+FHB6PICB4tUpDj29znyKxTr6gLUA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "netstandard.wasm",
        "name": "netstandard.rlmmztza4s.wasm",
        "integrity": "sha256-72IHO9W59TY6igOYVhiFZOYffu8vpVj9yEACGr4peFs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CommonComponents.wasm",
        "name": "CommonComponents.9qq3yfvcup.wasm",
        "integrity": "sha256-72eWc3uj7+qcp4o5jqIFw2CoozWwGeURO+CfjDIDvk8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharedComponents.wasm",
        "name": "SharedComponents.hpwj5vkf75.wasm",
        "integrity": "sha256-q9LJ9O5YKy3WUNtsPTN28QFyUPeoWTypiryCvWfmXnA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharedModels.wasm",
        "name": "SharedModels.vau9rx21ds.wasm",
        "integrity": "sha256-p4AjLXCRPYFwXlWtSXxbdKXFbjdHXqdiZoR9EPzPC54=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Web.wasm",
        "name": "Web.bnrouwbcxx.wasm",
        "integrity": "sha256-CVDzPmNdo/ijbIN4ClYIYzbZGB+m0wUCMaGbjMsJdUs=",
        "cache": "force-cache"
      }
    ],
    "lazyAssembly": [
      {
        "virtualPath": "AIDemoComponents.wasm",
        "name": "AIDemoComponents.b66tv5r4t4.wasm",
        "integrity": "sha256-fdjeFxhhcKjIKroaSABSWHPkiQbp2av9evyBlKk6mmc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "BaseComponents.wasm",
        "name": "BaseComponents.diaooevaex.wasm",
        "integrity": "sha256-yzTfJhaw5y6bs9aiXWcwJ6DZNZyVpRq1a4objdZwaBA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "BlazorDemoComponents.wasm",
        "name": "BlazorDemoComponents.ha9xb6zlnv.wasm",
        "integrity": "sha256-tx8RASy2hXL7u0jwA7yyn10aGdqolL40dazBF+66z50=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CachingDemoComponents.wasm",
        "name": "CachingDemoComponents.aan8lyoxoz.wasm",
        "integrity": "sha256-HhvyHj0nH4zYnIxhWxhnmRD17iFBYNfPClVybmQckCo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DatabaseDemoComponents.wasm",
        "name": "DatabaseDemoComponents.4cqzi52ad7.wasm",
        "integrity": "sha256-7Gl+2tvbxRrfH7LHE26rzhYWtObO6EJPjbzAp2gKocw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DDDDemoComponents.wasm",
        "name": "DDDDemoComponents.05k2hrif3o.wasm",
        "integrity": "sha256-jFDDriUSm8TOnk/zBBiNScxPYJFMEkj3fup4QbRpDn4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DependencyInjectionDemoComponents.wasm",
        "name": "DependencyInjectionDemoComponents.vs3u04scga.wasm",
        "integrity": "sha256-v77oIOnRLul1cmRNKK2FBDZ7wA374Y2SnUacByXmjSs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DesignPatternDemoComponents.wasm",
        "name": "DesignPatternDemoComponents.71iwxw9yq2.wasm",
        "integrity": "sha256-tYuIWaThxKP5/3wc+uQtb82+qGakrZuRGQg7fxoZbPQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "HTTPClientDemoComponents.wasm",
        "name": "HTTPClientDemoComponents.6r9coe1yf5.wasm",
        "integrity": "sha256-nqPUDtHFP7LrMc17dxOpcFFO2L3/llPa+p/7kAOaQCU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "JSONDemoComponents.wasm",
        "name": "JSONDemoComponents.8xorj6g0j1.wasm",
        "integrity": "sha256-FH7NFPdSC+CiQ70SVn8mc39oY+7nkByeX2kVNiXPw5Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "LINQDemoComponents.wasm",
        "name": "LINQDemoComponents.h46gst4c5u.wasm",
        "integrity": "sha256-AfOBO8tZmesbfidveerh19aCB8NW5G6K/uCptboN7QE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "LoggingDemoComponents.wasm",
        "name": "LoggingDemoComponents.tok85vj196.wasm",
        "integrity": "sha256-vYvtbwAI3a8+yfEA45L8RnAe+CuBEY27HCPm29dMRSM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MAUIDemoComponents.wasm",
        "name": "MAUIDemoComponents.adkkt6p16e.wasm",
        "integrity": "sha256-Hok6BjmpkB4AnDWLkfjRaTeeKObaCO/4iY+7Ab681e4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MCPDemoComponents.wasm",
        "name": "MCPDemoComponents.58x4hrv3vg.wasm",
        "integrity": "sha256-y9FsumzqFZmvRJtW8UPtZwIWBWqXgNydug8M/ZEVK2k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MiddlewareDemoComponents.wasm",
        "name": "MiddlewareDemoComponents.uzvrjeevd9.wasm",
        "integrity": "sha256-umVtiMk2Lt4TSdVn9Dc+UjCFSBNFiHnk5hb2clw4pXY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MLNETDemoComponents.wasm",
        "name": "MLNETDemoComponents.ym2aox3kle.wasm",
        "integrity": "sha256-JmiLOs2EOjyOQ26g5aBrnKeuQwhFizalMhsMIZR5vTw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MSBuildDemoComponents.wasm",
        "name": "MSBuildDemoComponents.64yxw3kiw7.wasm",
        "integrity": "sha256-9vYF+XC+Sdd0/fGWC6AqHl4w6YeQQrDdDYbMzjfFsJ0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "OOPSDemoComponents.wasm",
        "name": "OOPSDemoComponents.sninld0tnu.wasm",
        "integrity": "sha256-OIcRIqH2pPJZkm3Y4zfo7mBmNDmZEXifqhDn4PIuAps=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "OWASPDemoComponents.wasm",
        "name": "OWASPDemoComponents.8jfatx4kjs.wasm",
        "integrity": "sha256-JA7qdzk2P1luSOPJFVe1LOMuU5MrZb3NyTkx8EXv3xU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PythonDemoComponents.wasm",
        "name": "PythonDemoComponents.595w1rb5og.wasm",
        "integrity": "sha256-Z0CrnEmMANGLZ1by30Lw4wULEFQ8oNusl8fsb6cYuF4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "RegexDemoComponents.wasm",
        "name": "RegexDemoComponents.ey0ugjx8k6.wasm",
        "integrity": "sha256-VqwIx5ZvXZRVlCbN+V2xsftah9zk8HvcmNucZw5cuCg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ReportDemoComponents.wasm",
        "name": "ReportDemoComponents.6b4vof80ik.wasm",
        "integrity": "sha256-klvnw3Emlr8MR983aI/I3J4+4Gv230NGusSbX9wKQGI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SecurityDemoComponents.wasm",
        "name": "SecurityDemoComponents.y59p23pxqv.wasm",
        "integrity": "sha256-WgcWsD5G3aiuPhtrsi127Ymbuq9zGFU3AUpBiS8bLRU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SignalRDemoComponents.wasm",
        "name": "SignalRDemoComponents.p00qo5p2yx.wasm",
        "integrity": "sha256-md4Zn9nqm6hXlZMoIMPMOUpd/E1vmUzmMMtjQZfpAG4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SOLIDDemoComponents.wasm",
        "name": "SOLIDDemoComponents.f1y7af3pyx.wasm",
        "integrity": "sha256-7QEzRIgP1nGBFuE7v47H/0Qnh4mcfejs5/BDaaay1MU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TalkDemoComponents.wasm",
        "name": "TalkDemoComponents.gxviixn1bv.wasm",
        "integrity": "sha256-JJhpLRjwbxV3+M9cntP3kCzK/T+AEqIJ6rYxmKA+3ew=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TDDDemoComponents.wasm",
        "name": "TDDDemoComponents.sdct7qex9w.wasm",
        "integrity": "sha256-8BJPMXKpnFoNwbRH3kNCfaHKcFX5DfSTG8mlPrxrucM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TestingDemoComponents.wasm",
        "name": "TestingDemoComponents.x2ych8d3vg.wasm",
        "integrity": "sha256-iZjfQsLG6Zg9x6P/7LuQRzzHh0a6ojoxawie5qr5iIo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "WebAPIDemoComponents.wasm",
        "name": "WebAPIDemoComponents.d3fkptmfxs.wasm",
        "integrity": "sha256-lwm6SOrtNBrjlbx8V51pa/qqqqC96bBZfjFmVmHAExc=",
        "cache": "force-cache"
      }
    ],
    "libraryInitializers": [
      {
        "name": "_content/Microsoft.DotNet.HotReload.WebAssembly.Browser/Microsoft.DotNet.HotReload.WebAssembly.Browser.99zm1jdh75.lib.module.js"
      },
      {
        "name": "BlazorWasmPreRendering.Build.lfyg69o9wu.lib.module.js"
      }
    ],
    "modulesAfterConfigLoaded": [
      {
        "name": "../_content/Microsoft.DotNet.HotReload.WebAssembly.Browser/Microsoft.DotNet.HotReload.WebAssembly.Browser.99zm1jdh75.lib.module.js"
      },
      {
        "name": "../BlazorWasmPreRendering.Build.lfyg69o9wu.lib.module.js"
      }
    ]
  },
  "debugLevel": 0,
  "linkerEnabled": true,
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
        "System.Diagnostics.Metrics.Meter.IsSupported": false,
        "System.Diagnostics.Tracing.EventSource.IsSupported": false,
        "System.GC.Server": true,
        "System.Globalization.Invariant": false,
        "System.TimeZoneInfo.Invariant": true,
        "System.Linq.Enumerable.IsSizeOptimized": true,
        "System.Net.Http.EnableActivityPropagation": false,
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
        "Microsoft.AspNetCore.Components.Endpoints.NavigationManager.DisableThrowNavigationException": false
      }
    }
  }
}/*json-end*/);export{gt as default,ft as dotnet,mt as exit};
