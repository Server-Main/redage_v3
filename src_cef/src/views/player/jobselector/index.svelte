<script>
    import { translateText } from 'lang'
    import { executeClient } from 'api/rage'
    import { charLVL, charWorkID } from 'store/chars';

    import PadUnlock from './assets/images/padlock-unlock.svg';
    import PadLock from './assets/images/lock.svg';
    import './assets/css/main.css'
    import {hasJsonStructure} from "api/functions";

    import border from './assets/images/border.png';

    export let viewData;

    const imagePath = `/src/views/player/jobselector/assets/images/`;

    if (hasJsonStructure (viewData))
        viewData = JSON.parse(viewData);

    let list = [
            {class: "electro", name: translateText('player', 'Электрик'), level: 0, jobid: 1},
            {class: "kosilka", name: translateText('player', 'Газонокосильщик'), level: 0, jobid: 5},
            {class: "pochta", name: translateText('player', 'Почтальон'), level: 0, jobid: 2},
            {class: "taxi", name: translateText('player', 'Таксист'), level: 0, jobid: 3},
            {class: "bus", name: translateText('player', 'Водитель автобуса'), level: 0, jobid: 4},
            {class: "mechanic", name: translateText('player', 'Автомеханик'), level: 0, jobid: 8},
            {class: "truck", name: translateText('player', 'Дальнобойщик'), level: 1, jobid: 6},
            {class: "mechanic", name: translateText('player', 'Инкассатор'), level: 1, jobid: 7},
        ];

    const selectJob = (selectedjob) => {
        executeClient ("selectJob", selectedjob);
    }
    const closeJobMenu = () => {
        executeClient ("closeJobMenu");
    }
    
</script> 

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="main-wrapper1">
    <div class="header1">
        <p class="title1">{translateText('player', 'Устроиться на работу')}</p>
        <p class="description1">Привет! Я Эмма, и я помогу тебе найте работу. Не знаешь кто я? Я помогаю находить вакансии, даю советы и помогаю составить резюме.</p>
        <div class="close1" on:click={() => closeJobMenu()}>x</div>
    </div>
    <div class="job-list1">
        {#each list as forjob, index}
            <div key={index} class={"job-box1 " + forjob.class}>
                <p class="job-name1">{forjob.name}</p>
                <img src={`${imagePath}${forjob.class}.svg`} class={`job-logo1 ${forjob.class}`} alt="job-logo">
                <img src={border} class="border1" alt="border">
                {#if viewData[forjob.jobid] <= $charLVL}
                    <div class="job-name1" style="top: 12.7vh">
                        <img class="workimg1" src={PadUnlock} alt="PadUnlock"/>
                    </div>   
                {:else}
                    <div class="job-name1" style="top: 12.7vh">
                        <img class="workimg1" src={PadLock} alt="PadLock"/>
                    </div>
                {/if}
                 <div class="inkoslvl1" style="margin-top: 0.5vh;margin-left: 1vh;">
                    <p class="textlvl1">LVL {viewData[forjob.jobid]}</p>
                </div>
                {#if (viewData[forjob.jobid] <= $charLVL)}
                    {#if (forjob.jobid == $charWorkID)}
                        <div>
                            <div class="job-name1" style="top: 17.4vh;color: red" on:click={() => selectJob(-1)}>
                                <p сlass="buttontxtgetsettled1">{translateText('player', 'Уволиться')}</p>
                            </div>
                        </div>
                        {:else}
                        <div>
                            <div class="job-name1" style="top: 17.4vh;color: green" on:click={() => selectJob(forjob.jobid)}>
                                <div сlass="buttontxtgetsettled1">{translateText('player', 'Устроиться')}</div>
                            </div>
                        </div>
                    {/if}
                {/if}
            </div>
        {/each}
    </div>
</div>