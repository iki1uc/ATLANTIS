export const OLDCITY = {

    HY() {
        return {
            name: "HY",
            seg: ["◉", 3, 9, "◎", 81, "3↺"],
            cache: false
        };
    },

    PE() {
        return {
            name: "PE",
            seg: ["◉", 9, "◎", 81, "◆", "△", "▣"],
            cache: false
        };
    },

    PER() {
        return {
            name: "PER",
            seg: [3, 9, 81, "◆", 756, "△", 27, "▣", "3↺"],
            cache: false
        };
    },

    all() {
        return {
            HY: this.HY(),
            PE: this.PE(),
            PER: this.PER()
        };
    }
};
