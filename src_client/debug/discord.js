gm.discord = (detailedStatus) => {
    let state = "на RAGECODE V3";

    if (global.localplayer && typeof global.localplayer.remoteId !== "undefined")
        state = translateText('на RAGECODE V3 под ID {0}', global.localplayer.remoteId);

    mp.discord.update(detailedStatus, state);
}

global.discordDefault = () => {
    gm.discord(translateText('Наслаждается жизнью'))
};

discordDefault ();