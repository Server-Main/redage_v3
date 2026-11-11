<script>
    import { translateText } from 'lang'
    import { fly } from 'svelte/transition';
    import { getQuest } from 'json/quests/quests.js';
    import { storeQuests, selectQuest } from 'store/quest'
    import keys from 'store/keys'
    import keysName from 'json/keys.js'
    import { executeClient } from 'api/rage'
    import { isHelp } from 'store/hud'

    import quest_icon from '../img/quest_icon.svg';
    import quest_icon2 from '../img/quest_icon2.svg';

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
</script>
<!-- {#if quest && quest.Title && $isHelp}
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
{/if} -->

