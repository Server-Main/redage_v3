<script>
    import { translateText } from 'lang'
    import { executeClient } from 'api/rage'
    import './css/main.sass'
    //import './fonts/style.css'
    import { ItemType, itemsInfo, ItemId } from 'json/itemsInfo.js'
    import { getPng } from './getPng.js' 


    let
        title = '',
        titleIcon = '',
        btn = '',
        elements = [],
        type = 1;

    window.smOpen = (_title, _titleIcon, _btn, _json, _type = 0) => {
        title = _title;
        titleIcon = _titleIcon;
        btn = _btn;
        elements = JSON.parse (_json);
        type = _type;
    }
    const onExit = () => {
        executeClient ('escape');
    }
    const configImages = [
        { name: 'Сим-карта', url: 'inventoryItems/items/sm-icon-sim.png' },
        { name: 'Рабочий топор', url: 'inventoryItems/items/244.png' },
        { name: 'Обычная кирка', url: 'inventoryItems/items/234.png' },
        { name: 'Усиленная кирка', url: 'inventoryItems/items/235.png' },
        { name: 'Профессиональная кирка', url: 'inventoryItems/items/236.png' },
        { name: 'Сумка', url: 'inventoryItems/clothes/male/bags/40_0.png' },
        { name: 'Сумка с дрелью', url: 'inventoryItems/items/15.png' },
        { name: 'Стяжки', url: 'inventoryItems/items/18.png' },
        { name: 'Мешок', url: 'inventoryItems/items/17.png' },
        { name: 'Бронежилет', url: 'inventoryItems/items/-9.png' },
        { name: 'Отмычка для замков', url: 'inventoryItems/items/11.png' },
        { name: 'Военная отмычка', url: 'inventoryItems/items/16-war.png' },
        { name: 'Радиоперехватчик', url: 'inventoryItems/items/279.png' },
        { name: 'QR-код', url: 'inventoryItems/items/270.png' },
        { name: 'Услуга по отмыву денег', url: 'inventoryItems/items/otmyv.png' },
        { name: 'Понизить розыск', url: 'inventoryItems/items/rozysk.png' },
        { name: 'Взломать наручники', url: 'inventoryItems/items/naruchniki.png' },
        { name: 'Мед. карта', url: 'inventoryItems/items/med.png' },
        { name: 'Лотерейный билет', url: 'inventoryItems/items/lottery.png' },
        { name: 'Лицензия на оружие', url: 'inventoryItems/items/litsenzia.png' },
        { name: 'Угон автотранспорта', url: 'inventoryItems/items/ugon.png' },
        { name: 'Перевозка автотранспорта', url: 'inventoryItems/items/perevozkaa.png' },
        { name: 'Перевозка оружия', url: 'inventoryItems/items/perevozkaw.png' },
        { name: 'Перевозка денег', url: 'inventoryItems/items/perevozkam.png' },
        { name: 'Перевозка трупов', url: 'inventoryItems/items/perevozkat.png' },
        { name: 'Сдать бронежилет', url: 'inventoryItems/items/-9.png' },
        { name: 'Дубинка', url: 'inventoryItems/items/181.png' },
        { name: 'Stun Gun', url: 'inventoryItems/items/109.png' },
        { name: 'Combat Pistol', url: 'inventoryItems/items/101.png' },
        { name: 'Heavy Pistol', url: 'inventoryItems/items/104.png' },
        { name: 'Pistol Mk2', url: 'inventoryItems/items/112.png' },
        { name: 'Pistol 50', url: 'inventoryItems/items/102.png' },
        { name: 'Ceramic Pistol', url: 'inventoryItems/items/151.png' },
        { name: 'Pump Shotgun Mk2', url: 'inventoryItems/items/149.png' },
        { name: 'Carbine Rifle Mk2', url: 'inventoryItems/items/133.png' },
        { name: 'Special Carbine', url: 'inventoryItems/items/129.png' },
        { name: 'Special Carbine Mk2', url: 'inventoryItems/items/134.png' },
        { name: 'SMG', url: 'inventoryItems/items/117.png' },
        { name: 'Bullpup Shotgun', url: 'inventoryItems/items/143.png' },
        { name: 'SawnOff Shotgun', url: 'inventoryItems/items/142.png' },
        { name: 'Heavy Shotgun', url: 'inventoryItems/items/146.png' },
        { name: 'Carbine Rifle', url: 'inventoryItems/items/127.png' },
        { name: 'Sniper Rifle', url: 'inventoryItems/items/136.png' },
        { name: 'Combat PDW', url: 'inventoryItems/items/119.png' },
        { name: 'Combat MG', url: 'inventoryItems/items/121.png' },
        { name: 'Combat MG Mk2', url: 'inventoryItems/items/125.png' },
        { name: 'Bullpup Rifle', url: 'inventoryItems/items/130.png' },
        { name: 'Аптечка', url: 'inventoryItems/items/1.png' },
        { name: 'Дробь', url: 'inventoryItems/items/204.png' },
        { name: 'Малый калибр', url: 'inventoryItems/items/201.png' },
        { name: 'Автоматный калибр', url: 'inventoryItems/items/202.png' },
        { name: 'Снайперский калибр', url: 'inventoryItems/items/203.png' },
        { name: 'Пистолетный калибр', url: 'inventoryItems/items/200.png' },
        { name: 'AP Pistol', url: 'inventoryItems/items/108.png' },
        { name: 'Assault SMG', url: 'inventoryItems/items/118.png' },
        { name: 'Sweeper Shotgun', url: 'inventoryItems/items/148.png' },
        { name: 'Assault Shotgun', url: 'inventoryItems/items/144.png' },
        { name: 'Advanced Rifle', url: 'inventoryItems/items/128.png' },
        { name: 'Bullpup Rifle Mk2', url: 'inventoryItems/items/135.png' },
        { name: 'Heavy Sniper', url: 'inventoryItems/items/137.png' },
        { name: 'Marksman Rifle Mk2', url: 'inventoryItems/items/140.png' },
        { name: 'Бейдж', url: 'inventoryItems/items/-7.png' },
        { name: 'Фейерверк обычный', url: 'inventoryItems/items/216.png' },
        { name: 'Фейерверк звезда', url: 'inventoryItems/items/217.png' },
        { name: 'Фейерверк взрывной', url: 'inventoryItems/items/218.png' },
        { name: 'Фейерверк фонтан', url: 'inventoryItems/items/219.png' },
        { name: 'Материалы', url: 'inventoryItems/items/13.png' },
        { name: 'Наркотики', url: 'inventoryItems/items/14.png' },
        { name: 'Эксклюзивный кейс', url: 'inventoryItems/items/281.png' },
        { name: 'Стул', url: 'inventoryItems/items/313.png' },
        { name: 'Конус', url: 'inventoryItems/items/371.png' },
        { name: 'Светящийся конус', url: 'inventoryItems/items/372.png' },
        { name: 'Отбойник', url: 'inventoryItems/items/373.png' },
        { name: 'Отбойник', url: 'inventoryItems/items/374.png' },
        { name: 'Перекрытие', url: 'inventoryItems/items/375.png' },
        { name: 'Знак STOP', url: 'inventoryItems/items/376.png' },
        { name: 'Знак НЕТ ПРОЕЗДА', url: 'inventoryItems/items/377.png' },
        { name: 'КПП', url: 'inventoryItems/items/378.png' },
        { name: 'Большой забор', url: 'inventoryItems/items/379.png' },
        { name: 'Маленький забор', url: 'inventoryItems/items/380.png' },
        { name: 'Ночной свет', url: 'inventoryItems/items/381.png' },
        { name: 'Камера видеонаблюдения', url: 'inventoryItems/items/382.png' },
        { name: 'Камера видеонаблюдения', url: 'inventoryItems/items/383.png' },


    ];

    const getOtherImageUrl = (name) => {
        let ind = configImages.findIndex(x => x.name === name);
        let url = document.cloud + configImages[ind].url;
        return url;
    }

    const getTypeName = (type) => {
        if(!type) return `Купить`;
        else if(type == 1) return `Взять`;
        else return `Сдать`;
    }

    const HandleKeyDown = (event) => {
        const { keyCode } = event;
        if (keyCode !== 27) return;
        executeClient ('escape');
        //onExit();
        //executeClient ('client.sm.exit');
    }
</script>
<svelte:window on:keyup={HandleKeyDown} />
<div id='shop'>
    <div class="box-ch">
        <div class="box-info">
            <div class="l">

                <div class="title"><span class="i-title {titleIcon}" />{title}</div>
            </div>
            <div class="button-box">
                <div class="btn red" on:click={onExit}><svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 30 30" fill="none">
                <path d="M6.18278 6.18281C6.51681 5.84888 6.9698 5.66129 7.44212 5.66129C7.91445 5.66129 8.36743 5.84888 8.70147 6.18281L15 12.4813L21.2985 6.18281C21.6344 5.85834 22.0844 5.6788 22.5514 5.68286C23.0184 5.68692 23.4652 5.87425 23.7955 6.20451C24.1257 6.53477 24.313 6.98153 24.3171 7.44857C24.3212 7.9156 24.1416 8.36555 23.8172 8.7015L17.5187 15L23.8172 21.2985C24.1416 21.6344 24.3212 22.0844 24.3171 22.5514C24.313 23.0185 24.1257 23.4652 23.7955 23.7955C23.4652 24.1257 23.0184 24.3131 22.5514 24.3171C22.0844 24.3212 21.6344 24.1417 21.2985 23.8172L15 17.5187L8.70147 23.8172C8.36552 24.1417 7.91557 24.3212 7.44854 24.3171C6.9815 24.3131 6.53474 24.1257 6.20448 23.7955C5.87422 23.4652 5.68689 23.0185 5.68283 22.5514C5.67877 22.0844 5.85831 21.6344 6.18278 21.2985L12.4813 15L6.18278 8.7015C5.84885 8.36747 5.66125 7.91448 5.66125 7.44216C5.66125 6.96983 5.84885 6.51684 6.18278 6.18281Z" fill="white"/>
                </svg></div>
            </div>
        </div>
        <div class="item-info">

            <ul class="items">
                {#each elements as value, index}
                <li id={value.id} key={index} class="block" on:click={() => executeClient ('client.sm.click', value.Id)}>
                    <div class="box">
                        <div class="name">{@html value.Name}</div>
                       <!--<div class="name">{@html getPng(value, itemsInfo[value.ItemId])}</div>
                          <span class="item-img {value.Icon}" />  
                         <img src="{getPng(value, itemsInfo[value.ItemId])}">
                         <span class="item-img" style="background-image: url({getPng(value, itemsInfo[value.ItemId])})" />-->

                         {#if value.ItemId == 0}
                            <div class="item-img"><img src="{getOtherImageUrl(value.Name)}"></div>
                            {:else}
                            <div class="item-img"><img src="{getPng(value, itemsInfo[value.ItemId])}"></div>
                        {/if}

                        {#if value.Price}
                            <div class="price">
                                {value.Price.replace(/[^\d]+/g,'')}
                                {value.Price.replace(/[0-9]+/,'')}  
                            </div>
                        {/if}
                    </div>
                    <div class="btn {btn}">{getTypeName(value.Name.match(/Сдать/g) ? 2 : type)}</div>
                </li>
                {/each}
            </ul>

        </div>
        
        
    </div>
</div>