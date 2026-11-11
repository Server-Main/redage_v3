<script>
    import { inVehicle } from 'store/hud';

    let speed = 10;
    let maxSpeed = 100;
    let fuel = 0;

    import petrol from '../img/petrol-0.228c9ad11.svg';
    import petrol_icon from '../img/petrol-icon.68ae308b.svg';
    import petrol_228c9ad14 from '../img/petrol-0.228c9ad14.svg';
    import petrol_228c9ad110 from '../img/petrol-0.228c9ad110.svg';
    import petrol_228c9ad120 from '../img/petrol-0.228c9ad120.svg';
    import petrol_228c9ad12 from '../img/petrol-0.228c9ad12.svg';
    import petrol_228c9ad130 from '../img/petrol-0.228c9ad130.svg';
    import petrol_228c9ad13 from '../img/petrol-0.228c9ad13.svg';
    import petrol_228c9ad140 from '../img/petrol-0.228c9ad140.svg';

    import strapped from '../img/strapped.7de3ef6a0.svg';
    import strapped_7de3ef6a from '../img/strapped.7de3ef6a.svg';
    import lock from '../img/lock.78d295cf.svg';
    import lock_78d295cf0 from '../img/lock.78d295cf0.svg';
    import carburetor from '../img/carburetor.eeefd8880.svg';
    import carburetor_eeefd888 from '../img/carburetor.eeefd888.svg';

    let FuelProcent = 0;

    window.vehicleState = {}

    window.vehicleState.speed = (value) => {
        speed = value;
    }

    window.vehicleState.maxSpeed = (value) => {
        maxSpeed = value * 1.15;
    }

    window.vehicleState.fuel = (value) => {
        fuel = value;
        FuelProcent = value;
    }

    // Simple color interpolator for Speed.
    const InterpolateColor = (start, end, steps, count) => {
        var
            s = start,
            e = end,
            final = s + (((e - s) / steps) * count);
        return Math.floor(final);
    }

    const Color = (_r, _g, _b) => {
        return {
            r: _r,
            g: _g,
            b: _b
        };
    }

    const getSpeedColor = (value, max) => {
        let speed_percent = Math.floor(value / max * 100);
        if(speed_percent > 100) speed_percent = 100;

        let
            start = Color(255, 255, 255),
            end = Color(225, 228, 66);
    
        if (speed_percent > 50) {
            speed_percent = speed_percent % 51;
            start = Color(225, 228, 66);
            end = Color(228, 66, 66);
        }

        const start_colors = start;
        const end_colors = end;
        
        const 
            r = InterpolateColor(start_colors.r, end_colors.r, 50, speed_percent),
            g = InterpolateColor(start_colors.g, end_colors.g, 50, speed_percent),
            b = InterpolateColor(start_colors.b, end_colors.b, 50, speed_percent);

        return `rgb(${r}, ${g}, ${b})`;          
    }

    let autoPilot = false;
    window.vehicleState.autoPilot = (value) => autoPilot = value;

    let belt = false;
    window.vehicleState.belt = (value) => belt = value;

    let cruiseControl = false;
    window.vehicleState.cruiseControl = (value) => cruiseControl = value;

    let rightIL = false;
    window.vehicleState.rightIL = (value) => rightIL = value;

    let leftIL = false;
    window.vehicleState.leftIL = (value) => leftIL = value;

    let doors = false;
    window.vehicleState.doors = (value) => doors = value;

    let engine = false;
    window.vehicleState.engine = (value) => engine = value;

    let isToggledVehicleHud = true;
    window.vehicleState.isToggledVehicleHud = (value) => isToggledVehicleHud = value;
</script>

<!-- svelte-ignore empty-block -->
{#if $inVehicle && isToggledVehicleHud}
    <div id="safezone">
        <div id="root">
            <section class="hud" style="opacity: 1; display: block;">
                <div class="hud-speedometer">
                    <div class="petrol-wrap">
                        <div v-if="(fuel / maxfuel) >= (maxfuel - fuel * 1.3249)" class="petrol-level">
                            <img src={petrol_228c9ad110} width="24" height="24" alt="petrol">
                        </div>
                        <div v-else class="petrol-level">
                            <img src={petrol} width="24" height="24" alt="petrol">
                        </div>
                        <div v-if="(fuel / maxfuel) >= (maxfuel - fuel * 1.99)" class="petrol-level">
                            <img src={petrol_228c9ad120} width="24" height="24" alt="petrol">
                        </div>
                        <div v-else class="petrol-level">
                            <img src={petrol_228c9ad12} width="24" height="24" alt="petrol">
                        </div>
                        <div v-if="(fuel / maxfuel) >= (maxfuel - fuel * 3.959)" class="petrol-level">
                            <img src={petrol_228c9ad130} width="24" height="24" alt="petrol">
                        </div>
                        <div v-else class="petrol-level">
                            <img src={petrol_228c9ad13} width="24" height="24" alt="petrol">
                        </div>
                        <div v-if="fuel > 0" class="petrol-level">
                            <img src={petrol_228c9ad140} width="24" height="24" alt="petrol">
                        </div>
                        <div v-else class="petrol-level">
                            <img src={petrol_228c9ad14} width="24" height="24" alt="petrol">
                        </div>
                        <div class="icon-wrap">
                            <img src={petrol_icon} width="24" height="24" alt="petrol-icon">
                            <i>{fuel}%</i>
                        </div>
                    </div>
                    <div class="circle-speedometer-wrap">
                        <div class="circle-speedometer">
                            <div class="circle-wrap">
                                <div id="circlespeedometer">
                                    <svg viewBox="0 0 100 50">
                                    <path d="M 50,50 m -46,0 a 46,46 0 1 1 92,0" stroke="rgba(255, 255, 255, 0.2)"
                                        stroke-width="8" fill-opacity="0"></path>
                                        
                                    <path d="M 50,50 m -46,0 a 46,46 0 1 1 92,0" stroke="#ffffff" stroke-width="2"
                                        fill-opacity="0">
                                    </path>
                                </svg>
                                </div>
                            </div>
                            <div class="block-rotate">
                                <div class="in-speed-wrapper">
                                    <div class="in-speed-wrap">
                                        <div class="in-speed-opacity">
                                            {#if speed < 10}
                                                    <span style="color: {engine ? getSpeedColor (speed, maxSpeed) : 'rgba(255, 255, 255, 0.3)'};">00{speed}</span>
                                                {:else if speed < 100}
                                                    <span style="color: {engine ? getSpeedColor (speed, maxSpeed) : 'rgba(255, 255, 255, 0.3)'};">0{speed}</span>
                                                {:else}
                                                    <span style="color: {engine ? getSpeedColor (speed, maxSpeed) : 'rgba(255, 255, 255, 0.3)'};">{speed}</span>
                                            {/if}
                                        </div>
                                    </div>
                                    <p class="p-descr">км/ч</p>
                                </div>
                            </div>
                            <div class="other-value-wrap">
                                <div class="other-value-item">
                                    {#if belt}
                                        <img src={strapped} width="24" height="24" alt="strapped">
                                    {/if}
                                    {#if belt}
                                        {:else}
                                        <img src={strapped_7de3ef6a} width="24" height="24" alt="strapped_7de3ef6a">
                                    {/if}
                                    <p></p>
                                </div>
                                <div class="other-value-item">
                                    {#if lock}
                                        <img src={lock} alt="lock">
                                    {/if}
                                    {#if lock}
                                        {:else}
                                        <img src={lock_78d295cf0} alt="lock_78d295cf0">
                                    {/if}
                                    <p></p>
                                </div>
                                <div class="other-value-item">
                                    {#if engine}
                                        <img src={carburetor} width="24" height="24" alt="carburetor">
                                    {/if}
                                    {#if engine}
                                        {:else}
                                        <img src={carburetor_eeefd888} width="24" height="24" alt="carburetor_eeefd888">
                                    {/if}
                                    <p></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
{/if}