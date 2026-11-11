<script>
    import { translateText } from "lang";
    import "./main.sass";
    import "./main.css";
    export let popupData;

    const statuses = [
    ];

    let currentStatus = 1;
</script>

<!-- <div id="mainpopup" class="{popupData.Type}">
    <div class="mainpopup__back hud__icon-warning"></div>
    <div class="mainpopup__main {popupData.Type}">
        <div class="mainpopup__smallback hud__icon-warning"></div>
        <div class="popup__title {popupData.Type}">{popupData.Title}</div>
        <div class="popup__subtitle">{popupData.Text}</div>
        {#if false}
        <div class="box-between">
            <div class="popup__button big {popupData.Type}">{statuses[currentStatus].button}</div>
            <div class="popup__button red">{translateText('popups', 'Назад')}</div>
        </div>
        {/if}
    </div>
</div> -->

<div class="{popupData.Type}_main">
    <div class="{popupData.Type}_main_background" />
    <span class="{popupData.Type}_main_text">{popupData.Title}</span>
    <span class="{popupData.Type}_main_reason">ПРИЧИНА:</span>
    <span class="{popupData.Type}_main_reason_text">{popupData.Text}</span>
    {#if false}
        <div class="box-between">
            <div class="popup__button big {popupData.Type}">
                {statuses[currentStatus].button}
            </div>
            <div class="popup__button red">
                {translateText("popups", "Назад")}
            </div>
        </div>
    {/if}
</div>
