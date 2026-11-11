const KEYS_LIST = [
    { name: "E", keyCode: 69 },
    { name: "W", keyCode: 87 },
    { name: "A", keyCode: 65 },
    { name: "S", keyCode: 83 },
    { name: "D", keyCode: 68 },
    { name: "F", keyCode: 70 },
    { name: "G", keyCode: 71 },
    { name: "H", keyCode: 72 },
    { name: "X", keyCode: 88 },
    { name: "Z", keyCode: 90 },
    { name: "Q", keyCode: 81 },
]

const COUNT_OF_PROGRESS_WHEN_PRESS = 10;
const COUNT_OF_PROGRESS_WHILE_INACTIVE = 1;

var app = new Vue({
    el: "#app",
    data: {
        opened: false,
        key: 0,

        progress: 0,

        interval: null,
    },
    methods: {
        init() {
            this.key = Math.floor(Math.random() * KEYS_LIST.length);
            this.opened = true;
			this.progress = 0;

            this.interval = setInterval(() => {
                if (this.progress <= 0) return;
                this.progress -= COUNT_OF_PROGRESS_WHILE_INACTIVE;
            }, 100);
        },

        keyUp(event) {
            if (!this.opened) return;

            var keyData = KEYS_LIST[this.key];
            if (keyData === undefined) return;

            if (event.keyCode === keyData.keyCode) {
                this.progress += COUNT_OF_PROGRESS_WHEN_PRESS;

                if (this.progress >= 100) {
                    clearInterval(this.interval);
                    this.interval = null;
                    this.opened = false;

                    mp.trigger("merumond.keysMinigame.end");
                }
            }
        }
    }
});

document.addEventListener("keyup", app.keyUp);