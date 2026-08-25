import { GEOG } from "./GEO.geo.js";

export const NEUCITY = {

    // Leitbahn: Breite / Höhe / Tiefe / Trans
    leitbahn(v) {
        return {
            breite: v * 3,
            hoehe: v * 9,
            tiefe: v * 27,
            trans: v * 81
        };
    },

    // Gegen‑Cache: invertierte GEO‑Segmente
    gegenCache() {
        return {
            HY: GEOG.HY().reverse(),
            PE: GEOG.PE().reverse(),
            PER: GEOG.PER().reverse()
        };
    },

    // TMP‑Resultierende: α / β / γ
    tmpResult(v) {
        return {
            TMPa: GEOG.TMPa().map(x => x + "_a"),
            TMPb: GEOG.TMPb().map(x => x + "_b"),
            TMPg: GEOG.TMPg().map(x => x + "_g"),
            value: v
        };
    },

    // Gesamtpaket
    city(v) {
        return {
            leitbahn: this.leitbahn(v),
            gegenCache: this.gegenCache(),
            tmp: this.tmpResult(v),
            mode: "NEUCITY"
        };
    }
};
