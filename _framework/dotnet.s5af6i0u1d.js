//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

var e=!1;const t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,4,1,96,0,0,3,2,1,0,10,8,1,6,0,6,64,25,11,11])),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),n=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),r=Symbol.for("wasm promise_control");function i(e,t){let o=null;const n=new Promise((function(n,r){o={isDone:!1,promise:null,resolve:t=>{o.isDone||(o.isDone=!0,n(t),e&&e())},reject:e=>{o.isDone||(o.isDone=!0,r(e),t&&t())}}}));o.promise=n;const i=n;return i[r]=o,{promise:i,promise_control:o}}function s(e){return e[r]}function a(e){e&&function(e){return void 0!==e[r]}(e)||Be(!1,"Promise is not controllable")}const l="__mono_message__",c=["debug","log","trace","warn","info","error"],d="MONO_WASM: ";let u,f,m,g,p,h;function w(e){g=e}function b(e){if(Pe.diagnosticTracing){const t="function"==typeof e?e():e;console.debug(d+t)}}function y(e,...t){console.info(d+e,...t)}function v(e,...t){console.info(e,...t)}function E(e,...t){console.warn(d+e,...t)}function _(e,...t){if(t&&t.length>0&&t[0]&&"object"==typeof t[0]){if(t[0].silent)return;if(t[0].toString)return void console.error(d+e,t[0].toString())}console.error(d+e,...t)}function x(e,t,o){return function(...n){try{let r=n[0];if(void 0===r)r="undefined";else if(null===r)r="null";else if("function"==typeof r)r=r.toString();else if("string"!=typeof r)try{r=JSON.stringify(r)}catch(e){r=r.toString()}t(o?JSON.stringify({method:e,payload:r,arguments:n.slice(1)}):[e+r,...n.slice(1)])}catch(e){m.error(`proxyConsole failed: ${e}`)}}}function j(e,t,o){f=t,g=e,m={...t};const n=`${o}/console`.replace("https://","wss://").replace("http://","ws://");u=new WebSocket(n),u.addEventListener("error",A),u.addEventListener("close",S),function(){for(const e of c)f[e]=x(`console.${e}`,T,!0)}()}function R(e){let t=30;const o=()=>{u?0==u.bufferedAmount||0==t?(e&&v(e),function(){for(const e of c)f[e]=x(`console.${e}`,m.log,!1)}(),u.removeEventListener("error",A),u.removeEventListener("close",S),u.close(1e3,e),u=void 0):(t--,globalThis.setTimeout(o,100)):e&&m&&m.log(e)};o()}function T(e){u&&u.readyState===WebSocket.OPEN?u.send(e):m.log(e)}function A(e){m.error(`[${g}] proxy console websocket error: ${e}`,e)}function S(e){m.debug(`[${g}] proxy console websocket closed: ${e}`,e)}function D(){Pe.preferredIcuAsset=O(Pe.config);let e="invariant"==Pe.config.globalizationMode;if(!e)if(Pe.preferredIcuAsset)Pe.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===Pe.config.globalizationMode||"all"===Pe.config.globalizationMode||"sharded"===Pe.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}Pe.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,Pe.preferredIcuAsset=null}const t="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",o=Pe.config.environmentVariables;if(void 0===o[t]&&e&&(o[t]="1"),void 0===o.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(o.TZ=e)}catch(e){y("failed to detect timezone, will fallback to UTC")}}function O(e){var t;if((null===(t=e.resources)||void 0===t?void 0:t.icu)&&"invariant"!=e.globalizationMode){const t=e.applicationCulture||(ke?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale),o=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(o.length>=1)return o[0].name}else t&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const t=e.split("-")[0];return"en"===t||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(t)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(t)):n="icudt.dat";if(n)for(let e=0;e<o.length;e++){const t=o[e];if(t.virtualPath===n)return t.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const C=class{constructor(e){this.url=e}toString(){return this.url}};async function k(e,t){try{const o="function"==typeof globalThis.fetch;if(Se){const n=e.startsWith("file://");if(!n&&o)return globalThis.fetch(e,t||{credentials:"same-origin"});p||(h=Ne.require("url"),p=Ne.require("fs")),n&&(e=h.fileURLToPath(e));const r=await p.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>r,json:()=>JSON.parse(r),text:()=>{throw new Error("NotImplementedException")}}}if(o)return globalThis.fetch(e,t||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(t){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t},text:()=>{throw t}}}throw new Error("No fetch implementation available")}function I(e){return"string"!=typeof e&&Be(!1,"url must be a string"),!M(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}const U=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,P=/[a-zA-Z]:[\\/]/;function M(e){return Se||Ie?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||P.test(e):U.test(e)}let L,N=0;const $=[],z=[],W=new Map,F={"js-module-threads":!0,"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},B={...F,"js-module-library-initializer":!0},V={...F,dotnetwasm:!0,heap:!0,manifest:!0},q={...B,manifest:!0},H={...B,dotnetwasm:!0},J={dotnetwasm:!0,symbols:!0},Z={...B,dotnetwasm:!0,symbols:!0},Q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=Pe.preferredIcuAsset)}function K(e,t,o){null!=t||(t=[]),Be(1==t.length,`Expect to have one ${o} asset in resources`);const n=t[0];return n.behavior=o,X(n),e.push(n),n}function X(e){V[e.behavior]&&W.set(e.behavior,e)}function Y(e){Be(V[e],`Unknown single asset behavior ${e}`);const t=W.get(e);if(t&&!t.resolvedUrl)if(t.resolvedUrl=Pe.locateFile(t.name),F[t.behavior]){const e=ge(t);e?("string"!=typeof e&&Be(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),t.resolvedUrl=e):t.resolvedUrl=ce(t.resolvedUrl,t.behavior)}else if("dotnetwasm"!==t.behavior)throw new Error(`Unknown single asset behavior ${e}`);return t}function ee(e){const t=Y(e);return Be(t,`Single asset for ${e} not found`),t}let te=!1;async function oe(){if(!te){te=!0,Pe.diagnosticTracing&&b("mono_download_assets");try{const e=[],t=[],o=(e,t)=>{!Z[e.behavior]&&G(e)&&Pe.expected_instantiated_assets_count++,!H[e.behavior]&&G(e)&&(Pe.expected_downloaded_assets_count++,t.push(se(e)))};for(const t of $)o(t,e);for(const e of z)o(e,t);Pe.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...t]).then((()=>{Pe.allDownloadsFinished.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),await Pe.runtimeModuleLoaded.promise;const n=async e=>{const t=await e;if(t.buffer){if(!Z[t.behavior]){t.buffer&&"object"==typeof t.buffer||Be(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof t.resolvedUrl&&Be(!1,"resolvedUrl must be string");const e=t.resolvedUrl,o=await t.buffer,n=new Uint8Array(o);pe(t),await Ue.beforeOnRuntimeInitialized.promise,Ue.instantiate_asset(t,e,n)}}else J[t.behavior]?("symbols"===t.behavior&&(await Ue.instantiate_symbols_asset(t),pe(t)),J[t.behavior]&&++Pe.actual_downloaded_assets_count):(t.isOptional||Be(!1,"Expected asset to have the downloaded buffer"),!H[t.behavior]&&G(t)&&Pe.expected_downloaded_assets_count--,!Z[t.behavior]&&G(t)&&Pe.expected_instantiated_assets_count--)},r=[],i=[];for(const t of e)r.push(n(t));for(const e of t)i.push(n(e));Promise.all(r).then((()=>{Ce||Ue.coreAssetsInMemory.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),Promise.all(i).then((async()=>{Ce||(await Ue.coreAssetsInMemory.promise,Ue.allAssetsInMemory.promise_control.resolve())})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e}))}catch(e){throw Pe.err("Error in mono_download_assets: "+e),e}}}let ne=!1;function re(){if(ne)return;ne=!0;const e=Pe.config,t=[];if(e.assets)for(const t of e.assets)"object"!=typeof t&&Be(!1,`asset must be object, it was ${typeof t} : ${t}`),"string"!=typeof t.behavior&&Be(!1,"asset behavior must be known string"),"string"!=typeof t.name&&Be(!1,"asset name must be string"),t.resolvedUrl&&"string"!=typeof t.resolvedUrl&&Be(!1,"asset resolvedUrl could be string"),t.hash&&"string"!=typeof t.hash&&Be(!1,"asset resolvedUrl could be string"),t.pendingDownload&&"object"!=typeof t.pendingDownload&&Be(!1,"asset pendingDownload could be object"),t.isCore?$.push(t):z.push(t),X(t);else if(e.resources){const o=e.resources;o.wasmNative||Be(!1,"resources.wasmNative must be defined"),o.jsModuleNative||Be(!1,"resources.jsModuleNative must be defined"),o.jsModuleRuntime||Be(!1,"resources.jsModuleRuntime must be defined"),K(z,o.wasmNative,"dotnetwasm"),K(t,o.jsModuleNative,"js-module-native"),K(t,o.jsModuleRuntime,"js-module-runtime"),o.jsModuleDiagnostics&&K(t,o.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,t,o)=>{const n=e;n.behavior=t,o?(n.isCore=!0,$.push(n)):z.push(n)};if(o.coreAssembly)for(let e=0;e<o.coreAssembly.length;e++)n(o.coreAssembly[e],"assembly",!0);if(o.assembly)for(let e=0;e<o.assembly.length;e++)n(o.assembly[e],"assembly",!o.coreAssembly);if(0!=e.debugLevel&&Pe.isDebuggingSupported()){if(o.corePdb)for(let e=0;e<o.corePdb.length;e++)n(o.corePdb[e],"pdb",!0);if(o.pdb)for(let e=0;e<o.pdb.length;e++)n(o.pdb[e],"pdb",!o.corePdb)}if(e.loadAllSatelliteResources&&o.satelliteResources)for(const e in o.satelliteResources)for(let t=0;t<o.satelliteResources[e].length;t++){const r=o.satelliteResources[e][t];r.culture=e,n(r,"resource",!o.coreAssembly)}if(o.coreVfs)for(let e=0;e<o.coreVfs.length;e++)n(o.coreVfs[e],"vfs",!0);if(o.vfs)for(let e=0;e<o.vfs.length;e++)n(o.vfs[e],"vfs",!o.coreVfs);const r=O(e);if(r&&o.icu)for(let e=0;e<o.icu.length;e++){const t=o.icu[e];t.name===r&&n(t,"icu",!1)}if(o.wasmSymbols)for(let e=0;e<o.wasmSymbols.length;e++)n(o.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let t=0;t<e.appsettings.length;t++){const o=e.appsettings[t],n=he(o);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||z.push({name:o,behavior:"vfs",cache:"no-cache",useCredentials:!0})}e.assets=[...$,...z,...t]}async function ie(e){const t=await se(e);return await t.pendingDownloadInternal.response,t.buffer}async function se(e){try{return await ae(e)}catch(t){if(!Pe.enableDownloadRetry)throw t;if(Ie||Se)throw t;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw t;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw t;if(t&&404==t.status)throw t;e.pendingDownloadInternal=void 0,await Pe.allDownloadsQueued.promise;try{return Pe.diagnosticTracing&&b(`Retrying download '${e.name}'`),await ae(e)}catch(t){return e.pendingDownloadInternal=void 0,await new Promise((e=>globalThis.setTimeout(e,100))),Pe.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await ae(e)}}}async function ae(e){for(;L;)await L.promise;try{++N,N==Pe.maxParallelDownloads&&(Pe.diagnosticTracing&&b("Throttling further parallel downloads"),L=i());const t=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const t=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>t,json:()=>JSON.parse(new TextDecoder("utf-8").decode(t)),text:()=>{throw new Error("NotImplementedException")},headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const t=e.loadRemote&&Pe.config.remoteSources?Pe.config.remoteSources:[""];let o;for(let n of t){n=n.trim(),"./"===n&&(n="");const t=le(e,n);e.name===t?Pe.diagnosticTracing&&b(`Attempting to download '${t}'`):Pe.diagnosticTracing&&b(`Attempting to download '${t}' for ${e.name}`);try{e.resolvedUrl=t;const n=fe(e);if(e.pendingDownloadInternal=n,o=await n.response,!o||!o.ok)continue;return o}catch(e){o||(o={ok:!1,url:t,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&Pe.config.ignorePdbLoadErrors;if(o||Be(!1,`Response undefined ${e.name}`),!n){const t=new Error(`download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`);throw t.status=o.status,t}y(`optional download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`)}(e);return t?(J[e.behavior]||(e.buffer=await t.arrayBuffer(),++Pe.actual_downloaded_assets_count),e):e}finally{if(--N,L&&N==Pe.maxParallelDownloads-1){Pe.diagnosticTracing&&b("Resuming more parallel downloads");const e=L;L=void 0,e.promise_control.resolve()}}}function le(e,t){let o;return null==t&&Be(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?o=e.resolvedUrl:(o=""===t?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:t+e.name,o=ce(Pe.locateFile(o),e.behavior)),o&&"string"==typeof o||Be(!1,"attemptUrl need to be path or url string"),o}function ce(e,t){return Pe.modulesUniqueQuery&&q[t]&&(e+=Pe.modulesUniqueQuery),e}let de=0;const ue=new Set;function fe(e){try{e.resolvedUrl||Be(!1,"Request's resolvedUrl must be set");const t=function(e){let t=e.resolvedUrl;if(Pe.loadBootResource){const o=ge(e);if(o instanceof Promise)return o;"string"==typeof o&&(t=o)}const o={};return e.cache?o.cache=e.cache:Pe.config.disableNoCacheFetch||(o.cache="no-cache"),e.useCredentials?o.credentials="include":!Pe.config.disableIntegrityCheck&&e.hash&&(o.integrity=e.hash),Pe.fetch_like(t,o)}(e),o={name:e.name,url:e.resolvedUrl,response:t};return ue.add(e.name),o.response.then((()=>{"assembly"==e.behavior&&Pe.loadedAssemblies.push(e.name),de++,Pe.onDownloadResourceProgress&&Pe.onDownloadResourceProgress(de,ue.size)})),o}catch(t){const o={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(o)}}}const me={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs","js-module-threads":"dotnetjs"};function ge(e){var t;if(Pe.loadBootResource){const o=null!==(t=e.hash)&&void 0!==t?t:"",n=e.resolvedUrl,r=me[e.behavior];if(r){const t=Pe.loadBootResource(r,e.name,n,o,e.behavior);return"string"==typeof t?I(t):t}}}function pe(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function he(e){let t=e.lastIndexOf("/");return t>=0&&t++,e.substring(t)}async function we(e){e&&await Promise.all((null!=e?e:[]).map((e=>async function(e){try{const t=e.name;if(!e.moduleExports){const o=ce(Pe.locateFile(t),"js-module-library-initializer");Pe.diagnosticTracing&&b(`Attempting to import '${o}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */o)}Pe.libraryInitializers.push({scriptName:t,exports:e.moduleExports})}catch(t){E(`Failed to import library initializer '${e}': ${t}`)}}(e))))}async function be(e,t){if(!Pe.libraryInitializers)return;const o=[];for(let n=0;n<Pe.libraryInitializers.length;n++){const r=Pe.libraryInitializers[n];r.exports[e]&&o.push(ye(r.scriptName,e,(()=>r.exports[e](...t))))}await Promise.all(o)}async function ye(e,t,o){try{await o()}catch(o){throw E(`Failed to invoke '${t}' on library initializer '${e}': ${o}`),Xe(1,o),o}}function ve(e,t){if(e===t)return e;const o={...t};return void 0!==o.assets&&o.assets!==e.assets&&(o.assets=[...e.assets||[],...o.assets||[]]),void 0!==o.resources&&(o.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},o.resources)),void 0!==o.environmentVariables&&(o.environmentVariables={...e.environmentVariables||{},...o.environmentVariables||{}}),void 0!==o.runtimeOptions&&o.runtimeOptions!==e.runtimeOptions&&(o.runtimeOptions=[...e.runtimeOptions||[],...o.runtimeOptions||[]]),Object.assign(e,o)}function Ee(e,t){if(e===t)return e;const o={...t};return o.config&&(e.config||(e.config={}),o.config=ve(e.config,o.config)),Object.assign(e,o)}function _e(e,t){if(e===t)return e;const o={...t};return void 0!==o.coreAssembly&&(o.coreAssembly=[...e.coreAssembly||[],...o.coreAssembly||[]]),void 0!==o.assembly&&(o.assembly=[...e.assembly||[],...o.assembly||[]]),void 0!==o.lazyAssembly&&(o.lazyAssembly=[...e.lazyAssembly||[],...o.lazyAssembly||[]]),void 0!==o.corePdb&&(o.corePdb=[...e.corePdb||[],...o.corePdb||[]]),void 0!==o.pdb&&(o.pdb=[...e.pdb||[],...o.pdb||[]]),void 0!==o.jsModuleWorker&&(o.jsModuleWorker=[...e.jsModuleWorker||[],...o.jsModuleWorker||[]]),void 0!==o.jsModuleNative&&(o.jsModuleNative=[...e.jsModuleNative||[],...o.jsModuleNative||[]]),void 0!==o.jsModuleDiagnostics&&(o.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...o.jsModuleDiagnostics||[]]),void 0!==o.jsModuleRuntime&&(o.jsModuleRuntime=[...e.jsModuleRuntime||[],...o.jsModuleRuntime||[]]),void 0!==o.wasmSymbols&&(o.wasmSymbols=[...e.wasmSymbols||[],...o.wasmSymbols||[]]),void 0!==o.wasmNative&&(o.wasmNative=[...e.wasmNative||[],...o.wasmNative||[]]),void 0!==o.icu&&(o.icu=[...e.icu||[],...o.icu||[]]),void 0!==o.satelliteResources&&(o.satelliteResources=function(e,t){if(e===t)return e;for(const o in t)e[o]=[...e[o]||[],...t[o]||[]];return e}(e.satelliteResources||{},o.satelliteResources||{})),void 0!==o.modulesAfterConfigLoaded&&(o.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...o.modulesAfterConfigLoaded||[]]),void 0!==o.modulesAfterRuntimeReady&&(o.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...o.modulesAfterRuntimeReady||[]]),void 0!==o.extensions&&(o.extensions={...e.extensions||{},...o.extensions||{}}),void 0!==o.vfs&&(o.vfs=[...e.vfs||[],...o.vfs||[]]),Object.assign(e,o)}function xe(){const e=Pe.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleWorker:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){Pe.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const t of e.assets){const o={};switch(t.behavior){case"assembly":o.assembly=[t];break;case"pdb":o.pdb=[t];break;case"resource":o.satelliteResources={},o.satelliteResources[t.culture]=[t];break;case"icu":o.icu=[t];break;case"symbols":o.wasmSymbols=[t];break;case"vfs":o.vfs=[t];break;case"dotnetwasm":o.wasmNative=[t];break;case"js-module-threads":o.jsModuleWorker=[t];break;case"js-module-runtime":o.jsModuleRuntime=[t];break;case"js-module-native":o.jsModuleNative=[t];break;case"js-module-diagnostics":o.jsModuleDiagnostics=[t];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${t.behavior} of asset ${t.name}`)}_e(e.resources,o)}}e.debugLevel,e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Ue.diagnosticTracing=Pe.diagnosticTracing=!!e.diagnosticTracing,Ue.waitForDebugger=e.waitForDebugger,Pe.maxParallelDownloads=e.maxParallelDownloads||Pe.maxParallelDownloads,Pe.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:Pe.enableDownloadRetry}let je=!1;async function Re(e){var t;if(je)return void await Pe.afterConfigLoaded.promise;let o;try{if(e.configSrc||Pe.config&&0!==Object.keys(Pe.config).length&&(Pe.config.assets||Pe.config.resources)||(e.configSrc="dotnet.boot.js"),o=e.configSrc,je=!0,o&&(Pe.diagnosticTracing&&b("mono_wasm_load_config"),await async function(e){const t=e.configSrc,o=Pe.locateFile(t);let n=null;void 0!==Pe.loadBootResource&&(n=Pe.loadBootResource("manifest",t,o,"","manifest"));let r,i=null;if(n)if("string"==typeof n)n.includes(".json")?(i=await s(I(n)),r=await Ae(i)):r=(await import(I(n))).config;else{const e=await n;"function"==typeof e.json?(i=e,r=await Ae(i)):r=e.config}else o.includes(".json")?(i=await s(ce(o,"manifest")),r=await Ae(i)):r=(await import(ce(o,"manifest"))).config;function s(e){return Pe.fetch_like(e,{method:"GET",credentials:"include",cache:"no-cache"})}Pe.config.applicationEnvironment&&(r.applicationEnvironment=Pe.config.applicationEnvironment),ve(Pe.config,r)}(e)),xe(),await we(null===(t=Pe.config.resources)||void 0===t?void 0:t.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[Pe.config]),e.onConfigLoaded)try{await e.onConfigLoaded(Pe.config,Le),xe()}catch(e){throw _("onConfigLoaded() failed",e),e}xe(),Pe.afterConfigLoaded.promise_control.resolve(Pe.config)}catch(t){const n=`Failed to load config file ${o} ${t} ${null==t?void 0:t.stack}`;throw Pe.config=e.config=Object.assign(Pe.config,{message:n,error:t,isError:!0}),Xe(1,new Error(n)),t}}function Te(){return!!globalThis.navigator&&(Pe.isChromium||Pe.isFirefox)}async function Ae(e){const t=Pe.config,o=await e.json();t.applicationEnvironment||o.applicationEnvironment||(o.applicationEnvironment=e.headers.get("Blazor-Environment")||e.headers.get("DotNet-Environment")||void 0),o.environmentVariables||(o.environmentVariables={});const n=e.headers.get("DOTNET-MODIFIABLE-ASSEMBLIES");n&&(o.environmentVariables.DOTNET_MODIFIABLE_ASSEMBLIES=n);const r=e.headers.get("ASPNETCORE-BROWSER-TOOLS");return r&&(o.environmentVariables.__ASPNETCORE_BROWSER_TOOLS=r),o}"function"!=typeof importScripts||globalThis.onmessage||(globalThis.dotnetSidecar=!0);const Se="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,De="function"==typeof importScripts,Oe=De&&"undefined"!=typeof dotnetSidecar,Ce=De&&!Oe,ke="object"==typeof window||De&&!Se,Ie=!ke&&!Se;let Ue={},Pe={},Me={},Le={},Ne={},$e=!1;const ze={},We={config:ze},Fe={mono:{},binding:{},internal:Ne,module:We,loaderHelpers:Pe,runtimeHelpers:Ue,diagnosticHelpers:Me,api:Le};function Be(e,t){if(e)return;const o="Assert failed: "+("function"==typeof t?t():t),n=new Error(o);_(o,n),Ue.nativeAbort(n)}function Ve(){return void 0!==Pe.exitCode}function qe(){return Ue.runtimeReady&&!Ve()}function He(){Ve()&&Be(!1,`.NET runtime already exited with ${Pe.exitCode} ${Pe.exitReason}. You can use runtime.runMain() which doesn't exit the runtime.`),Ue.runtimeReady||Be(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function Je(){ke&&(globalThis.addEventListener("unhandledrejection",et),globalThis.addEventListener("error",tt))}let Ze,Qe;function Ge(e){Qe&&Qe(e),Xe(e,Pe.exitReason)}function Ke(e){Ze&&Ze(e||Pe.exitReason),Xe(1,e||Pe.exitReason)}function Xe(t,o){var n,r;const i=o&&"object"==typeof o;t=i&&"number"==typeof o.status?o.status:void 0===t?-1:t;const s=i&&"string"==typeof o.message?o.message:""+o;(o=i?o:Ue.ExitStatus?function(e,t){const o=new Ue.ExitStatus(e);return o.message=t,o.toString=()=>t,o}(t,s):new Error("Exit with code "+t+" "+s)).status=t,o.message||(o.message=s);const a=""+(o.stack||(new Error).stack);try{Object.defineProperty(o,"stack",{get:()=>a})}catch(e){}const l=!!o.silent;if(o.silent=!0,Ve())Pe.diagnosticTracing&&b("mono_exit called after exit");else{try{We.onAbort==Ke&&(We.onAbort=Ze),We.onExit==Ge&&(We.onExit=Qe),ke&&(globalThis.removeEventListener("unhandledrejection",et),globalThis.removeEventListener("error",tt)),Ue.runtimeReady?(Ue.jiterpreter_dump_stats&&Ue.jiterpreter_dump_stats(!1),0===t&&(null===(n=Pe.config)||void 0===n?void 0:n.interopCleanupOnExit)&&Ue.forceDisposeProxies(!0,!0),e&&0!==t&&(null===(r=Pe.config)||void 0===r||r.dumpThreadsOnNonZeroExit)):(Pe.diagnosticTracing&&b(`abort_startup, reason: ${o}`),function(e){Pe.allDownloadsQueued.promise_control.reject(e),Pe.allDownloadsFinished.promise_control.reject(e),Pe.afterConfigLoaded.promise_control.reject(e),Pe.wasmCompilePromise.promise_control.reject(e),Pe.runtimeModuleLoaded.promise_control.reject(e),Ue.dotnetReady&&(Ue.dotnetReady.promise_control.reject(e),Ue.afterInstantiateWasm.promise_control.reject(e),Ue.beforePreInit.promise_control.reject(e),Ue.afterPreInit.promise_control.reject(e),Ue.afterPreRun.promise_control.reject(e),Ue.beforeOnRuntimeInitialized.promise_control.reject(e),Ue.afterOnRuntimeInitialized.promise_control.reject(e),Ue.afterPostRun.promise_control.reject(e))}(o))}catch(e){E("mono_exit A failed",e)}try{l||(function(e,t){if(0!==e&&t){const e=Ue.ExitStatus&&t instanceof Ue.ExitStatus?b:_;"string"==typeof t?e(t):(void 0===t.stack&&(t.stack=(new Error).stack+""),t.message?e(Ue.stringify_as_error_with_stack?Ue.stringify_as_error_with_stack(t.message+"\n"+t.stack):t.message+"\n"+t.stack):e(JSON.stringify(t)))}!Ce&&Pe.config&&(Pe.config.logExitCode?Pe.config.forwardConsoleLogsToWS?R("WASM EXIT "+e):v("WASM EXIT "+e):Pe.config.forwardConsoleLogsToWS&&R())}(t,o),function(e){if(ke&&!Ce&&Pe.config&&Pe.config.appendElementOnExit&&document){const t=document.createElement("label");t.id="tests_done",0!==e&&(t.style.background="red"),t.innerHTML=""+e,document.body.appendChild(t)}}(t))}catch(e){E("mono_exit B failed",e)}Pe.exitCode=t,Pe.exitReason||(Pe.exitReason=o),!Ce&&Ue.runtimeReady&&We.runtimeKeepalivePop()}if(Pe.config&&Pe.config.asyncFlushOnExit&&0===t)throw(async()=>{try{await async function(){try{const e=await import(/*! webpackIgnore: true */"process"),t=e=>new Promise(((t,o)=>{e.on("error",o),e.end("","utf8",t)})),o=t(e.stderr),n=t(e.stdout);let r;const i=new Promise((e=>{r=setTimeout((()=>e("timeout")),1e3)}));await Promise.race([Promise.all([n,o]),i]),clearTimeout(r)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Ye(t,o)}})(),o;Ye(t,o)}function Ye(e,t){if(Ue.runtimeReady&&Ue.nativeExit)try{Ue.nativeExit(e)}catch(e){!Ue.ExitStatus||e instanceof Ue.ExitStatus||E("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!ke)throw Se&&Ne.process?Ne.process.exit(e):Ue.quit&&Ue.quit(e,t),t}function et(e){ot(e,e.reason,"rejection")}function tt(e){ot(e,e.error,"error")}function ot(e,t,o){e.preventDefault();try{t||(t=new Error("Unhandled "+o)),void 0===t.stack&&(t.stack=(new Error).stack),t.stack=t.stack+"",t.silent||(_("Unhandled error:",t),Xe(1,t))}catch(e){}}!function(e){if($e)throw new Error("Loader module already loaded");$e=!0,Ue=e.runtimeHelpers,Pe=e.loaderHelpers,Me=e.diagnosticHelpers,Le=e.api,Ne=e.internal,Object.assign(Le,{INTERNAL:Ne,invokeLibraryInitializers:be}),Object.assign(e.module,{config:ve(ze,{environmentVariables:{}})});const r={mono_wasm_bindings_is_ready:!1,config:e.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"0427b9abbead3fa64c6655eb4290c537397131fa",config:e.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:i(),allDownloadsQueued:i(),allDownloadsFinished:i(),wasmCompilePromise:i(),runtimeModuleLoaded:i(),loadingWorkers:i(),is_exited:Ve,is_runtime_running:qe,assert_runtime_running:He,mono_exit:Xe,createPromiseController:i,getPromiseController:s,assertIsControllablePromise:a,mono_download_assets:oe,resolve_single_asset_path:ee,setup_proxy_console:j,set_thread_prefix:w,installUnhandledErrorHandler:Je,retrieve_asset_download:ie,invokeLibraryInitializers:be,isDebuggingSupported:Te,exceptions:t,simd:n,relaxedSimd:o};Object.assign(Ue,r),Object.assign(Pe,l)}(Fe);let nt,rt,it,st=!1,at=!1;async function lt(e){if(!at){if(at=!0,ke&&Pe.config.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&j("main",globalThis.console,globalThis.location.origin),We||Be(!1,"Null moduleConfig"),Pe.config||Be(!1,"Null moduleConfig.config"),"function"==typeof e){const t=e(Fe.api);if(t.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(We,t),Ee(We,t)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");Ee(We,e)}await async function(e){if(Se){const e=await import(/*! webpackIgnore: true */"process"),t=14;if(e.versions.node.split(".")[0]<t)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${t}. See also https://aka.ms/dotnet-wasm-features`)}const t=/*! webpackIgnore: true */import.meta.url,o=t.indexOf("?");var n;if(o>0&&(Pe.modulesUniqueQuery=t.substring(o)),Pe.scriptUrl=t.replace(/\\/g,"/").replace(/[?#].*/,""),Pe.scriptDirectory=(n=Pe.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",Pe.locateFile=e=>"URL"in globalThis&&globalThis.URL!==C?new URL(e,Pe.scriptDirectory).toString():M(e)?e:Pe.scriptDirectory+e,Pe.fetch_like=k,Pe.out=console.log,Pe.err=console.error,Pe.onDownloadResourceProgress=e.onDownloadResourceProgress,ke&&globalThis.navigator){const e=globalThis.navigator,t=e.userAgentData&&e.userAgentData.brands;t&&t.length>0?Pe.isChromium=t.some((e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand)):e.userAgent&&(Pe.isChromium=e.userAgent.includes("Chrome"),Pe.isFirefox=e.userAgent.includes("Firefox"))}Ne.require=Se?await import(/*! webpackIgnore: true */"module").then((e=>e.createRequire(/*! webpackIgnore: true */import.meta.url))):Promise.resolve((()=>{throw new Error("require not supported")})),void 0===globalThis.URL&&(globalThis.URL=C)}(We)}}async function ct(e){return await lt(e),Ze=We.onAbort,Qe=We.onExit,We.onAbort=Ke,We.onExit=Ge,We.ENVIRONMENT_IS_PTHREAD?async function(){(function(){const e=new MessageChannel,t=e.port1,o=e.port2;t.addEventListener("message",(e=>{var n,r;n=JSON.parse(e.data.config),r=JSON.parse(e.data.monoThreadInfo),st?Pe.diagnosticTracing&&b("mono config already received"):(ve(Pe.config,n),Ue.monoThreadInfo=r,xe(),Pe.diagnosticTracing&&b("mono config received"),st=!0,Pe.afterConfigLoaded.promise_control.resolve(Pe.config),ke&&n.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&Pe.setup_proxy_console("worker-idle",console,globalThis.location.origin)),t.close(),o.close()}),{once:!0}),t.start(),self.postMessage({[l]:{monoCmd:"preload",port:o}},[o])})(),await Pe.afterConfigLoaded.promise,function(){const e=Pe.config;e.assets||Be(!1,"config.assets must be defined");for(const t of e.assets)X(t),Q[t.behavior]&&z.push(t)}(),setTimeout((async()=>{try{await oe()}catch(e){Xe(1,e)}}),0);const e=dt(),t=await Promise.all(e);return await ut(t),We}():async function(){var e;await Re(We),re();const t=dt();(async function(){try{const e=ee("dotnetwasm");await se(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||Be(!1,"Can't load dotnet.native.wasm");const t=await e.pendingDownloadInternal.response,o=t.headers&&t.headers.get?t.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===o)n=await WebAssembly.compileStreaming(t);else{ke&&"application/wasm"!==o&&E('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await t.arrayBuffer();Pe.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Ie?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,Pe.wasmCompilePromise.promise_control.resolve(n)}catch(e){Pe.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout((async()=>{try{D(),await oe()}catch(e){Xe(1,e)}}),0);const o=await Promise.all(t);return await ut(o),await Ue.dotnetReady.promise,await we(null===(e=Pe.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[Fe.api]),Le}()}function dt(){const e=ee("js-module-runtime"),t=ee("js-module-native");if(nt&&rt)return[nt,rt,it];"object"==typeof e.moduleExports?nt=e.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),nt=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof t.moduleExports?rt=t.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),rt=import(/*! webpackIgnore: true */t.resolvedUrl));const o=Y("js-module-diagnostics");return o&&("object"==typeof o.moduleExports?it=o.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),it=import(/*! webpackIgnore: true */o.resolvedUrl))),[nt,rt,it]}async function ut(e){const{initializeExports:t,initializeReplacements:o,configureRuntimeStartup:n,configureEmscriptenStartup:r,configureWorkerStartup:i,setRuntimeGlobals:s,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];s(Fe),t(Fe),c&&c.setRuntimeGlobals(Fe),await n(We),Pe.runtimeModuleLoaded.promise_control.resolve(),l((e=>(Object.assign(We,{ready:e.ready,__dotnet_runtime:{initializeReplacements:o,configureEmscriptenStartup:r,configureWorkerStartup:i,passEmscriptenInternals:a}}),We))).catch((e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize. See also https://aka.ms/dotnet-wasm-features");throw e}))}const ft=new class{withModuleConfig(e){try{return Ee(We,e),this}catch(e){throw Xe(1,e),e}}withOnConfigLoaded(e){try{return Ee(We,{onConfigLoaded:e}),this}catch(e){throw Xe(1,e),e}}withConsoleForwarding(){try{return ve(ze,{forwardConsoleLogsToWS:!0}),this}catch(e){throw Xe(1,e),e}}withExitOnUnhandledError(){try{return ve(ze,{exitOnUnhandledError:!0}),Je(),this}catch(e){throw Xe(1,e),e}}withAsyncFlushOnExit(){try{return ve(ze,{asyncFlushOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withExitCodeLogging(){try{return ve(ze,{logExitCode:!0}),this}catch(e){throw Xe(1,e),e}}withElementOnExit(){try{return ve(ze,{appendElementOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withInteropCleanupOnExit(){try{return ve(ze,{interopCleanupOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withDumpThreadsOnNonZeroExit(){try{return ve(ze,{dumpThreadsOnNonZeroExit:!0}),this}catch(e){throw Xe(1,e),e}}withWaitingForDebugger(e){try{return ve(ze,{waitForDebugger:e}),this}catch(e){throw Xe(1,e),e}}withInterpreterPgo(e,t){try{return ve(ze,{interpreterPgo:e,interpreterPgoSaveDelay:t}),ze.runtimeOptions?ze.runtimeOptions.push("--interp-pgo-recording"):ze.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Xe(1,e),e}}withConfig(e){try{return ve(ze,e),this}catch(e){throw Xe(1,e),e}}withConfigSrc(e){try{return e&&"string"==typeof e||Be(!1,"must be file path or URL"),Ee(We,{configSrc:e}),this}catch(e){throw Xe(1,e),e}}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||Be(!1,"must be directory path"),ve(ze,{virtualWorkingDirectory:e}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariable(e,t){try{const o={};return o[e]=t,ve(ze,{environmentVariables:o}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||Be(!1,"must be dictionary object"),ve(ze,{environmentVariables:e}),this}catch(e){throw Xe(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&Be(!1,"must be boolean"),ve(ze,{diagnosticTracing:e}),this}catch(e){throw Xe(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||Be(!1,"must be number"),ve(ze,{debugLevel:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ve(ze,{applicationArguments:e}),this}catch(e){throw Xe(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ze.runtimeOptions?ze.runtimeOptions.push(...e):ze.runtimeOptions=e,this}catch(e){throw Xe(1,e),e}}withMainAssembly(e){try{return ve(ze,{mainAssemblyName:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Xe(1,e),e}}withApplicationEnvironment(e){try{return ve(ze,{applicationEnvironment:e}),this}catch(e){throw Xe(1,e),e}}withApplicationCulture(e){try{return ve(ze,{applicationCulture:e}),this}catch(e){throw Xe(1,e),e}}withResourceLoader(e){try{return Pe.loadBootResource=e,this}catch(e){throw Xe(1,e),e}}async download(){try{await async function(){lt(We),await Re(We),re(),D(),oe(),await Pe.allDownloadsFinished.promise}()}catch(e){throw Xe(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await ct(We),Fe.api}()),this.instance}catch(e){throw Xe(1,e),e}}async run(){try{return We.config||Be(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Xe(1,e),e}}},mt=Xe,gt=ct;Ie||"function"==typeof globalThis.URL||Be(!1,"This browser/engine doesn't support URL API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),"function"!=typeof globalThis.BigInt64Array&&Be(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),ft.withConfig(/*json-start*/{
  "mainAssemblyName": "Web",
  "resources": {
    "hash": "sha256-mDJ28N3deJqTi9xAQKzCEup+Kl2LLYN6kePiSdfsdAU=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.km221qzxwv.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.5nc8i5dl74.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.vm2nol4th4.wasm",
        "hash": "sha256-6Ya+9ekajhbL54MOxUXeV52qJoVZY1Kri5UhwbrN4PU=",
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
        "name": "System.Runtime.InteropServices.JavaScript.2jjyfzobb9.wasm",
        "hash": "sha256-XrJyxfNRVFdVJHQDTfn8u2iNz16ve8POLFrT7hLHCgo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.CoreLib.wasm",
        "name": "System.Private.CoreLib.wp3y0oefeu.wasm",
        "hash": "sha256-VEcGIlHdLfcdr7GxSOXZuPFIH7d0DBV6E86SDj2eqY0=",
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
        "name": "FluentValidation.x8zt1bp2on.wasm",
        "hash": "sha256-sRStDbbNBwN7d/DUuIkWLC4ehOtqrkxOzmC/B7Ju/Qw=",
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
        "name": "Microsoft.AspNetCore.Authorization.78ieqqvlqf.wasm",
        "hash": "sha256-x4XNNajBR64RK3sbQSfIqwzchKvj63zkTR/NBQhGx9M=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.wasm",
        "name": "Microsoft.AspNetCore.Components.7tag2l3o20.wasm",
        "hash": "sha256-u4BDvVEgxdBJINe3G/pBjzh8wusH+fF/FOkYHwqCgX4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Components.Authorization.e95k74mlfq.wasm",
        "hash": "sha256-XhFm/Or5S3XOGpLynNdCdAGiOc6VtjVr5urlpFKwlUU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Forms.wasm",
        "name": "Microsoft.AspNetCore.Components.Forms.yuohbolrnc.wasm",
        "hash": "sha256-5gwmCzjwDw68WQnjH+pxeqwFxPIbs4HwhWLHTsRmYxc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Web.wasm",
        "name": "Microsoft.AspNetCore.Components.Web.8rod59t2o2.wasm",
        "hash": "sha256-bAjVu5V82NSuBHoQZu+fgKcZIvYwSY9Eak2JxKOVrrk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.1thlsacvj5.wasm",
        "hash": "sha256-my5jMRM7fCS1AD0draMmsDQb7PKsVPTt4Dt5LN6axCY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.ewu4zjgoov.wasm",
        "hash": "sha256-bqS5aZS9vcwH+KjZU4olXZhVU6Wzi+OzGb3kb05f1Cs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Metadata.wasm",
        "name": "Microsoft.AspNetCore.Metadata.btp3yvzsmd.wasm",
        "hash": "sha256-bxN85TZ9a+hC5QUdVgt519QIGYl6seJAwWjB04kkA94=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.wasm",
        "name": "Microsoft.Extensions.Configuration.x516eatd24.wasm",
        "hash": "sha256-ZZHl53TaUKUpn4WIqCGc1CReHPoKVkEngBHD54arYr4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Abstractions.wasm",
        "name": "Microsoft.Extensions.Configuration.Abstractions.hu9vtlt0go.wasm",
        "hash": "sha256-ZztuaEKfKHmG+V5FBHcixx0cTWaFPyiktKHAZfkGtjg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Binder.wasm",
        "name": "Microsoft.Extensions.Configuration.Binder.s6qium65tg.wasm",
        "hash": "sha256-7BTpSrAnGTyXAi5iCapXMLQzlJJbbI0SEFYA8/EDNk0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Json.wasm",
        "name": "Microsoft.Extensions.Configuration.Json.frazqtnewj.wasm",
        "hash": "sha256-O//lmM7iBWUtBjH2FJ3NMhOI8xRMCAko/BmV8+lm5Do=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.189i9znxqr.wasm",
        "hash": "sha256-zzsR01ucXULnjBYy8NQ5m+R4sWJXU9h4xn8nIs9iGSI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.Abstractions.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.Abstractions.31i9rv4md7.wasm",
        "hash": "sha256-h1oT7cl8qjQQxnFbRaht7Tv+t44Uhus+Ac1SZx9bD9k=",
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
        "name": "Microsoft.Extensions.Diagnostics.bxaqihm2zw.wasm",
        "hash": "sha256-MTCw9IHKT/embejhT2xgfr+gG3E4FfApCb7E/Zs3Xls=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.Abstractions.wasm",
        "name": "Microsoft.Extensions.Diagnostics.Abstractions.ujo8ugvduy.wasm",
        "hash": "sha256-toPsyAb36WLbxAvfr71x5DRiqK64Tnu/ekc0TGb4kko=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Http.wasm",
        "name": "Microsoft.Extensions.Http.c6rao7l3f4.wasm",
        "hash": "sha256-7LMATEsxD2nipMpGNMSbhUv7fLZdISUua8ILm1ltLV8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.wasm",
        "name": "Microsoft.Extensions.Logging.908v71ui33.wasm",
        "hash": "sha256-t1r43RHLa/HU2aIPgO5cnSyzzzC9v+YCcFXRavbVGTw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.Abstractions.wasm",
        "name": "Microsoft.Extensions.Logging.Abstractions.sfuz9eqdfs.wasm",
        "hash": "sha256-2AWV3XEaOZE1qgf3JPY/6sQe0Druezpui41Q1pkO43g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Options.wasm",
        "name": "Microsoft.Extensions.Options.5gk997zllc.wasm",
        "hash": "sha256-O5sINdYRaMqBUD2CHz29yF4VuzRsd1w1N5DscRC7Tw0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Primitives.wasm",
        "name": "Microsoft.Extensions.Primitives.1w0lc1mcj1.wasm",
        "hash": "sha256-wYd6P1RQVdx1wLGbVUXdca6pRBBwPJthqnLyxO6UMf0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Validation.wasm",
        "name": "Microsoft.Extensions.Validation.lspj8863f6.wasm",
        "hash": "sha256-q66DgMDVe0j5t5W6CBWuOybJfxVf1p7oarfQQ5bXyH0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.JSInterop.wasm",
        "name": "Microsoft.JSInterop.g6c0n47xtu.wasm",
        "hash": "sha256-qPTcQllcWp4FujCDA0/PO+Np4GbNwHeZYhltTSCZV3I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.JSInterop.WebAssembly.wasm",
        "name": "Microsoft.JSInterop.WebAssembly.gfego8d62w.wasm",
        "hash": "sha256-9MrMSaF3Ux7qJdu19tfa59lYtIOsndS8NEbyw4VNnOA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Core.wasm",
        "name": "Microsoft.ML.Core.fi8frnfw3y.wasm",
        "hash": "sha256-13/4jC8VEhg/6A7SUDZRyBELAs2x3Seb+xekqrYwO+c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Data.wasm",
        "name": "Microsoft.ML.Data.ir50houut5.wasm",
        "hash": "sha256-0nHs3l7aRp3fSW3AbndcLMw7Bq3DyOauPw5D64jsvBk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.KMeansClustering.wasm",
        "name": "Microsoft.ML.KMeansClustering.brd0467i5f.wasm",
        "hash": "sha256-WW5+HhbxbADaiszGEbXt5Kg670kqtvsclXDNbeTomqo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.PCA.wasm",
        "name": "Microsoft.ML.PCA.4unjzb6aw4.wasm",
        "hash": "sha256-bO7MKxYip019p6vvXwCp+L4q3SbO70KmAyPxCxnscfo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.StandardTrainers.wasm",
        "name": "Microsoft.ML.StandardTrainers.4aspj3vogm.wasm",
        "hash": "sha256-bCp6Pwo+ThA0R6KtX1p5100AXAwtpm6nyekAktvREA0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.Transforms.wasm",
        "name": "Microsoft.ML.Transforms.oh388whpfa.wasm",
        "hash": "sha256-rJvi5PdU4mZ1fxcGR+ur9rLaCU9d34xS6KJ+4tKjRS4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.CpuMath.wasm",
        "name": "Microsoft.ML.CpuMath.56fwqwjf04.wasm",
        "hash": "sha256-zd7pAwFizYQ9i3YpkUl4Ysxr4yWxptGQQDnrta4Uu8o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.ML.DataView.wasm",
        "name": "Microsoft.ML.DataView.h4aud8fxy4.wasm",
        "hash": "sha256-wQuC6ot8iNYATMict1DPO+P2pu3zMYWN7fP2xUdguA8=",
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
        "name": "QuestPDF.zx8jhw0022.wasm",
        "hash": "sha256-+RqQdhI3JPLRn1tEiUZZeTCk/x7uS5NwjYFqJkc/tPY=",
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
        "name": "Microsoft.CSharp.c7k0uaf6pg.wasm",
        "hash": "sha256-uOMziuhSwqilvI7W5omKJjcSXokOKfl5b5hQuARCq+s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Buffers.wasm",
        "name": "System.Buffers.70xyqngo10.wasm",
        "hash": "sha256-l/G/tMZBmhEfWayHgtUgzDDI2pcW+g+waKkRoIHoeLw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.92emyrdmd9.wasm",
        "hash": "sha256-se6ks+fDwGEej0rZoz7sOtNZkaX032NigLA8P5/96RU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.ubnx3qgk79.wasm",
        "hash": "sha256-Wn/iBDur6r5RiGy+4kYn5zQzcy5gZi8FS2vio+edyEI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.NonGeneric.wasm",
        "name": "System.Collections.NonGeneric.9zpk35xu9p.wasm",
        "hash": "sha256-e5hFbXDOb721aLB1yDS4CS1UMM8+t7ZERT3bUQHMs0Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Specialized.wasm",
        "name": "System.Collections.Specialized.7jc60db21m.wasm",
        "hash": "sha256-8whaugbjG4NwGr9W/dzjTkYbaRVQIrXlzNVKpPqjcd8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.n3bsrfi5cn.wasm",
        "hash": "sha256-zeEZJn4mkK6PrwaEK34ZTw3BZmE42eJapit9tShrw8c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Annotations.wasm",
        "name": "System.ComponentModel.Annotations.otf47h13ra.wasm",
        "hash": "sha256-5LvXUopyXXCkocCgED+Nny2/QRrN4bDCThtMm9Km7gQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Primitives.wasm",
        "name": "System.ComponentModel.Primitives.jvefkhf8k4.wasm",
        "hash": "sha256-D2Go3mOivUoxTpQPtYsMW+f/dD3hujJreAn1izGJoRo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.TypeConverter.wasm",
        "name": "System.ComponentModel.TypeConverter.4wlmesky4q.wasm",
        "hash": "sha256-VJaO7ewyiDEbwLbhTrEHm1BA5sKa5XqJ40Gh+lmLZj0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.vcj9uwgm8b.wasm",
        "hash": "sha256-wVelT5q2Ayvz9D1JWzDUNVki/rxamE8kOKs/ckVx+70=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.re79fhrz99.wasm",
        "hash": "sha256-Pa/N+B8iBmq2OWdgnZSxQg/8L1QtGkheZ+AU/E9YpIU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Core.wasm",
        "name": "System.Core.nl4hywcvly.wasm",
        "hash": "sha256-dWzsWnNsk9vOu/qlR/ykSLRYnd/VH6L+ZJBaW1q5kxY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.Common.wasm",
        "name": "System.Data.Common.435jc8fqam.wasm",
        "hash": "sha256-gEMTJLDFdc3A4itwyQJ4kXyR7q3ZdxD8xODnUSQnwRU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Debug.wasm",
        "name": "System.Diagnostics.Debug.gz40n8kz70.wasm",
        "hash": "sha256-xE9m2hOKmxSH9eSnEmHI8Uo3hxIWm+I1iZbELpn3Z1s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.17wmdod89s.wasm",
        "hash": "sha256-fakrIOiqat122bG2hGGlhmXZgiyUKeLbXcr7uvJMNPU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Process.wasm",
        "name": "System.Diagnostics.Process.k2wd79n0tz.wasm",
        "hash": "sha256-nH4Qg3kdh1cFvaylSOivOcbIzhcRSzQYRo2DSFXqcxc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Tools.wasm",
        "name": "System.Diagnostics.Tools.2kiz2vga7t.wasm",
        "hash": "sha256-Thk/1xhkqIDgS6s76+wiIxTIgof/X5lXyLJj+P5LIZk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.TraceSource.wasm",
        "name": "System.Diagnostics.TraceSource.h7w1l7u4vr.wasm",
        "hash": "sha256-Tepd3m836x0Ie0o51vLOmbAVRLtbmvWZSpvlLi8Cs/0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.Primitives.wasm",
        "name": "System.Drawing.Primitives.6jki77mp6q.wasm",
        "hash": "sha256-cruof8tq8MlhkEIHtWYVihfTrL0TcSE6yg61rrYFl7Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.wasm",
        "name": "System.Drawing.r5f079tipk.wasm",
        "hash": "sha256-v8fB+rYlrZlKumwHku/jCAAn4sl4ga5n9OfjpWDMAxo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.Brotli.wasm",
        "name": "System.IO.Compression.Brotli.hjo9efox3l.wasm",
        "hash": "sha256-fQ6Ua6igKJ4JYoXxJf7rPOy8dNqAuUJzmqGoJL14Bls=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.ZipFile.wasm",
        "name": "System.IO.Compression.ZipFile.ynnf1slukr.wasm",
        "hash": "sha256-jobmfEC14iAjMVlOaDOFomaxd+QeCtkirA7uVxgQgv0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.wasm",
        "name": "System.IO.Compression.c25c6ckt2i.wasm",
        "hash": "sha256-XGVpHzEk70TPLWflQ9oiPMGNDskrZYGDAIdvDMLH2gk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.wasm",
        "name": "System.IO.FileSystem.l5mfws7n7e.wasm",
        "hash": "sha256-6V1pagIStWx7EMSNHOpWMqgt0+/U72kmN0Vc9Hu4iLc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.MemoryMappedFiles.wasm",
        "name": "System.IO.MemoryMappedFiles.3i6k1xkp00.wasm",
        "hash": "sha256-FV0n/faB4oX0qNWjiOxABu96F1Lj06T6MU/4rJXqMSA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.d9jdqrkwk1.wasm",
        "hash": "sha256-P3efMSOfVCOb4KRJxC8w5mdhwshNltAbNhD4m3E4ekU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Expressions.wasm",
        "name": "System.Linq.Expressions.jycvcmt0cz.wasm",
        "hash": "sha256-W8fAwEjHfkAz3etxYLEnnIwKRML45JsVZYhVz6+CEaw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Parallel.wasm",
        "name": "System.Linq.Parallel.apw4b79y22.wasm",
        "hash": "sha256-cgoy9+zTCbn2YVMkFQpdKb2t1lVB5p1uQpAL2BXMWGM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Queryable.wasm",
        "name": "System.Linq.Queryable.h7i9yd7cb1.wasm",
        "hash": "sha256-kxUE2oePfJzCH3dzAte9jf6aTIoD6ZEjLdFXaeX+SqE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.6fcxbdn4ya.wasm",
        "hash": "sha256-ImyJSZ6BcwY+8m+kxxsKJfQSAkalk/zl5bzAnIcTzmU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.5ffq1zzuq6.wasm",
        "hash": "sha256-b3KyWS2YVIueKC9XPt/NGBKHrfr8q5ohXrm1JpWpDjc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.gp44xxxa5u.wasm",
        "hash": "sha256-1GV8rclBDGIObkhok6oWNlq43xkUCPf1l9uiRNixdg8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.q0v81vgq8d.wasm",
        "hash": "sha256-CkwZh+iTBD0UM0dGpU6UVauTEI0+4+sdrYI1UpzBSiU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Requests.wasm",
        "name": "System.Net.Requests.5tau62ec41.wasm",
        "hash": "sha256-x38VTYfwGczqQPQ+5w/mcD+GAzXvgtXEdEhZts6xGjQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebHeaderCollection.wasm",
        "name": "System.Net.WebHeaderCollection.3edxsvlu1z.wasm",
        "hash": "sha256-CwrEPN6MrTsmNPj6CrEstgWgtr7eiW7gW2CH8BHtJnk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.Vectors.wasm",
        "name": "System.Numerics.Vectors.n6e1s79ls4.wasm",
        "hash": "sha256-8RmoiMsx0suHbXdJxCG3xzi046QYLMXOB9mhTKaPia4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ObjectModel.wasm",
        "name": "System.ObjectModel.wg5p2hi7w6.wasm",
        "hash": "sha256-8O0UGChjHMxbq8Yo+ImQLEn+6guZoBTAZNzWlku14lg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.DataContractSerialization.wasm",
        "name": "System.Private.DataContractSerialization.svxm9twvyk.wasm",
        "hash": "sha256-+K3mMsRiBm2W8Wse8hwHwHfD3xy8TPs6HYVi1QkUQqg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.uqg9w5uitc.wasm",
        "hash": "sha256-+o7ewHqexv3kgngA4uikaLa82ycN2rD4M/I6ReprUz8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.Linq.wasm",
        "name": "System.Private.Xml.Linq.6g2doi5yaq.wasm",
        "hash": "sha256-pr3xCQFO4Et89ftTpcAY/qYiXg4aSUU/fvVsMvs8JSw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.wasm",
        "name": "System.Private.Xml.ehxgq49aox.wasm",
        "hash": "sha256-ggczlvXbnFs83OWzi2dXbvbEztPkJH96bXfnOAnjTAQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.ILGeneration.wasm",
        "name": "System.Reflection.Emit.ILGeneration.e1getdoit8.wasm",
        "hash": "sha256-TibduNlUzQLgQcsJZQR2qlmuLima987KTMD8u1B+5MM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.Lightweight.wasm",
        "name": "System.Reflection.Emit.Lightweight.bj2uzyyheu.wasm",
        "hash": "sha256-qWwn+J/8DRqtj7vKmvZq1qqSVfWl+Rk5Ru1vZnhxK60=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.wasm",
        "name": "System.Reflection.Emit.szamwhcvc4.wasm",
        "hash": "sha256-KGWDLCTSISUd7iGFZD79fV/OsWn1UmPpWxyHNCHivws=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Metadata.wasm",
        "name": "System.Reflection.Metadata.lginkimne0.wasm",
        "hash": "sha256-NfmBDKMVuVhgHaBrej/er/9q1e7A3WWgSNiRljGgowQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Primitives.wasm",
        "name": "System.Reflection.Primitives.hix6z1bv0a.wasm",
        "hash": "sha256-KQY45UYGK3hAhxayOw4/KVMEnXKjWSocHcLiiEA1/5U=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.ResourceManager.wasm",
        "name": "System.Resources.ResourceManager.38scs7vqj7.wasm",
        "hash": "sha256-HV76S/fdryzSwBi2ivfBlljpLDXusgDj9JprHg2FV1I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.CompilerServices.Unsafe.wasm",
        "name": "System.Runtime.CompilerServices.Unsafe.4i0hcdhs1i.wasm",
        "hash": "sha256-zAzi9HCT0oZTbjJDBynJVskDqxK/mfJIUq4xN4ukdLk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Extensions.wasm",
        "name": "System.Runtime.Extensions.fcxbadrdu3.wasm",
        "hash": "sha256-jwtdwRDa9o8CkxzBVgzUpGnA3AAuV7gb23hrQLdLTnU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.RuntimeInformation.wasm",
        "name": "System.Runtime.InteropServices.RuntimeInformation.6iyhb6yd5i.wasm",
        "hash": "sha256-EhTjluanBeSBcNQ34+JLT9Uo3v6InPiqTpVPpWseOfQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.wasm",
        "name": "System.Runtime.InteropServices.r34ha4xdvi.wasm",
        "hash": "sha256-+aanVTQ0gjD8fRltvwbFcnHGn2n/ucyNz/IgA05LOm8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Intrinsics.wasm",
        "name": "System.Runtime.Intrinsics.wyz1tbls8r.wasm",
        "hash": "sha256-WAUd5TEuFH4uKhHIy3e51fBNRCLnV17+KKcRQ7kb9Ag=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Numerics.wasm",
        "name": "System.Runtime.Numerics.s23d456qh4.wasm",
        "hash": "sha256-1OjSdfbedd82/OApnOkg28Jc3HUOXos8kgibFqsTgUA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Formatters.wasm",
        "name": "System.Runtime.Serialization.Formatters.5a6fizjzcc.wasm",
        "hash": "sha256-jXupdnDfPRMI5VVX0k9XO581MVuAGFUo/RwaGb6PBIc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Primitives.wasm",
        "name": "System.Runtime.Serialization.Primitives.zklzxo4acq.wasm",
        "hash": "sha256-AznV8rGe3jEzsfG4wdQ2aHByWgsNbEgMyHWScyuPJh8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Xml.wasm",
        "name": "System.Runtime.Serialization.Xml.p7lbfr0yjv.wasm",
        "hash": "sha256-24RUXPE5MgLCT95POmOjcch9xEH5PlZmoGxwPeNwpEk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.wasm",
        "name": "System.Runtime.znw1hd3rgf.wasm",
        "hash": "sha256-n1BphJZchppkJ3TjptufUiDMyobNSZXAYTUO8JWzjeo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Claims.wasm",
        "name": "System.Security.Claims.xdiganl8nm.wasm",
        "hash": "sha256-6+gxydF54Mzyu4fNFyJoD0TtZA5k25N4aLTJXw2lFzg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.wasm",
        "name": "System.Security.Cryptography.6dti62qiq3.wasm",
        "hash": "sha256-1myVFaCY0XGWd4sjluUcb2PU1CirtG373ANLekerC8Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.Extensions.wasm",
        "name": "System.Text.Encoding.Extensions.ossad590l5.wasm",
        "hash": "sha256-uF51o2akkBfYdRZfqe4FJkun9DPGti9FT4qulezA4vU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.rlaiw7pjsx.wasm",
        "hash": "sha256-L/Kydhpdf8pNN1kertWZnYnIsZx8cP2i/vszfww7G+8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.u294q10u1g.wasm",
        "hash": "sha256-Cnt7AiQ2qZOigFMacFOKJ9TC1lJ9u5NjdvqfWjPIf08=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.p00rpldh0s.wasm",
        "hash": "sha256-2XWpObYJH0VEzABPvJztYqPu6Dr8P8D/+V0bbYMlOX0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Channels.wasm",
        "name": "System.Threading.Channels.4piynrgzv1.wasm",
        "hash": "sha256-2OlDHD/LgvCw0PpFTcz+56frQXTWvX7FxUR6f57OmgQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Extensions.wasm",
        "name": "System.Threading.Tasks.Extensions.srbd7vybue.wasm",
        "hash": "sha256-KGDGqI7XxUuxTwuZr/zWgU/bdNsjsACXgIMgecRLfBY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Parallel.wasm",
        "name": "System.Threading.Tasks.Parallel.qmm979w47b.wasm",
        "hash": "sha256-0gXoP7/IgmFWsJVC+J4n4+kW52mmsPKzyfUa3fe7JYg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.wasm",
        "name": "System.Threading.Tasks.cd8sm4fveq.wasm",
        "hash": "sha256-oNBbGZZE++qAyTDPQVTtO32gsRTc9rtMyQX9vwE6RaQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Timer.wasm",
        "name": "System.Threading.Timer.q6bldwufkw.wasm",
        "hash": "sha256-4nhuBtnruDJd83yVbxgWuoUNx4Na2cT8vOHpStiLwzc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.wasm",
        "name": "System.Threading.llr0u7xb3v.wasm",
        "hash": "sha256-R0oHmr/HzL/p7PINoTKRCA5llX4vugESeGVNJ2rG49Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.Linq.wasm",
        "name": "System.Xml.Linq.h1uux8jz0h.wasm",
        "hash": "sha256-lcR9aQbNWsZio37Z/qtSMPYIx7GKzkXTik1dInQJosE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.ReaderWriter.wasm",
        "name": "System.Xml.ReaderWriter.y2stk1arlz.wasm",
        "hash": "sha256-asvo8BHqavscuNmVWftkar19Qj2zwyfkmY4ieturpSk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XDocument.wasm",
        "name": "System.Xml.XDocument.bof5ubs4hw.wasm",
        "hash": "sha256-m4hpjEmyk3Ir5jHktCzPCurc+O4xxRBESnordFAX2Zs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.wasm",
        "name": "System.a3z5tpzyvn.wasm",
        "hash": "sha256-Gs5bAip0Y07Y7rdk4CYnpzrBeGplAudFAELUELfBy80=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "netstandard.wasm",
        "name": "netstandard.irnku2cf75.wasm",
        "hash": "sha256-sqRFmlDNXpHrrXn4UuiQPrCVHhnzYZRNw1ZOBmFrXqE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CommonComponents.wasm",
        "name": "CommonComponents.hgu1e9q0rx.wasm",
        "hash": "sha256-j32hVfes1RT8sfVkkJ0qhSgPuukDZtHp5gkyHTWkyO4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharedComponents.wasm",
        "name": "SharedComponents.7l4w78n30p.wasm",
        "hash": "sha256-xWkX4wKOyy+5eFmgsZ0WGxooJSoT8sGN4BVo+0SHcX0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharedModels.wasm",
        "name": "SharedModels.mgrpfbbl44.wasm",
        "hash": "sha256-RsI8UDltJ+pGNUgJh8uDWSwKhOZ1ZmmT1CyOFgOsdp0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Web.wasm",
        "name": "Web.phcjhsk5dh.wasm",
        "hash": "sha256-/eaQNvaEthNkyWR9kGFOCgd5xnxhTkRVZ7QNT2Bw3ck=",
        "cache": "force-cache"
      }
    ],
    "lazyAssembly": [
      {
        "virtualPath": "AIDemoComponents.wasm",
        "name": "AIDemoComponents.wqhxz27imv.wasm",
        "hash": "sha256-6OylclsRoc6jnhrWf1+A/Dna5y1ETOQwOnB/Kw2igzM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "BaseComponents.wasm",
        "name": "BaseComponents.3463pluwgj.wasm",
        "hash": "sha256-s066ONGSh+hC3MxloQDbppfplNHKfX+t3gQ9P9BwOZg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "BlazorDemoComponents.wasm",
        "name": "BlazorDemoComponents.nev1ihh1xv.wasm",
        "hash": "sha256-wSRBxMlIDvGaLgxqrmYwY480CN/uKMXuUGJ3BE7XWic=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CachingDemoComponents.wasm",
        "name": "CachingDemoComponents.61cksyvjsv.wasm",
        "hash": "sha256-6o3vLtJlMO4bjd+MWj+59Ogyai/70dEWy13s22/fkEc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DatabaseDemoComponents.wasm",
        "name": "DatabaseDemoComponents.3r63eyupmf.wasm",
        "hash": "sha256-7/q8/3oa1QNXYkd5sSVYkgnBHjefQYlt9Ne0DOdXre8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DDDDemoComponents.wasm",
        "name": "DDDDemoComponents.tho9mav8v9.wasm",
        "hash": "sha256-v+1CEVR4LhuKK814Uucg/At0z6zRmHtCm5rdnasMDAs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DependencyInjectionDemoComponents.wasm",
        "name": "DependencyInjectionDemoComponents.8l9ag93xa0.wasm",
        "hash": "sha256-9GxRCC+RoC2RbqaQ4rJVKy/TXn+/BrSm7McKIZnUvmg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DesignPatternDemoComponents.wasm",
        "name": "DesignPatternDemoComponents.dcin6ehx0s.wasm",
        "hash": "sha256-ncp166TPcPh3jnv11pCh8QAckjRpWvwWgQjKl/znnks=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "EFCoreDemoComponents.wasm",
        "name": "EFCoreDemoComponents.cb1tyx443y.wasm",
        "hash": "sha256-GI5kzfQC5EfahNa1+qJUUIzQ3v1VjRuctFa0M2gF2go=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "HTTPClientDemoComponents.wasm",
        "name": "HTTPClientDemoComponents.4gj4svnlvy.wasm",
        "hash": "sha256-dGP3wnk7M8oKH82NIvNqzp4mIFrLdlR/ZZoOWj5Xc5Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "JSONDemoComponents.wasm",
        "name": "JSONDemoComponents.e8n7l9e2q6.wasm",
        "hash": "sha256-XsogVjCQJkpP9bvaUszTFOECyEP+mTX9Qrh4ERnctBg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "LINQDemoComponents.wasm",
        "name": "LINQDemoComponents.3exbuf7xdt.wasm",
        "hash": "sha256-NVhWyD8JvQe/QtQni1evVvrJZYovLQSRBZ+75lKsmmM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "LoggingDemoComponents.wasm",
        "name": "LoggingDemoComponents.xuhuibg6rt.wasm",
        "hash": "sha256-6ZMdp5ShKT92X24/vRRkBBB7GhicU62xRdBAKC51F3U=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MAUIDemoComponents.wasm",
        "name": "MAUIDemoComponents.rw2tjuj2zh.wasm",
        "hash": "sha256-+468Ng4Y4RpmsARpTp18QZDidukmsk8bN0INuZOgeA0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MCPDemoComponents.wasm",
        "name": "MCPDemoComponents.lo3yt3tqs8.wasm",
        "hash": "sha256-2z+9M52t1rWh28+mYXA/Viwnco9dbIDuty3KW2VFafs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MemoryDemoComponents.wasm",
        "name": "MemoryDemoComponents.sqcp8ujrn7.wasm",
        "hash": "sha256-xDyA6F2ts2gefTe7V0YkwnWxSjvAr6/u1O2ZZ2hoxfE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MiddlewareDemoComponents.wasm",
        "name": "MiddlewareDemoComponents.io5fweulud.wasm",
        "hash": "sha256-ggLV0EYF6gk7FgS97C+0Usk0J12lUGKPOua+a3illEU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MLNETDemoComponents.wasm",
        "name": "MLNETDemoComponents.td7unok2kj.wasm",
        "hash": "sha256-H+iDZQ0APo6FuFdDHNRbQ6i9leDgC/g2u3Bd1HvnwIM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MSBuildDemoComponents.wasm",
        "name": "MSBuildDemoComponents.9g4vxlyr5o.wasm",
        "hash": "sha256-t8KWRW6VhzSuXEOi8gWtNYaDjgpxXuGshBs5BElQJu0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "NugetDemoComponents.wasm",
        "name": "NugetDemoComponents.tdjinedan9.wasm",
        "hash": "sha256-ofFcbfjZCKtyalMFms3HUG3PUQauPrrkApF6sxPvfK0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "OOPSDemoComponents.wasm",
        "name": "OOPSDemoComponents.qk9mot5f3b.wasm",
        "hash": "sha256-+pVbrNrmX7ZkSL+eyxWcSc5R3KnAXB737DTSLACupHU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "OWASPDemoComponents.wasm",
        "name": "OWASPDemoComponents.iavaknvwbx.wasm",
        "hash": "sha256-FgMJe8+wxrSIA5T5rqeNq4jkqQ7WL9Knj1/A1fSFueo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PythonDemoComponents.wasm",
        "name": "PythonDemoComponents.hymjftk559.wasm",
        "hash": "sha256-+X2wa5jqVF8TgsfMMYMzQcC8LCiDWfcJIMOgHDpVcgU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "RegexDemoComponents.wasm",
        "name": "RegexDemoComponents.lpl25ud3v9.wasm",
        "hash": "sha256-aceyP8ct/PIBJ0VGKBtry60MpS+npbH2eloX/mBjj+Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ReportDemoComponents.wasm",
        "name": "ReportDemoComponents.b94gsjkm0g.wasm",
        "hash": "sha256-jwE6i3hNmZFX1X3F1GZzzTN6pkG8+wKgMJKSGt6VBDM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SecurityDemoComponents.wasm",
        "name": "SecurityDemoComponents.k82u9d07a8.wasm",
        "hash": "sha256-LNy7QTlU3aiWqLx7ciO7SONZes2rAMHSEkVvWluMCJ8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SignalRDemoComponents.wasm",
        "name": "SignalRDemoComponents.8c6pdz14gr.wasm",
        "hash": "sha256-WJWn/O+M1kFiGlclj6bAiee81Wr0XTV1uB4bBtsTeq0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SOLIDDemoComponents.wasm",
        "name": "SOLIDDemoComponents.1myms0kyqr.wasm",
        "hash": "sha256-ubQl5ElBSVBJDDdnOKS16P+LY9VkyfOMLB5Rwu7utho=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TalkDemoComponents.wasm",
        "name": "TalkDemoComponents.z0lsldg39x.wasm",
        "hash": "sha256-c6IBy0baHvctbWK+cEwDJMCeRxk/dCwyokbkUtOcGLQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TDDDemoComponents.wasm",
        "name": "TDDDemoComponents.laj34r867b.wasm",
        "hash": "sha256-bVRJdN15uOQEMcfedc/U+vDfbK++n7KZOwmHq5YH7kw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TestingDemoComponents.wasm",
        "name": "TestingDemoComponents.14iphjwox0.wasm",
        "hash": "sha256-D/tmoUVX9TiLqHXoCQAYvlr4r3ppdnHZr+FJ8oTxp2Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "WebAPIDemoComponents.wasm",
        "name": "WebAPIDemoComponents.bf6fbqcefb.wasm",
        "hash": "sha256-R6GAU3vbtX1mICrLmZnzGQSRGTWoYeoTSojOnNkrZSE=",
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
