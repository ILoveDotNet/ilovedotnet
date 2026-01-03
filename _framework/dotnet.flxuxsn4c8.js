//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

var e=!1;const t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,4,1,96,0,0,3,2,1,0,10,8,1,6,0,6,64,25,11,11])),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),n=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),r=Symbol.for("wasm promise_control");function i(e,t){let o=null;const n=new Promise((function(n,r){o={isDone:!1,promise:null,resolve:t=>{o.isDone||(o.isDone=!0,n(t),e&&e())},reject:e=>{o.isDone||(o.isDone=!0,r(e),t&&t())}}}));o.promise=n;const i=n;return i[r]=o,{promise:i,promise_control:o}}function s(e){return e[r]}function a(e){e&&function(e){return void 0!==e[r]}(e)||Be(!1,"Promise is not controllable")}const l="__mono_message__",c=["debug","log","trace","warn","info","error"],d="MONO_WASM: ";let u,f,m,g,p,h;function w(e){g=e}function b(e){if(Pe.diagnosticTracing){const t="function"==typeof e?e():e;console.debug(d+t)}}function y(e,...t){console.info(d+e,...t)}function v(e,...t){console.info(e,...t)}function E(e,...t){console.warn(d+e,...t)}function _(e,...t){if(t&&t.length>0&&t[0]&&"object"==typeof t[0]){if(t[0].silent)return;if(t[0].toString)return void console.error(d+e,t[0].toString())}console.error(d+e,...t)}function x(e,t,o){return function(...n){try{let r=n[0];if(void 0===r)r="undefined";else if(null===r)r="null";else if("function"==typeof r)r=r.toString();else if("string"!=typeof r)try{r=JSON.stringify(r)}catch(e){r=r.toString()}t(o?JSON.stringify({method:e,payload:r,arguments:n.slice(1)}):[e+r,...n.slice(1)])}catch(e){m.error(`proxyConsole failed: ${e}`)}}}function j(e,t,o){f=t,g=e,m={...t};const n=`${o}/console`.replace("https://","wss://").replace("http://","ws://");u=new WebSocket(n),u.addEventListener("error",A),u.addEventListener("close",S),function(){for(const e of c)f[e]=x(`console.${e}`,T,!0)}()}function R(e){let t=30;const o=()=>{u?0==u.bufferedAmount||0==t?(e&&v(e),function(){for(const e of c)f[e]=x(`console.${e}`,m.log,!1)}(),u.removeEventListener("error",A),u.removeEventListener("close",S),u.close(1e3,e),u=void 0):(t--,globalThis.setTimeout(o,100)):e&&m&&m.log(e)};o()}function T(e){u&&u.readyState===WebSocket.OPEN?u.send(e):m.log(e)}function A(e){m.error(`[${g}] proxy console websocket error: ${e}`,e)}function S(e){m.debug(`[${g}] proxy console websocket closed: ${e}`,e)}function D(){Pe.preferredIcuAsset=O(Pe.config);let e="invariant"==Pe.config.globalizationMode;if(!e)if(Pe.preferredIcuAsset)Pe.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===Pe.config.globalizationMode||"all"===Pe.config.globalizationMode||"sharded"===Pe.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}Pe.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,Pe.preferredIcuAsset=null}const t="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",o=Pe.config.environmentVariables;if(void 0===o[t]&&e&&(o[t]="1"),void 0===o.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(o.TZ=e)}catch(e){y("failed to detect timezone, will fallback to UTC")}}function O(e){var t;if((null===(t=e.resources)||void 0===t?void 0:t.icu)&&"invariant"!=e.globalizationMode){const t=e.applicationCulture||(ke?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale),o=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(o.length>=1)return o[0].name}else t&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const t=e.split("-")[0];return"en"===t||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(t)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(t)):n="icudt.dat";if(n)for(let e=0;e<o.length;e++){const t=o[e];if(t.virtualPath===n)return t.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const C=class{constructor(e){this.url=e}toString(){return this.url}};async function k(e,t){try{const o="function"==typeof globalThis.fetch;if(Se){const n=e.startsWith("file://");if(!n&&o)return globalThis.fetch(e,t||{credentials:"same-origin"});p||(h=Ne.require("url"),p=Ne.require("fs")),n&&(e=h.fileURLToPath(e));const r=await p.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>r,json:()=>JSON.parse(r),text:()=>{throw new Error("NotImplementedException")}}}if(o)return globalThis.fetch(e,t||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(t){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t},text:()=>{throw t}}}throw new Error("No fetch implementation available")}function I(e){return"string"!=typeof e&&Be(!1,"url must be a string"),!M(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}const U=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,P=/[a-zA-Z]:[\\/]/;function M(e){return Se||Ie?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||P.test(e):U.test(e)}let L,N=0;const $=[],z=[],W=new Map,F={"js-module-threads":!0,"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},B={...F,"js-module-library-initializer":!0},V={...F,dotnetwasm:!0,heap:!0,manifest:!0},q={...B,manifest:!0},H={...B,dotnetwasm:!0},J={dotnetwasm:!0,symbols:!0},Z={...B,dotnetwasm:!0,symbols:!0},Q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=Pe.preferredIcuAsset)}function K(e,t,o){null!=t||(t=[]),Be(1==t.length,`Expect to have one ${o} asset in resources`);const n=t[0];return n.behavior=o,X(n),e.push(n),n}function X(e){V[e.behavior]&&W.set(e.behavior,e)}function Y(e){Be(V[e],`Unknown single asset behavior ${e}`);const t=W.get(e);if(t&&!t.resolvedUrl)if(t.resolvedUrl=Pe.locateFile(t.name),F[t.behavior]){const e=ge(t);e?("string"!=typeof e&&Be(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),t.resolvedUrl=e):t.resolvedUrl=ce(t.resolvedUrl,t.behavior)}else if("dotnetwasm"!==t.behavior)throw new Error(`Unknown single asset behavior ${e}`);return t}function ee(e){const t=Y(e);return Be(t,`Single asset for ${e} not found`),t}let te=!1;async function oe(){if(!te){te=!0,Pe.diagnosticTracing&&b("mono_download_assets");try{const e=[],t=[],o=(e,t)=>{!Z[e.behavior]&&G(e)&&Pe.expected_instantiated_assets_count++,!H[e.behavior]&&G(e)&&(Pe.expected_downloaded_assets_count++,t.push(se(e)))};for(const t of $)o(t,e);for(const e of z)o(e,t);Pe.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...t]).then((()=>{Pe.allDownloadsFinished.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),await Pe.runtimeModuleLoaded.promise;const n=async e=>{const t=await e;if(t.buffer){if(!Z[t.behavior]){t.buffer&&"object"==typeof t.buffer||Be(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof t.resolvedUrl&&Be(!1,"resolvedUrl must be string");const e=t.resolvedUrl,o=await t.buffer,n=new Uint8Array(o);pe(t),await Ue.beforeOnRuntimeInitialized.promise,Ue.instantiate_asset(t,e,n)}}else J[t.behavior]?("symbols"===t.behavior&&(await Ue.instantiate_symbols_asset(t),pe(t)),J[t.behavior]&&++Pe.actual_downloaded_assets_count):(t.isOptional||Be(!1,"Expected asset to have the downloaded buffer"),!H[t.behavior]&&G(t)&&Pe.expected_downloaded_assets_count--,!Z[t.behavior]&&G(t)&&Pe.expected_instantiated_assets_count--)},r=[],i=[];for(const t of e)r.push(n(t));for(const e of t)i.push(n(e));Promise.all(r).then((()=>{Ce||Ue.coreAssetsInMemory.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),Promise.all(i).then((async()=>{Ce||(await Ue.coreAssetsInMemory.promise,Ue.allAssetsInMemory.promise_control.resolve())})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e}))}catch(e){throw Pe.err("Error in mono_download_assets: "+e),e}}}let ne=!1;function re(){if(ne)return;ne=!0;const e=Pe.config,t=[];if(e.assets)for(const t of e.assets)"object"!=typeof t&&Be(!1,`asset must be object, it was ${typeof t} : ${t}`),"string"!=typeof t.behavior&&Be(!1,"asset behavior must be known string"),"string"!=typeof t.name&&Be(!1,"asset name must be string"),t.resolvedUrl&&"string"!=typeof t.resolvedUrl&&Be(!1,"asset resolvedUrl could be string"),t.hash&&"string"!=typeof t.hash&&Be(!1,"asset resolvedUrl could be string"),t.pendingDownload&&"object"!=typeof t.pendingDownload&&Be(!1,"asset pendingDownload could be object"),t.isCore?$.push(t):z.push(t),X(t);else if(e.resources){const o=e.resources;o.wasmNative||Be(!1,"resources.wasmNative must be defined"),o.jsModuleNative||Be(!1,"resources.jsModuleNative must be defined"),o.jsModuleRuntime||Be(!1,"resources.jsModuleRuntime must be defined"),K(z,o.wasmNative,"dotnetwasm"),K(t,o.jsModuleNative,"js-module-native"),K(t,o.jsModuleRuntime,"js-module-runtime"),o.jsModuleDiagnostics&&K(t,o.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,t,o)=>{const n=e;n.behavior=t,o?(n.isCore=!0,$.push(n)):z.push(n)};if(o.coreAssembly)for(let e=0;e<o.coreAssembly.length;e++)n(o.coreAssembly[e],"assembly",!0);if(o.assembly)for(let e=0;e<o.assembly.length;e++)n(o.assembly[e],"assembly",!o.coreAssembly);if(0!=e.debugLevel&&Pe.isDebuggingSupported()){if(o.corePdb)for(let e=0;e<o.corePdb.length;e++)n(o.corePdb[e],"pdb",!0);if(o.pdb)for(let e=0;e<o.pdb.length;e++)n(o.pdb[e],"pdb",!o.corePdb)}if(e.loadAllSatelliteResources&&o.satelliteResources)for(const e in o.satelliteResources)for(let t=0;t<o.satelliteResources[e].length;t++){const r=o.satelliteResources[e][t];r.culture=e,n(r,"resource",!o.coreAssembly)}if(o.coreVfs)for(let e=0;e<o.coreVfs.length;e++)n(o.coreVfs[e],"vfs",!0);if(o.vfs)for(let e=0;e<o.vfs.length;e++)n(o.vfs[e],"vfs",!o.coreVfs);const r=O(e);if(r&&o.icu)for(let e=0;e<o.icu.length;e++){const t=o.icu[e];t.name===r&&n(t,"icu",!1)}if(o.wasmSymbols)for(let e=0;e<o.wasmSymbols.length;e++)n(o.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let t=0;t<e.appsettings.length;t++){const o=e.appsettings[t],n=he(o);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||z.push({name:o,behavior:"vfs",noCache:!0,useCredentials:!0})}e.assets=[...$,...z,...t]}async function ie(e){const t=await se(e);return await t.pendingDownloadInternal.response,t.buffer}async function se(e){try{return await ae(e)}catch(t){if(!Pe.enableDownloadRetry)throw t;if(Ie||Se)throw t;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw t;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw t;if(t&&404==t.status)throw t;e.pendingDownloadInternal=void 0,await Pe.allDownloadsQueued.promise;try{return Pe.diagnosticTracing&&b(`Retrying download '${e.name}'`),await ae(e)}catch(t){return e.pendingDownloadInternal=void 0,await new Promise((e=>globalThis.setTimeout(e,100))),Pe.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await ae(e)}}}async function ae(e){for(;L;)await L.promise;try{++N,N==Pe.maxParallelDownloads&&(Pe.diagnosticTracing&&b("Throttling further parallel downloads"),L=i());const t=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const t=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>t,json:()=>JSON.parse(new TextDecoder("utf-8").decode(t)),text:()=>{throw new Error("NotImplementedException")},headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const t=e.loadRemote&&Pe.config.remoteSources?Pe.config.remoteSources:[""];let o;for(let n of t){n=n.trim(),"./"===n&&(n="");const t=le(e,n);e.name===t?Pe.diagnosticTracing&&b(`Attempting to download '${t}'`):Pe.diagnosticTracing&&b(`Attempting to download '${t}' for ${e.name}`);try{e.resolvedUrl=t;const n=fe(e);if(e.pendingDownloadInternal=n,o=await n.response,!o||!o.ok)continue;return o}catch(e){o||(o={ok:!1,url:t,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&Pe.config.ignorePdbLoadErrors;if(o||Be(!1,`Response undefined ${e.name}`),!n){const t=new Error(`download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`);throw t.status=o.status,t}y(`optional download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`)}(e);return t?(J[e.behavior]||(e.buffer=await t.arrayBuffer(),++Pe.actual_downloaded_assets_count),e):e}finally{if(--N,L&&N==Pe.maxParallelDownloads-1){Pe.diagnosticTracing&&b("Resuming more parallel downloads");const e=L;L=void 0,e.promise_control.resolve()}}}function le(e,t){let o;return null==t&&Be(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?o=e.resolvedUrl:(o=""===t?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:t+e.name,o=ce(Pe.locateFile(o),e.behavior)),o&&"string"==typeof o||Be(!1,"attemptUrl need to be path or url string"),o}function ce(e,t){return Pe.modulesUniqueQuery&&q[t]&&(e+=Pe.modulesUniqueQuery),e}let de=0;const ue=new Set;function fe(e){try{e.resolvedUrl||Be(!1,"Request's resolvedUrl must be set");const t=function(e){let t=e.resolvedUrl;if(Pe.loadBootResource){const o=ge(e);if(o instanceof Promise)return o;"string"==typeof o&&(t=o)}const o={};return Pe.config.disableNoCacheFetch||(o.cache="no-cache"),e.useCredentials?o.credentials="include":!Pe.config.disableIntegrityCheck&&e.hash&&(o.integrity=e.hash),Pe.fetch_like(t,o)}(e),o={name:e.name,url:e.resolvedUrl,response:t};return ue.add(e.name),o.response.then((()=>{"assembly"==e.behavior&&Pe.loadedAssemblies.push(e.name),de++,Pe.onDownloadResourceProgress&&Pe.onDownloadResourceProgress(de,ue.size)})),o}catch(t){const o={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(o)}}}const me={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs","js-module-threads":"dotnetjs"};function ge(e){var t;if(Pe.loadBootResource){const o=null!==(t=e.hash)&&void 0!==t?t:"",n=e.resolvedUrl,r=me[e.behavior];if(r){const t=Pe.loadBootResource(r,e.name,n,o,e.behavior);return"string"==typeof t?I(t):t}}}function pe(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function he(e){let t=e.lastIndexOf("/");return t>=0&&t++,e.substring(t)}async function we(e){e&&await Promise.all((null!=e?e:[]).map((e=>async function(e){try{const t=e.name;if(!e.moduleExports){const o=ce(Pe.locateFile(t),"js-module-library-initializer");Pe.diagnosticTracing&&b(`Attempting to import '${o}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */o)}Pe.libraryInitializers.push({scriptName:t,exports:e.moduleExports})}catch(t){E(`Failed to import library initializer '${e}': ${t}`)}}(e))))}async function be(e,t){if(!Pe.libraryInitializers)return;const o=[];for(let n=0;n<Pe.libraryInitializers.length;n++){const r=Pe.libraryInitializers[n];r.exports[e]&&o.push(ye(r.scriptName,e,(()=>r.exports[e](...t))))}await Promise.all(o)}async function ye(e,t,o){try{await o()}catch(o){throw E(`Failed to invoke '${t}' on library initializer '${e}': ${o}`),Xe(1,o),o}}function ve(e,t){if(e===t)return e;const o={...t};return void 0!==o.assets&&o.assets!==e.assets&&(o.assets=[...e.assets||[],...o.assets||[]]),void 0!==o.resources&&(o.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},o.resources)),void 0!==o.environmentVariables&&(o.environmentVariables={...e.environmentVariables||{},...o.environmentVariables||{}}),void 0!==o.runtimeOptions&&o.runtimeOptions!==e.runtimeOptions&&(o.runtimeOptions=[...e.runtimeOptions||[],...o.runtimeOptions||[]]),Object.assign(e,o)}function Ee(e,t){if(e===t)return e;const o={...t};return o.config&&(e.config||(e.config={}),o.config=ve(e.config,o.config)),Object.assign(e,o)}function _e(e,t){if(e===t)return e;const o={...t};return void 0!==o.coreAssembly&&(o.coreAssembly=[...e.coreAssembly||[],...o.coreAssembly||[]]),void 0!==o.assembly&&(o.assembly=[...e.assembly||[],...o.assembly||[]]),void 0!==o.lazyAssembly&&(o.lazyAssembly=[...e.lazyAssembly||[],...o.lazyAssembly||[]]),void 0!==o.corePdb&&(o.corePdb=[...e.corePdb||[],...o.corePdb||[]]),void 0!==o.pdb&&(o.pdb=[...e.pdb||[],...o.pdb||[]]),void 0!==o.jsModuleWorker&&(o.jsModuleWorker=[...e.jsModuleWorker||[],...o.jsModuleWorker||[]]),void 0!==o.jsModuleNative&&(o.jsModuleNative=[...e.jsModuleNative||[],...o.jsModuleNative||[]]),void 0!==o.jsModuleDiagnostics&&(o.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...o.jsModuleDiagnostics||[]]),void 0!==o.jsModuleRuntime&&(o.jsModuleRuntime=[...e.jsModuleRuntime||[],...o.jsModuleRuntime||[]]),void 0!==o.wasmSymbols&&(o.wasmSymbols=[...e.wasmSymbols||[],...o.wasmSymbols||[]]),void 0!==o.wasmNative&&(o.wasmNative=[...e.wasmNative||[],...o.wasmNative||[]]),void 0!==o.icu&&(o.icu=[...e.icu||[],...o.icu||[]]),void 0!==o.satelliteResources&&(o.satelliteResources=function(e,t){if(e===t)return e;for(const o in t)e[o]=[...e[o]||[],...t[o]||[]];return e}(e.satelliteResources||{},o.satelliteResources||{})),void 0!==o.modulesAfterConfigLoaded&&(o.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...o.modulesAfterConfigLoaded||[]]),void 0!==o.modulesAfterRuntimeReady&&(o.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...o.modulesAfterRuntimeReady||[]]),void 0!==o.extensions&&(o.extensions={...e.extensions||{},...o.extensions||{}}),void 0!==o.vfs&&(o.vfs=[...e.vfs||[],...o.vfs||[]]),Object.assign(e,o)}function xe(){const e=Pe.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleWorker:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){Pe.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const t of e.assets){const o={};switch(t.behavior){case"assembly":o.assembly=[t];break;case"pdb":o.pdb=[t];break;case"resource":o.satelliteResources={},o.satelliteResources[t.culture]=[t];break;case"icu":o.icu=[t];break;case"symbols":o.wasmSymbols=[t];break;case"vfs":o.vfs=[t];break;case"dotnetwasm":o.wasmNative=[t];break;case"js-module-threads":o.jsModuleWorker=[t];break;case"js-module-runtime":o.jsModuleRuntime=[t];break;case"js-module-native":o.jsModuleNative=[t];break;case"js-module-diagnostics":o.jsModuleDiagnostics=[t];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${t.behavior} of asset ${t.name}`)}_e(e.resources,o)}}e.debugLevel,e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Ue.diagnosticTracing=Pe.diagnosticTracing=!!e.diagnosticTracing,Ue.waitForDebugger=e.waitForDebugger,Pe.maxParallelDownloads=e.maxParallelDownloads||Pe.maxParallelDownloads,Pe.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:Pe.enableDownloadRetry}let je=!1;async function Re(e){var t;if(je)return void await Pe.afterConfigLoaded.promise;let o;try{if(e.configSrc||Pe.config&&0!==Object.keys(Pe.config).length&&(Pe.config.assets||Pe.config.resources)||(e.configSrc="dotnet.boot.js"),o=e.configSrc,je=!0,o&&(Pe.diagnosticTracing&&b("mono_wasm_load_config"),await async function(e){const t=e.configSrc,o=Pe.locateFile(t);let n=null;void 0!==Pe.loadBootResource&&(n=Pe.loadBootResource("manifest",t,o,"","manifest"));let r,i=null;if(n)if("string"==typeof n)n.includes(".json")?(i=await s(I(n)),r=await Ae(i)):r=(await import(I(n))).config;else{const e=await n;"function"==typeof e.json?(i=e,r=await Ae(i)):r=e.config}else o.includes(".json")?(i=await s(ce(o,"manifest")),r=await Ae(i)):r=(await import(ce(o,"manifest"))).config;function s(e){return Pe.fetch_like(e,{method:"GET",credentials:"include",cache:"no-cache"})}Pe.config.applicationEnvironment&&(r.applicationEnvironment=Pe.config.applicationEnvironment),ve(Pe.config,r)}(e)),xe(),await we(null===(t=Pe.config.resources)||void 0===t?void 0:t.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[Pe.config]),e.onConfigLoaded)try{await e.onConfigLoaded(Pe.config,Le),xe()}catch(e){throw _("onConfigLoaded() failed",e),e}xe(),Pe.afterConfigLoaded.promise_control.resolve(Pe.config)}catch(t){const n=`Failed to load config file ${o} ${t} ${null==t?void 0:t.stack}`;throw Pe.config=e.config=Object.assign(Pe.config,{message:n,error:t,isError:!0}),Xe(1,new Error(n)),t}}function Te(){return!!globalThis.navigator&&(Pe.isChromium||Pe.isFirefox)}async function Ae(e){const t=Pe.config,o=await e.json();t.applicationEnvironment||o.applicationEnvironment||(o.applicationEnvironment=e.headers.get("Blazor-Environment")||e.headers.get("DotNet-Environment")||void 0),o.environmentVariables||(o.environmentVariables={});const n=e.headers.get("DOTNET-MODIFIABLE-ASSEMBLIES");n&&(o.environmentVariables.DOTNET_MODIFIABLE_ASSEMBLIES=n);const r=e.headers.get("ASPNETCORE-BROWSER-TOOLS");return r&&(o.environmentVariables.__ASPNETCORE_BROWSER_TOOLS=r),o}"function"!=typeof importScripts||globalThis.onmessage||(globalThis.dotnetSidecar=!0);const Se="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,De="function"==typeof importScripts,Oe=De&&"undefined"!=typeof dotnetSidecar,Ce=De&&!Oe,ke="object"==typeof window||De&&!Se,Ie=!ke&&!Se;let Ue={},Pe={},Me={},Le={},Ne={},$e=!1;const ze={},We={config:ze},Fe={mono:{},binding:{},internal:Ne,module:We,loaderHelpers:Pe,runtimeHelpers:Ue,diagnosticHelpers:Me,api:Le};function Be(e,t){if(e)return;const o="Assert failed: "+("function"==typeof t?t():t),n=new Error(o);_(o,n),Ue.nativeAbort(n)}function Ve(){return void 0!==Pe.exitCode}function qe(){return Ue.runtimeReady&&!Ve()}function He(){Ve()&&Be(!1,`.NET runtime already exited with ${Pe.exitCode} ${Pe.exitReason}. You can use runtime.runMain() which doesn't exit the runtime.`),Ue.runtimeReady||Be(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function Je(){ke&&(globalThis.addEventListener("unhandledrejection",et),globalThis.addEventListener("error",tt))}let Ze,Qe;function Ge(e){Qe&&Qe(e),Xe(e,Pe.exitReason)}function Ke(e){Ze&&Ze(e||Pe.exitReason),Xe(1,e||Pe.exitReason)}function Xe(t,o){var n,r;const i=o&&"object"==typeof o;t=i&&"number"==typeof o.status?o.status:void 0===t?-1:t;const s=i&&"string"==typeof o.message?o.message:""+o;(o=i?o:Ue.ExitStatus?function(e,t){const o=new Ue.ExitStatus(e);return o.message=t,o.toString=()=>t,o}(t,s):new Error("Exit with code "+t+" "+s)).status=t,o.message||(o.message=s);const a=""+(o.stack||(new Error).stack);try{Object.defineProperty(o,"stack",{get:()=>a})}catch(e){}const l=!!o.silent;if(o.silent=!0,Ve())Pe.diagnosticTracing&&b("mono_exit called after exit");else{try{We.onAbort==Ke&&(We.onAbort=Ze),We.onExit==Ge&&(We.onExit=Qe),ke&&(globalThis.removeEventListener("unhandledrejection",et),globalThis.removeEventListener("error",tt)),Ue.runtimeReady?(Ue.jiterpreter_dump_stats&&Ue.jiterpreter_dump_stats(!1),0===t&&(null===(n=Pe.config)||void 0===n?void 0:n.interopCleanupOnExit)&&Ue.forceDisposeProxies(!0,!0),e&&0!==t&&(null===(r=Pe.config)||void 0===r||r.dumpThreadsOnNonZeroExit)):(Pe.diagnosticTracing&&b(`abort_startup, reason: ${o}`),function(e){Pe.allDownloadsQueued.promise_control.reject(e),Pe.allDownloadsFinished.promise_control.reject(e),Pe.afterConfigLoaded.promise_control.reject(e),Pe.wasmCompilePromise.promise_control.reject(e),Pe.runtimeModuleLoaded.promise_control.reject(e),Ue.dotnetReady&&(Ue.dotnetReady.promise_control.reject(e),Ue.afterInstantiateWasm.promise_control.reject(e),Ue.beforePreInit.promise_control.reject(e),Ue.afterPreInit.promise_control.reject(e),Ue.afterPreRun.promise_control.reject(e),Ue.beforeOnRuntimeInitialized.promise_control.reject(e),Ue.afterOnRuntimeInitialized.promise_control.reject(e),Ue.afterPostRun.promise_control.reject(e))}(o))}catch(e){E("mono_exit A failed",e)}try{l||(function(e,t){if(0!==e&&t){const e=Ue.ExitStatus&&t instanceof Ue.ExitStatus?b:_;"string"==typeof t?e(t):(void 0===t.stack&&(t.stack=(new Error).stack+""),t.message?e(Ue.stringify_as_error_with_stack?Ue.stringify_as_error_with_stack(t.message+"\n"+t.stack):t.message+"\n"+t.stack):e(JSON.stringify(t)))}!Ce&&Pe.config&&(Pe.config.logExitCode?Pe.config.forwardConsoleLogsToWS?R("WASM EXIT "+e):v("WASM EXIT "+e):Pe.config.forwardConsoleLogsToWS&&R())}(t,o),function(e){if(ke&&!Ce&&Pe.config&&Pe.config.appendElementOnExit&&document){const t=document.createElement("label");t.id="tests_done",0!==e&&(t.style.background="red"),t.innerHTML=""+e,document.body.appendChild(t)}}(t))}catch(e){E("mono_exit B failed",e)}Pe.exitCode=t,Pe.exitReason||(Pe.exitReason=o),!Ce&&Ue.runtimeReady&&We.runtimeKeepalivePop()}if(Pe.config&&Pe.config.asyncFlushOnExit&&0===t)throw(async()=>{try{await async function(){try{const e=await import(/*! webpackIgnore: true */"process"),t=e=>new Promise(((t,o)=>{e.on("error",o),e.end("","utf8",t)})),o=t(e.stderr),n=t(e.stdout);let r;const i=new Promise((e=>{r=setTimeout((()=>e("timeout")),1e3)}));await Promise.race([Promise.all([n,o]),i]),clearTimeout(r)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Ye(t,o)}})(),o;Ye(t,o)}function Ye(e,t){if(Ue.runtimeReady&&Ue.nativeExit)try{Ue.nativeExit(e)}catch(e){!Ue.ExitStatus||e instanceof Ue.ExitStatus||E("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!ke)throw Se&&Ne.process?Ne.process.exit(e):Ue.quit&&Ue.quit(e,t),t}function et(e){ot(e,e.reason,"rejection")}function tt(e){ot(e,e.error,"error")}function ot(e,t,o){e.preventDefault();try{t||(t=new Error("Unhandled "+o)),void 0===t.stack&&(t.stack=(new Error).stack),t.stack=t.stack+"",t.silent||(_("Unhandled error:",t),Xe(1,t))}catch(e){}}!function(e){if($e)throw new Error("Loader module already loaded");$e=!0,Ue=e.runtimeHelpers,Pe=e.loaderHelpers,Me=e.diagnosticHelpers,Le=e.api,Ne=e.internal,Object.assign(Le,{INTERNAL:Ne,invokeLibraryInitializers:be}),Object.assign(e.module,{config:ve(ze,{environmentVariables:{}})});const r={mono_wasm_bindings_is_ready:!1,config:e.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"fad253f51b461736dfd3cd9c15977bb7493becef",config:e.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:i(),allDownloadsQueued:i(),allDownloadsFinished:i(),wasmCompilePromise:i(),runtimeModuleLoaded:i(),loadingWorkers:i(),is_exited:Ve,is_runtime_running:qe,assert_runtime_running:He,mono_exit:Xe,createPromiseController:i,getPromiseController:s,assertIsControllablePromise:a,mono_download_assets:oe,resolve_single_asset_path:ee,setup_proxy_console:j,set_thread_prefix:w,installUnhandledErrorHandler:Je,retrieve_asset_download:ie,invokeLibraryInitializers:be,isDebuggingSupported:Te,exceptions:t,simd:n,relaxedSimd:o};Object.assign(Ue,r),Object.assign(Pe,l)}(Fe);let nt,rt,it,st=!1,at=!1;async function lt(e){if(!at){if(at=!0,ke&&Pe.config.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&j("main",globalThis.console,globalThis.location.origin),We||Be(!1,"Null moduleConfig"),Pe.config||Be(!1,"Null moduleConfig.config"),"function"==typeof e){const t=e(Fe.api);if(t.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(We,t),Ee(We,t)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");Ee(We,e)}await async function(e){if(Se){const e=await import(/*! webpackIgnore: true */"process"),t=14;if(e.versions.node.split(".")[0]<t)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${t}. See also https://aka.ms/dotnet-wasm-features`)}const t=/*! webpackIgnore: true */import.meta.url,o=t.indexOf("?");var n;if(o>0&&(Pe.modulesUniqueQuery=t.substring(o)),Pe.scriptUrl=t.replace(/\\/g,"/").replace(/[?#].*/,""),Pe.scriptDirectory=(n=Pe.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",Pe.locateFile=e=>"URL"in globalThis&&globalThis.URL!==C?new URL(e,Pe.scriptDirectory).toString():M(e)?e:Pe.scriptDirectory+e,Pe.fetch_like=k,Pe.out=console.log,Pe.err=console.error,Pe.onDownloadResourceProgress=e.onDownloadResourceProgress,ke&&globalThis.navigator){const e=globalThis.navigator,t=e.userAgentData&&e.userAgentData.brands;t&&t.length>0?Pe.isChromium=t.some((e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand)):e.userAgent&&(Pe.isChromium=e.userAgent.includes("Chrome"),Pe.isFirefox=e.userAgent.includes("Firefox"))}Ne.require=Se?await import(/*! webpackIgnore: true */"module").then((e=>e.createRequire(/*! webpackIgnore: true */import.meta.url))):Promise.resolve((()=>{throw new Error("require not supported")})),void 0===globalThis.URL&&(globalThis.URL=C)}(We)}}async function ct(e){return await lt(e),Ze=We.onAbort,Qe=We.onExit,We.onAbort=Ke,We.onExit=Ge,We.ENVIRONMENT_IS_PTHREAD?async function(){(function(){const e=new MessageChannel,t=e.port1,o=e.port2;t.addEventListener("message",(e=>{var n,r;n=JSON.parse(e.data.config),r=JSON.parse(e.data.monoThreadInfo),st?Pe.diagnosticTracing&&b("mono config already received"):(ve(Pe.config,n),Ue.monoThreadInfo=r,xe(),Pe.diagnosticTracing&&b("mono config received"),st=!0,Pe.afterConfigLoaded.promise_control.resolve(Pe.config),ke&&n.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&Pe.setup_proxy_console("worker-idle",console,globalThis.location.origin)),t.close(),o.close()}),{once:!0}),t.start(),self.postMessage({[l]:{monoCmd:"preload",port:o}},[o])})(),await Pe.afterConfigLoaded.promise,function(){const e=Pe.config;e.assets||Be(!1,"config.assets must be defined");for(const t of e.assets)X(t),Q[t.behavior]&&z.push(t)}(),setTimeout((async()=>{try{await oe()}catch(e){Xe(1,e)}}),0);const e=dt(),t=await Promise.all(e);return await ut(t),We}():async function(){var e;await Re(We),re();const t=dt();(async function(){try{const e=ee("dotnetwasm");await se(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||Be(!1,"Can't load dotnet.native.wasm");const t=await e.pendingDownloadInternal.response,o=t.headers&&t.headers.get?t.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===o)n=await WebAssembly.compileStreaming(t);else{ke&&"application/wasm"!==o&&E('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await t.arrayBuffer();Pe.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Ie?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,Pe.wasmCompilePromise.promise_control.resolve(n)}catch(e){Pe.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout((async()=>{try{D(),await oe()}catch(e){Xe(1,e)}}),0);const o=await Promise.all(t);return await ut(o),await Ue.dotnetReady.promise,await we(null===(e=Pe.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[Fe.api]),Le}()}function dt(){const e=ee("js-module-runtime"),t=ee("js-module-native");if(nt&&rt)return[nt,rt,it];"object"==typeof e.moduleExports?nt=e.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),nt=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof t.moduleExports?rt=t.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),rt=import(/*! webpackIgnore: true */t.resolvedUrl));const o=Y("js-module-diagnostics");return o&&("object"==typeof o.moduleExports?it=o.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),it=import(/*! webpackIgnore: true */o.resolvedUrl))),[nt,rt,it]}async function ut(e){const{initializeExports:t,initializeReplacements:o,configureRuntimeStartup:n,configureEmscriptenStartup:r,configureWorkerStartup:i,setRuntimeGlobals:s,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];s(Fe),t(Fe),c&&c.setRuntimeGlobals(Fe),await n(We),Pe.runtimeModuleLoaded.promise_control.resolve(),l((e=>(Object.assign(We,{ready:e.ready,__dotnet_runtime:{initializeReplacements:o,configureEmscriptenStartup:r,configureWorkerStartup:i,passEmscriptenInternals:a}}),We))).catch((e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize. See also https://aka.ms/dotnet-wasm-features");throw e}))}const ft=new class{withModuleConfig(e){try{return Ee(We,e),this}catch(e){throw Xe(1,e),e}}withOnConfigLoaded(e){try{return Ee(We,{onConfigLoaded:e}),this}catch(e){throw Xe(1,e),e}}withConsoleForwarding(){try{return ve(ze,{forwardConsoleLogsToWS:!0}),this}catch(e){throw Xe(1,e),e}}withExitOnUnhandledError(){try{return ve(ze,{exitOnUnhandledError:!0}),Je(),this}catch(e){throw Xe(1,e),e}}withAsyncFlushOnExit(){try{return ve(ze,{asyncFlushOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withExitCodeLogging(){try{return ve(ze,{logExitCode:!0}),this}catch(e){throw Xe(1,e),e}}withElementOnExit(){try{return ve(ze,{appendElementOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withInteropCleanupOnExit(){try{return ve(ze,{interopCleanupOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withDumpThreadsOnNonZeroExit(){try{return ve(ze,{dumpThreadsOnNonZeroExit:!0}),this}catch(e){throw Xe(1,e),e}}withWaitingForDebugger(e){try{return ve(ze,{waitForDebugger:e}),this}catch(e){throw Xe(1,e),e}}withInterpreterPgo(e,t){try{return ve(ze,{interpreterPgo:e,interpreterPgoSaveDelay:t}),ze.runtimeOptions?ze.runtimeOptions.push("--interp-pgo-recording"):ze.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Xe(1,e),e}}withConfig(e){try{return ve(ze,e),this}catch(e){throw Xe(1,e),e}}withConfigSrc(e){try{return e&&"string"==typeof e||Be(!1,"must be file path or URL"),Ee(We,{configSrc:e}),this}catch(e){throw Xe(1,e),e}}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||Be(!1,"must be directory path"),ve(ze,{virtualWorkingDirectory:e}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariable(e,t){try{const o={};return o[e]=t,ve(ze,{environmentVariables:o}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||Be(!1,"must be dictionary object"),ve(ze,{environmentVariables:e}),this}catch(e){throw Xe(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&Be(!1,"must be boolean"),ve(ze,{diagnosticTracing:e}),this}catch(e){throw Xe(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||Be(!1,"must be number"),ve(ze,{debugLevel:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ve(ze,{applicationArguments:e}),this}catch(e){throw Xe(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ze.runtimeOptions?ze.runtimeOptions.push(...e):ze.runtimeOptions=e,this}catch(e){throw Xe(1,e),e}}withMainAssembly(e){try{return ve(ze,{mainAssemblyName:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Xe(1,e),e}}withApplicationEnvironment(e){try{return ve(ze,{applicationEnvironment:e}),this}catch(e){throw Xe(1,e),e}}withApplicationCulture(e){try{return ve(ze,{applicationCulture:e}),this}catch(e){throw Xe(1,e),e}}withResourceLoader(e){try{return Pe.loadBootResource=e,this}catch(e){throw Xe(1,e),e}}async download(){try{await async function(){lt(We),await Re(We),re(),D(),oe(),await Pe.allDownloadsFinished.promise}()}catch(e){throw Xe(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await ct(We),Fe.api}()),this.instance}catch(e){throw Xe(1,e),e}}async run(){try{return We.config||Be(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Xe(1,e),e}}},mt=Xe,gt=ct;Ie||"function"==typeof globalThis.URL||Be(!1,"This browser/engine doesn't support URL API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),"function"!=typeof globalThis.BigInt64Array&&Be(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),ft.withConfig(/*json-start*/{
  "mainAssemblyName": "Web",
  "resources": {
    "hash": "sha256-U7QbRx3SG9Ij74trxdePL6P+rWlPXDX0ygwpdphibKc=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.nax01e3wmq.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.0j6ezsi0n0.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.vaz5ish0hl.wasm",
        "integrity": "sha256-CWxvpyFOlImUizcpa5t+p9bCp9nO9RMq79ZMHV0C7/c="
      }
    ],
    "icu": [
      {
        "virtualPath": "icudt_CJK.dat",
        "name": "icudt_CJK.tjcz0u77k5.dat",
        "integrity": "sha256-SZLtQnRc0JkwqHab0VUVP7T3uBPSeYzxzDnpxPpUnHk="
      },
      {
        "virtualPath": "icudt_EFIGS.dat",
        "name": "icudt_EFIGS.tptq2av103.dat",
        "integrity": "sha256-8fItetYY8kQ0ww6oxwTLiT3oXlBwHKumbeP2pRF4yTc="
      },
      {
        "virtualPath": "icudt_no_CJK.dat",
        "name": "icudt_no_CJK.lfu7j35m59.dat",
        "integrity": "sha256-L7sV7NEYP37/Qr2FPCePo5cJqRgTXRwGHuwF5Q+0Nfs="
      }
    ],
    "coreAssembly": [
      {
        "virtualPath": "System.Runtime.InteropServices.JavaScript.wasm",
        "name": "System.Runtime.InteropServices.JavaScript.8adgr7i8lr.wasm",
        "integrity": "sha256-wMGzfRS9pRr7N6gxXPdn9yHFV1QOsg46RzXxkf5eRSw="
      },
      {
        "virtualPath": "System.Private.CoreLib.wasm",
        "name": "System.Private.CoreLib.hay3xneaey.wasm",
        "integrity": "sha256-p1LzY2YvoLXTpVqrqT6yPWle9EKPZghnEw+bFqf2FjQ="
      }
    ],
    "assembly": [
      {
        "virtualPath": "Blazor-Analytics.wasm",
        "name": "Blazor-Analytics.4ensyheg7d.wasm",
        "integrity": "sha256-bIvCtkn88pVsOm5DqLGKrhR3kj1VwhhG8KdF1Hk8yLE="
      },
      {
        "virtualPath": "ClosedXML.wasm",
        "name": "ClosedXML.6d8ybop3kc.wasm",
        "integrity": "sha256-Jga3CUuNfwvpwnsHZxpWURvivrlrEqnUo9XR5EOLYGc="
      },
      {
        "virtualPath": "ClosedXML.Parser.wasm",
        "name": "ClosedXML.Parser.kcd8ka7nog.wasm",
        "integrity": "sha256-bN619zkNwVyqGT2tFn5NMXPDIo3OUVaRYMG/tXRtn+A="
      },
      {
        "virtualPath": "ClosedXML.Report.wasm",
        "name": "ClosedXML.Report.fcz87vijt6.wasm",
        "integrity": "sha256-74fGlfMz4HhXehO/K3vaVnzZ53lEmUNw68kwmSzDXVA="
      },
      {
        "virtualPath": "DocumentFormat.OpenXml.wasm",
        "name": "DocumentFormat.OpenXml.0gr15g35pw.wasm",
        "integrity": "sha256-MBbZzfjPOSIquvoCx+NxtLYAI63vk0mstqecqcKULfc="
      },
      {
        "virtualPath": "DocumentFormat.OpenXml.Framework.wasm",
        "name": "DocumentFormat.OpenXml.Framework.hhtgzs2q6p.wasm",
        "integrity": "sha256-u3MueHh2gEf2RLS+UmJWjOGBzFZ5Gl0WU5GjsX7xBxs="
      },
      {
        "virtualPath": "ExcelNumberFormat.wasm",
        "name": "ExcelNumberFormat.zkc9yronjy.wasm",
        "integrity": "sha256-DWrWJf+NGbjj12iqpO+Ufl4YS0bkW1yz6JwjocU15wY="
      },
      {
        "virtualPath": "FluentValidation.wasm",
        "name": "FluentValidation.yuptatg8bv.wasm",
        "integrity": "sha256-1vkL1fNCyvLkWYtavCvKdUZS0BPY392ec7LutoPTps4="
      },
      {
        "virtualPath": "HarfBuzzSharp.wasm",
        "name": "HarfBuzzSharp.4hlg18yscd.wasm",
        "integrity": "sha256-uLEJA5ByBiQq38q3IGoYXLmTF16imsrHhqzqIXEa9Ec="
      },
      {
        "virtualPath": "Humanizer.wasm",
        "name": "Humanizer.pak7mh5wtp.wasm",
        "integrity": "sha256-gWwyCllTD2EmGIEQ7jn930yf1Jaz9dMWgxRb43MBWUg="
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Authorization.24wpdespix.wasm",
        "integrity": "sha256-0pM0DfXTlXwt5byjg8GY6hI7WZqD7bp0yuAF9a5a3CE="
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.wasm",
        "name": "Microsoft.AspNetCore.Components.b0xsgk9bzh.wasm",
        "integrity": "sha256-5TlhLNLhvVeIU/sgLUCgFahGtSgSTzv89GeS2f8LmzU="
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Authorization.wasm",
        "name": "Microsoft.AspNetCore.Components.Authorization.cs2uyjnf1s.wasm",
        "integrity": "sha256-nCdY/poYzSsbiacv3NG/YUho33Sf32fiTRiEHhpC4Dw="
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Forms.wasm",
        "name": "Microsoft.AspNetCore.Components.Forms.nnpv7o2u2x.wasm",
        "integrity": "sha256-XQUHUs/koGf1zpxogmNrEkTaN0F9/0r6y4lKA9Zo6iQ="
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Web.wasm",
        "name": "Microsoft.AspNetCore.Components.Web.qh6s5svrgv.wasm",
        "integrity": "sha256-Iivy1A5pgvmtGPsWxUXMvW9JzJjDkjFo7DozxxA21Xk="
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.jdbv6k0qex.wasm",
        "integrity": "sha256-V2E5mmNwkll19GXcdxle/pQ0gmvl1Ap/yNOUOWC4AMk="
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.Authentication.p8ruaz49sn.wasm",
        "integrity": "sha256-aRuPSobzJT9+36eEXaqpPXiOZxWC1h0obkqjzqLuxB8="
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Metadata.wasm",
        "name": "Microsoft.AspNetCore.Metadata.ozafg5wh8q.wasm",
        "integrity": "sha256-nJbxgy1FNXzidLw/dYrSWJyKsQvcIylgs/E1Gwlz53E="
      },
      {
        "virtualPath": "Microsoft.DotNet.HotReload.WebAssembly.Browser.wasm",
        "name": "Microsoft.DotNet.HotReload.WebAssembly.Browser.vdogoxs6cm.wasm",
        "integrity": "sha256-c1tB08GIbOIai2bA4PgA2IQ6tQSbxkg1YfB8lhzFdh4="
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.wasm",
        "name": "Microsoft.Extensions.Configuration.b4ag5ca3hy.wasm",
        "integrity": "sha256-O8yFoXOCuagjAnA3Wvrxey9pIj9SnutRhynHG3xAptk="
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Abstractions.wasm",
        "name": "Microsoft.Extensions.Configuration.Abstractions.u6kdl0idi5.wasm",
        "integrity": "sha256-isNdV/HEWJzlxPbIjWWKw9ZwypgiMiCsGq+LL/Q6580="
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Binder.wasm",
        "name": "Microsoft.Extensions.Configuration.Binder.l8fo6ohol4.wasm",
        "integrity": "sha256-JZkFdMBcLMd9Xp4qd24Bm9ulPl4jBKlH9+BciewrYzo="
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Json.wasm",
        "name": "Microsoft.Extensions.Configuration.Json.3ped9ou2lc.wasm",
        "integrity": "sha256-WQurBbuDtkPNdWdC7Lbk07OTfLLaEGN9l68/4BxEU7c="
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.8fjxoklqth.wasm",
        "integrity": "sha256-bGfswqTBTTjC8gYtP/JoAtYuEC44sUQZ9WRCgCprUHU="
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.Abstractions.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.Abstractions.b51gqony2h.wasm",
        "integrity": "sha256-zxPqCMTJYEM/rIJgRoHPhFoOsvhUz763q+hsW2GzfuU="
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyModel.wasm",
        "name": "Microsoft.Extensions.DependencyModel.pkiabcs8hj.wasm",
        "integrity": "sha256-dyjqOZrh0fL9qXtviAqzKuApsnl/SKAIMcg5Eusesac="
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.wasm",
        "name": "Microsoft.Extensions.Diagnostics.d43fimjtkc.wasm",
        "integrity": "sha256-jZeB93jicHAdser4C1CbwiaWaOTSHfI/8msbTLoR7zI="
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.Abstractions.wasm",
        "name": "Microsoft.Extensions.Diagnostics.Abstractions.kg82yi04eu.wasm",
        "integrity": "sha256-rH5o3AJmiF2jvEdUdw/YSDjjw9gJkl3NJzqc+LtyoGE="
      },
      {
        "virtualPath": "Microsoft.Extensions.Http.wasm",
        "name": "Microsoft.Extensions.Http.ehl11free4.wasm",
        "integrity": "sha256-ApAWljeW1DQchZrcjzk77hqzEY6HBkFDtKw0slIegaI="
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.wasm",
        "name": "Microsoft.Extensions.Logging.kiwzmfszb4.wasm",
        "integrity": "sha256-XIGgkaZTKTdkXhP7cHCrDzVlj166SOYO40M/GdLWDRs="
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.Abstractions.wasm",
        "name": "Microsoft.Extensions.Logging.Abstractions.13xf8wazun.wasm",
        "integrity": "sha256-PT2A+lutjRsRKmVVbhj0sTJv/O/ltbxJFCMev5x7mnw="
      },
      {
        "virtualPath": "Microsoft.Extensions.Options.wasm",
        "name": "Microsoft.Extensions.Options.9sst0jsocv.wasm",
        "integrity": "sha256-+SIz1P0XCX0619m4ayLxuTO6YvxE0CCk54AmItSx0TI="
      },
      {
        "virtualPath": "Microsoft.Extensions.Primitives.wasm",
        "name": "Microsoft.Extensions.Primitives.p672mzndxd.wasm",
        "integrity": "sha256-H31NJrVFPp2OG96Xx3ncGsD6GKT7dPmJqzg7vrTmgLY="
      },
      {
        "virtualPath": "Microsoft.Extensions.Validation.wasm",
        "name": "Microsoft.Extensions.Validation.76ntqv5qki.wasm",
        "integrity": "sha256-jyCj0FXEPC8tSTyFwUJmED4+KOGNxFY1gvM//ZXXDuQ="
      },
      {
        "virtualPath": "Microsoft.JSInterop.wasm",
        "name": "Microsoft.JSInterop.wftbgy7wkk.wasm",
        "integrity": "sha256-zLHxODEC7FIdEfQV01BFNBzzlCQg4+kVRn9+wZJI/Sg="
      },
      {
        "virtualPath": "Microsoft.JSInterop.WebAssembly.wasm",
        "name": "Microsoft.JSInterop.WebAssembly.0o8pok1eno.wasm",
        "integrity": "sha256-4I+0kHd4SomBH7+AYwAomyTpYchOHG0eb6kHl9ZrDTk="
      },
      {
        "virtualPath": "Microsoft.ML.Core.wasm",
        "name": "Microsoft.ML.Core.bxjtgvvl3m.wasm",
        "integrity": "sha256-HyZST47FO+wUJSHgSeHBC2RS1CJEuaEnqzP3Y8DwrHE="
      },
      {
        "virtualPath": "Microsoft.ML.Data.wasm",
        "name": "Microsoft.ML.Data.ox7wm0gfo5.wasm",
        "integrity": "sha256-1HkCliT+BpD5WDDfdo402a4qwu2lXcM8Kz1zbZ6N5xU="
      },
      {
        "virtualPath": "Microsoft.ML.KMeansClustering.wasm",
        "name": "Microsoft.ML.KMeansClustering.6vabkiwx18.wasm",
        "integrity": "sha256-PpieiRwzMSrDcGs+ztUYAcDRCl6VW0LTTCj4TEeqVmE="
      },
      {
        "virtualPath": "Microsoft.ML.PCA.wasm",
        "name": "Microsoft.ML.PCA.xl08hf2lwq.wasm",
        "integrity": "sha256-FWJaxOd7G/Bj5EABuaqKHK4pvoWVEGb9/YIBx4L/jrg="
      },
      {
        "virtualPath": "Microsoft.ML.StandardTrainers.wasm",
        "name": "Microsoft.ML.StandardTrainers.9t28s6ey65.wasm",
        "integrity": "sha256-w2vrFkWFeiK5MkIZVSY9+8V7sGKYgriV6woC+26wJsA="
      },
      {
        "virtualPath": "Microsoft.ML.Transforms.wasm",
        "name": "Microsoft.ML.Transforms.ckk36w4c50.wasm",
        "integrity": "sha256-JO8iJi00DIvJJ+3uRV/tnFjkX6SPCF1frku+URJXAVU="
      },
      {
        "virtualPath": "Microsoft.ML.CpuMath.wasm",
        "name": "Microsoft.ML.CpuMath.75p1cvfdjn.wasm",
        "integrity": "sha256-i80Ipsg5RG0DMfpPpt4jr77JDgECkptfxVsLNYUa8sw="
      },
      {
        "virtualPath": "Microsoft.ML.DataView.wasm",
        "name": "Microsoft.ML.DataView.75um0vs3tz.wasm",
        "integrity": "sha256-5WG8HDaTA9yHrhowktgauIqri1ZFw2VwAP5QaV2SUZ0="
      },
      {
        "virtualPath": "MoreLinq.wasm",
        "name": "MoreLinq.t9qndd4vav.wasm",
        "integrity": "sha256-P5qbdkn5DK+SW/6U7HrwZaXF92oMBCozV8hlIQDDy/0="
      },
      {
        "virtualPath": "Newtonsoft.Json.wasm",
        "name": "Newtonsoft.Json.qkbufwhni2.wasm",
        "integrity": "sha256-GlXMWKvDs45M2pACoR3Y4Qh8mcrOZGljqmvJY+6JZ5s="
      },
      {
        "virtualPath": "QuestPDF.wasm",
        "name": "QuestPDF.urfme4jgvb.wasm",
        "integrity": "sha256-pnix/kZGAJXSMR5FZKT+njhgafD1XFfExT7rA0Py5XU="
      },
      {
        "virtualPath": "RBush.wasm",
        "name": "RBush.048ali807d.wasm",
        "integrity": "sha256-cHsLmPL/ZYfNtaqW793aQui9cR4N7JXjHm+xggcoz7w="
      },
      {
        "virtualPath": "Serilog.wasm",
        "name": "Serilog.gyu309shhb.wasm",
        "integrity": "sha256-eINX94J6ml442Owg464gw2C0HWnn4gb6U4urj876uoE="
      },
      {
        "virtualPath": "Serilog.Extensions.Logging.wasm",
        "name": "Serilog.Extensions.Logging.4aarm1ia2h.wasm",
        "integrity": "sha256-NHk6xRS3dIv7nLWuBUZvce3EXz7kvxiBsrjE7KvSMcI="
      },
      {
        "virtualPath": "Serilog.Formatting.Compact.wasm",
        "name": "Serilog.Formatting.Compact.4jmpy34ji2.wasm",
        "integrity": "sha256-UIyeQvWYOcthNH9MPe2VOkn3DuLVkXXwUkQ7siKIUno="
      },
      {
        "virtualPath": "Serilog.Settings.Configuration.wasm",
        "name": "Serilog.Settings.Configuration.aio2pf68g1.wasm",
        "integrity": "sha256-kp2SCXz4fdsDb+fR+WFGpxe7oRlXLYbU5tSruqQUGHw="
      },
      {
        "virtualPath": "Serilog.Sinks.BrowserConsole.wasm",
        "name": "Serilog.Sinks.BrowserConsole.x47anjwz3v.wasm",
        "integrity": "sha256-oaUyoVo2a89AwFRLViIDIdBnhpi17mAo03uxs1QLd2g="
      },
      {
        "virtualPath": "Serilog.Sinks.BrowserHttp.wasm",
        "name": "Serilog.Sinks.BrowserHttp.yuh16li3u5.wasm",
        "integrity": "sha256-qipma+046bRHJU4HS+vEXCZCvxb1llPL3nDMRRDlALk="
      },
      {
        "virtualPath": "Serilog.Sinks.PeriodicBatching.wasm",
        "name": "Serilog.Sinks.PeriodicBatching.bkp0ftramr.wasm",
        "integrity": "sha256-a7SsUJRYu0ocL5GIOv+h90XsR4Ge1a45cP4pvKiiQrE="
      },
      {
        "virtualPath": "SixLabors.Fonts.wasm",
        "name": "SixLabors.Fonts.1ntl3xbxha.wasm",
        "integrity": "sha256-TYAwAMga8iYHgcD5+CMUwDAmCjtNUbzJJP6/e+Xtl5w="
      },
      {
        "virtualPath": "SkiaSharp.wasm",
        "name": "SkiaSharp.osajynwy76.wasm",
        "integrity": "sha256-mKSwRVPnye7ocT8vO4jAjG0B8tt4+AkMF6mnQcubm7M="
      },
      {
        "virtualPath": "SkiaSharp.HarfBuzz.wasm",
        "name": "SkiaSharp.HarfBuzz.7puwmda4yf.wasm",
        "integrity": "sha256-ay0z9UhyLxhPHwJnyVJBy2zKfj0Rk5bU3N23ZEhXX3g="
      },
      {
        "virtualPath": "SkiaSharp.Views.Blazor.wasm",
        "name": "SkiaSharp.Views.Blazor.5bdyj4d3j1.wasm",
        "integrity": "sha256-H3XgHwsVuSSf+ZPfo79+JJmXR7W/KT9vPQOXsQRE+As="
      },
      {
        "virtualPath": "System.CodeDom.wasm",
        "name": "System.CodeDom.yx1nyu4kqy.wasm",
        "integrity": "sha256-lkNyquW9zP8bN5VGHdTOgbzfceKVzlYMq3eVPRmhT6Q="
      },
      {
        "virtualPath": "System.IO.Packaging.wasm",
        "name": "System.IO.Packaging.ig3wndboch.wasm",
        "integrity": "sha256-fqsFQ1qc3dTVq0FGMdKgnnlf8TUxqyCy7KI6iB7F8ao="
      },
      {
        "virtualPath": "System.Linq.Dynamic.Core.wasm",
        "name": "System.Linq.Dynamic.Core.yt3i0kvfqy.wasm",
        "integrity": "sha256-5mMcR6Lmx8cWJWZek6QwlfKvlcYMgwRBLA5/5KeAVes="
      },
      {
        "virtualPath": "System.Numerics.Tensors.wasm",
        "name": "System.Numerics.Tensors.wa44xt9pq0.wasm",
        "integrity": "sha256-OOwE74r8Tvv7VD1EAagYTCkS9/3GRpY8Lo2veul0oiw="
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.wasm",
        "name": "Toolbelt.Blazor.HeadElement.3owkc4sscm.wasm",
        "integrity": "sha256-Y75VwEf7HtcK38P94hintQ/mS9jYJIMHjHBeLlbbuMI="
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.Abstractions.wasm",
        "name": "Toolbelt.Blazor.HeadElement.Abstractions.x5jlech633.wasm",
        "integrity": "sha256-u501aGqqctPbubaom/I8a06VWX49+BPMkFdMD9DZ6Ns="
      },
      {
        "virtualPath": "Toolbelt.Blazor.HeadElement.Services.wasm",
        "name": "Toolbelt.Blazor.HeadElement.Services.d2s2vfnjr9.wasm",
        "integrity": "sha256-lUUkjiF9VItGEXZ1LJzjXzsABjxlCf4xn7aj66M3xQQ="
      },
      {
        "virtualPath": "Toolbelt.Blazor.HotKeys2.wasm",
        "name": "Toolbelt.Blazor.HotKeys2.v7h04l627d.wasm",
        "integrity": "sha256-1dB3wka+FnCTPvSyPFyi/V36ja0wRgn9n/nEgbNlIMM="
      },
      {
        "virtualPath": "Microsoft.CSharp.wasm",
        "name": "Microsoft.CSharp.aed559hjql.wasm",
        "integrity": "sha256-EozgKlhyUz4iSrkfJJSydGffvx/0pS994UzyRU/dIQ8="
      },
      {
        "virtualPath": "System.Buffers.wasm",
        "name": "System.Buffers.0rjii1srtc.wasm",
        "integrity": "sha256-hFUXSZrGMpGzVJiVUXosiKJDEzee8T58DnNTCaMu6+Q="
      },
      {
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.s9m6dg6quc.wasm",
        "integrity": "sha256-wPzIinSAqRLChwDWd6eu3phPDdmqshGLYmrjCRVantk="
      },
      {
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.9zxnyilx86.wasm",
        "integrity": "sha256-xYUaSIPo4TspwQcYMt6OHvH6LdU/JFRlxvFvVb5jBoE="
      },
      {
        "virtualPath": "System.Collections.NonGeneric.wasm",
        "name": "System.Collections.NonGeneric.5o6nlxs4ra.wasm",
        "integrity": "sha256-e2XEy9dKq4OWfdVDI4xWVvW990pB/SUYP71MKAAFk1U="
      },
      {
        "virtualPath": "System.Collections.Specialized.wasm",
        "name": "System.Collections.Specialized.3ppkjhpqfg.wasm",
        "integrity": "sha256-F6ZkiqEzkJMnE2TTRYXB6sHba9NX+ZuMeCvJKZKLrf0="
      },
      {
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.optqj0cats.wasm",
        "integrity": "sha256-7Eu6wfW0wpM1Q3qEhgjtZK17Kj8AKbN2BxWIzkXvS/A="
      },
      {
        "virtualPath": "System.ComponentModel.Annotations.wasm",
        "name": "System.ComponentModel.Annotations.cam2iu77eo.wasm",
        "integrity": "sha256-rGhWJ1UA0hn8encuA+h3A0RPlnNl3oT/w0/TMM0CE54="
      },
      {
        "virtualPath": "System.ComponentModel.Primitives.wasm",
        "name": "System.ComponentModel.Primitives.d2kkbxnypz.wasm",
        "integrity": "sha256-laH7D7jhKYlr+6szqeHn1kb40hJLrJWB2THycYz23+c="
      },
      {
        "virtualPath": "System.ComponentModel.TypeConverter.wasm",
        "name": "System.ComponentModel.TypeConverter.samijo0d05.wasm",
        "integrity": "sha256-nCuvR3qocPCSDSC6jwl4R4VTsTU4w5pgs5/Fo4M0tPc="
      },
      {
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.fehd3jdv4r.wasm",
        "integrity": "sha256-qcdxtsrqwCXIRdZ2by51UTMA0IhoYj2lWlh7ECraNmQ="
      },
      {
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.0dr2di376y.wasm",
        "integrity": "sha256-BJz9jz5wxINNIXvtsEE206CMjq8VLO/Iw4RkMqfZDaY="
      },
      {
        "virtualPath": "System.Core.wasm",
        "name": "System.Core.oqfjfnse12.wasm",
        "integrity": "sha256-cEFphYHMZ+cBCmpoo71Dgeu741t4ZMlJE9SI9akH+aM="
      },
      {
        "virtualPath": "System.Data.Common.wasm",
        "name": "System.Data.Common.whe85xcqjc.wasm",
        "integrity": "sha256-ZIRv1mt2MPs9LNKK93jhK6GGA5ISeBhfl2GaR5yoqhY="
      },
      {
        "virtualPath": "System.Diagnostics.Debug.wasm",
        "name": "System.Diagnostics.Debug.xj3bdthl2l.wasm",
        "integrity": "sha256-QwgFddXs3rOd0R6aaR9Jr1FkU4CpLL1+3y5i/+1qggA="
      },
      {
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.0qnoy52ybn.wasm",
        "integrity": "sha256-GMTXkVlhuqoPB8orlWmgdP02AzzpP8HJII6YZvizVE4="
      },
      {
        "virtualPath": "System.Diagnostics.Process.wasm",
        "name": "System.Diagnostics.Process.kkje9561nm.wasm",
        "integrity": "sha256-lNe9b4m2TYc7a78KFUqcAMU4wtq4Zq6e3ZcV38G7zHo="
      },
      {
        "virtualPath": "System.Diagnostics.Tools.wasm",
        "name": "System.Diagnostics.Tools.zvhw0dg88j.wasm",
        "integrity": "sha256-cYm3uKQPsY2mJqOHk1D9D+y14p8IuBR0Syuy0Ch55pw="
      },
      {
        "virtualPath": "System.Diagnostics.TraceSource.wasm",
        "name": "System.Diagnostics.TraceSource.niiba27apy.wasm",
        "integrity": "sha256-f7QWcmNifJVzFXzDtfAqHXGwBX0BO137WN3u5i92mD0="
      },
      {
        "virtualPath": "System.Drawing.Primitives.wasm",
        "name": "System.Drawing.Primitives.10nm2qwwg3.wasm",
        "integrity": "sha256-8Qfe6ONqKy4+7UUxWb5TJCSjWsz0ZsG7Q8Fl1U0JFYY="
      },
      {
        "virtualPath": "System.Drawing.wasm",
        "name": "System.Drawing.8v1a6ukkfm.wasm",
        "integrity": "sha256-DJoelqUhVeHGNEhPHthja1BgXjgCleJwptglGHOM9OA="
      },
      {
        "virtualPath": "System.IO.Compression.Brotli.wasm",
        "name": "System.IO.Compression.Brotli.dsk3h9bi2z.wasm",
        "integrity": "sha256-/TDqh3TfOKgEN6Qbdan5epqECRLhZkNhrt/6rPQg3WY="
      },
      {
        "virtualPath": "System.IO.Compression.ZipFile.wasm",
        "name": "System.IO.Compression.ZipFile.rhrzz7kd7w.wasm",
        "integrity": "sha256-EYs9Rpugu4CPLPGzLOA8rqqiaKj2GD0TaUr3EGuIj+M="
      },
      {
        "virtualPath": "System.IO.Compression.wasm",
        "name": "System.IO.Compression.psdvhaezpk.wasm",
        "integrity": "sha256-mRCDj2mQ0cJ2FY5kR4cYomyebH+Th2rQ5ehe7r5QZao="
      },
      {
        "virtualPath": "System.IO.FileSystem.wasm",
        "name": "System.IO.FileSystem.yxfxk8n583.wasm",
        "integrity": "sha256-Cs0cNNz/VL3Bp5wnIaaSd5JfzNx0zduBWzB/jxNx8Wc="
      },
      {
        "virtualPath": "System.IO.MemoryMappedFiles.wasm",
        "name": "System.IO.MemoryMappedFiles.q2smtnx4p0.wasm",
        "integrity": "sha256-Xr+Q7Dh3UcmDZfCsUqhR7GRCwGjH5mb2gVCpWFtZSUU="
      },
      {
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.k38bln6ppa.wasm",
        "integrity": "sha256-QHFZl18RxrSUXjF28DJPOafp5QWBQfHDNVjjU/MlDnU="
      },
      {
        "virtualPath": "System.Linq.Expressions.wasm",
        "name": "System.Linq.Expressions.cp0pnzfgxd.wasm",
        "integrity": "sha256-0KJucYrl5gFXXPK9b7TNbsuXXMI0ZuFB5CA3axTTOwk="
      },
      {
        "virtualPath": "System.Linq.Parallel.wasm",
        "name": "System.Linq.Parallel.ml0nz9n9i4.wasm",
        "integrity": "sha256-NibSjm1TIwvdKlA2p2QhNPkYPLfExqd4EczXuseOxQ0="
      },
      {
        "virtualPath": "System.Linq.Queryable.wasm",
        "name": "System.Linq.Queryable.0pfw2dqvgj.wasm",
        "integrity": "sha256-jPKtDETtY62NUXpFwjS4gp3ILndHH6LcT4ENusouuc8="
      },
      {
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.xm8h5mku0w.wasm",
        "integrity": "sha256-+UIfzGkHXSpknDlwUsz3iNlVKSEtrZQUF6r4n6OSGZ0="
      },
      {
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.i3mc39el3c.wasm",
        "integrity": "sha256-3sG2lzDWT1UXzwehH2BmoT/nepuXrVVgDW67nbcWj2g="
      },
      {
        "virtualPath": "System.Net.Http.Json.wasm",
        "name": "System.Net.Http.Json.pezicbw9i6.wasm",
        "integrity": "sha256-P6QNfwt9kxneWjlpe+qfeD8sJjRJpwlX3nEUfekE5r8="
      },
      {
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.blx1fapg0m.wasm",
        "integrity": "sha256-sTivojo2SIPF/XXGFUsfvKqRd+0BrxabzmS/9FVOczE="
      },
      {
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.6b4z6magly.wasm",
        "integrity": "sha256-kn6kJMwrlaN2imzvhbWyN5hzKH/sWXEjTLFfu6UNyH0="
      },
      {
        "virtualPath": "System.Net.Requests.wasm",
        "name": "System.Net.Requests.qa1exckrl9.wasm",
        "integrity": "sha256-7pBhucCmiDmwzav2rs0te8SHvUiD+3otqv/KZB8sUcw="
      },
      {
        "virtualPath": "System.Net.WebHeaderCollection.wasm",
        "name": "System.Net.WebHeaderCollection.tziwbn6m1m.wasm",
        "integrity": "sha256-11BtIMdfVhf8AzvrhWdOdYvEBQ5UsJd7emoDXye05gc="
      },
      {
        "virtualPath": "System.Numerics.Vectors.wasm",
        "name": "System.Numerics.Vectors.6kbacfx87b.wasm",
        "integrity": "sha256-BpHAFX2p3rMGv5djy88todDwX1fMEA/vWHm6Uxw6XsU="
      },
      {
        "virtualPath": "System.ObjectModel.wasm",
        "name": "System.ObjectModel.2tiysm1row.wasm",
        "integrity": "sha256-Sug3cr4yP7qv6aiAg6LrN0BG/ckbOnzz05qZ8ES84xc="
      },
      {
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.z1v61nmhc9.wasm",
        "integrity": "sha256-t98Q1sEIOAt5j4czp6XGqLfNpm0F+hh0/XMOvGj5ZvU="
      },
      {
        "virtualPath": "System.Private.Xml.Linq.wasm",
        "name": "System.Private.Xml.Linq.jtcrdqn8l8.wasm",
        "integrity": "sha256-WUHJce8bVCbdUPb82ho2oCcNGwV23ukb0QVia5GIlzo="
      },
      {
        "virtualPath": "System.Private.Xml.wasm",
        "name": "System.Private.Xml.9j04vwkahv.wasm",
        "integrity": "sha256-S2XllwSI9u6B6rl0h3g3DbSv/XCNZ75fIrLb31wJVbw="
      },
      {
        "virtualPath": "System.Reflection.Emit.ILGeneration.wasm",
        "name": "System.Reflection.Emit.ILGeneration.cdfcab6n7b.wasm",
        "integrity": "sha256-0E4TQ7Ba5l1ZVAEWwJ5Pduywsu30OFG6sHsgnwMzuRM="
      },
      {
        "virtualPath": "System.Reflection.Emit.Lightweight.wasm",
        "name": "System.Reflection.Emit.Lightweight.rllaqc97f8.wasm",
        "integrity": "sha256-IRrkSCHDvxy2L57RTN9g7qudJ/88H9+9yxsEeziOtg8="
      },
      {
        "virtualPath": "System.Reflection.Emit.wasm",
        "name": "System.Reflection.Emit.ep5aaaceuu.wasm",
        "integrity": "sha256-Yi1EKRIrltBpPeo925NvnhrfFMn5CpOs0Krt2XZ/QpU="
      },
      {
        "virtualPath": "System.Reflection.Metadata.wasm",
        "name": "System.Reflection.Metadata.bz1ucydll8.wasm",
        "integrity": "sha256-h8l23/KLt3MRZPapJHm/NGuwjVkMg2/zppGoeGAbxHs="
      },
      {
        "virtualPath": "System.Reflection.Primitives.wasm",
        "name": "System.Reflection.Primitives.gep9eginc1.wasm",
        "integrity": "sha256-2NaObkwoP7Agp2ayvIKSgzJtWIeWP1z5fp4koewJLPA="
      },
      {
        "virtualPath": "System.Resources.ResourceManager.wasm",
        "name": "System.Resources.ResourceManager.z2attlmvtt.wasm",
        "integrity": "sha256-S6lhtCjGkpIfIvczn3/vACxAuznUae/6X4N8Ip+QCrk="
      },
      {
        "virtualPath": "System.Runtime.CompilerServices.Unsafe.wasm",
        "name": "System.Runtime.CompilerServices.Unsafe.gjlzj0lo0m.wasm",
        "integrity": "sha256-zJo6g1jH/ZNpgUalIIWNspjgOKPQiHKjZ/LMOW95y3A="
      },
      {
        "virtualPath": "System.Runtime.Extensions.wasm",
        "name": "System.Runtime.Extensions.6p9ufnnb26.wasm",
        "integrity": "sha256-mueLtNVrTUxlzPjPqlnWnMbzalmnT3/JjmfXzE/nD8c="
      },
      {
        "virtualPath": "System.Runtime.InteropServices.RuntimeInformation.wasm",
        "name": "System.Runtime.InteropServices.RuntimeInformation.ocwgxadidg.wasm",
        "integrity": "sha256-OH/aLex9MbjdT9haHVkdaxt35NZ71nRbgI6d4RlwX3o="
      },
      {
        "virtualPath": "System.Runtime.InteropServices.wasm",
        "name": "System.Runtime.InteropServices.2redfs3uk0.wasm",
        "integrity": "sha256-7poQTtCLhqdBoJ2x12fhDfdhy5F9ZPvTfMS/4RiVPEc="
      },
      {
        "virtualPath": "System.Runtime.Intrinsics.wasm",
        "name": "System.Runtime.Intrinsics.p0nmpxwdcr.wasm",
        "integrity": "sha256-98S8ryPb5aXWpYADJCGMdhSQED2pXkfM706Mp9LuiHc="
      },
      {
        "virtualPath": "System.Runtime.Loader.wasm",
        "name": "System.Runtime.Loader.mv8zuo36tg.wasm",
        "integrity": "sha256-Tvp2/QpqInzXRpsexYlIlz0bT26ySgNw/4HWTxHLxqQ="
      },
      {
        "virtualPath": "System.Runtime.Numerics.wasm",
        "name": "System.Runtime.Numerics.dn766ayok6.wasm",
        "integrity": "sha256-x0UV1o8YT5fMHTwez9Gv6g6CxUegPS7U0rx+LR0mQDI="
      },
      {
        "virtualPath": "System.Runtime.Serialization.Formatters.wasm",
        "name": "System.Runtime.Serialization.Formatters.udlfr8kedd.wasm",
        "integrity": "sha256-/gpWFa/UDjSbNJRJJsZD99TikVBIzoRS3vb53N9aO1I="
      },
      {
        "virtualPath": "System.Runtime.Serialization.Primitives.wasm",
        "name": "System.Runtime.Serialization.Primitives.df619ahp99.wasm",
        "integrity": "sha256-yTdjv2VTQlcJxfAEBApTZ2aYKPyS1BaUg/B13IO8jb0="
      },
      {
        "virtualPath": "System.Runtime.wasm",
        "name": "System.Runtime.zmn2kq72no.wasm",
        "integrity": "sha256-KxA0oA5EoTE/KD8spous3b8O8HcSbj33vfT+QSRjFIA="
      },
      {
        "virtualPath": "System.Security.Claims.wasm",
        "name": "System.Security.Claims.wibqgzwbnc.wasm",
        "integrity": "sha256-2PimD6uk9ZI7ZphemkZj8R+fZ3JxHWfPaQwTSSeLmHE="
      },
      {
        "virtualPath": "System.Security.Cryptography.wasm",
        "name": "System.Security.Cryptography.na5euk5e36.wasm",
        "integrity": "sha256-T5CNXmePYvl5Hn7f/KJ2asHlg2SaBEypldKHq65Krww="
      },
      {
        "virtualPath": "System.Text.Encoding.Extensions.wasm",
        "name": "System.Text.Encoding.Extensions.x98uzfezfg.wasm",
        "integrity": "sha256-mzvF6YD1bkeW0pDutvF+xE+1uVuZvA0LVHLm+oKdnFc="
      },
      {
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.imlv6nv0qk.wasm",
        "integrity": "sha256-OvEBMYP3m6YFPp/NJCvAnsJA09AWIOxl2eIGdIvP8+g="
      },
      {
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.efiowy1i79.wasm",
        "integrity": "sha256-thQ2zYmY1gOpJf0l0St9F0ivP68ojI8ZN8rSXp1VlkM="
      },
      {
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.3pq5ojnn9t.wasm",
        "integrity": "sha256-Z4r0U5zHdCdi3mnQDtImbeOtpiFIB0mLDfJmXqOK4G4="
      },
      {
        "virtualPath": "System.Threading.Channels.wasm",
        "name": "System.Threading.Channels.ds3i2xymfw.wasm",
        "integrity": "sha256-UxY4huxpYuyVAn67gQyzqJQ3gtm+BiIwscD4vLsNUr4="
      },
      {
        "virtualPath": "System.Threading.Tasks.Extensions.wasm",
        "name": "System.Threading.Tasks.Extensions.2eumyhkq3y.wasm",
        "integrity": "sha256-WkmXZTw1pTtyvH1Y64GdFDIJnuHgieE/HGKYcXHyMBY="
      },
      {
        "virtualPath": "System.Threading.Tasks.Parallel.wasm",
        "name": "System.Threading.Tasks.Parallel.hukkoofb53.wasm",
        "integrity": "sha256-dzEbIAxy/TvtRr2QizsgO4tQ0zzE2/QEwEOdwvri/zw="
      },
      {
        "virtualPath": "System.Threading.Tasks.wasm",
        "name": "System.Threading.Tasks.vnabrugfxh.wasm",
        "integrity": "sha256-u/1WSYP0KOI58f3LR9tYgaqay8GS7JdHtMUqTivrzec="
      },
      {
        "virtualPath": "System.Threading.Timer.wasm",
        "name": "System.Threading.Timer.d8oztd702a.wasm",
        "integrity": "sha256-bWa+63PRLf4C6wj5B25BOWzF6XwY3wtQR53f2SKI5aw="
      },
      {
        "virtualPath": "System.Threading.wasm",
        "name": "System.Threading.4yr17k39g0.wasm",
        "integrity": "sha256-RBXJdtbC7qO7xTaXUe1dQ/1DtjPB2IEUtzLoR+GY13k="
      },
      {
        "virtualPath": "System.Web.HttpUtility.wasm",
        "name": "System.Web.HttpUtility.u52ab5heog.wasm",
        "integrity": "sha256-ctiVUxatCh8M5yF3Uq5aUEvpEKyUJTCSzys2SaHbrfY="
      },
      {
        "virtualPath": "System.Xml.Linq.wasm",
        "name": "System.Xml.Linq.ezielfqkzx.wasm",
        "integrity": "sha256-msiP+hhmOL4pxMsH0pIsueaLh78LNDe1D7Gz60N0jvk="
      },
      {
        "virtualPath": "System.Xml.ReaderWriter.wasm",
        "name": "System.Xml.ReaderWriter.owqphk7z93.wasm",
        "integrity": "sha256-eAUsf0HCZcdvql/CSWJAVVgoY+ec4Ju0WMcYx984X78="
      },
      {
        "virtualPath": "System.Xml.XDocument.wasm",
        "name": "System.Xml.XDocument.w3nwgorqlr.wasm",
        "integrity": "sha256-/NxqYmf0+HY7Vd3gSnaylRUo2GJI5OCW8tq7T9L98V8="
      },
      {
        "virtualPath": "System.wasm",
        "name": "System.73436igil7.wasm",
        "integrity": "sha256-cwWOZaOlWiAEvO+KvJwrGbZmVGU/y9yYgansWdR1DsQ="
      },
      {
        "virtualPath": "netstandard.wasm",
        "name": "netstandard.wu5f3a9bhv.wasm",
        "integrity": "sha256-aGYj5r09ERVHBX7X5tnywLoOqp/tqW0PGHXoqjxvp30="
      },
      {
        "virtualPath": "CommonComponents.wasm",
        "name": "CommonComponents.x6glew7muc.wasm",
        "integrity": "sha256-OYPL3vy8A5R0Cvl3tV8YUAP/r7HcKEfxuDiEgEiMwcI="
      },
      {
        "virtualPath": "SharedComponents.wasm",
        "name": "SharedComponents.taf9f5eguh.wasm",
        "integrity": "sha256-tThVZaDoTY9QuJpVHwZF8XMYj9a3EF0U2uaFgR/mTjQ="
      },
      {
        "virtualPath": "SharedModels.wasm",
        "name": "SharedModels.6f3hj5jpf4.wasm",
        "integrity": "sha256-ksJp9PewWF927ZMU1KaecFuDoXWu2zgvJ5YFpHVE1eI="
      },
      {
        "virtualPath": "Web.wasm",
        "name": "Web.ozzpi5azcp.wasm",
        "integrity": "sha256-dGjudFjYqKZswyyNMgyN1R4ojB4/ssqb36I8Iv66FA8="
      }
    ],
    "lazyAssembly": [
      {
        "virtualPath": "AIDemoComponents.wasm",
        "name": "AIDemoComponents.t1yaqlirii.wasm",
        "integrity": "sha256-4XjL1fpaHzwoZ9f1w979KsjMhgJrPH3as7oheS8yXs4="
      },
      {
        "virtualPath": "BaseComponents.wasm",
        "name": "BaseComponents.r6xt774r1u.wasm",
        "integrity": "sha256-vc8mLsjMXOzZBFX+2uW2xVJNY2bgLGI9mbiHE04IEy8="
      },
      {
        "virtualPath": "BlazorDemoComponents.wasm",
        "name": "BlazorDemoComponents.htoqywtr0h.wasm",
        "integrity": "sha256-JXIY7td/y2BPzOhcc5ZNpfoAsSm6AanjaEm0uaT45D4="
      },
      {
        "virtualPath": "CachingDemoComponents.wasm",
        "name": "CachingDemoComponents.0jf4i4omnr.wasm",
        "integrity": "sha256-ZPYepPxBqLqq/7ngB+Xglqs61xhW215EeExuv1OcmLU="
      },
      {
        "virtualPath": "DatabaseDemoComponents.wasm",
        "name": "DatabaseDemoComponents.1qr2riqh3m.wasm",
        "integrity": "sha256-2VtqI49iGaIzCMDwKflkXBkJmpYEHe9Kb8uffMb8BP8="
      },
      {
        "virtualPath": "DDDDemoComponents.wasm",
        "name": "DDDDemoComponents.dojk5290vw.wasm",
        "integrity": "sha256-nZ0qkTNh1bFa+Ol6qVQ9S2cih+B4GwXt1z7OpWLKcK0="
      },
      {
        "virtualPath": "DependencyInjectionDemoComponents.wasm",
        "name": "DependencyInjectionDemoComponents.xewdm7fvty.wasm",
        "integrity": "sha256-Wef4Hz8uCIV+YjrRA4ljUkyr/FbLrvtuEROe1cVwXRM="
      },
      {
        "virtualPath": "DesignPatternDemoComponents.wasm",
        "name": "DesignPatternDemoComponents.xuihg06su9.wasm",
        "integrity": "sha256-uelRDpKjjytmvTggkdkLbs9ub+1a5TSijWEnL+jI4jQ="
      },
      {
        "virtualPath": "HTTPClientDemoComponents.wasm",
        "name": "HTTPClientDemoComponents.6nvqzwpvsn.wasm",
        "integrity": "sha256-svVy/pgb90QOQGvjCbYeqjUSjYqRx8AhNZcc2ZhUS10="
      },
      {
        "virtualPath": "JSONDemoComponents.wasm",
        "name": "JSONDemoComponents.2cu5044xlx.wasm",
        "integrity": "sha256-LscFxyLBWa7mJgeViE1pSL/lb4MQVV1SyJbtKe+K8kY="
      },
      {
        "virtualPath": "LINQDemoComponents.wasm",
        "name": "LINQDemoComponents.6beskuzcaf.wasm",
        "integrity": "sha256-cjPUWIoIvDVOPVna1EG62iQ17VpamCfgWvoMO0sn9ZM="
      },
      {
        "virtualPath": "MAUIDemoComponents.wasm",
        "name": "MAUIDemoComponents.nk4fpa23xn.wasm",
        "integrity": "sha256-58U7HuvRcK4ewJrJH426Y9ir3sy/g1fZgyCiZ/mVf0U="
      },
      {
        "virtualPath": "MCPDemoComponents.wasm",
        "name": "MCPDemoComponents.97l4ub1v67.wasm",
        "integrity": "sha256-Vf4zclhm2+B1pICb6yzYTyo5bJAMh0CJFcYhoHBQU9g="
      },
      {
        "virtualPath": "MiddlewareDemoComponents.wasm",
        "name": "MiddlewareDemoComponents.ia8eiqdzbv.wasm",
        "integrity": "sha256-hlTmhehdu1SNGLFMHmEvvaAKbGYJt7Ikcx5gXXt6ees="
      },
      {
        "virtualPath": "MLNETDemoComponents.wasm",
        "name": "MLNETDemoComponents.vwy5taahsg.wasm",
        "integrity": "sha256-ATs1CPi2Lb2pNSAPLY2tEI90meWo10nVSIzJYkVpvP4="
      },
      {
        "virtualPath": "MSBuildDemoComponents.wasm",
        "name": "MSBuildDemoComponents.yadt66qnqq.wasm",
        "integrity": "sha256-muYf9/nMTe4IeH3kdhRP9eWsJoYlW2b47gf44vnHaTI="
      },
      {
        "virtualPath": "OOPSDemoComponents.wasm",
        "name": "OOPSDemoComponents.7ihumeevck.wasm",
        "integrity": "sha256-HyIXrfhlisRx/5nekiAv/+hihc12kl1RTpxxjtytKiA="
      },
      {
        "virtualPath": "OWASPDemoComponents.wasm",
        "name": "OWASPDemoComponents.hkqfgyynqy.wasm",
        "integrity": "sha256-vxP3xlsHnnHZ/61DZE4pjNZXRslS0aC+7vHqLuy84ew="
      },
      {
        "virtualPath": "PythonDemoComponents.wasm",
        "name": "PythonDemoComponents.6tcgns0ojs.wasm",
        "integrity": "sha256-2guMTmt4vIxsJvUwbSdTlgfj7f4c7vSovgLJ0eTVbo8="
      },
      {
        "virtualPath": "RegexDemoComponents.wasm",
        "name": "RegexDemoComponents.8i5co2t4vk.wasm",
        "integrity": "sha256-ICnem27g64KkOKsT4bFSuLcV8+Y/ky5+ykNIwxiVmYU="
      },
      {
        "virtualPath": "ReportDemoComponents.wasm",
        "name": "ReportDemoComponents.ni2gctfnwl.wasm",
        "integrity": "sha256-Qb9Ozhjw9HKjRo2wbAdL94XPLroiKmMPljPSXtCrazA="
      },
      {
        "virtualPath": "SecurityDemoComponents.wasm",
        "name": "SecurityDemoComponents.r9qzgq9kq9.wasm",
        "integrity": "sha256-v7fqz0XrF2VuwQXutl1z4lyP9Y8u+4FZo48PxErnrk4="
      },
      {
        "virtualPath": "SignalRDemoComponents.wasm",
        "name": "SignalRDemoComponents.8b1ndt5hgd.wasm",
        "integrity": "sha256-nEicXsMnOy7lr6+9Fd8aOhAwWd9NLP1sAMQQS9Nr+l8="
      },
      {
        "virtualPath": "SOLIDDemoComponents.wasm",
        "name": "SOLIDDemoComponents.m6ixk2ychv.wasm",
        "integrity": "sha256-sinqIBBbgwyBY3+Izk4u+oQXskL3ifrLXy/s11h4y14="
      },
      {
        "virtualPath": "TalkDemoComponents.wasm",
        "name": "TalkDemoComponents.uoqpqmvftl.wasm",
        "integrity": "sha256-XnXQZVCZQicP9sQ8BojWo7H1SdN/2yPT0sCbAMUfvtE="
      },
      {
        "virtualPath": "TDDDemoComponents.wasm",
        "name": "TDDDemoComponents.4mmqwby5rd.wasm",
        "integrity": "sha256-BH/9uRP8R4edifWEfq68mIkQmpKa0VdWk9PzkicCndA="
      },
      {
        "virtualPath": "TestingDemoComponents.wasm",
        "name": "TestingDemoComponents.haazbkz9qf.wasm",
        "integrity": "sha256-J7YyXUIURA+W+B+GjBJX9USakGTux0anBdMPs4zvx4w="
      },
      {
        "virtualPath": "WebAPIDemoComponents.wasm",
        "name": "WebAPIDemoComponents.ea6rh5y70y.wasm",
        "integrity": "sha256-auJXoDJOWFqqscDy48nZ4dN01GmIZyn9i7Fnwh/k4kE="
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
