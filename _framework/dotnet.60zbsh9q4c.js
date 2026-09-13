//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

var e=!1;const t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,4,1,96,0,0,3,2,1,0,10,8,1,6,0,6,64,25,11,11])),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),n=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),r=Symbol.for("wasm promise_control");function i(e,t){let o=null;const n=new Promise((function(n,r){o={isDone:!1,promise:null,resolve:t=>{o.isDone||(o.isDone=!0,n(t),e&&e())},reject:e=>{o.isDone||(o.isDone=!0,r(e),t&&t())}}}));o.promise=n;const i=n;return i[r]=o,{promise:i,promise_control:o}}function s(e){return e[r]}function a(e){e&&function(e){return void 0!==e[r]}(e)||Be(!1,"Promise is not controllable")}const l="__mono_message__",c=["debug","log","trace","warn","info","error"],d="MONO_WASM: ";let u,f,m,g,p,h;function w(e){g=e}function b(e){if(Pe.diagnosticTracing){const t="function"==typeof e?e():e;console.debug(d+t)}}function y(e,...t){console.info(d+e,...t)}function v(e,...t){console.info(e,...t)}function E(e,...t){console.warn(d+e,...t)}function _(e,...t){if(t&&t.length>0&&t[0]&&"object"==typeof t[0]){if(t[0].silent)return;if(t[0].toString)return void console.error(d+e,t[0].toString())}console.error(d+e,...t)}function x(e,t,o){return function(...n){try{let r=n[0];if(void 0===r)r="undefined";else if(null===r)r="null";else if("function"==typeof r)r=r.toString();else if("string"!=typeof r)try{r=JSON.stringify(r)}catch(e){r=r.toString()}t(o?JSON.stringify({method:e,payload:r,arguments:n.slice(1)}):[e+r,...n.slice(1)])}catch(e){m.error(`proxyConsole failed: ${e}`)}}}function j(e,t,o){f=t,g=e,m={...t};const n=`${o}/console`.replace("https://","wss://").replace("http://","ws://");u=new WebSocket(n),u.addEventListener("error",A),u.addEventListener("close",S),function(){for(const e of c)f[e]=x(`console.${e}`,T,!0)}()}function R(e){let t=30;const o=()=>{u?0==u.bufferedAmount||0==t?(e&&v(e),function(){for(const e of c)f[e]=x(`console.${e}`,m.log,!1)}(),u.removeEventListener("error",A),u.removeEventListener("close",S),u.close(1e3,e),u=void 0):(t--,globalThis.setTimeout(o,100)):e&&m&&m.log(e)};o()}function T(e){u&&u.readyState===WebSocket.OPEN?u.send(e):m.log(e)}function A(e){m.error(`[${g}] proxy console websocket error: ${e}`,e)}function S(e){m.debug(`[${g}] proxy console websocket closed: ${e}`,e)}function D(){Pe.preferredIcuAsset=O(Pe.config);let e="invariant"==Pe.config.globalizationMode;if(!e)if(Pe.preferredIcuAsset)Pe.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===Pe.config.globalizationMode||"all"===Pe.config.globalizationMode||"sharded"===Pe.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}Pe.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,Pe.preferredIcuAsset=null}const t="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",o=Pe.config.environmentVariables;if(void 0===o[t]&&e&&(o[t]="1"),void 0===o.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(o.TZ=e)}catch(e){y("failed to detect timezone, will fallback to UTC")}}function O(e){var t;if((null===(t=e.resources)||void 0===t?void 0:t.icu)&&"invariant"!=e.globalizationMode){const t=e.applicationCulture||(ke?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale),o=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(o.length>=1)return o[0].name}else t&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const t=e.split("-")[0];return"en"===t||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(t)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(t)):n="icudt.dat";if(n)for(let e=0;e<o.length;e++){const t=o[e];if(t.virtualPath===n)return t.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const C=class{constructor(e){this.url=e}toString(){return this.url}};async function k(e,t){try{const o="function"==typeof globalThis.fetch;if(Se){const n=e.startsWith("file://");if(!n&&o)return globalThis.fetch(e,t||{credentials:"same-origin"});p||(h=Ne.require("url"),p=Ne.require("fs")),n&&(e=h.fileURLToPath(e));const r=await p.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>r,json:()=>JSON.parse(r),text:()=>{throw new Error("NotImplementedException")}}}if(o)return globalThis.fetch(e,t||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(t){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t},text:()=>{throw t}}}throw new Error("No fetch implementation available")}function I(e){return"string"!=typeof e&&Be(!1,"url must be a string"),!M(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}const U=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,P=/[a-zA-Z]:[\\/]/;function M(e){return Se||Ie?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||P.test(e):U.test(e)}let L,N=0;const $=[],z=[],W=new Map,F={"js-module-threads":!0,"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},B={...F,"js-module-library-initializer":!0},V={...F,dotnetwasm:!0,heap:!0,manifest:!0},q={...B,manifest:!0},H={...B,dotnetwasm:!0},J={dotnetwasm:!0,symbols:!0},Z={...B,dotnetwasm:!0,symbols:!0},Q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=Pe.preferredIcuAsset)}function K(e,t,o){null!=t||(t=[]),Be(1==t.length,`Expect to have one ${o} asset in resources`);const n=t[0];return n.behavior=o,X(n),e.push(n),n}function X(e){V[e.behavior]&&W.set(e.behavior,e)}function Y(e){Be(V[e],`Unknown single asset behavior ${e}`);const t=W.get(e);if(t&&!t.resolvedUrl)if(t.resolvedUrl=Pe.locateFile(t.name),F[t.behavior]){const e=ge(t);e?("string"!=typeof e&&Be(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),t.resolvedUrl=e):t.resolvedUrl=ce(t.resolvedUrl,t.behavior)}else if("dotnetwasm"!==t.behavior)throw new Error(`Unknown single asset behavior ${e}`);return t}function ee(e){const t=Y(e);return Be(t,`Single asset for ${e} not found`),t}let te=!1;async function oe(){if(!te){te=!0,Pe.diagnosticTracing&&b("mono_download_assets");try{const e=[],t=[],o=(e,t)=>{!Z[e.behavior]&&G(e)&&Pe.expected_instantiated_assets_count++,!H[e.behavior]&&G(e)&&(Pe.expected_downloaded_assets_count++,t.push(se(e)))};for(const t of $)o(t,e);for(const e of z)o(e,t);Pe.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...t]).then((()=>{Pe.allDownloadsFinished.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),await Pe.runtimeModuleLoaded.promise;const n=async e=>{const t=await e;if(t.buffer){if(!Z[t.behavior]){t.buffer&&"object"==typeof t.buffer||Be(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof t.resolvedUrl&&Be(!1,"resolvedUrl must be string");const e=t.resolvedUrl,o=await t.buffer,n=new Uint8Array(o);pe(t),await Ue.beforeOnRuntimeInitialized.promise,Ue.instantiate_asset(t,e,n)}}else J[t.behavior]?("symbols"===t.behavior&&(await Ue.instantiate_symbols_asset(t),pe(t)),J[t.behavior]&&++Pe.actual_downloaded_assets_count):(t.isOptional||Be(!1,"Expected asset to have the downloaded buffer"),!H[t.behavior]&&G(t)&&Pe.expected_downloaded_assets_count--,!Z[t.behavior]&&G(t)&&Pe.expected_instantiated_assets_count--)},r=[],i=[];for(const t of e)r.push(n(t));for(const e of t)i.push(n(e));Promise.all(r).then((()=>{Ce||Ue.coreAssetsInMemory.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),Promise.all(i).then((async()=>{Ce||(await Ue.coreAssetsInMemory.promise,Ue.allAssetsInMemory.promise_control.resolve())})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e}))}catch(e){throw Pe.err("Error in mono_download_assets: "+e),e}}}let ne=!1;function re(){if(ne)return;ne=!0;const e=Pe.config,t=[];if(e.assets)for(const t of e.assets)"object"!=typeof t&&Be(!1,`asset must be object, it was ${typeof t} : ${t}`),"string"!=typeof t.behavior&&Be(!1,"asset behavior must be known string"),"string"!=typeof t.name&&Be(!1,"asset name must be string"),t.resolvedUrl&&"string"!=typeof t.resolvedUrl&&Be(!1,"asset resolvedUrl could be string"),t.hash&&"string"!=typeof t.hash&&Be(!1,"asset resolvedUrl could be string"),t.pendingDownload&&"object"!=typeof t.pendingDownload&&Be(!1,"asset pendingDownload could be object"),t.isCore?$.push(t):z.push(t),X(t);else if(e.resources){const o=e.resources;o.wasmNative||Be(!1,"resources.wasmNative must be defined"),o.jsModuleNative||Be(!1,"resources.jsModuleNative must be defined"),o.jsModuleRuntime||Be(!1,"resources.jsModuleRuntime must be defined"),K(z,o.wasmNative,"dotnetwasm"),K(t,o.jsModuleNative,"js-module-native"),K(t,o.jsModuleRuntime,"js-module-runtime"),o.jsModuleDiagnostics&&K(t,o.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,t,o)=>{const n=e;n.behavior=t,o?(n.isCore=!0,$.push(n)):z.push(n)};if(o.coreAssembly)for(let e=0;e<o.coreAssembly.length;e++)n(o.coreAssembly[e],"assembly",!0);if(o.assembly)for(let e=0;e<o.assembly.length;e++)n(o.assembly[e],"assembly",!o.coreAssembly);if(0!=e.debugLevel&&Pe.isDebuggingSupported()){if(o.corePdb)for(let e=0;e<o.corePdb.length;e++)n(o.corePdb[e],"pdb",!0);if(o.pdb)for(let e=0;e<o.pdb.length;e++)n(o.pdb[e],"pdb",!o.corePdb)}if(e.loadAllSatelliteResources&&o.satelliteResources)for(const e in o.satelliteResources)for(let t=0;t<o.satelliteResources[e].length;t++){const r=o.satelliteResources[e][t];r.culture=e,n(r,"resource",!o.coreAssembly)}if(o.coreVfs)for(let e=0;e<o.coreVfs.length;e++)n(o.coreVfs[e],"vfs",!0);if(o.vfs)for(let e=0;e<o.vfs.length;e++)n(o.vfs[e],"vfs",!o.coreVfs);const r=O(e);if(r&&o.icu)for(let e=0;e<o.icu.length;e++){const t=o.icu[e];t.name===r&&n(t,"icu",!1)}if(o.wasmSymbols)for(let e=0;e<o.wasmSymbols.length;e++)n(o.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let t=0;t<e.appsettings.length;t++){const o=e.appsettings[t],n=he(o);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||z.push({name:o,behavior:"vfs",cache:"no-cache",useCredentials:!0})}e.assets=[...$,...z,...t]}async function ie(e){const t=await se(e);return await t.pendingDownloadInternal.response,t.buffer}async function se(e){try{return await ae(e)}catch(t){if(!Pe.enableDownloadRetry)throw t;if(Ie||Se)throw t;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw t;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw t;if(t&&404==t.status)throw t;e.pendingDownloadInternal=void 0,await Pe.allDownloadsQueued.promise;try{return Pe.diagnosticTracing&&b(`Retrying download '${e.name}'`),await ae(e)}catch(t){return e.pendingDownloadInternal=void 0,await new Promise((e=>globalThis.setTimeout(e,100))),Pe.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await ae(e)}}}async function ae(e){for(;L;)await L.promise;try{++N,N==Pe.maxParallelDownloads&&(Pe.diagnosticTracing&&b("Throttling further parallel downloads"),L=i());const t=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const t=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>t,json:()=>JSON.parse(new TextDecoder("utf-8").decode(t)),text:()=>{throw new Error("NotImplementedException")},headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const t=e.loadRemote&&Pe.config.remoteSources?Pe.config.remoteSources:[""];let o;for(let n of t){n=n.trim(),"./"===n&&(n="");const t=le(e,n);e.name===t?Pe.diagnosticTracing&&b(`Attempting to download '${t}'`):Pe.diagnosticTracing&&b(`Attempting to download '${t}' for ${e.name}`);try{e.resolvedUrl=t;const n=fe(e);if(e.pendingDownloadInternal=n,o=await n.response,!o||!o.ok)continue;return o}catch(e){o||(o={ok:!1,url:t,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&Pe.config.ignorePdbLoadErrors;if(o||Be(!1,`Response undefined ${e.name}`),!n){const t=new Error(`download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`);throw t.status=o.status,t}y(`optional download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`)}(e);return t?(J[e.behavior]||(e.buffer=await t.arrayBuffer(),++Pe.actual_downloaded_assets_count),e):e}finally{if(--N,L&&N==Pe.maxParallelDownloads-1){Pe.diagnosticTracing&&b("Resuming more parallel downloads");const e=L;L=void 0,e.promise_control.resolve()}}}function le(e,t){let o;return null==t&&Be(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?o=e.resolvedUrl:(o=""===t?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:t+e.name,o=ce(Pe.locateFile(o),e.behavior)),o&&"string"==typeof o||Be(!1,"attemptUrl need to be path or url string"),o}function ce(e,t){return Pe.modulesUniqueQuery&&q[t]&&(e+=Pe.modulesUniqueQuery),e}let de=0;const ue=new Set;function fe(e){try{e.resolvedUrl||Be(!1,"Request's resolvedUrl must be set");const t=function(e){let t=e.resolvedUrl;if(Pe.loadBootResource){const o=ge(e);if(o instanceof Promise)return o;"string"==typeof o&&(t=o)}const o={};return e.cache?o.cache=e.cache:Pe.config.disableNoCacheFetch||(o.cache="no-cache"),e.useCredentials?o.credentials="include":!Pe.config.disableIntegrityCheck&&e.hash&&(o.integrity=e.hash),Pe.fetch_like(t,o)}(e),o={name:e.name,url:e.resolvedUrl,response:t};return ue.add(e.name),o.response.then((()=>{"assembly"==e.behavior&&Pe.loadedAssemblies.push(e.name),de++,Pe.onDownloadResourceProgress&&Pe.onDownloadResourceProgress(de,ue.size)})),o}catch(t){const o={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(o)}}}const me={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs","js-module-threads":"dotnetjs"};function ge(e){var t;if(Pe.loadBootResource){const o=null!==(t=e.hash)&&void 0!==t?t:"",n=e.resolvedUrl,r=me[e.behavior];if(r){const t=Pe.loadBootResource(r,e.name,n,o,e.behavior);return"string"==typeof t?I(t):t}}}function pe(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function he(e){let t=e.lastIndexOf("/");return t>=0&&t++,e.substring(t)}async function we(e){e&&await Promise.all((null!=e?e:[]).map((e=>async function(e){try{const t=e.name;if(!e.moduleExports){const o=ce(Pe.locateFile(t),"js-module-library-initializer");Pe.diagnosticTracing&&b(`Attempting to import '${o}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */o)}Pe.libraryInitializers.push({scriptName:t,exports:e.moduleExports})}catch(t){E(`Failed to import library initializer '${e}': ${t}`)}}(e))))}async function be(e,t){if(!Pe.libraryInitializers)return;const o=[];for(let n=0;n<Pe.libraryInitializers.length;n++){const r=Pe.libraryInitializers[n];r.exports[e]&&o.push(ye(r.scriptName,e,(()=>r.exports[e](...t))))}await Promise.all(o)}async function ye(e,t,o){try{await o()}catch(o){throw E(`Failed to invoke '${t}' on library initializer '${e}': ${o}`),Xe(1,o),o}}function ve(e,t){if(e===t)return e;const o={...t};return void 0!==o.assets&&o.assets!==e.assets&&(o.assets=[...e.assets||[],...o.assets||[]]),void 0!==o.resources&&(o.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},o.resources)),void 0!==o.environmentVariables&&(o.environmentVariables={...e.environmentVariables||{},...o.environmentVariables||{}}),void 0!==o.runtimeOptions&&o.runtimeOptions!==e.runtimeOptions&&(o.runtimeOptions=[...e.runtimeOptions||[],...o.runtimeOptions||[]]),Object.assign(e,o)}function Ee(e,t){if(e===t)return e;const o={...t};return o.config&&(e.config||(e.config={}),o.config=ve(e.config,o.config)),Object.assign(e,o)}function _e(e,t){if(e===t)return e;const o={...t};return void 0!==o.coreAssembly&&(o.coreAssembly=[...e.coreAssembly||[],...o.coreAssembly||[]]),void 0!==o.assembly&&(o.assembly=[...e.assembly||[],...o.assembly||[]]),void 0!==o.lazyAssembly&&(o.lazyAssembly=[...e.lazyAssembly||[],...o.lazyAssembly||[]]),void 0!==o.corePdb&&(o.corePdb=[...e.corePdb||[],...o.corePdb||[]]),void 0!==o.pdb&&(o.pdb=[...e.pdb||[],...o.pdb||[]]),void 0!==o.jsModuleWorker&&(o.jsModuleWorker=[...e.jsModuleWorker||[],...o.jsModuleWorker||[]]),void 0!==o.jsModuleNative&&(o.jsModuleNative=[...e.jsModuleNative||[],...o.jsModuleNative||[]]),void 0!==o.jsModuleDiagnostics&&(o.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...o.jsModuleDiagnostics||[]]),void 0!==o.jsModuleRuntime&&(o.jsModuleRuntime=[...e.jsModuleRuntime||[],...o.jsModuleRuntime||[]]),void 0!==o.wasmSymbols&&(o.wasmSymbols=[...e.wasmSymbols||[],...o.wasmSymbols||[]]),void 0!==o.wasmNative&&(o.wasmNative=[...e.wasmNative||[],...o.wasmNative||[]]),void 0!==o.icu&&(o.icu=[...e.icu||[],...o.icu||[]]),void 0!==o.satelliteResources&&(o.satelliteResources=function(e,t){if(e===t)return e;for(const o in t)e[o]=[...e[o]||[],...t[o]||[]];return e}(e.satelliteResources||{},o.satelliteResources||{})),void 0!==o.modulesAfterConfigLoaded&&(o.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...o.modulesAfterConfigLoaded||[]]),void 0!==o.modulesAfterRuntimeReady&&(o.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...o.modulesAfterRuntimeReady||[]]),void 0!==o.extensions&&(o.extensions={...e.extensions||{},...o.extensions||{}}),void 0!==o.vfs&&(o.vfs=[...e.vfs||[],...o.vfs||[]]),Object.assign(e,o)}function xe(){const e=Pe.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleWorker:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){Pe.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const t of e.assets){const o={};switch(t.behavior){case"assembly":o.assembly=[t];break;case"pdb":o.pdb=[t];break;case"resource":o.satelliteResources={},o.satelliteResources[t.culture]=[t];break;case"icu":o.icu=[t];break;case"symbols":o.wasmSymbols=[t];break;case"vfs":o.vfs=[t];break;case"dotnetwasm":o.wasmNative=[t];break;case"js-module-threads":o.jsModuleWorker=[t];break;case"js-module-runtime":o.jsModuleRuntime=[t];break;case"js-module-native":o.jsModuleNative=[t];break;case"js-module-diagnostics":o.jsModuleDiagnostics=[t];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${t.behavior} of asset ${t.name}`)}_e(e.resources,o)}}e.debugLevel,e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Ue.diagnosticTracing=Pe.diagnosticTracing=!!e.diagnosticTracing,Ue.waitForDebugger=e.waitForDebugger,Pe.maxParallelDownloads=e.maxParallelDownloads||Pe.maxParallelDownloads,Pe.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:Pe.enableDownloadRetry}let je=!1;async function Re(e){var t;if(je)return void await Pe.afterConfigLoaded.promise;let o;try{if(e.configSrc||Pe.config&&0!==Object.keys(Pe.config).length&&(Pe.config.assets||Pe.config.resources)||(e.configSrc="dotnet.boot.js"),o=e.configSrc,je=!0,o&&(Pe.diagnosticTracing&&b("mono_wasm_load_config"),await async function(e){const t=e.configSrc,o=Pe.locateFile(t);let n=null;void 0!==Pe.loadBootResource&&(n=Pe.loadBootResource("manifest",t,o,"","manifest"));let r,i=null;if(n)if("string"==typeof n)n.includes(".json")?(i=await s(I(n)),r=await Ae(i)):r=(await import(I(n))).config;else{const e=await n;"function"==typeof e.json?(i=e,r=await Ae(i)):r=e.config}else o.includes(".json")?(i=await s(ce(o,"manifest")),r=await Ae(i)):r=(await import(ce(o,"manifest"))).config;function s(e){return Pe.fetch_like(e,{method:"GET",credentials:"include",cache:"no-cache"})}Pe.config.applicationEnvironment&&(r.applicationEnvironment=Pe.config.applicationEnvironment),ve(Pe.config,r)}(e)),xe(),await we(null===(t=Pe.config.resources)||void 0===t?void 0:t.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[Pe.config]),e.onConfigLoaded)try{await e.onConfigLoaded(Pe.config,Le),xe()}catch(e){throw _("onConfigLoaded() failed",e),e}xe(),Pe.afterConfigLoaded.promise_control.resolve(Pe.config)}catch(t){const n=`Failed to load config file ${o} ${t} ${null==t?void 0:t.stack}`;throw Pe.config=e.config=Object.assign(Pe.config,{message:n,error:t,isError:!0}),Xe(1,new Error(n)),t}}function Te(){return!!globalThis.navigator&&(Pe.isChromium||Pe.isFirefox)}async function Ae(e){const t=Pe.config,o=await e.json();t.applicationEnvironment||o.applicationEnvironment||(o.applicationEnvironment=e.headers.get("Blazor-Environment")||e.headers.get("DotNet-Environment")||void 0),o.environmentVariables||(o.environmentVariables={});const n=e.headers.get("DOTNET-MODIFIABLE-ASSEMBLIES");n&&(o.environmentVariables.DOTNET_MODIFIABLE_ASSEMBLIES=n);const r=e.headers.get("ASPNETCORE-BROWSER-TOOLS");return r&&(o.environmentVariables.__ASPNETCORE_BROWSER_TOOLS=r),o}"function"!=typeof importScripts||globalThis.onmessage||(globalThis.dotnetSidecar=!0);const Se="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,De="function"==typeof importScripts,Oe=De&&"undefined"!=typeof dotnetSidecar,Ce=De&&!Oe,ke="object"==typeof window||De&&!Se,Ie=!ke&&!Se;let Ue={},Pe={},Me={},Le={},Ne={},$e=!1;const ze={},We={config:ze},Fe={mono:{},binding:{},internal:Ne,module:We,loaderHelpers:Pe,runtimeHelpers:Ue,diagnosticHelpers:Me,api:Le};function Be(e,t){if(e)return;const o="Assert failed: "+("function"==typeof t?t():t),n=new Error(o);_(o,n),Ue.nativeAbort(n)}function Ve(){return void 0!==Pe.exitCode}function qe(){return Ue.runtimeReady&&!Ve()}function He(){Ve()&&Be(!1,`.NET runtime already exited with ${Pe.exitCode} ${Pe.exitReason}. You can use runtime.runMain() which doesn't exit the runtime.`),Ue.runtimeReady||Be(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function Je(){ke&&(globalThis.addEventListener("unhandledrejection",et),globalThis.addEventListener("error",tt))}let Ze,Qe;function Ge(e){Qe&&Qe(e),Xe(e,Pe.exitReason)}function Ke(e){Ze&&Ze(e||Pe.exitReason),Xe(1,e||Pe.exitReason)}function Xe(t,o){var n,r;const i=o&&"object"==typeof o;t=i&&"number"==typeof o.status?o.status:void 0===t?-1:t;const s=i&&"string"==typeof o.message?o.message:""+o;(o=i?o:Ue.ExitStatus?function(e,t){const o=new Ue.ExitStatus(e);return o.message=t,o.toString=()=>t,o}(t,s):new Error("Exit with code "+t+" "+s)).status=t,o.message||(o.message=s);const a=""+(o.stack||(new Error).stack);try{Object.defineProperty(o,"stack",{get:()=>a})}catch(e){}const l=!!o.silent;if(o.silent=!0,Ve())Pe.diagnosticTracing&&b("mono_exit called after exit");else{try{We.onAbort==Ke&&(We.onAbort=Ze),We.onExit==Ge&&(We.onExit=Qe),ke&&(globalThis.removeEventListener("unhandledrejection",et),globalThis.removeEventListener("error",tt)),Ue.runtimeReady?(Ue.jiterpreter_dump_stats&&Ue.jiterpreter_dump_stats(!1),0===t&&(null===(n=Pe.config)||void 0===n?void 0:n.interopCleanupOnExit)&&Ue.forceDisposeProxies(!0,!0),e&&0!==t&&(null===(r=Pe.config)||void 0===r||r.dumpThreadsOnNonZeroExit)):(Pe.diagnosticTracing&&b(`abort_startup, reason: ${o}`),function(e){Pe.allDownloadsQueued.promise_control.reject(e),Pe.allDownloadsFinished.promise_control.reject(e),Pe.afterConfigLoaded.promise_control.reject(e),Pe.wasmCompilePromise.promise_control.reject(e),Pe.runtimeModuleLoaded.promise_control.reject(e),Ue.dotnetReady&&(Ue.dotnetReady.promise_control.reject(e),Ue.afterInstantiateWasm.promise_control.reject(e),Ue.beforePreInit.promise_control.reject(e),Ue.afterPreInit.promise_control.reject(e),Ue.afterPreRun.promise_control.reject(e),Ue.beforeOnRuntimeInitialized.promise_control.reject(e),Ue.afterOnRuntimeInitialized.promise_control.reject(e),Ue.afterPostRun.promise_control.reject(e))}(o))}catch(e){E("mono_exit A failed",e)}try{l||(function(e,t){if(0!==e&&t){const e=Ue.ExitStatus&&t instanceof Ue.ExitStatus?b:_;"string"==typeof t?e(t):(void 0===t.stack&&(t.stack=(new Error).stack+""),t.message?e(Ue.stringify_as_error_with_stack?Ue.stringify_as_error_with_stack(t.message+"\n"+t.stack):t.message+"\n"+t.stack):e(JSON.stringify(t)))}!Ce&&Pe.config&&(Pe.config.logExitCode?Pe.config.forwardConsoleLogsToWS?R("WASM EXIT "+e):v("WASM EXIT "+e):Pe.config.forwardConsoleLogsToWS&&R())}(t,o),function(e){if(ke&&!Ce&&Pe.config&&Pe.config.appendElementOnExit&&document){const t=document.createElement("label");t.id="tests_done",0!==e&&(t.style.background="red"),t.innerHTML=""+e,document.body.appendChild(t)}}(t))}catch(e){E("mono_exit B failed",e)}Pe.exitCode=t,Pe.exitReason||(Pe.exitReason=o),!Ce&&Ue.runtimeReady&&We.runtimeKeepalivePop()}if(Pe.config&&Pe.config.asyncFlushOnExit&&0===t)throw(async()=>{try{await async function(){try{const e=await import(/*! webpackIgnore: true */"process"),t=e=>new Promise(((t,o)=>{e.on("error",o),e.end("","utf8",t)})),o=t(e.stderr),n=t(e.stdout);let r;const i=new Promise((e=>{r=setTimeout((()=>e("timeout")),1e3)}));await Promise.race([Promise.all([n,o]),i]),clearTimeout(r)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Ye(t,o)}})(),o;Ye(t,o)}function Ye(e,t){if(Ue.runtimeReady&&Ue.nativeExit)try{Ue.nativeExit(e)}catch(e){!Ue.ExitStatus||e instanceof Ue.ExitStatus||E("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!ke)throw Se&&Ne.process?Ne.process.exit(e):Ue.quit&&Ue.quit(e,t),t}function et(e){ot(e,e.reason,"rejection")}function tt(e){ot(e,e.error,"error")}function ot(e,t,o){e.preventDefault();try{t||(t=new Error("Unhandled "+o)),void 0===t.stack&&(t.stack=(new Error).stack),t.stack=t.stack+"",t.silent||(_("Unhandled error:",t),Xe(1,t))}catch(e){}}!function(e){if($e)throw new Error("Loader module already loaded");$e=!0,Ue=e.runtimeHelpers,Pe=e.loaderHelpers,Me=e.diagnosticHelpers,Le=e.api,Ne=e.internal,Object.assign(Le,{INTERNAL:Ne,invokeLibraryInitializers:be}),Object.assign(e.module,{config:ve(ze,{environmentVariables:{}})});const r={mono_wasm_bindings_is_ready:!1,config:e.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"95017c711e6afc1085133d440e42b4bd78155701",config:e.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:i(),allDownloadsQueued:i(),allDownloadsFinished:i(),wasmCompilePromise:i(),runtimeModuleLoaded:i(),loadingWorkers:i(),is_exited:Ve,is_runtime_running:qe,assert_runtime_running:He,mono_exit:Xe,createPromiseController:i,getPromiseController:s,assertIsControllablePromise:a,mono_download_assets:oe,resolve_single_asset_path:ee,setup_proxy_console:j,set_thread_prefix:w,installUnhandledErrorHandler:Je,retrieve_asset_download:ie,invokeLibraryInitializers:be,isDebuggingSupported:Te,exceptions:t,simd:n,relaxedSimd:o};Object.assign(Ue,r),Object.assign(Pe,l)}(Fe);let nt,rt,it,st=!1,at=!1;async function lt(e){if(!at){if(at=!0,ke&&Pe.config.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&j("main",globalThis.console,globalThis.location.origin),We||Be(!1,"Null moduleConfig"),Pe.config||Be(!1,"Null moduleConfig.config"),"function"==typeof e){const t=e(Fe.api);if(t.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(We,t),Ee(We,t)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");Ee(We,e)}await async function(e){if(Se){const e=await import(/*! webpackIgnore: true */"process"),t=14;if(e.versions.node.split(".")[0]<t)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${t}. See also https://aka.ms/dotnet-wasm-features`)}const t=/*! webpackIgnore: true */import.meta.url,o=t.indexOf("?");var n;if(o>0&&(Pe.modulesUniqueQuery=t.substring(o)),Pe.scriptUrl=t.replace(/\\/g,"/").replace(/[?#].*/,""),Pe.scriptDirectory=(n=Pe.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",Pe.locateFile=e=>"URL"in globalThis&&globalThis.URL!==C?new URL(e,Pe.scriptDirectory).toString():M(e)?e:Pe.scriptDirectory+e,Pe.fetch_like=k,Pe.out=console.log,Pe.err=console.error,Pe.onDownloadResourceProgress=e.onDownloadResourceProgress,ke&&globalThis.navigator){const e=globalThis.navigator,t=e.userAgentData&&e.userAgentData.brands;t&&t.length>0?Pe.isChromium=t.some((e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand)):e.userAgent&&(Pe.isChromium=e.userAgent.includes("Chrome"),Pe.isFirefox=e.userAgent.includes("Firefox"))}Ne.require=Se?await import(/*! webpackIgnore: true */"module").then((e=>e.createRequire(/*! webpackIgnore: true */import.meta.url))):Promise.resolve((()=>{throw new Error("require not supported")})),void 0===globalThis.URL&&(globalThis.URL=C)}(We)}}async function ct(e){return await lt(e),Ze=We.onAbort,Qe=We.onExit,We.onAbort=Ke,We.onExit=Ge,We.ENVIRONMENT_IS_PTHREAD?async function(){(function(){const e=new MessageChannel,t=e.port1,o=e.port2;t.addEventListener("message",(e=>{var n,r;n=JSON.parse(e.data.config),r=JSON.parse(e.data.monoThreadInfo),st?Pe.diagnosticTracing&&b("mono config already received"):(ve(Pe.config,n),Ue.monoThreadInfo=r,xe(),Pe.diagnosticTracing&&b("mono config received"),st=!0,Pe.afterConfigLoaded.promise_control.resolve(Pe.config),ke&&n.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&Pe.setup_proxy_console("worker-idle",console,globalThis.location.origin)),t.close(),o.close()}),{once:!0}),t.start(),self.postMessage({[l]:{monoCmd:"preload",port:o}},[o])})(),await Pe.afterConfigLoaded.promise,function(){const e=Pe.config;e.assets||Be(!1,"config.assets must be defined");for(const t of e.assets)X(t),Q[t.behavior]&&z.push(t)}(),setTimeout((async()=>{try{await oe()}catch(e){Xe(1,e)}}),0);const e=dt(),t=await Promise.all(e);return await ut(t),We}():async function(){var e;await Re(We),re();const t=dt();(async function(){try{const e=ee("dotnetwasm");await se(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||Be(!1,"Can't load dotnet.native.wasm");const t=await e.pendingDownloadInternal.response,o=t.headers&&t.headers.get?t.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===o)n=await WebAssembly.compileStreaming(t);else{ke&&"application/wasm"!==o&&E('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await t.arrayBuffer();Pe.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Ie?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,Pe.wasmCompilePromise.promise_control.resolve(n)}catch(e){Pe.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout((async()=>{try{D(),await oe()}catch(e){Xe(1,e)}}),0);const o=await Promise.all(t);return await ut(o),await Ue.dotnetReady.promise,await we(null===(e=Pe.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[Fe.api]),Le}()}function dt(){const e=ee("js-module-runtime"),t=ee("js-module-native");if(nt&&rt)return[nt,rt,it];"object"==typeof e.moduleExports?nt=e.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),nt=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof t.moduleExports?rt=t.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),rt=import(/*! webpackIgnore: true */t.resolvedUrl));const o=Y("js-module-diagnostics");return o&&("object"==typeof o.moduleExports?it=o.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),it=import(/*! webpackIgnore: true */o.resolvedUrl))),[nt,rt,it]}async function ut(e){const{initializeExports:t,initializeReplacements:o,configureRuntimeStartup:n,configureEmscriptenStartup:r,configureWorkerStartup:i,setRuntimeGlobals:s,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];s(Fe),t(Fe),c&&c.setRuntimeGlobals(Fe),await n(We),Pe.runtimeModuleLoaded.promise_control.resolve(),l((e=>(Object.assign(We,{ready:e.ready,__dotnet_runtime:{initializeReplacements:o,configureEmscriptenStartup:r,configureWorkerStartup:i,passEmscriptenInternals:a}}),We))).catch((e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize. See also https://aka.ms/dotnet-wasm-features");throw e}))}const ft=new class{withModuleConfig(e){try{return Ee(We,e),this}catch(e){throw Xe(1,e),e}}withOnConfigLoaded(e){try{return Ee(We,{onConfigLoaded:e}),this}catch(e){throw Xe(1,e),e}}withConsoleForwarding(){try{return ve(ze,{forwardConsoleLogsToWS:!0}),this}catch(e){throw Xe(1,e),e}}withExitOnUnhandledError(){try{return ve(ze,{exitOnUnhandledError:!0}),Je(),this}catch(e){throw Xe(1,e),e}}withAsyncFlushOnExit(){try{return ve(ze,{asyncFlushOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withExitCodeLogging(){try{return ve(ze,{logExitCode:!0}),this}catch(e){throw Xe(1,e),e}}withElementOnExit(){try{return ve(ze,{appendElementOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withInteropCleanupOnExit(){try{return ve(ze,{interopCleanupOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withDumpThreadsOnNonZeroExit(){try{return ve(ze,{dumpThreadsOnNonZeroExit:!0}),this}catch(e){throw Xe(1,e),e}}withWaitingForDebugger(e){try{return ve(ze,{waitForDebugger:e}),this}catch(e){throw Xe(1,e),e}}withInterpreterPgo(e,t){try{return ve(ze,{interpreterPgo:e,interpreterPgoSaveDelay:t}),ze.runtimeOptions?ze.runtimeOptions.push("--interp-pgo-recording"):ze.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Xe(1,e),e}}withConfig(e){try{return ve(ze,e),this}catch(e){throw Xe(1,e),e}}withConfigSrc(e){try{return e&&"string"==typeof e||Be(!1,"must be file path or URL"),Ee(We,{configSrc:e}),this}catch(e){throw Xe(1,e),e}}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||Be(!1,"must be directory path"),ve(ze,{virtualWorkingDirectory:e}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariable(e,t){try{const o={};return o[e]=t,ve(ze,{environmentVariables:o}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||Be(!1,"must be dictionary object"),ve(ze,{environmentVariables:e}),this}catch(e){throw Xe(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&Be(!1,"must be boolean"),ve(ze,{diagnosticTracing:e}),this}catch(e){throw Xe(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||Be(!1,"must be number"),ve(ze,{debugLevel:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ve(ze,{applicationArguments:e}),this}catch(e){throw Xe(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ze.runtimeOptions?ze.runtimeOptions.push(...e):ze.runtimeOptions=e,this}catch(e){throw Xe(1,e),e}}withMainAssembly(e){try{return ve(ze,{mainAssemblyName:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Xe(1,e),e}}withApplicationEnvironment(e){try{return ve(ze,{applicationEnvironment:e}),this}catch(e){throw Xe(1,e),e}}withApplicationCulture(e){try{return ve(ze,{applicationCulture:e}),this}catch(e){throw Xe(1,e),e}}withResourceLoader(e){try{return Pe.loadBootResource=e,this}catch(e){throw Xe(1,e),e}}async download(){try{await async function(){lt(We),await Re(We),re(),D(),oe(),await Pe.allDownloadsFinished.promise}()}catch(e){throw Xe(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await ct(We),Fe.api}()),this.instance}catch(e){throw Xe(1,e),e}}async run(){try{return We.config||Be(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Xe(1,e),e}}},mt=Xe,gt=ct;Ie||"function"==typeof globalThis.URL||Be(!1,"This browser/engine doesn't support URL API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),"function"!=typeof globalThis.BigInt64Array&&Be(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),ft.withConfig(/*json-start*/{
  "mainAssemblyName": "Web",
  "resources": {
    "hash": "sha256-dCwkhn0ar0Irze1JC71IGP1nFEx+jHcNK6T1ic2CMhU=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.cx9csz6vm2.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.v06hirbjsv.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.gvmspw3nls.wasm",
        "hash": "sha256-NHQsQEtsKKfwS4jOEz+LflUHm40dufYOXayfzo7FMK4=",
        "cache": "force-cache"
      }
    ],
    "icu": [
      {
        "virtualPath": "icudt_CJK.dat",
        "name": "icudt_CJK.tjcz0u77k5.dat",
        "hash": "sha256-SZLtQnRc0JkwqHab0VUVP7T3uBPSeYzxzDnpxPpUnHk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_EFIGS.dat",
        "name": "icudt_EFIGS.tptq2av103.dat",
        "hash": "sha256-8fItetYY8kQ0ww6oxwTLiT3oXlBwHKumbeP2pRF4yTc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_no_CJK.dat",
        "name": "icudt_no_CJK.lfu7j35m59.dat",
        "hash": "sha256-L7sV7NEYP37/Qr2FPCePo5cJqRgTXRwGHuwF5Q+0Nfs=",
        "cache": "force-cache"
      }
    ],
    "coreAssembly": [
      {
        "virtualPath": "System.Runtime.InteropServices.JavaScript.wasm",
        "name": "System.Runtime.InteropServices.JavaScript.jog1645w0h.wasm",
        "hash": "sha256-TU+Tqf1VEy/MtwXb5KGlQR1XiwHKZ6EDaYt2psfQgu4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.CoreLib.wasm",
        "name": "System.Private.CoreLib.bmg3k47pbp.wasm",
        "hash": "sha256-HbWVfM1JO4DpMBl/PxI3E4cA76fKexYB3lrWFni0TZw=",
        "cache": "force-cache"
      }
    ],
    "assembly": [
      {
        "virtualPath": "Blazor-Analytics.wasm",
        "name": "Blazor-Analytics.4ensyheg7d.wasm",
        "hash": "sha256-bIvCtkn88pVsOm5DqLGKrhR3kj1VwhhG8KdF1Hk8yLE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ClosedXML.wasm",
        "name": "ClosedXML.tis3xpyycb.wasm",
        "hash": "sha256-2+dYIXNW+2yeLJSW75Rx6bGDGMM2l4wIgxgwjUJji4A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ClosedXML.Parser.wasm",
        "name": "ClosedXML.Parser.kcd8ka7nog.wasm",
        "hash": "sha256-bN619zkNwVyqGT2tFn5NMXPDIo3OUVaRYMG/tXRtn+A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ClosedXML.Report.wasm",
        "name": "ClosedXML.Report.fcz87vijt6.wasm",
        "hash": "sha256-74fGlfMz4HhXehO/K3vaVnzZ53lEmUNw68kwmSzDXVA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DocumentFormat.OpenXml.wasm",
        "name": "DocumentFormat.OpenXml.0gr15g35pw.wasm",
        "hash": "sha256-MBbZzfjPOSIquvoCx+NxtLYAI63vk0mstqecqcKULfc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DocumentFormat.OpenXml.Framework.wasm",
        "name": "DocumentFormat.OpenXml.Framework.hhtgzs2q6p.wasm",
        "hash": "sha256-u3MueHh2gEf2RLS+UmJWjOGBzFZ5Gl0WU5GjsX7xBxs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ExcelNumberFormat.wasm",
        "name": "ExcelNumberFormat.zkc9yronjy.wasm",
        "hash": "sha256-DWrWJf+NGbjj12iqpO+Ufl4YS0bkW1yz6JwjocU15wY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "FluentValidation.wasm",
        "name": "FluentValidation.yuptatg8bv.wasm",
        "hash": "sha256-1vkL1fNCyvLkWYtavCvKdUZS0BPY392ec7LutoPTps4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Humanizer.wasm",
        "name": "Humanizer.0y075xbmmv.wasm",
        "hash": "sha256-iHkFsQ2zqQtfhrtiRjN28v45pdzEtFoGqoyvxXjE4ik=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Authorization.c34a6yxwq6.wasm",
        "hash": "sha256-ONHV19xBFJR4lzxqLBusiYZFlZpz+79C2VtX9Y6XBnE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.wasm",
        "name": "Microsoft.AspNetCore.Components.2ozsmwl4cm.wasm",
        "hash": "sha256-kklWdnyI2ydstTesXxoKcLmnJNHCbNbdEah3sDMLXlg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Components.Authorization.5e70usbtun.wasm",
        "hash": "sha256-kxyo6nOvtUOrkkGnHH+LXmVhFkILymwujRxZugmMCIk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Forms.wasm",
        "name": "Microsoft.AspNetCore.Components.Forms.xsl1nryf2v.wasm",
        "hash": "sha256-occh2hesbqcS/N7d2xVr+Zr3XkgOFKIzbfQTUHZUPt8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Web.wasm",
        "name": "Microsoft.AspNetCore.Components.Web.9hmle1q69f.wasm",
        "hash": "sha256-DffSFVx2Ci1UDw3KQxCVictYsDLIqpsGgM/HwXL5Jys=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.tz6yhi1bid.wasm",
        "hash": "sha256-6WDdPAXOaakyQ5WklIuY1ux1dxBCUB53AQ8QdFRlJDc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.uxs221fevs.wasm",
        "hash": "sha256-/qqn5YDwo/PG6pxG1COmA5Km2qskfzl8YVWdgsH/0fw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Metadata.wasm",
        "name": "Microsoft.AspNetCore.Metadata.y7h4m7pc8o.wasm",
        "hash": "sha256-0u0Ct437zEjgol+ji65NAQFHjZLisAcF6YpztFBRRsU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.wasm",
        "name": "Microsoft.Extensions.Configuration.okajyikg82.wasm",
        "hash": "sha256-uBlK4RehVhSpsF1uJD4SsaeDb5MEvuJ8noCiI5HCsrw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Abstractions.wasm",
        "name": "Microsoft.Extensions.Configuration.Abstractions.uzrhipg8tb.wasm",
        "hash": "sha256-+v0vQLNCCQ0TAKy7JZD5dA7Pp6ufzUipKhhxjNVd2OY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Binder.wasm",
        "name": "Microsoft.Extensions.Configuration.Binder.1i1ctlju5n.wasm",
        "hash": "sha256-ZwwmKNHhocSKAAaT14QvHmFaX2d+sCyDXyRrmkacBSY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Json.wasm",
        "name": "Microsoft.Extensions.Configuration.Json.jygd6wn873.wasm",
        "hash": "sha256-5bJobl2CmVmcJLLFnz9CCdj3xIaEyZ6a0VAD0d5/IkQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.xa8mcvv3rq.wasm",
        "hash": "sha256-ievtSZOu0hQYH9TSzTM4RVtXKGjTj/wO/4MxYdFxIzI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.Abstractions.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.Abstractions.sgxsolwoz2.wasm",
        "hash": "sha256-bCROpGyX9S8zqatnWgBsaZWks/e8B/Qgp3gHiKB5KDY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyModel.wasm",
        "name": "Microsoft.Extensions.DependencyModel.pkiabcs8hj.wasm",
        "hash": "sha256-dyjqOZrh0fL9qXtviAqzKuApsnl/SKAIMcg5Eusesac=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.wasm",
        "name": "Microsoft.Extensions.Diagnostics.9k7gklzza3.wasm",
        "hash": "sha256-SYLTEt2DTCtUluoNTbHyenQ206zhP8tHa/eYkEE48Vo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.Abstractions.wasm",
        "name": "Microsoft.Extensions.Diagnostics.Abstractions.xgtxfsk0cx.wasm",
        "hash": "sha256-cbLP+c3z02AoaYnHKKVv0zoEw2as5tY8p4GuPVlBilw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Http.wasm",
        "name": "Microsoft.Extensions.Http.r1dbvtbs1m.wasm",
        "hash": "sha256-z9mUHPDA3IcXnNaX6/cvJJkBgl7QoWJjvzNb7vhl9Gk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.wasm",
        "name": "Microsoft.Extensions.Logging.7anr9zq4to.wasm",
        "hash": "sha256-YZyDKGayb1L/3x7vasniMuxwhFXrM4SyiumrWGNLv8E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.Abstractions.wasm",
        "name": "Microsoft.Extensions.Logging.Abstractions.wihlprsung.wasm",
        "hash": "sha256-+JDHudJP/kxxqGF+U+wg146Ttz0aGq5F3vHJN30d1nA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Options.wasm",
        "name": "Microsoft.Extensions.Options.br35jek1v9.wasm",
        "hash": "sha256-Rw2esiTEqgwRDWewmVAiz0FyiE6Hdm+V2cpCYGKaDWg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Primitives.wasm",
        "name": "Microsoft.Extensions.Primitives.ic9wbsf5sl.wasm",
        "hash": "sha256-rv3r6coMKuvow6Hc6OAxeNvyB7RbniAzC9Kjjvs0zPs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Validation.wasm",
        "name": "Microsoft.Extensions.Validation.1igmz3eei4.wasm",
        "hash": "sha256-CUwN0UEngVN3nal+jQtEX6uTejg/tFi0raGAfpTxz0c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.JSInterop.wasm",
        "name": "Microsoft.JSInterop.firnnj76bk.wasm",
        "hash": "sha256-+YGNM3hLj/LzLIT2CZa1e837lSxARk1AJRzOVxcTpoY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.JSInterop.WebAssembly.wasm",
        "name": "Microsoft.JSInterop.WebAssembly.cok88e5v83.wasm",
        "hash": "sha256-rCqRR1uovyMtEUZWreQRHObzOIBt/Tb6+4Hr1hjrJiM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Core.wasm",
        "name": "Microsoft.ML.Core.bxjtgvvl3m.wasm",
        "hash": "sha256-HyZST47FO+wUJSHgSeHBC2RS1CJEuaEnqzP3Y8DwrHE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Data.wasm",
        "name": "Microsoft.ML.Data.ox7wm0gfo5.wasm",
        "hash": "sha256-1HkCliT+BpD5WDDfdo402a4qwu2lXcM8Kz1zbZ6N5xU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.KMeansClustering.wasm",
        "name": "Microsoft.ML.KMeansClustering.6vabkiwx18.wasm",
        "hash": "sha256-PpieiRwzMSrDcGs+ztUYAcDRCl6VW0LTTCj4TEeqVmE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.PCA.wasm",
        "name": "Microsoft.ML.PCA.xl08hf2lwq.wasm",
        "hash": "sha256-FWJaxOd7G/Bj5EABuaqKHK4pvoWVEGb9/YIBx4L/jrg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.StandardTrainers.wasm",
        "name": "Microsoft.ML.StandardTrainers.9t28s6ey65.wasm",
        "hash": "sha256-w2vrFkWFeiK5MkIZVSY9+8V7sGKYgriV6woC+26wJsA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Transforms.wasm",
        "name": "Microsoft.ML.Transforms.ckk36w4c50.wasm",
        "hash": "sha256-JO8iJi00DIvJJ+3uRV/tnFjkX6SPCF1frku+URJXAVU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.CpuMath.wasm",
        "name": "Microsoft.ML.CpuMath.75p1cvfdjn.wasm",
        "hash": "sha256-i80Ipsg5RG0DMfpPpt4jr77JDgECkptfxVsLNYUa8sw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.DataView.wasm",
        "name": "Microsoft.ML.DataView.75um0vs3tz.wasm",
        "hash": "sha256-5WG8HDaTA9yHrhowktgauIqri1ZFw2VwAP5QaV2SUZ0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MoreLinq.wasm",
        "name": "MoreLinq.t9qndd4vav.wasm",
        "hash": "sha256-P5qbdkn5DK+SW/6U7HrwZaXF92oMBCozV8hlIQDDy/0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Newtonsoft.Json.wasm",
        "name": "Newtonsoft.Json.qkbufwhni2.wasm",
        "hash": "sha256-GlXMWKvDs45M2pACoR3Y4Qh8mcrOZGljqmvJY+6JZ5s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "QuestPDF.wasm",
        "name": "QuestPDF.papw2xci6v.wasm",
        "hash": "sha256-7xHfZNsUDbCdY4guDIZu8cCywEwks0MasHc7lzHnSpE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "RBush.wasm",
        "name": "RBush.048ali807d.wasm",
        "hash": "sha256-cHsLmPL/ZYfNtaqW793aQui9cR4N7JXjHm+xggcoz7w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.wasm",
        "name": "Serilog.ukw07rj3st.wasm",
        "hash": "sha256-ErhjJIDOHT2tonMsHGQo934Ut9nvencbYbIlh8HbgZ0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Extensions.Logging.wasm",
        "name": "Serilog.Extensions.Logging.4aarm1ia2h.wasm",
        "hash": "sha256-NHk6xRS3dIv7nLWuBUZvce3EXz7kvxiBsrjE7KvSMcI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Formatting.Compact.wasm",
        "name": "Serilog.Formatting.Compact.4jmpy34ji2.wasm",
        "hash": "sha256-UIyeQvWYOcthNH9MPe2VOkn3DuLVkXXwUkQ7siKIUno=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Settings.Configuration.wasm",
        "name": "Serilog.Settings.Configuration.19ux1r71j2.wasm",
        "hash": "sha256-m0tMCRO1iG7T5hwKE5+IBQ6ZBCN6GriLj1CXbqbIjwc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Sinks.BrowserConsole.wasm",
        "name": "Serilog.Sinks.BrowserConsole.x47anjwz3v.wasm",
        "hash": "sha256-oaUyoVo2a89AwFRLViIDIdBnhpi17mAo03uxs1QLd2g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Sinks.BrowserHttp.wasm",
        "name": "Serilog.Sinks.BrowserHttp.yuh16li3u5.wasm",
        "hash": "sha256-qipma+046bRHJU4HS+vEXCZCvxb1llPL3nDMRRDlALk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Serilog.Sinks.PeriodicBatching.wasm",
        "name": "Serilog.Sinks.PeriodicBatching.bkp0ftramr.wasm",
        "hash": "sha256-a7SsUJRYu0ocL5GIOv+h90XsR4Ge1a45cP4pvKiiQrE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SixLabors.Fonts.wasm",
        "name": "SixLabors.Fonts.1ntl3xbxha.wasm",
        "hash": "sha256-TYAwAMga8iYHgcD5+CMUwDAmCjtNUbzJJP6/e+Xtl5w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SkiaSharp.wasm",
        "name": "SkiaSharp.1uj5ipbtyz.wasm",
        "hash": "sha256-qTl8ecfvWiFM87BUlOGte7zB+yNJIEOWk6t7pKvP0tQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SkiaSharp.Views.Blazor.wasm",
        "name": "SkiaSharp.Views.Blazor.j718ja6tmi.wasm",
        "hash": "sha256-4UzDR9QvsX353IYZTOsm+HYvENOEB3ncGHJIAaYjyAk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.CodeDom.wasm",
        "name": "System.CodeDom.yx1nyu4kqy.wasm",
        "hash": "sha256-lkNyquW9zP8bN5VGHdTOgbzfceKVzlYMq3eVPRmhT6Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Packaging.wasm",
        "name": "System.IO.Packaging.ig3wndboch.wasm",
        "hash": "sha256-fqsFQ1qc3dTVq0FGMdKgnnlf8TUxqyCy7KI6iB7F8ao=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Dynamic.Core.wasm",
        "name": "System.Linq.Dynamic.Core.dp4yy9354s.wasm",
        "hash": "sha256-rx2oV+dppyuZSKaZd8b6iariQgXO4BvGE4vYbnjWeMg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.Tensors.wasm",
        "name": "System.Numerics.Tensors.wa44xt9pq0.wasm",
        "hash": "sha256-OOwE74r8Tvv7VD1EAagYTCkS9/3GRpY8Lo2veul0oiw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.wasm",
        "name": "Toolbelt.Blazor.HeadElement.3owkc4sscm.wasm",
        "hash": "sha256-Y75VwEf7HtcK38P94hintQ/mS9jYJIMHjHBeLlbbuMI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.Abstractions.wasm",
        "name": "Toolbelt.Blazor.HeadElement.Abstractions.x5jlech633.wasm",
        "hash": "sha256-u501aGqqctPbubaom/I8a06VWX49+BPMkFdMD9DZ6Ns=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.Services.wasm",
        "name": "Toolbelt.Blazor.HeadElement.Services.d2s2vfnjr9.wasm",
        "hash": "sha256-lUUkjiF9VItGEXZ1LJzjXzsABjxlCf4xn7aj66M3xQQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HotKeys2.wasm",
        "name": "Toolbelt.Blazor.HotKeys2.kla3w3gszb.wasm",
        "hash": "sha256-mJv65i9iDpSWZkXqdHIebNlB3kKZyTDFr1+XlUlH3Zw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.CSharp.wasm",
        "name": "Microsoft.CSharp.uvqm9neciz.wasm",
        "hash": "sha256-ZonI7+8EmsaZs3xtEuJzsdZi1hnSNiy+TUrViOvMsjE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Buffers.wasm",
        "name": "System.Buffers.pjm4uleil8.wasm",
        "hash": "sha256-Jf27kWlVTv9oLvx0KOyDGlFRl/JwlMC4wCWiFigSsWs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.zj4nboxcom.wasm",
        "hash": "sha256-MS7KsGEkGPCGm0V67ufYZmoRJuSTZDFuzc0374qU1ls=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.8fqpzmko0m.wasm",
        "hash": "sha256-/FIXq2sd9VOoTcXaRiJ2Zz41x3xc+sYuNA4tts1tGj0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.NonGeneric.wasm",
        "name": "System.Collections.NonGeneric.1yj2rhtbvl.wasm",
        "hash": "sha256-eUDPYyIZRaMUil3gKJMALqTJT/3XRcodfYBhW3ojquk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Specialized.wasm",
        "name": "System.Collections.Specialized.zmyjv2nvlg.wasm",
        "hash": "sha256-5dJojm6NWfzk7A2qCXed9j4kSvL0LTBjqmuDKr2P8LA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.gqpv0bthjf.wasm",
        "hash": "sha256-+E8+8CYj90OF2UXJdUyWRma+kc7h2I8C5f8If3HGdTI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Annotations.wasm",
        "name": "System.ComponentModel.Annotations.vl32mwh4l1.wasm",
        "hash": "sha256-PRu0COe78+fOi9t8xpWMj3o1HJZi2Df7e7gs5fZbtYw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Primitives.wasm",
        "name": "System.ComponentModel.Primitives.0eng7c5itk.wasm",
        "hash": "sha256-mD4YhpoU9oOtnlJLAdQ3tQUK+kaxYpe8TzTaJlZ89fQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.TypeConverter.wasm",
        "name": "System.ComponentModel.TypeConverter.ied232q5r2.wasm",
        "hash": "sha256-Wrs12UGwz4c3TblcrdI0z/I5cv5gtZiZK5qNLNEporY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.cphsh41vl6.wasm",
        "hash": "sha256-oFm4vMv+Q9oyEejMC9gEG9e7ko8IcrpkPHiy/Dg8F3Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.6a4fllfiht.wasm",
        "hash": "sha256-kiNEVGcy/COPKw4MIO6vjusCuYnHS7o69Mbtbn2imwM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Core.wasm",
        "name": "System.Core.oukq8npmv4.wasm",
        "hash": "sha256-8D8668KatzrWo01yEXgsTkMbeq6/y5S8tDp0iQbWO/E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.Common.wasm",
        "name": "System.Data.Common.g04ipog9pb.wasm",
        "hash": "sha256-0D3TROJOAZ9RngrEryAhApfN+PFuGb0hg28i3mwVeII=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Debug.wasm",
        "name": "System.Diagnostics.Debug.k38ray8cnl.wasm",
        "hash": "sha256-wPPbAyJ/n7gF/K2SWe5W5yOfKq0N/xUYqroLwqglbXk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.yp1dhxvo3i.wasm",
        "hash": "sha256-CvGPwNhjsJT7nJf742M1hq+y6My6PbRiqGUHSJHXCd0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Process.wasm",
        "name": "System.Diagnostics.Process.289ug2s23t.wasm",
        "hash": "sha256-zt1RSrwoUPr41Kz2Ae9Y89PxqzNxFNVRnaMyDwfP0vo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Tools.wasm",
        "name": "System.Diagnostics.Tools.y9nb3hrudb.wasm",
        "hash": "sha256-lkF1ZwihBRkDPCHg9Ml9BotAHITUUV1jKioPsKtf2K4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.TraceSource.wasm",
        "name": "System.Diagnostics.TraceSource.ruvejpbigv.wasm",
        "hash": "sha256-w7+dvb7zzglRibUpFBdx3cK/QR9km3R80+VN0YtLjBs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.Primitives.wasm",
        "name": "System.Drawing.Primitives.yk7ftkj9om.wasm",
        "hash": "sha256-IlEV0D8uaAZlGAfIlRLGWvw/qs2/3L5ljLPGTGXvJjo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.wasm",
        "name": "System.Drawing.c6tmp1gjc8.wasm",
        "hash": "sha256-NFpXLgCTsyBU+AK+27QTsQQxyAoaLo/HDVh9RRvDIL4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.Brotli.wasm",
        "name": "System.IO.Compression.Brotli.wa56i7859c.wasm",
        "hash": "sha256-WO4UFSX8owperBNBTRJSEU6UUjC8rWvIuu3QDmpZzvA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.ZipFile.wasm",
        "name": "System.IO.Compression.ZipFile.yeunonfj5y.wasm",
        "hash": "sha256-un06mZpBnJ0MrGlb2bXyuqrr+dedifr3TL2DXo/tdeg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.wasm",
        "name": "System.IO.Compression.zbh7lahp2n.wasm",
        "hash": "sha256-f0Ac/c8t7MhmiqliJXMlPLDeJTo07ZdD5/MyEOocVdI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.wasm",
        "name": "System.IO.FileSystem.0s5eswzbeb.wasm",
        "hash": "sha256-QN9dS2pNrgjtUxRL717Lmor+9zmBhxzHyzFWMhno3dk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.MemoryMappedFiles.wasm",
        "name": "System.IO.MemoryMappedFiles.0net1hglt5.wasm",
        "hash": "sha256-pEsfLjQdTZn3OvoSBF0MV1x/Lv/oKK2f2KjCPqp2TRM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.px0vglstfo.wasm",
        "hash": "sha256-fZpB2NFmoDJXAqnCYU4+XAKkZbE51ViM3KsQ+bxqVOo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Expressions.wasm",
        "name": "System.Linq.Expressions.wz8jxc4k9g.wasm",
        "hash": "sha256-TGVtThC76ccORrzcCcB81nPM2KbmNT4UwM32YcSqLOw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Parallel.wasm",
        "name": "System.Linq.Parallel.0ektnu5hkw.wasm",
        "hash": "sha256-iLQc0QFXbQWSnRyPgt1FY3nrJRmXi8mCRntxVxHuY9g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Queryable.wasm",
        "name": "System.Linq.Queryable.qjwqxaxq60.wasm",
        "hash": "sha256-uh+Lg4hGEv30VbtHKXtzgHhy9e2se782IH9coswIVZw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.b46xdbvxak.wasm",
        "hash": "sha256-xfniA+UJiOTYJYMdu9+DWiia3RQ1OzpXIca6vAui6bg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.r8ju4o2a6x.wasm",
        "hash": "sha256-60DSxfREtWohiLamx7BlMNSMW4p4sB7Fv/Mm5jKt8uM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.aucmw9sx00.wasm",
        "hash": "sha256-fjHgwSqYS6697cdLFpJr9WJ/h9o97qCNDvqJXTmrINM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.llz1zzlk2t.wasm",
        "hash": "sha256-eSkmLWnZ3RkSwQTH7dunFXMwU+BM4C35r9dvdzASvUk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Requests.wasm",
        "name": "System.Net.Requests.zq2q6k64pi.wasm",
        "hash": "sha256-a4blK1k2mfEX32NP7nYiTuntAs9kuohBfvZ6XIUzl3M=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebHeaderCollection.wasm",
        "name": "System.Net.WebHeaderCollection.h9w49fqrop.wasm",
        "hash": "sha256-q95xvWqlwv3pzKV207d/x6IiS6z+bbG2asCfT1SdjZ0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.Vectors.wasm",
        "name": "System.Numerics.Vectors.1cu6lue4lt.wasm",
        "hash": "sha256-79McEGIgNQ2rjc9UWp3vSmL/n3+lsMy4apD8D8OULLs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ObjectModel.wasm",
        "name": "System.ObjectModel.5xu12pzyl6.wasm",
        "hash": "sha256-yUhPDlo9WzdiYAHiPf8FuEQ1m5zBQqgs4YBMYEOPUQw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.DataContractSerialization.wasm",
        "name": "System.Private.DataContractSerialization.sm5hinehof.wasm",
        "hash": "sha256-xGS3VudzgfUzajmIv98dbTa7pOGCxpf0lssnJp27BlA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.uhnucfzvmi.wasm",
        "hash": "sha256-curPTIIE0w4Ysgv3eM5Lnsx2uI/fOCUQAUP54gOCffU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.Linq.wasm",
        "name": "System.Private.Xml.Linq.5ijvab11ch.wasm",
        "hash": "sha256-fRrpzxuXN9skRnCj9ONmQMgUc9q5MSZP3pVgdiV1lI4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.wasm",
        "name": "System.Private.Xml.n4jaoo3kmb.wasm",
        "hash": "sha256-Vyeq7Mf0SBdgre18mvuJLEBbXvKtMSqwtmke3Ko0fSE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.ILGeneration.wasm",
        "name": "System.Reflection.Emit.ILGeneration.fqw9ai6chf.wasm",
        "hash": "sha256-9z+51k3YIppeigvMeP6L7ybqb7CaORMmkCUFgYvXpAA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.Lightweight.wasm",
        "name": "System.Reflection.Emit.Lightweight.b1x1ic91s4.wasm",
        "hash": "sha256-fy+wN9mbUBdeCRP1jFWwc8hGeCtD9zAWI/uir0tevn0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.wasm",
        "name": "System.Reflection.Emit.3dcvtmas60.wasm",
        "hash": "sha256-V0WTBYmBpL1y3VydapsfqsJhi6aHtQT7jXWSamSkXLw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Metadata.wasm",
        "name": "System.Reflection.Metadata.8stcmarjxe.wasm",
        "hash": "sha256-ODA6hCryUqD9jHYB5buPbvrDkeWAef5a5aYyXI6fiSw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Primitives.wasm",
        "name": "System.Reflection.Primitives.y0t06ffh5m.wasm",
        "hash": "sha256-DtvUByUM7iugi+iZbOUqRTAFa9/57eM6sEANIyhYeI4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.ResourceManager.wasm",
        "name": "System.Resources.ResourceManager.9hr4affkg0.wasm",
        "hash": "sha256-48WtQORRYle1yu7mKz0UK+B8bPrEFMKWdBSReowbgK0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.CompilerServices.Unsafe.wasm",
        "name": "System.Runtime.CompilerServices.Unsafe.wcpi0i4qbx.wasm",
        "hash": "sha256-4JjqZOhbK+h0/CIQTGVqHAWAjiTF/q0vHcj07po/Jhc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Extensions.wasm",
        "name": "System.Runtime.Extensions.8tpy5mv29f.wasm",
        "hash": "sha256-LGSXhRDo+/sVzkiRyxtq6gMsIfEHM9JGLLIvlCCrsIk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.RuntimeInformation.wasm",
        "name": "System.Runtime.InteropServices.RuntimeInformation.puxzs0c4xu.wasm",
        "hash": "sha256-3w2SYeMOGTH3LZJTu8pr0JD5A6K5H8Rv40A/qv+1pgw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.wasm",
        "name": "System.Runtime.InteropServices.zzm5uixm61.wasm",
        "hash": "sha256-UQaHV6tdp4S3b8eJQ+gIlRPujo9SVcjenl5D0bj9cUY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Intrinsics.wasm",
        "name": "System.Runtime.Intrinsics.8y1uyvaye2.wasm",
        "hash": "sha256-oDyIxPkV49ruQqC8Cc9AxkQmMhVj/DypI6JOwJPAX94=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Numerics.wasm",
        "name": "System.Runtime.Numerics.yn2gfjlc4o.wasm",
        "hash": "sha256-flxRAPXC/rZ+aQr7Uh1LzHn0p4VllDW0uxhnUHmKvXw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Formatters.wasm",
        "name": "System.Runtime.Serialization.Formatters.vdanj49dzd.wasm",
        "hash": "sha256-U+fJqRnd9Td/HQ7LQ9tHwaLDvYjj+cVdSV5PFwYiJFo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Primitives.wasm",
        "name": "System.Runtime.Serialization.Primitives.8eijdxaj8k.wasm",
        "hash": "sha256-4JQX/etN/GM7XKNGcM7jq/RppTuumCjxFQNbmcnMvIg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Xml.wasm",
        "name": "System.Runtime.Serialization.Xml.36zst2lo76.wasm",
        "hash": "sha256-9YlNOHF+C9mNbmCj/sx00FToV1P025KG3zQm0s55WO4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.wasm",
        "name": "System.Runtime.tol2gp4tf4.wasm",
        "hash": "sha256-TW5+x57nvmxXd0/KT6CbgwF91zjj/vs+Q2gZfQg7A4s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Claims.wasm",
        "name": "System.Security.Claims.i3og96n5pp.wasm",
        "hash": "sha256-/g7RF7oCtjsKeDl0O+opFcRhCXdewTDaCIFP76Vo0ok=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.wasm",
        "name": "System.Security.Cryptography.2vg9aq9j2v.wasm",
        "hash": "sha256-YgBg6yyCbtjb52idfBdrg6FjsaSDyCcdWQ1t0TnMl+U=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.Extensions.wasm",
        "name": "System.Text.Encoding.Extensions.ajdtrmih3n.wasm",
        "hash": "sha256-xlzDXaEN8BlJZ/cmxpvjDbpYqmXAnU9S7pNhE10wL74=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.zmqv2qq7w9.wasm",
        "hash": "sha256-ZR1iHx10GyHDjW88DLb2dv0cLYu29h2RhNwFC/zJWiA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.40b62lsfv3.wasm",
        "hash": "sha256-NLOtBxUvjUd7TVAAYcgfHGe9LOXCdxM1YewdDgllA0w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.95205lvign.wasm",
        "hash": "sha256-3ROfcvDqeyoYB1vTklJ7Ddtz5AVRV52/C9acEIFSydo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Channels.wasm",
        "name": "System.Threading.Channels.agivsn1ucb.wasm",
        "hash": "sha256-1qjUeDHfCtyP1wyAIxAfl7Fi31RsRkVQZ0h+WDl5Mmk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Extensions.wasm",
        "name": "System.Threading.Tasks.Extensions.gih607iuyy.wasm",
        "hash": "sha256-2EX8xozjGUTUGIzfUpEl7YGMkAB/iy5dsWNR8+TQsrU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Parallel.wasm",
        "name": "System.Threading.Tasks.Parallel.adcgypca0z.wasm",
        "hash": "sha256-YrdjcnGkCmHlsI/5Ygl9VOARFPvLaZef3EeXSNdVQZQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.wasm",
        "name": "System.Threading.Tasks.ctfwriplch.wasm",
        "hash": "sha256-8JRBdaDGZUfvbQoffg02rVTLJrFgoW0DnteUD7jxYCU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Timer.wasm",
        "name": "System.Threading.Timer.mefvnqgk3z.wasm",
        "hash": "sha256-vn5l7U4CTopiqGHGM5dB2uNVSuRQOYS95zdF47aupWs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.wasm",
        "name": "System.Threading.cnyfzdv6ip.wasm",
        "hash": "sha256-KCS1TtcsrIYJjrQKotlgiYuDWfB2dCiVIPDgMFHShKA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Web.HttpUtility.wasm",
        "name": "System.Web.HttpUtility.f8h35mglyv.wasm",
        "hash": "sha256-//Z2H9zfRG5wTEuH0dxZSQnT0QdMiCBNDuzQQL9Wtks=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.Linq.wasm",
        "name": "System.Xml.Linq.pzw688hdq1.wasm",
        "hash": "sha256-e5txtle0V/jlZ+5laxHe+k2o2xewHh66AVpDrj8yXlc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.ReaderWriter.wasm",
        "name": "System.Xml.ReaderWriter.rwp16xe5gi.wasm",
        "hash": "sha256-lbxDheBvMQrtMB5TUbC2dqbCTl/50xjWr42cs70xRgA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XDocument.wasm",
        "name": "System.Xml.XDocument.mtfg4ozd64.wasm",
        "hash": "sha256-GggwMr3AE147MGBM3KluNnokYN4z8PXOzqOHRYSJARo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.wasm",
        "name": "System.400l3mulw9.wasm",
        "hash": "sha256-vPEm2KqgXqruGwnz718iSgmDoB8Img7iH3FzO57b0Rg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "netstandard.wasm",
        "name": "netstandard.nk62f4uiyj.wasm",
        "hash": "sha256-OROhsUoHvs7UUEsFNabKS/hpZWTbQwuMcsI4UKW8ZVg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CommonComponents.wasm",
        "name": "CommonComponents.2xgz4t0eom.wasm",
        "hash": "sha256-ZtirdYLYKUEY+wp1ophUL3WtEUaWhrODviU2RsvpFMg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharedComponents.wasm",
        "name": "SharedComponents.j8ndlu8dar.wasm",
        "hash": "sha256-HRxFIPdsoYbmVBkjqtlgInDcZql7rYwEDOHaoUy2Hu4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharedModels.wasm",
        "name": "SharedModels.3zley03dvy.wasm",
        "hash": "sha256-QcdQ1uQw4PrWFfoYdBNZ5+90JHsw2JULF9SE2V/44Hw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Web.wasm",
        "name": "Web.xjoyi9261a.wasm",
        "hash": "sha256-M5mt+6nnYsLozMSbhugQ9LN/PN4YdNNQctbT0v4aOIQ=",
        "cache": "force-cache"
      }
    ],
    "lazyAssembly": [
      {
        "virtualPath": "AIDemoComponents.wasm",
        "name": "AIDemoComponents.h95221tkug.wasm",
        "hash": "sha256-28JQW0gzeq3yzd2R1XVNt0mNLLu4wvzINJkiNOO+DcA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "BaseComponents.wasm",
        "name": "BaseComponents.uyaomfc6bt.wasm",
        "hash": "sha256-hv/dFCOyKaFol6AE7g21CoprgzzI9FIufrBUmDyhpyw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "BlazorDemoComponents.wasm",
        "name": "BlazorDemoComponents.3e3nrl5up1.wasm",
        "hash": "sha256-66/XwsuD0Fwmfh/0u6bJO4Pa5Ihy5hFwGrTiSwF20NE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CachingDemoComponents.wasm",
        "name": "CachingDemoComponents.x94549viy6.wasm",
        "hash": "sha256-G6OYn7sIl6PRF+3D273OMcll+oIPePU4Ioapp60TMmM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DatabaseDemoComponents.wasm",
        "name": "DatabaseDemoComponents.frqtx63cye.wasm",
        "hash": "sha256-u/WUW22HKoN8lFQKKUhuSaFmxorFkS53K6y3B+YYq4I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DDDDemoComponents.wasm",
        "name": "DDDDemoComponents.i68dh6wsgv.wasm",
        "hash": "sha256-VgzmANuWt9OqyV9LPvurdWMSqJ2kgZ3iOIytl2cdL/4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DependencyInjectionDemoComponents.wasm",
        "name": "DependencyInjectionDemoComponents.fqgib6j51l.wasm",
        "hash": "sha256-N1jBlO4iOLUS63iG8cSGueR11jcPlPUlAs0jLaN1MN8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DesignPatternDemoComponents.wasm",
        "name": "DesignPatternDemoComponents.orfp0n9gxi.wasm",
        "hash": "sha256-7JfkuuhUkIeoU9af751zmz0EFYGZca2gNNYaHdyJL1g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "HTTPClientDemoComponents.wasm",
        "name": "HTTPClientDemoComponents.gb841t474h.wasm",
        "hash": "sha256-HDipT9xW8zQK8D+io04Yt5U8/sF1RZl375d8ddKyS8c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "JSONDemoComponents.wasm",
        "name": "JSONDemoComponents.fu44r7twgp.wasm",
        "hash": "sha256-hzhSqdri7H8yhWS4UHwxCl+MkuqS27Yluu9ANUpBdfc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "LINQDemoComponents.wasm",
        "name": "LINQDemoComponents.xm4ezs1cnu.wasm",
        "hash": "sha256-+RHeHngl6IhMcaCARobzI54Z7WAD8DQt6sSs8yPRSCs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "LoggingDemoComponents.wasm",
        "name": "LoggingDemoComponents.ulbl1opn2j.wasm",
        "hash": "sha256-/oCZ1KF1KS7Ri7JWd7zo8KNj3b6KzRUGt4HgC+/dZDM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MAUIDemoComponents.wasm",
        "name": "MAUIDemoComponents.qunbd3xa2t.wasm",
        "hash": "sha256-fgll6cknLFekzYki+B7K6FHK+96QXYUnQsp1FE9pAKg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MCPDemoComponents.wasm",
        "name": "MCPDemoComponents.xnnl9ibrt4.wasm",
        "hash": "sha256-DQx5vl2jxI48U1HS3a22+WhoiGnIJklR6xHW/qWD2xA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MemoryDemoComponents.wasm",
        "name": "MemoryDemoComponents.96b6ma01vs.wasm",
        "hash": "sha256-7wOAnn37u2eRz+sPoHDPuOC8sK/PJyhCeMW0ssSVYA0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MiddlewareDemoComponents.wasm",
        "name": "MiddlewareDemoComponents.1rltrutvk8.wasm",
        "hash": "sha256-oymlW3T/EaKgpsLKtliQqyujdo07uwuE2yluq+O7pNU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MLNETDemoComponents.wasm",
        "name": "MLNETDemoComponents.slqkiksav6.wasm",
        "hash": "sha256-UNqjAl8S+CDpu7jDZImsGX58fl5wc982gefy++AtFRY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MSBuildDemoComponents.wasm",
        "name": "MSBuildDemoComponents.u57obct8x9.wasm",
        "hash": "sha256-QqWma/3Yz7J57+95mbeehgnxXCyppR44rcxz+garU0I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "NugetDemoComponents.wasm",
        "name": "NugetDemoComponents.qbtvqd5abk.wasm",
        "hash": "sha256-NiSAjmtPzX1iyRLuD3BQficSmtqoiY52cylQkzb1Mrs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "OOPSDemoComponents.wasm",
        "name": "OOPSDemoComponents.mkuo0tvmwm.wasm",
        "hash": "sha256-xrTKDn3nu81pcI3UQYwnwt+kN5vWxITeqhYawVUezJY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "OWASPDemoComponents.wasm",
        "name": "OWASPDemoComponents.zi7glwmbi1.wasm",
        "hash": "sha256-5aAnqKJdg7K69Fud8D8JjxRmyPVXF35OdXQNCYe4yvs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PythonDemoComponents.wasm",
        "name": "PythonDemoComponents.sangh2jnbr.wasm",
        "hash": "sha256-DD0egrt69tHI7kP65YhCxw8eIEYal9IBJMneFRe6i4k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "RegexDemoComponents.wasm",
        "name": "RegexDemoComponents.bufjperjzd.wasm",
        "hash": "sha256-DQiAfx6lB36Rph8FZe/YpTyRGzYBJz4JgTqphtPVnGY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ReportDemoComponents.wasm",
        "name": "ReportDemoComponents.756kmowghi.wasm",
        "hash": "sha256-G5IQtg+AYg1CBvRcl9cXqTM32ypeWehNuGO3sPMflYg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SecurityDemoComponents.wasm",
        "name": "SecurityDemoComponents.vl4pva2smt.wasm",
        "hash": "sha256-bRXiI0Dd8ruhNsaXaGg9C2OWoJW2MThEeH7JdDv4bWU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SignalRDemoComponents.wasm",
        "name": "SignalRDemoComponents.w2272rl9tv.wasm",
        "hash": "sha256-uNtCKGrtucH7dnTXHgK0JTCFhoGPzBIbWbYsbwLfFOc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SOLIDDemoComponents.wasm",
        "name": "SOLIDDemoComponents.oox6gbs80j.wasm",
        "hash": "sha256-+DOYmWdZlXet6Cn964QT5es1nEsQktV7e/EL/mJnbuo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TalkDemoComponents.wasm",
        "name": "TalkDemoComponents.bbra0d803d.wasm",
        "hash": "sha256-OYP+7cC+flOFvHLohr/kN62tSSuJLwMlN8d8egKwTy0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TDDDemoComponents.wasm",
        "name": "TDDDemoComponents.4utk7ca4ff.wasm",
        "hash": "sha256-DIvcsP3wNHQ+OS/tHI5Z54DPpNNIYQpNC5KtnykDq9Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TestingDemoComponents.wasm",
        "name": "TestingDemoComponents.jpd4vy8iu8.wasm",
        "hash": "sha256-Z2V+Et12tuVAgrFuYrgn+7x1hl2YXrnLWI7lOL1ygSU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "WebAPIDemoComponents.wasm",
        "name": "WebAPIDemoComponents.nyxgjcjevm.wasm",
        "hash": "sha256-72KP+VUOrptg1w7I515qvABHucPmAk/u2VGA54z21w8=",
        "cache": "force-cache"
      }
    ],
    "libraryInitializers": [
      {
        "name": "BlazorWasmPreRendering.Build.lfyg69o9wu.lib.module.js"
      }
    ],
    "modulesAfterConfigLoaded": [
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
