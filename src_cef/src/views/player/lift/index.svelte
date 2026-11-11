<script>
    import { executeClient } from "api/rage";

    import "./assets/sass/lift.sass";
    import "./main.css"
    export let viewData;

    let floors = viewData,
        floor = 1;

    const start = () => {
        executeClient("lift", "start", floor);
    };

    const stop = () => {
        executeClient("lift", "stop");
    };

    const up = () => {
        floor++;
        if (floor == floors.length) {
            floor = floors.length - 1;
        }
    };

    const down = () => {
        floor--;
        if (floor < 0) {
            floor = 0;
        }
    };
</script>

<!-- <div id="lift">
    <div class="module_lift">
        <h1>{floors[floor]}</h1>
        <div class="flex">
            <div class="button up">
                <div id="btn-down" class="btn arrow" on:click={down} />
                <div id="btn-stop" class="btn set" on:click={stop}>STOP</div>
            </div>
            <div class="button down">
                <div id="btn-up" class="btn arrow" on:click={up} />
                <div id="btn-start" class="btn set" on:click={start}>START</div>
            </div>
        </div>
    </div>
</div> -->

<link href="https://fonts.googleapis.com/css?family=Montserrat&display=swap" rel="stylesheet" />

<div class="lift_main">
    <div class="lift_main_background" />
    <div class="lift_main_stop" on:click={stop} />
    <div class="lift_main_start" on:click={start} />
    <div class="lift_main_poloska" />
    <div class="lift_main_buttons">
        <div class="lift_main_buttons_up" on:click={up} />
        <div class="lift_main_buttons_down" on:click={down} />
    </div>
    <span class="lift_main_text_floor">{floors[floor]}</span>
    <span class="lift_main_text_elevator">Лифт</span>
</div>
