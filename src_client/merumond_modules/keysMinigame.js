const browser = mp.browsers.new(`http://package/interface/modules/KeysMinigame/index.html`);

let callbackEvent = "";
mp.events.add("merumond.keysMinigame.start", (callback) => {
    callbackEvent = callback;
    browser.execute(`app.init()`);

    global.menuOpen();
});

mp.events.add("merumond.keysMinigame.end", () => {
    mp.events.callRemote(callbackEvent);
    callbackEvent = "";
    
    global.menuClose();
});