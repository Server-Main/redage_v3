<script>
    import { translateText } from 'lang'
    import { fly } from 'svelte/transition';
    import keys from 'store/keys'
    import keysName from 'json/keys.js'

    const InfoData = {
        Warehouse: translateText('player2', 'Открыть склад'),
        DriveSchool: translateText('player2', 'Открыть меню автошколы'),
        RentCar: translateText('player2', 'Открыть меню аренды'),
        Festive: translateText('player2', 'Открыть обмен Coin'),
        SaluteShop: translateText('player2', 'Открыть магазин фейерверков'),
        BlackMarket: translateText('player2', 'Открыть чёрный рынок'),
        TankRoyale: translateText('player2', 'Регистрация Tank Royale'),
        Casino: translateText('player2', 'двери в казино'),
        Atm: translateText('player2', 'Открыть меню банкомата'),
        Electrician: translateText('player2', 'Переодеться'),
        Repair: translateText('player2', 'Ремонтировать транспорт'),
        SeatingArrangements: translateText('player2', 'Взаимодействовать'),
        BuyPoints: translateText('player2', 'Купить алкоголь'),
        EnterGarage: translateText('player2', 'Войти в гараж'),
        ExitGarage: translateText('player2', 'Выйти из гаража'),
        EnterHouse: [
            {
                text: translateText('player2', 'Войти в дом'),
                key: 9
            },
            {
                text: translateText('player2', 'Открыть/закрыть дом'),
                key: 39
            }
        ],
        ExitHouse: translateText('player2', 'Выйти из дома'),
        HealkitHouse: translateText('player2', 'Использовать аптечку'),
        EnterHotel: translateText('player2', 'Войти в отель'),
        ExitHotel: translateText('player2', 'Выйти из отеля'),
        CarRentHotel: translateText('player2', 'Арендовать скутер'),
        FractionCityhallBeginWorkDay: translateText('player2', 'Раздевалка CityHall'),
        FractionCityhallGunMenu: translateText('player2', 'Оружейный склад CityHall'),
        JobGoPosta: translateText('player2', 'Начать работу почтальона'),
        JobGoPostaCar: translateText('player2', 'Взять рабочий транспорт'),
        TakeMoney: translateText('player2', 'Взять мешок с деньгами'),
        ChangeAutoNumber: translateText('player2', 'Открыть меню смены номеров'),
        ElectionPoint: translateText('player2', 'Открыть меню выборов'),
        Bear: translateText('player2', 'Нажмите чтобы поднять мишку'),

        BoatFix: translateText('player2', 'Нажмите чтобы починить лодку'),
            
        buyMetro: translateText('player2', 'Купить билет'),
        exitMetro: translateText('player2', 'Для выхода'),
        Seating: translateText('player2', 'Чтобы сесть'),
        SeatingUp: translateText('player2', 'Встать'),
    }

    let visible = false;
    window.hudEnter = (text) => {
        if (text === -1)
            visible = false;
        else
            visible = InfoData[text] ? InfoData[text] : translateText('player2', 'Нажмите для взаимодействия');
    }
</script>

{#if visible}
    {#if typeof visible == "string"}
        <div class="enter-e">
            <div class="enter-e-box">
                <svg class="svg" width="71" height="67" viewBox="0 0 71 67" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M28.7257 61.0276C28.306 62.2525 28.9573 63.5944 30.2092 63.9247C36.36 65.5476 42.8708 65.3337 48.9282 63.2815C55.7279 60.9779 61.5734 56.4877 65.5522 50.5118C69.5311 44.536 71.4192 37.4109 70.9219 30.2488C70.4247 23.0868 67.5699 16.291 62.8034 10.9223C58.0369 5.55371 51.6269 1.91442 44.5741 0.572653C37.5213 -0.769117 30.2227 0.262151 23.8178 3.50545C17.4129 6.74876 12.2621 12.0215 9.16968 18.5006C6.41479 24.2724 5.43148 30.7122 6.31467 37.0118C6.49443 38.294 7.74985 39.0996 9.01579 38.8279V38.8279C10.2817 38.5562 11.0773 37.31 10.9131 36.0257C10.2378 30.742 11.0925 25.3571 13.4012 20.5203C16.0475 14.976 20.4551 10.4639 25.936 7.68849C31.4169 4.91309 37.6625 4.03061 43.6978 5.1788C49.7331 6.32699 55.2183 9.44124 59.2971 14.0353C63.376 18.6294 65.8189 24.4448 66.2444 30.5736C66.67 36.7024 65.0542 42.7996 61.6494 47.9133C58.2446 53.027 53.2424 56.8694 47.4237 58.8407C42.3476 60.5604 36.8994 60.7716 31.7328 59.4754C30.477 59.1603 29.1453 59.8027 28.7257 61.0276V61.0276Z" fill="#112D95" fill-opacity="0.2"/>
                    <path d="M22 58.3333C22 58.9227 21.7659 59.4879 21.3491 59.9047C20.9324 60.3214 20.3671 60.5556 19.7778 60.5556H6.44444L2 65V47.2222C2 46.6329 2.23413 46.0676 2.65087 45.6509C3.06762 45.2341 3.63285 45 4.22222 45H19.7778C20.3671 45 20.9324 45.2341 21.3491 45.6509C21.7659 46.0676 22 46.6329 22 47.2222V58.3333Z" stroke="#375BDE" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
                <div class="t">E</div>
            </div>
            <div class="enter-e-text">{visible}</div>
        </div>
    {/if}
{/if}
<!-- {#if visible}
    {#if typeof visible == "string"}
        <div class="vzaem">
            <div>
                <svg class="svg" width="71" height="67" viewBox="0 0 71 67" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M28.7257 61.0276C28.306 62.2525 28.9573 63.5944 30.2092 63.9247C36.36 65.5476 42.8708 65.3337 48.9282 63.2815C55.7279 60.9779 61.5734 56.4877 65.5522 50.5118C69.5311 44.536 71.4192 37.4109 70.9219 30.2488C70.4247 23.0868 67.5699 16.291 62.8034 10.9223C58.0369 5.55371 51.6269 1.91442 44.5741 0.572653C37.5213 -0.769117 30.2227 0.262151 23.8178 3.50545C17.4129 6.74876 12.2621 12.0215 9.16968 18.5006C6.41479 24.2724 5.43148 30.7122 6.31467 37.0118C6.49443 38.294 7.74985 39.0996 9.01579 38.8279V38.8279C10.2817 38.5562 11.0773 37.31 10.9131 36.0257C10.2378 30.742 11.0925 25.3571 13.4012 20.5203C16.0475 14.976 20.4551 10.4639 25.936 7.68849C31.4169 4.91309 37.6625 4.03061 43.6978 5.1788C49.7331 6.32699 55.2183 9.44124 59.2971 14.0353C63.376 18.6294 65.8189 24.4448 66.2444 30.5736C66.67 36.7024 65.0542 42.7996 61.6494 47.9133C58.2446 53.027 53.2424 56.8694 47.4237 58.8407C42.3476 60.5604 36.8994 60.7716 31.7328 59.4754C30.477 59.1603 29.1453 59.8027 28.7257 61.0276V61.0276Z" fill="#D92948" fill-opacity="0.2"/>
                    <path d="M22 58.3333C22 58.9227 21.7659 59.4879 21.3491 59.9047C20.9324 60.3214 20.3671 60.5556 19.7778 60.5556H6.44444L2 65V47.2222C2 46.6329 2.23413 46.0676 2.65087 45.6509C3.06762 45.2341 3.63285 45 4.22222 45H19.7778C20.3671 45 20.9324 45.2341 21.3491 45.6509C21.7659 46.0676 22 46.6329 22 47.2222V58.3333Z" stroke="#FF284F" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
                <div class="t">{keysName[$keys[9]]}</div>
            </div>
            <div class="text">{visible}</div>
        </div>
    {:else} 
        <div class="vzaem">
            {#each visible as item}
                <div>
                    <svg class="svg" width="71" height="67" viewBox="0 0 71 67" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M28.7257 61.0276C28.306 62.2525 28.9573 63.5944 30.2092 63.9247C36.36 65.5476 42.8708 65.3337 48.9282 63.2815C55.7279 60.9779 61.5734 56.4877 65.5522 50.5118C69.5311 44.536 71.4192 37.4109 70.9219 30.2488C70.4247 23.0868 67.5699 16.291 62.8034 10.9223C58.0369 5.55371 51.6269 1.91442 44.5741 0.572653C37.5213 -0.769117 30.2227 0.262151 23.8178 3.50545C17.4129 6.74876 12.2621 12.0215 9.16968 18.5006C6.41479 24.2724 5.43148 30.7122 6.31467 37.0118C6.49443 38.294 7.74985 39.0996 9.01579 38.8279V38.8279C10.2817 38.5562 11.0773 37.31 10.9131 36.0257C10.2378 30.742 11.0925 25.3571 13.4012 20.5203C16.0475 14.976 20.4551 10.4639 25.936 7.68849C31.4169 4.91309 37.6625 4.03061 43.6978 5.1788C49.7331 6.32699 55.2183 9.44124 59.2971 14.0353C63.376 18.6294 65.8189 24.4448 66.2444 30.5736C66.67 36.7024 65.0542 42.7996 61.6494 47.9133C58.2446 53.027 53.2424 56.8694 47.4237 58.8407C42.3476 60.5604 36.8994 60.7716 31.7328 59.4754C30.477 59.1603 29.1453 59.8027 28.7257 61.0276V61.0276Z" fill="#D92948" fill-opacity="0.2"/>
                        <path d="M22 58.3333C22 58.9227 21.7659 59.4879 21.3491 59.9047C20.9324 60.3214 20.3671 60.5556 19.7778 60.5556H6.44444L2 65V47.2222C2 46.6329 2.23413 46.0676 2.65087 45.6509C3.06762 45.2341 3.63285 45 4.22222 45H19.7778C20.3671 45 20.9324 45.2341 21.3491 45.6509C21.7659 46.0676 22 46.6329 22 47.2222V58.3333Z" stroke="#FF284F" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                    <div class="t">{keysName[$keys[item.key]]}</div>
                </div>
                <div class="text">{item.text}</div>
            {/each}
        </div>
    {/if}
{/if} -->

<style>
.enter-e {
    position: absolute;
    left: 30%;
    top: 50%;
    transform: translate(-50%,-50%);

    display: flex;
    gap: 1.25vh;
    align-items: center;
}

.enter-e-box svg {
    height: 3.65vw;
    width: auto;
}

.t {
    position: absolute;
    top: 2.35vh;
    left: 3.1vh;


    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 700;
    font-size: 1.5vw;
    letter-spacing: 0.02em;
    color: #FFFFFF;
}

.enter-e-text {
    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 600;
    font-size: 0.85vw;
    line-height: 1.1vw;
    letter-spacing: 0.05em;
    color: #FFFFFF;
}
</style>