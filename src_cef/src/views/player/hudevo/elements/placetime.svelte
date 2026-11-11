<script>
    import homeimg from '../HosteImages/home.png';
    import invimg from '../HosteImages/inv.png';
    import vzaimimg from '../HosteImages/vzaim.png';
    import micimg from '../HosteImages/mic.png';
    import telimg from '../HosteImages/tel.png';
    import chatimg from '../HosteImages/chat.png';

    import { charIsPet } from 'store/chars';
    import { isHelp } from 'store/hud';

    import { getQuest } from 'json/quests/quests.js';
    import { storeQuests, selectQuest } from 'store/quest'
    import { executeClient } from 'api/rage'

    let userData = {
        targetMoney: 10000,
        changeMoney: 0,
        timerIdMoney: 0,
        Money: 0,
        targetBank: 0,
        changeBank: 0,
        timerIdBank: 0,
        Bank: 0,
    };

    import { onMount } from 'svelte';

    onMount(async () => {
        charMoney.subscribe(value => {
            if (userData.Money !== value) {
                CounterUpdate ("Money", value);
            }
        });
        charBankMoney.subscribe(value => {
            if (userData.Bank !== value) {
                CounterUpdate ("Bank", value);
            }
        });
    });

    let QuestsList = [];
    let OldQuest = [];

    let quest = false;
    const onSelectQuest = (actorName) => {

        const listIndex = QuestsList.findIndex(q => q.ActorName == actorName);
        if (QuestsList [listIndex]) {
            quest = QuestsList [listIndex];
            return;
        }
        quest = false;
    }

    storeQuests.subscribe((value) => {
        if (value && value.length && OldQuest != value) {
            executeClient ("client.quest.selectQuest.Clear");

            OldQuest = value;
            QuestsList = [];

            value.forEach(questData => {
                const questInfo = getQuest(questData.ActorName, questData.Line);

                if (questInfo && questInfo.Title && questInfo.Tasks && questInfo.Tasks [questData.Stage]) {
                    executeClient ("client.quest.selectQuest.Add", questData.ActorName, (questInfo.Tasks.length - 1) === questData.Stage);

                    QuestsList.push ({
                        ActorName: questData.ActorName,
                        Title: questInfo.Title,
                        Text: questInfo.Tasks [questData.Stage]
                    });
                }
            });
            QuestsList = QuestsList;

            if (!quest && QuestsList.length && typeof QuestsList [0] === "object" && typeof $selectQuest !== "string") {
                quest = QuestsList [0];
                selectQuest.set (quest.ActorName);
                return;
            }

            onSelectQuest ($selectQuest);
        }
    });

    selectQuest.subscribe((value) => {
        onSelectQuest (value);
    });

    const CounterUpdate = (args, value) => {
        if (userData["timerId" + args])
            clearTimeout (userData["timerId" + args]);
        userData["change" + args] = userData[args] > value ? (0 - (userData[args] - value)) : (value - userData[args]);
        userData[args] = value;
        userData["timerId" + args] = setTimeout (() => {
            userData["timerId" + args] = 0;
            userData["change" + args] = 0;
            if (!userData["target" + args]) {
                userData["target" + args] = new CountUp("target" + args, value);
            }
            else
                userData["target" + args].update(value);
        }, !userData["target" + args] ? 0 : 5000)
    }

    let isWorld = true;
    window.hudStore.isWorld = (value) => isWorld = value;

    let direction = "NE";
    window.hudStore.direction = (value) => direction = value;

    let greenZone = false;
    window.hudStore.greenZone = (value) => greenZone = value;

    let street = "";
    window.hudStore.street = (value) => street = value;

    let area = "";
    window.hudStore.area = (value) => area = value;

    let micIcon = "hud__icon-micoff";
    let microphone = 0;
    
    window.hudStore.microphone = (value) => {
        microphone = value;
        UpdateMicrophoneIcon ();
    }

    let isMute = false;
    window.hudStore.isMute = (value) => {
        isMute = value;
        UpdateMicrophoneIcon ();
    }

    const UpdateMicrophoneIcon = () => {
        if (isMute) micIcon = "hud__icon-micmute";
        else if (microphone) micIcon = "hud__icon-micon";
        else micIcon = "hud__icon-micoff";
    }

    let polygon = 0;
    window.hudStore.polygon = (value) => polygon = value;

    let radio = 0;
    window.hudStore.radio = (value) => radio = value;
    
    let serverPlayerId = 0;
    window.serverStore.serverPlayerId = (value) => serverPlayerId = value;

    let serverOnline = 0;
    window.serverStore.serverOnline = (value) => serverOnline = value;
	
	let uuid = 0;
    window.serverStore.uuid = (value) => uuid = value;
</script>

<div class="hudevo__placetime">
    <div class="leftmenu">
        {#if quest.Title}
            <div class="kvest">
                <div class="kvestitem" style="top: .5vw;position: absolute;">
                    <div class="kvestitem_romb">
                        <div class="kvestitem_romb1">
                            <div class="kvestitem_romb2"></div>
                        </div>
                    </div>
                    <span class="kvestitem_tilte">{@html quest.Title}</span>
                </div>
                <div class="kvestitem">
                    <div class="kvestitem_krug"></div>
                    <span class="kvestitem_subtilte">{@html quest.Text}</span>
                </div>
            </div>
        {/if}
        
        {#if !$charIsPet && $isHelp}
            <div class="buttons">
                <div class="m">M</div>
                <div class="m">TAB</div>
                <div class="m">F2</div>
                <div class="m" >ALT</div>
                <div class="m">P</div>
                <div class="m">T</div>
                <img class="home" src={homeimg} />
                <img class="inv" src={invimg} />
                <img class="vzaim" src={vzaimimg} />
                <img class="mic" src={micimg} />
                <img class="tel" src={telimg} />
                <img class="chat" src={chatimg} />
            </div>
        {/if}

        <div class="street">
            <div class="compas">NE</div>
            <div class="linia"></div>
            <div class="name">{street}</div>
            <div class="name2">{area}</div>
        </div>
    </div>
</div>