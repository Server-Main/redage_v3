
<script>
    import { executeClient } from 'api/rage'
    import { translateText } from 'lang'
    import { format } from 'api/formatter'
    import './css/main.sass';
    import './fonts/style.css';
    import './css/new.css';
    import authInfo from './authInfo';
    const authColors =[
        "#000",
        "#fff",
        "#e60000",
        "#ff7300",
        "#f0f000",
        "#00e600",
        "#00cdff",
        "#0000e6",
        "#be3ca5",
    ];
    let list = [];
    let select = 0;
    let selectTime = -1;
    let sordId = -1;
    let colorId = 0;

    let isDonateAutoroom = false;

    window.authShop = {
        data: (value, isDonate = false) => {
            let returnList = [];
            let modelInfo;
            JSON.parse(value).forEach(value => {
                modelInfo = authInfo [value.modelName] || false;
                
                returnList = [
                    ...returnList, {
                        ...value,
                        speed: (!modelInfo || (modelInfo && !modelInfo.maxSpeed)) ? value.speed : modelInfo.maxSpeed,
                        boost: (!modelInfo || (modelInfo && !modelInfo.acceleration)) ? value.boost : modelInfo.acceleration,
                        seat: (!modelInfo || (modelInfo && !modelInfo.seats)) ? value.seat : modelInfo.seats,
                        invslots: (value.invslots !== undefined) ? value.invslots : 25,
                        desc: !modelInfo ? false : modelInfo.desc,
                    }
                ];
            });
            list = returnList;
            isDonateAutoroom = isDonate;
            return;
        }
    }


    const sort = (value) => {
        if (sordId === value) return;
        let sortList = list;
        let sortSelect = -1;
        if (select !== -1 && list[select]) sortSelect = list[select].index;
        sordId = value;
        sortList.sort(( a, b ) =>  b[value] - a[value]);
        if (sortSelect !== -1) {
            sortList.forEach((value, index) => {
                if (sortSelect === value.index) {
                    select = index;
                }
            });
        }
        list = sortList;
    }


    const setItem = (index) => {
        if (!list[index]) return;
        else if (index === select) return;
        //else if (selectTime > new Date().getTime()) return;
        //selectTime = new Date().getTime() + 1000;
        select = index;
        executeClient ('auto', 'model', list[index].index);
    }

    const setColor = (index) => {
        if (index === colorId) return;
        colorId = index;
        executeClient ('auto', 'color', index);
    }

    const startTestDrive = (type) => {
        if (select === -1 || !list[select]) return;
        executeClient ('testDrive', type);
    }

    const HandleKeyDown = (event) => {
        const { keyCode } = event;
        if (keyCode !== 27) return;

        executeClient ('closeAuto');
    }
</script>

<svelte:window on:keyup={HandleKeyDown} />
<div id="autoshop">

    <div class="colors-menu">
        <a>Выберите цвет</a>
        <div class="colors-menu__item white"></div>
        <div class="colors-menu__item purple"></div>
        <div class="colors-menu__item purp"></div>
        <div class="colors-menu__item red"></div>
        <div class="colors-menu__item yellow"></div>
        <div class="colors-menu__item green"></div>
      </div>
      <div class="selectnameauto1">
			<a>Гос. цена</a>
			$ 451.250$
		</div>
    <div class="selectnameauto228">
        Дилерский цвет можно подобрать приммо в автосалон
    </div>
    {#if (select !== -1 && list [select])}
        <div on:mouseenter={() => executeClient ("client.camera.toggled", false)} on:mouseleave={() => executeClient ("client.camera.toggled", true)}>
            <div class="selectnameauto">
                <a style="color: #fff;">МОДЕЛЬ</a>
                {list [select].modelName}
            </div>
        </div>
    {/if}

    <div class="box-content">
        <div on:mouseenter={() => executeClient ("client.camera.toggled", false)} on:mouseleave={() => executeClient ("client.camera.toggled", true)}>
            <div class="list228">
                {#each list as value, index}
                    <div class="block228" class:active={select === index} on:click={() => setItem (index)}>
                        <a>{@html value.modelName}</a>
                        <b>{format("money", value.price)}{isDonateAutoroom === true ? 'RB' : '$'}</b>
                    </div>
                {/each}
            </div>
        </div>
    </div>
    <div class="testdrive" on:click={() => startTestDrive (2)}>{translateText('business', 'Тест-драйв FT')} (300$)</div>

    {#if (select !== -1 && list [select])}
    <div class="buy228" on:click={() => executeClient ('buyAuto', 1)}>{translateText('business', 'Купить за')} {isDonateAutoroom === true ? `${format("money", list [select].price)}RB` : `$${format("money", list [select].price)}`}</div>
    <div class="falily123" on:click={() => executeClient ('buyAuto', 2)}>{translateText('business', 'Купить для семьи за')} {isDonateAutoroom === true ? `${format("money", list [select].price)}RB` : `$${format("money", list [select].gosPrice)}`}</div>
    {/if}

    <div class="namesalon">АВТОСАЛОН</div>
    <div class="namesalon2">ПРЕМИАЛЬНЫЙ</div>
    <div class="namesalon3">Добро пожаловать в автосалон, примсотрите себе новенькое авто.</div>
    
    <div class="box-KeyInfo relative" on:mouseenter={() => executeClient ("client.camera.toggled", false)} on:mouseleave={() => executeClient ("client.camera.toggled", true)}>



        <div on:click={() => executeClient ('closeAuto')} class="masks__exit228">
			{translateText('business', 'Выйти')}
		</div>
    </div>
</div>