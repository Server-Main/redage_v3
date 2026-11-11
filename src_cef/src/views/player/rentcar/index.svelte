<script>
    import './assets/css/iconsarenda.css'
    import './assets/css/main.sass'
	import { fade } from 'svelte/transition';
    import { accountVip } from 'store/account'
    import { charMoney, charLVL } from 'store/chars'
    import rangeslider from 'components/rangeslider/index'
    import { format } from 'api/formatter'
    import { executeClient } from 'api/rage'
    export let viewData;

    // if (!viewData) viewData = '[{"Model":"M5cs","Price":1000,"Id":0,"IsJob":false},{"Model":"M5cs","Price":1000,"Id":0,"IsJob":false},{"Model":"M5cs","Price":1000,"Id":0,"IsJob":false},{"Model":"NIGHT M8","Price":1000,"Id":0,"IsJob":false},{"Model":"S73","Price":1000,"Id":0,"IsJob":false}]';
    if (!viewData) viewData = '[{"Model":"NIGHT M8","Price":1000,"Id":0,"IsJob":false},{"Model":"NIGHT M8","Price":1000,"Id":0,"IsJob":false},{"Model":"NIGHT M8","Price":1000,"Id":0,"IsJob":false},{"Model":"NIGHT M8","Price":1000,"Id":0,"IsJob":false},{"Model":"NIGHT M8","Price":1000,"Id":0,"IsJob":false},{"Model":"NIGHT M8","Price":1000,"Id":0,"IsJob":false}]';
    
    let HourValue = 0;

    const VehicleArray = JSON.parse (viewData);

    let SelectVehicle = -1;

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
    let colorId = 0;

    const setColor = (index) => {
        if (index === colorId) return;
        colorId = index;
    }

    const rangeslidercreate = () => {
        const max = 8;
        HourValue = 1;
        setTimeout(() => {
            rangeslider.create(document.getElementById("rangeslider"), {min: 1, max: max, value: 1, step: 1, onSlide: (value, percent, position) => {
                HourValue = Number(value);
            }});
        }, 0);
    }

    const onBuy = () => {
        if ($charMoney < GetRentCarCash (VehicleArray [SelectVehicle].Price * HourValue)) {            
            window.notificationAdd(1, 9, `У Вас не достаточно средств!`, 3000);
            return;
        }
        executeClient ('client.rentcar.buy', VehicleArray [SelectVehicle].Id, colorId, HourValue);
    }

    const onExit = () => {        
        executeClient ('client.rentcar.exit');
    }

    const GetRentCarCashToLevel = (Price) => {
        const level = $charLVL;

        if (level <= 2) Price = Math.round(Price * 1.0);
        else if (level <= 4) Price = Math.round(Price * 1.5);
        else if (level <= 6) Price = Math.round(Price * 2.0);
        else if (level <= 9) Price = Math.round(Price * 4.5);
        else if (level <= 19) Price = Math.round(Price * 6.0);
        else Price = Math.round(Price * 8.0);
        return Price;
    }

    const GetRentCarCash = (Price) => {

        switch ($accountVip)
        {
            case 1:
                Price = Math.round(Price * 0.95);
                break;
            case 2:
                Price = Math.round(Price * 0.9);
                break;
            case 3:
                Price = Math.round(Price * 0.85);
                break;
            case 4:
            case 5:
                Price = Math.round(Price * 0.8);
                break;
        }
        return GetRentCarCashToLevel (Price);
    }

    const handleKeyDown = (event) => {
        const { keyCode } = event;
        if (keyCode !== 27) return;

        if (SelectVehicle !== -1) SelectVehicle = -1;
        else onExit ();
    }

</script>

<svelte:window on:keyup={handleKeyDown}/>

<div id="arenda-page">
    <div class="newrentstories" class:nonactive={SelectVehicle !== -1}>
        <div class="rent-bg-hosty"></div>
        <header>
            <h1>АРЕНДА</h1>
            <h2>Необходимо транспортное средство для перемещения? По всему штату с помощью GPS можно найти похожие места, которые предоставляют разный транспорт в аренду: от лодок и мотоциклов до элитных автомобилей в любых цветах! Управлять арендой можно через телефон, при выхода со штата аренда не сбросится, если у Вас ещё осталось оплаченное время.</h2>
        </header>
        <div class="rent_list">
            <div class="rent_boxes">
                {#each VehicleArray as item, index}
                    <div class="rent_box">
                        <div class="rent_box_up">
                            <div class="rent_box_up-text">
                                <a>{item.Model}</a>
                                <p>{format("money", GetRentCarCash (item.Price))} / час</p>
                            </div>
                            <div class="img-main-rent">
                                <div style="background-image: url({document.cloud}inventoryItems/vehicle/{item.Model.toLowerCase()}.png)"></div>
                            </div>
                        </div>
                        <div class="rent_box_un" on:click={() => SelectVehicle = index}>Арендовать</div>
                    </div>
                {/each}
            </div>
        </div>
        <div class="rent-exit" on:click={onExit}>
            <a>ESC</a>
            <p>Закрыть</p>
        </div>
    </div>
    {#if SelectVehicle !== -1}
    <div class="props-arenda">
        <div class="rent-bg-host222y"></div>
        <div class="arenda-customize" transition:fade={{duration: 200}}>
            <div style="color: #4E4E4E;margin-right: 10.3vw;letter-spacing: .3vh;" class="arenda-customize__title">АРЕНДА</div>
            <div style="margin-right: 6.6vw;text-transform: uppercase;margin-top: -.6vw;width: 21vh;" class="arenda-customize__subtitle">{VehicleArray [SelectVehicle].Model}</div>
            <divs class="arenda-customize__img" style="background-image: url({document.cloud}inventoryItems/vehicle/{VehicleArray [SelectVehicle].Model.toLowerCase()}.png)" />
            {#if !VehicleArray [SelectVehicle].IsJob}
            <div class="arenda-customize__rangeslider">
                <input type="range" id="rangeslider" use:rangeslidercreate />
            </div>
            <div class="arenda-customize__input-description">
                <div class="arenda-customize_gray">1</div>
                <div class="arenda-customize_gray">8</div>
            </div>
            {/if}
            <div class="adrenda-customize__radio-buttons">
                {#each authColors as value, index}
                    <i key={index} class="color" class:active={colorId === index} on:click={() => setColor (index)} style="background: {value}" />
                {/each}
            </div>
            <div class="arenda-customize__button" on:click={onBuy}>
                К оплате
                <div class="arenda-customize__money">${format("money", GetRentCarCash (VehicleArray [SelectVehicle].Price * HourValue))}</div>
            </div>
            <div class="arenda-customize__exit" on:click={() => SelectVehicle = -1}>Закрыть</div>
        </div>
    </div>
    {/if}
</div>


<style>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;200;300;400;500;600;700;800;900&display=swap');

body {
    font-family: Montserrat;
}
.arenda-customize__title {
    font-family: Montserrat;
    font-weight: 400;
}
.arenda-customize__subtitle {
    font-family: Montserrat;
}
.arenda-customize__button {
    font-family: Montserrat;
}   
.arenda-customize_gray {
    font-family: Montserrat;
}
.arenda-customize__exit {
    font-family: Montserrat;
}

::-webkit-scrollbar {
    width: 4px;
    display: block;
    background-color: #2c2c2c;
    border-radius: 2px;
    direction: rtl;
}
::-webkit-scrollbar-track {
    background-color: #ffffff;
    border-radius: 10px;
    direction: rtl;
    background: #ffffff;
}
::-webkit-scrollbar-thumb {
    background-color: #ffffff;
    border-radius: 2px;
    direction: rtl;
    background: #ffffff;
}



.rent_box {
    display: grid;
    gap: 8.4vh;
    width: 40.6481vh;
    height: 9.6296vh;
    background: rgba(83, 83, 83, 0.356);
    border-radius: .7vh;
}

.rent_box_up {
    display: flex;
    align-items: center;
    display: flex;
    justify-content: flex-start;
}

.rent_box_up-text {
    position: relative;
    display: flex;
    top: 3.6vh;
    gap: .7vh;
    align-items: center;
    padding-left: 1vh;
}

.rent_box_up-text a {
    font-weight: 600;
    font-size: 2.8vh;
    color: #fff;
}

.rent_box_up-text p {
    font-weight: 600;
    font-size: 1.3vh;
    color: #DFFD2E;
}

.rent_box_un {
    height: 5.8333vh;
    width: 40.6481vh;

    border-radius: .7vh;

    background: #02CEF6;

    display: flex;
    align-items: center;
    justify-content: center;

    font-weight: 600;
    font-size: 2.6vh;
    color: #fff;
    letter-spacing: .3vh;

    transition: .33s all;
}

.rent_box_un:hover {
    opacity: .8;
}

.rent_list {
    position: absolute;
    top: 25.6481vh;
    direction: rtl;
    left: 3vh;
    width: 44.1667vh;
    height: 54.6vh;
    overflow: auto;
    overflow-y: auto;
    display: grid;
    gap: 5vh;
    flex-wrap: wrap;
    justify-content: space-between;
}

.rent_boxes {
    position: relative;
    display: grid;
    direction: ltr;
    gap: 10vh;
}


.rent-bg-hosty {
    width: 100%;
    height: 100%;
    position: absolute;
    top: 0;
    left: 0;
    z-index: -1;
    background: linear-gradient(90deg, #000 -1.13%, rgba(16, 16, 16, 0.329) 40.42%);
}

h1,h2,a {
    margin: 0;
}

header {
    position: absolute;
    display: grid;
    top: 4vh;
    gap: .5vh;
    left: 3vh;
}

header h1 {
    font-weight: 600;
    font-size: 4.5vh;
    letter-spacing: .4vh;
    color: #fff;
}

header h2 {
    font-weight: 500;
    font-size: 1.45vh;
    color: rgb(175, 175, 175);
    letter-spacing: .3vh;
    max-width: 60vh;
}

.img-main-rent {
    position: absolute;
    right: 1vh;
    top: 1vh;
    width: 8vh;
    height: 8vh;
    background-image: url(./assets/images/car.png);
    background-size: contain;
    background-position: center;
    background-repeat: no-repeat;
}



.rent-bg-host222y {
    width: 100%;
    height: 100%;
    position: absolute;
    top: 0;
    left: 0;
    background-color: rgba(0, 0, 0, 0.61);
    backdrop-filter: blur(10px);
    -webkit-backdrop-filter: blur(10px);
}

.rent-exit {
    position: absolute;
    bottom: 3vh;
    left: 3vh;
    width: 10vh;
    background: #232424;

    width: 15.9259vh;
    height: 4.537vh;

    display: flex;
    align-items: center;

    border-radius: 1vh;
    color: #fff;
    gap: 3vh;
}

.rent-exit a {
    position: relative;
    left: 1.3vh;
    font-weight: 600;
}

.rent-exit p {
    height: 100%;
    width: 9.8148vh;
    background: #353535;
    border-radius: 1vh;
    margin-right: 0;
    
    display: flex;
    align-items: center;
    justify-content: center;

    font-weight: 600;
}
</style>