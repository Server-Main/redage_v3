gm.events.add("render", () => {
    mp.players.forEachInStreamRange((player) => {

        if (mp.players.local === player) {
            return;
        }

        let needBlip = player['IS_MASK'] == false;

        if(player.blipId != undefined && !nativeInvoke("DOES_BLIP_EXIST", player.blipId)){
            delete player.blipId;
        }

        if (needBlip) {
            if (player.blipId == undefined) {
                gm.createPlayerBlip(player)
            }
        } else {
            deletePlayerBlip(player)
        }

    });
})