<script>
    import { translateText } from 'lang'
    import { accountUnique } from 'store/account'
    import { format } from 'api/formatter'
    export let SetView;
    export let SetPopup;


    let ItemList = {
        packages: true,
        cases: true
    };


    executeClientAsync("donate.getPack").then((result) => {
        if (result && typeof result === "string") {
            ItemList.packages = JSON.parse(result);
            getUnique ();
        }
    });

    executeClientAsync("donate.roulette.getList").then((result) => {
        if (result && typeof result === "string") {
            ItemList.cases = JSON.parse(result).caseData;
            getUnique ();
        }
    });

    let selectListId = {};
    let isBuy = false;

    const onSelectMenu = () => {
        getUnique ();
        if (isBuy)
            return window.notificationAdd(4, 9, `Вы уже использовали данное предложение`, 3000);
        let getList = $accountUnique.split("_")[0];
        if (getList === "cases") SetView("Cases")
        else SetPopup ("PopupDpPopup", selectListId);
    }

    import {executeClientAsync} from "api/rage";

    const getUnique = () => {
        if ($accountUnique) {
            let getData = $accountUnique.split("_");
            if (ItemList[getData[0]][getData[1]]) {
                selectListId = ItemList[getData[0]][getData[1]];
                isBuy = Number (getData[2]);
            } else
                isBuy = true;
        } else
            isBuy = true; 
    }

</script>

{#if ItemList.cases && ItemList.packages}
{/if}