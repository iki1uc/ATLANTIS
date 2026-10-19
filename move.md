<!DOCTYPE html>
<html lang="de">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>ATLANTIS · BOOT · ENGINE · COCKPIT</title>

<style>
:root {
    --fg: #0ff;
    --bg: #000;
    --gold: #ffd700;
    --step1: #f44;
    --step2: #4f4;
    --step3: #4ff;
    --step4: #f4f;
}
* { box-sizing: border-box; margin: 0; padding: 0; }
body {
    background: var(--bg);
    color: var(--fg);
    font-family: 'Consolas', monospace;
    padding: 12px;
    min-height: 100vh;
    display: grid;
    grid-template-rows: auto 1fr auto;
    gap: 10px;
}

header {
    border-bottom: 2px solid var(--gold);
    padding-bottom: 8px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex-wrap: wrap;
    gap: 6px;
}
header h1 {
    font-size: 18px;
    color: var(--gold);
    letter-spacing: 2px;
}
#clock { font-size: 12px; color: #666; }

main {
    display: grid;
    grid-template-columns: 2fr 1fr;
    gap: 10px;
    height: 100%;
    min-height: 400px;
}

#sequence {
    background: #0a0a12;
    border: 1px solid #1a1a2a;
    border-radius: 10px;
    padding: 12px;
    display: flex;
    flex-direction: column;
    gap: 4px;
}
.step {
    display: flex;
    align-items: center;
    gap: 10px;
    padding: 6px 12px;
    border-radius: 6px;
    background: #0e0e1a;
    border-left: 3px solid #222;
    transition: all 0.3s ease;
    cursor: pointer;
}
.step .num {
    width: 24px;
    height: 24px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 50%;
    background: #1a1a2a;
    color: #666;
    font-size: 10px;
    font-weight: bold;
    flex-shrink: 0;
}
.step .name { font-weight: bold; font-size: 13px; flex: 1; }
.step .status { font-size: 10px; color: #444; }
.step .indicator {
    width: 10px;
    height: 10px;
    border-radius: 50%;
    background: #222;
    flex-shrink: 0;
}
.step.active { border-left-color: var(--fg); background: #0f1a1a; }
.step.active .num { background: var(--fg); color: #000; }
.step.active .indicator { background: var(--fg); box-shadow: 0 0 12px var(--fg); }
.step.done { border-left-color: #4f4; opacity: 0.7; }
.step.done .num { background: #4f4; color: #000; }
.step.done .indicator { background: #4f4; }
.step.error { border-left-color: #f44; background: #1a0a0a; }
.step.error .num { background: #f44; color: #000; }
.step.error .indicator { background: #f44; }

#status-panel {
    background: #0a0a12;
    border: 1px solid #1a1a2a;
    border-radius: 10px;
    padding: 12px;
    display: flex;
    flex-direction: column;
    gap: 8px;
}
#status-panel h3 { font-size: 11px; color: #666; border-bottom: 1px solid #1a1a2a; padding-bottom: 6px; }
#live-log {
    flex: 1;
    overflow-y: auto;
    font-size: 10px;
    color: #6a6a8a;
    line-height: 1.5;
    min-height: 150px;
    max-height: 300px;
}
#live-log .entry { padding: 2px 0; border-bottom: 1px solid #0a0a12; }
#live-log .entry .time { color: #444; }
#live-log .entry .msg { color: #8af; }
#live-log .entry .msg.ok { color: #4f4; }
#live-log .entry .msg.err { color: #f44; }
#live-log .entry .msg.warn { color: #ff4; }

#controls {
    display: flex;
    gap: 6px;
    flex-wrap: wrap;
    margin-top: 6px;
}
button {
    background: #111;
    color: var(--fg);
    border: 1px solid #333;
    padding: 6px 14px;
    border-radius: 6px;
    cursor: pointer;
    font-family: inherit;
    font-size: 11px;
    transition: all 0.2s;
}
button:hover { background: #1a1a2a; border-color: var(--gold); }
button.primary { border-color: var(--gold); background: var(--gold); color: #000; }
button.danger { border-color: #f44; color: #f44; }
button.success { border-color: #4f4; color: #4f4; }

#geo-container {
    grid-column: 1 / -1;
    background: #0a0a12;
    border: 1px solid #1a1a2a;
    border-radius: 10px;
    padding: 12px;
    min-height: 80px;
    display: flex;
    align-items: center;
    justify-content: center;
}
#geo-container pre {
    font-size: 11px;
    color: #4a6a6a;
    text-align: center;
    line-height: 1.3;
    white-space: pre;
}

#config-panel {
    grid-column: 1 / -1;
    background: #0a0a12;
    border: 1px solid #1a1a2a;
    border-radius: 10px;
    padding: 8px 14px;
    display: flex;
    flex-wrap: wrap;
    gap: 6px 16px;
    font-size: 10px;
    color: #666;
}
#config-panel .item { display: flex; gap: 4px; align-items: center; }
#config-panel .item .label { color: #444; }
#config-panel .item .value { color: var(--gold); }

/* Cockpit-Richter */
.cockpit-grid {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    gap: 4px;
    font-size: 9px;
    color: #666;
    margin-top: 4px;
}
.cockpit-grid .wert { color: var(--fg); }
.cockpit-grid .gold { color: var(--gold); }

@media (max-width: 800px) {
    main { grid-template-columns: 1fr; }
    #geo-container { min-height: 60px; }
}
</style>
</head>
<body>

<header>
    <div>
        <h1>⚡ ATLANTIS · BOOT · ENGINE · COCKPIT</h1>
    </div>
    <div>
        <span id="clock">⏱️ --:--:--</span>
        <span style="margin-left:12px;color:#666;" id="cycle-count">Zyklus 0</span>
    </div>
</header>

<main>
    <div id="sequence">
        <div style="display:flex;justify-content:space-between;align-items:center;margin-bottom:6px;">
            <span style="color:#666;font-size:11px;">▶ BOOT-SEQUENZ</span>
            <span id="progress-text" style="font-size:10px;color:#444;">0/10</span>
        </div>

        <div class="step" data-step="0"><span class="num">1</span><span class="name">DOOR</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="1"><span class="num">2</span><span class="name">DOO</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="2"><span class="num">3</span><span class="name">RESPO</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="3"><span class="num">4</span><span class="name">OS</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="4"><span class="num">5</span><span class="name">NC.engine</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="5"><span class="num">6</span><span class="name">243</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="6"><span class="num">7</span><span class="name">SCORE</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="7"><span class="num">8</span><span class="name">WETTE</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="8"><span class="num">9</span><span class="name">SLIDE</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>
        <div class="step" data-step="9"><span class="num">10</span><span class="name">⚡ Axiom</span><span class="status">⏳ wartet</span><span class="indicator"></span></div>

        <div id="controls">
            <button class="primary" id="btn-boot">▶ BOOT</button>
            <button id="btn-step">⏭ STEP</button>
            <button class="danger" id="btn-reset">⟲ RESET</button>
            <button class="success" id="btn-auto">▶ AUTO</button>
            <button id="btn-richter">⚡ RICHTER</button>
            <button id="btn-admin">🟥 ADMIN‑0</button>
        </div>

        <!-- Cockpit-Richter Anzeige -->
        <div class="cockpit-grid">
            <div>Energie: <span class="gold" id="energie-wert">0</span></div>
            <div>Zustand: <span class="wert" id="zustand-wert">idle</span></div>
            <div>Entscheidung: <span class="wert" id="entscheidung-wert">—</span></div>
            <div>Admin: <span class="wert" id="admin-status">● bereit</span></div>
        </div>
    </div>

    <div id="status-panel">
        <h3>📊 SYSTEM-STATUS</h3>
        <div id="live-log">
            <div class="entry"><span class="time">[INIT]</span> <span class="msg ok">⚡ ATLANTIS · BOOT · ENGINE · COCKPIT</span></div>
            <div class="entry"><span class="time">[INIT]</span> <span class="msg">📦 10 Module — warte auf BOOT</span></div>
        </div>
        <div style="display:flex;gap:10px;font-size:10px;color:#666;border-top:1px solid #1a1a2a;padding-top:6px;">
            <span>🔴 <span id="status-led" style="color:#f44;">●</span> idle</span>
            <span>📦 <span id="module-count">0</span>/10 aktiv</span>
        </div>
    </div>
</main>

<div id="geo-container">
    <pre id="geo-display">
   ▲
  / \
 /   \
/_____\
    </pre>
</div>

<div id="config-panel">
    <span class="item"><span class="label">📂 System:</span> <span class="value">ATLANTIS</span></span>
    <span class="item"><span class="label">🔄 Status:</span> <span class="value" id="status-value">BEREIT</span></span>
    <span class="item"><span class="label">⚡ Modus:</span> <span class="value" id="mode-value">IDLE</span></span>
</div>

<script type="module">
// ============================================================
// ATLANTIS · BOOT · ENGINE · COCKPIT
// ============================================================

import { MXU } from './engine/mxu.js';
import { AXINXA } from './engine/AXINXA.js';
import { SLI } from './engine/SLI.js';
import { MXU_CH } from './engine/MXU_ch.js';

// ─── ADMIN‑0 ──────────────────────────────────────────────────
const ADMIN0 = MXU.init();
AXINXA.attach(ADMIN0);
SLI.attach(ADMIN0);
ADMIN0.mxu_ch = MXU_CH.init();
ADMIN0.hoehe = "81";
ADMIN0.breite = "9";
ADMIN0.tiefe = "3";
ADMIN0.axiom = "allxall · ANKER-Tech · Energie-Kugel";

// ─── RICHTER ──────────────────────────────────────────────────
const RICHTER = {
    energie: 0,
    zustand: 'idle',
    entscheidung: null,

    bewerte(triolet) {
        const energie = triolet.gewicht * 0.618;
        this.energie += energie;
        this.zustand = energie > 1.5 ? 'hoch' : 'niedrig';
        return { energie, zustand: this.zustand };
    },

    entscheide() {
        this.entscheidung = this.energie > 20 ? 'NARRATIV GÜLTIG' :
                           this.energie > 10 ? 'NARRATIV MÖGLICH' :
                           'NARRATIV UNSICHER';
        ADMIN0.level = this.energie > 15 ? 2 : 1;
        ADMIN0.score.eval = this.energie;
        ADMIN0.sli_addScore(this.energie);
        return this.entscheidung;
    },

    status() {
        return {
            energie: this.energie,
            zustand: this.zustand,
            entscheidung: this.entscheidung,
            admin: {
                axis: ADMIN0.axis,
                level: ADMIN0.level,
                score: ADMIN0.score.eval,
                slide: ADMIN0.sli_slide
            }
        };
    }
};

// ─── MODULES ──────────────────────────────────────────────────
const MODULES = [
    { id: 'DOOR', fn: 'doorOpen', label: 'Tür öffnen' },
    { id: 'DOO', fn: 'dooRun', label: 'Domain starten' },
    { id: 'RESPO', fn: 'respoCheck', label: 'RESPO prüfen' },
    { id: 'OS', fn: 'osStart', label: 'OS booten' },
    { id: 'NC.engine', fn: 'ncStart', label: 'NC-Engine starten' },
    { id: '243', fn: 'two43Start', label: '243-System aktivieren' },
    { id: 'SCORE', fn: 'scoreEval', label: 'SCORE bewerten' },
    { id: 'WETTE', fn: 'wettePredict', label: 'WETTE prognostizieren' },
    { id: 'SLIDE', fn: 'slideOptimize', label: 'SLIDE optimieren' },
    { id: 'AXIOM', fn: 'axiomActivate', label: '⚡ AXIOM aktivieren' }
];

const MODULE_FNS = {
    doorOpen: () => ({ status: 'open', message: 'Tür geöffnet' }),
    dooRun: () => ({ status: 'running', message: 'DOO läuft' }),
    respoCheck: () => ({ status: 'ok', message: 'RESPO: 3-9-27-81' }),
    osStart: () => ({ status: 'active', message: 'OS gestartet' }),
    ncStart: () => ({ status: 'active', message: 'NC.engine: 243 Neuronen' }),
    two43Start: () => ({ status: 'active', message: '243-System aktiv' }),
    scoreEval: () => ({ status: 'ok', message: 'SCORE: 0.87' }),
    wettePredict: () => ({ status: 'ok', message: 'WETTE: 0.618' }),
    slideOptimize: () => ({ status: 'ok', message: 'SLIDE optimiert' }),
    axiomActivate: () => ({ status: 'active', message: '⚡ AXIOM aktiviert — System treu' })
};

// ─── DOM ──────────────────────────────────────────────────────
const steps = document.querySelectorAll('.step');
const logDiv = document.getElementById('live-log');
const statusLed = document.getElementById('status-led');
const moduleCount = document.getElementById('module-count');
const progressText = document.getElementById('progress-text');
const cycleCountEl = document.getElementById('cycle-count');
const geoDisplay = document.getElementById('geo-display');
const statusValue = document.getElementById('status-value');
const modeValue = document.getElementById('mode-value');
const energieWert = document.getElementById('energie-wert');
const zustandWert = document.getElementById('zustand-wert');
const entscheidungWert = document.getElementById('entscheidung-wert');
const adminStatus = document.getElementById('admin-status');

const btnBoot = document.getElementById('btn-boot');
const btnStep = document.getElementById('btn-step');
const btnReset = document.getElementById('btn-reset');
const btnAuto = document.getElementById('btn-auto');
const btnRichter = document.getElementById('btn-richter');
const btnAdmin = document.getElementById('btn-admin');

let currentStep = -1;
let isRunning = false;
let autoMode = false;
let autoTimer = null;
let cycleCount = 0;
let stepTimeouts = [];

// ─── LOG ──────────────────────────────────────────────────────
function log(msg, type = 'info') {
    const entry = document.createElement('div');
    entry.className = 'entry';
    const time = new Date().toLocaleTimeString();
    const cls = type === 'ok' ? 'ok' : type === 'err' ? 'err' : type === 'warn' ? 'warn' : '';
    entry.innerHTML = `<span class="time">[${time}]</span> <span class="msg ${cls}">${msg}</span>`;
    logDiv.appendChild(entry);
    logDiv.scrollTop = logDiv.scrollHeight;
    while (logDiv.children.length > 100) logDiv.removeChild(logDiv.firstChild);
}

// ─── RICHTER ANZEIGEN ────────────────────────────────────────
function zeigeRichter() {
    const status = RICHTER.status();
    energieWert.textContent = RICHTER.energie.toFixed(2);
    zustandWert.textContent = RICHTER.zustand;
    entscheidungWert.textContent = RICHTER.entscheidung || '—';
    adminStatus.textContent = `● ${ADMIN0.state}`;
    log(`⚡ RICHTER: Energie = ${RICHTER.energie.toFixed(2)} · ${RICHTER.entscheidung}`, 'ok');
}

// ─── ADMIN ZEIGEN ─────────────────────────────────────────────
function zeigeAdmin() {
    log(`🟥 ADMIN‑0: ${JSON.stringify(ADMIN0, null, 2)}`, 'ok');
    adminStatus.textContent = '● ADMIN';
}

// ─── RESET ────────────────────────────────────────────────────
function resetAll() {
    isRunning = false;
    if (autoTimer) { clearInterval(autoTimer); autoTimer = null; }
    autoMode = false;
    btnAuto.textContent = '▶ AUTO';
    stepTimeouts.forEach(t => clearTimeout(t));
    stepTimeouts = [];
    currentStep = -1;
    cycleCount = 0;
    cycleCountEl.textContent = 'Zyklus 0';
    statusValue.textContent = 'RESET';
    modeValue.textContent = 'IDLE';
    RICHTER.energie = 0;
    RICHTER.zustand = 'idle';
    RICHTER.entscheidung = null;

    steps.forEach((step) => {
        step.className = 'step';
        const statusEl = step.querySelector('.status');
        statusEl.textContent = '⏳ wartet';
    });

    updateProgress();
    updateModuleCount();
    statusLed.textContent = '●';
    statusLed.style.color = '#f44';
    zeigeRichter();
    log('⟲ RESET — Alle Module zurückgesetzt', 'warn');
}

function updateProgress() {
    const done = document.querySelectorAll('.step.done').length;
    progressText.textContent = `${done}/${steps.length}`;
}

function updateModuleCount() {
    const active = document.querySelectorAll('.step.done, .step.active').length;
    moduleCount.textContent = active;
}

// ─── STEP ─────────────────────────────────────────────────────
function executeStep(index) {
    if (index >= MODULES.length) {
        log('✅ ALLE MODULE AUSGEFÜHRT — System bereit!', 'ok');
        statusLed.textContent = '●';
        statusLed.style.color = '#4f4';
        statusValue.textContent = 'BEREIT';
        modeValue.textContent = 'SYSTEM OK';
        isRunning = false;
        ADMIN0.state = 'BEREIT';

        if (autoMode) {
            cycleCount++;
            cycleCountEl.textContent = `Zyklus ${cycleCount}`;
            log(`🔄 Zyklus ${cycleCount} startet...`, 'warn');
            setTimeout(() => {
                resetAll();
                setTimeout(() => executeStep(0), 300);
            }, 3000);
        }
        return;
    }

    const step = steps[index];
    const module = MODULES[index];

    step.className = 'step active';
    const statusEl = step.querySelector('.status');
    statusEl.textContent = '⏳ läuft...';
    isRunning = true;
    statusValue.textContent = `BOOT ${index+1}/${MODULES.length}`;
    modeValue.textContent = 'RUNNING';
    ADMIN0.state = 'BOOTING';

    log(`▶ ${module.id} — ${module.label}`, 'info');

    const duration = 400 + Math.random() * 600;
    const timeout = setTimeout(() => {
        const success = Math.random() > 0.1;
        if (success) {
            step.className = 'step done';
            statusEl.textContent = '✅ erledigt';
            const result = MODULE_FNS[module.fn] ? MODULE_FNS[module.fn]() : { status: 'ok', message: 'erledigt' };
            log(`✅ ${module.id} — ${result.message || 'erfolgreich'}`, 'ok');

            // Richter bewertet bei 243
            if (module.id === '243' && result.status === 'active') {
                // Simuliere Triolets
                const triolets = [
                    { ecke1: 'a', ecke2: 'b', ecke3: 'c', gewicht: 2 },
                    { ecke1: 'x', ecke2: 'y', ecke3: 'z', gewicht: 3 },
                    { ecke1: 'OS', ecke2: 'BOOT', ecke3: '243', gewicht: 1 }
                ];
                for (const t of triolets) {
                    RICHTER.bewerte(t);
                }
                RICHTER.entscheide();
                zeigeRichter();
                log(`⚡ RICHTER: Energie = ${RICHTER.energie.toFixed(2)}`, 'ok');
            }

            if (module.id === 'RESPO') log('📊 RESPO-Matrix: 3-9-27-81', 'ok');
            if (module.id === 'NC.engine') log('🧠 NC.engine: 243 Neuronen', 'ok');
            if (module.id === 'AXIOM') {
                log('⚡ AXIOM aktiviert — System ist treu', 'ok');
                geoDisplay.textContent = `   ★\n  / \\\n /   \\\n/_____\\\n  ●  ●`;
                statusLed.textContent = '●';
                statusLed.style.color = '#ffd700';
                ADMIN0.state = 'AKTIV';
            }

            updateProgress();
            updateModuleCount();
            currentStep = index;
            stepTimeouts.push(setTimeout(() => executeStep(index + 1), 200 + Math.random() * 300));
        } else {
            step.className = 'step error';
            statusEl.textContent = '❌ Fehler';
            log(`❌ ${module.id} — FEHLER!`, 'err');
            statusLed.textContent = '●';
            statusLed.style.color = '#f44';
            statusValue.textContent = 'FEHLER';
            modeValue.textContent = 'ERROR';
            ADMIN0.state = 'ERROR';
            isRunning = false;
            log('⛔ Boot abgebrochen — bitte RESET', 'err');
        }
        updateModuleCount();
    }, duration);
    stepTimeouts.push(timeout);
}

function boot() {
    if (isRunning) { log('⚠️ System läuft bereits', 'warn'); return; }
    const hasDone = document.querySelectorAll('.step.done').length > 0;
    if (hasDone) { resetAll(); setTimeout(() => boot(), 300); return; }

    log('🚀 BOOT-SEQUENZ gestartet', 'warn');
    statusLed.textContent = '●';
    statusLed.style.color = '#ff4';
    statusValue.textContent = 'BOOTING';
    modeValue.textContent = 'START';
    ADMIN0.state = 'STARTING';
    currentStep = -1;
    executeStep(0);
}

function stepManual() {
    if (isRunning) { log('⚠️ System läuft bereits', 'warn'); return; }
    const nextIndex = currentStep + 1;
    if (nextIndex >= MODULES.length) { log('✅ Alle Module bereits ausgeführt', 'ok'); return; }
    if (currentStep >= 0) { executeStep(nextIndex); }
    else { boot(); }
}

function toggleAuto() {
    autoMode = !autoMode;
    btnAuto.textContent = autoMode ? '⏸ PAUSE' : '▶ AUTO';
    if (autoMode) {
        log('▶ AUTO-Modus gestartet', 'warn');
        if (!isRunning && currentStep < MODULES.length - 1) {
            resetAll();
            setTimeout(() => boot(), 500);
        }
    } else {
        log('⏸ AUTO-Modus pausiert', 'warn');
        if (autoTimer) { clearInterval(autoTimer); autoTimer = null; }
    }
}

// ─── RICHTER BEWERTEN ─────────────────────────────────────────
function richterBewerte() {
    // Simuliere Triolets
    const triolets = [
        { ecke1: 'a', ecke2: 'b', ecke3: 'c', gewicht: 2 },
        { ecke1: 'x', ecke2: 'y', ecke3: 'z', gewicht: 3 },
        { ecke1: 'OS', ecke2: 'BOOT', ecke3: '243', gewicht: 1 },
        { ecke1: 'DOOR', ecke2: 'DOO', ecke3: 'RESPO', gewicht: 2 }
    ];
    RICHTER.energie = 0;
    for (const t of triolets) {
        RICHTER.bewerte(t);
    }
    RICHTER.entscheide();
    zeigeRichter();
    log(`⚡ RICHTER: Energie = ${RICHTER.energie.toFixed(2)} · ${RICHTER.entscheidung}`, 'ok');
}

// ─── EVENTS ──────────────────────────────────────────────────
btnBoot.addEventListener('click', boot);
btnStep.addEventListener('click', stepManual);
btnReset.addEventListener('click', resetAll);
btnAuto.addEventListener('click', toggleAuto);
btnRichter.addEventListener('click', richterBewerte);
btnAdmin.addEventListener('click', zeigeAdmin);

// ─── TASTATUR ──────────────────────────────────────────────────
document.addEventListener('keydown', (e) => {
    if (e.key === 'b' || e.key === 'B') btnBoot.click();
    if (e.key === 's' || e.key === 'S') btnStep.click();
    if (e.key === 'r' || e.key === 'R') btnReset.click();
    if (e.key === 'a' || e.key === 'A') btnAuto.click();
    if (e.key === 'x' || e.key === 'X') btnRichter.click();
    if (e.key === ' ') { e.preventDefault(); toggleAuto(); }
});

// ─── UHR ──────────────────────────────────────────────────────
function updateClock() {
    document.getElementById('clock').textContent = `⏱️ ${new Date().toLocaleTimeString()}`;
}
setInterval(updateClock, 1000);
updateClock();

// ─── INIT ────────────────────────────────────────────────────
log('⚡ ATLANTIS · BOOT · ENGINE · COCKPIT geladen', 'ok');
log(`📦 ${MODULES.length} Module integriert`, 'info');
log('⌨️  b=Boot  s=Step  r=Reset  a=Auto  space=Toggle  x=Richter', 'info');

updateModuleCount();
updateProgress();
statusValue.textContent = 'BEREIT';
modeValue.textContent = 'IDLE';
zeigeRichter();

console.log('⚡ ATLANTIS · BOOT · ENGINE · COCKPIT');
console.log(`📦 ${MODULES.length} Module integriert`);
console.log('⌨️  b=Boot  s=Step  r=Reset  a=Auto  space=Toggle  x=Richter');

// Auto-Boot nach 1 Sekunde
setTimeout(() => {
    log('🔄 Auto-Boot aktiv — starte...', 'warn');
    boot();
}, 1000);
</script>

</body>
</html>
