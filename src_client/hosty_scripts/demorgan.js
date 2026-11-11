const Demorganbrowser = mp.browsers.new(`http://package/hosty_jail/index.html`);

mp.events.add("client.go.demorgan", (time, reason, admin) => {
    Demorganbrowser.execute(`Demorgan.Open()`);

    Demorganbrowser.execute(`Demorgan.demorgantime=${time}`);
    Demorganbrowser.execute(`Demorgan.demorganadmin='${admin}'`);
    Demorganbrowser.execute(`Demorgan.demorganreason='${reason}'`); 
});

mp.events.add("client.stop.demorgan", () => {
    Demorganbrowser.execute(`Demorgan.Close()`);
});
