<script>
    import { TimeFormat } from 'api/moment';
    import { serverDateTime } from 'store/server';
    import { itemsInfo } from 'json/itemsInfo.js'
    import { serverDonatMultiplier } from 'store/server';
    import { charMoney, charBankMoney } from 'store/chars';
    import { isWaterMark, isPlayer } from 'store/hud'
    import CountUp from 'api/countup';

    import logomain from '../HosteImages/logost.png';
    import online_hud from '../HosteImages/online.png';
    import amo_hud from '../HosteImages/ammo.png';
    import zone_hud from '../HosteImages/zone.png';
    import time_hud from '../HosteImages/time.png';

    


    let userData = {
        targetMoney: 0,
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
        }, !userData["target" + args] ? 0 : 1000)
    }

    let serverName = "";
    window.setServerName = (name) => serverName = name;

    let isRotate = false;
    
    const secretFunction = () => {
        isRotate = !isRotate;
    }

    let serverPlayerId = 100;
    window.serverStore.serverPlayerId = (value) => serverPlayerId = value;

    let serverOnline = 1000;
    window.serverStore.serverOnline = (value) => serverOnline = value;

    let weaponItemId = 0;
    window.hudStore.weaponItemId = (value) => weaponItemId = value;

    let clipSize = 0;
    window.hudStore.clipSize = (value) => clipSize = value;

    let ammo = 0;
    window.hudStore.ammo = (value) => ammo = value;

    let UUID = 0;
    window.serverStore.UUID = (value) => UUID = value;

    let greenZone = false;
    window.hudStore.greenZone = (value) => greenZone = value;

    let adminGm = false;
    window.hudStore.adminGm = (value) => adminGm = value;

    let isShow = false;

    serverDonatMultiplier.subscribe(value => {
        if (value > 1) {
            isShow = true;

            setTimeout(() => {
                isShow = false;
            }, 1000 * 30);
        }
    });
</script>


<div class="header">
    <div class="logo">
        <img src={logomain}/>   
        <div class="text1123123123">RAGECODE</div>
        <div class="text212312312">V3</div>
    </div>
    <div class="info">
        <div class="item">
            <img src={online_hud} />
            <div class="text">{serverOnline}</div>
        </div>
        <div class="item">
            <div class="text">ID {serverPlayerId}</div>
        </div>
        <div class="item">
            <div class="text">{UUID}</div>
        </div>
        <div class="item">
            <img src={time_hud} />
            <div class="text">{TimeFormat ($serverDateTime, "H:mm")}</div>
        </div>
    </div>
</div>
<div class="moneys_items">
    <div class="money">
        <a>$</a>
        <p id="targetMoney">0</p>
    </div>
    <div class="bank">
        <a>$</a>
        <p id="targetBank">0</p>
    </div>
</div> -->

{#if greenZone}
    <div class="greenzone">
        <div class="block">
        <!-- <div class="text1">ЗЕЛЁНАЯ</div>
        <div class="text2">ЗОНА</div> -->
        <div class="img"></div>
        <img src={zone_hud}></div>
    </div>
{/if}
