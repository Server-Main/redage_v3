<script>
    import './main.sass'
    import './main.css'
    import { executeClient } from 'api/rage'
    import { fly } from 'svelte/transition';
    import { translateText } from 'lang'

    import background from './bg.svg';

    export let popupData;

    let title = popupData.title,
        text = popupData.text;

    const HandleKeyDown = (event) => {
        const { keyCode } = event;
        if (keyCode == 27)
            executeClient ('client:OnHospitalDialogCallback', 1)
    }
</script>


<!--<svelte:window on:keyup={HandleKeyDown} />-->
<!-- svelte-ignore a11y-missing-attribute -->
<div id="popup__death">
<!--     <div class="popup__death_timer">11:26</div> -->
    <div class="text1">ВЫ БЕЗ СОЗНАНИЯ</div>
    <div class="bg">
        <img src="{background}" alt="bg">
    </div>
    <div class="text3">{@html text}</div>
    <div class="popup__death__buttons">
        <div class="text4" in:fly={{ y: 50, duration: 500 }} on:click={() => executeClient ('client:OnHospitalDialogCallback', 2)}>Вызвать медиков</div>
        <div class="text5">+ 10 минут к ожиданию</div>
        <div class="text6" in:fly={{ y: 50, duration: 750 }} on:click={() => executeClient ('client:OnHospitalDialogCallback', 3)}>Умереть</div>
    </div>
</div> 