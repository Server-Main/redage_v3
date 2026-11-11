<script>
    import { translateText } from 'lang'
    import Header from '../header.svelte'
    import HomeButton from '../homebutton.svelte'
    import {currentPage} from '../../stores'
    let selectPage = "radio"
    import { radioState, radioStation } from "@/views/player/hudevo/phonenew/stores";

    import { storeSettings } from 'store/settings'
    import {executeClient} from "api/rage";

    let isToggled = $radioState;
	let selectedStation = $radioStation;
	let radioStationsUrls = 
	[
		"https://pub0301.101.ru:8443/stream/air/mp3/256/200", // Relax FM
		"https://chanson.hostingradio.ru:8041/chanson256.mp3", // Шансон
		"https://icecast-newradio.cdnvideo.ru/newradio3", // Новое Радио
		"http://radio.real-drift.ru:8000/phonk.ogg", // Фонк
		"https://retro.hostingradio.ru:8014/retro320.mp3", // Ретро ФМ
		"https://dfm.hostingradio.ru/dfm96.aacp"  // DFM
	];
	let radioStationsNames = 
	[
		"Relax FM",
		"Шансон",
		"Новое Радио",
		"Фонк",
		"Ретро ФМ",
		"DFM"
	];
	
	const enableStation = () => 
	{
		if (radioStationsUrls.length <= selectedStation) return;
        executeClient("sounds.play2DRadio", radioStationsUrls[selectedStation], SettingsList.VolumePhoneRadio / 100);
	}

    const updasteToggled = () => {
        isToggled = !isToggled;
        radioState.set(isToggled);

        if (!isToggled) 
		{
			executeClient("sounds.stop2DRadio");
			return;
		}
		enableStation();
    }
    let SettingsList = {};
    let DefaultSettingsList = "";
    storeSettings.subscribe((value) => {
        if (SettingsList !== value) {
            SettingsList = value;
            DefaultSettingsList = JSON.stringify(SettingsList);
        }
    });

    const changeVolume = (value) => {
        let volume = Math.round(SettingsList.VolumePhoneRadio / 10);

        volume += value;

        if (volume < 0) volume = 0;
        else if (volume > 10) volume = 10;

        SettingsList.VolumePhoneRadio = volume * 10;
        if (!isToggled) return;
		
		executeClient("sounds.volume2DRadio", SettingsList.VolumePhoneRadio / 100)
    };
	
	const setRadioStation = (value) => 
	{
		if (selectedStation == value || radioStationsUrls.length <= value) return;
        selectedStation = value;
		radioStation.set(value);
		selectPage = "radio";
		enableStation();
    };


    import { onDestroy } from 'svelte';

    onDestroy(() => {
        if (DefaultSettingsList !== JSON.stringify (SettingsList))
            executeClient("chatconfig", JSON.stringify (SettingsList));
    });

    import { fade } from 'svelte/transition'
</script>
<div class="newphone__radio" in:fade>
    <Header />
    <div class="newphone__radio_content">
        {#if selectPage == "radio"}
            <div class="box-flex">
                <div class="newphone__maps_headerimage radio"></div>
                <div class="newphone__radio_headertitle">Radio <span class="purple">FM</span></div>
            </div>
            <div class="box-between m-t33">
                <div class="newphone__project_title">{radioStationsNames[selectedStation]}</div>
                <div class="box-flex">
                    <div class="newphone__radio_live"></div>
                    <div class="red">On Air</div>
                </div>
            </div>
            <div class="newphone__radio_headphones"></div>
            <div class="box-between">
                <div>{translateText('player2', 'Состояние радио')}:</div>
                <div class="sound__input-block switch-box">
                    <label class="switch" on:click={updasteToggled}>
                        <input type="checkbox" checked={isToggled} disabled >
                        <span class="slider round"></span>
                    </label>
                </div>
            </div>
            <div class="box-between">
                <div>{translateText('player2', 'Громкость')}:</div>
                <div class="box-flex">
                    <div class="newphone__radio_button" on:click={() => changeVolume (-1)}>-</div>
                    <div class="width-12">{Math.round(SettingsList.VolumePhoneRadio / 10)}</div>
                    <div class="newphone__radio_button m-l" on:click={() => changeVolume (+1)}>+</div>
                </div>
            </div>
            <div class="newphone__project_button radio" on:click={()=> selectPage = "radioList"}>Сменить станцию</div>
        {/if}
        {#if selectPage == "radioList"}
            <div class="newphone__project_title" in:fade>Выберите волну</div>
            <div class="newphone__radio_list">
			
                <div class="newphone__project_categorie" on:click={() => setRadioStation (0)}>
                    <div>{radioStationsNames[0]}</div>
                    <div class="box-flex">
                        <div class="newphone__radio_live"></div>
                        <div class="red">{selectedStation == 0 ? "On Air" : ""}</div>
                    </div>
                    <div class="phoneicons-Button newphone__chevron-back" ></div>
                </div>
				
                <div class="newphone__project_categorie" on:click={() => setRadioStation (1)}>
                    <div>{radioStationsNames[1]}</div>
                    <div class="box-flex">
                        <div class="newphone__radio_live"></div>
                        <div class="red">{selectedStation == 1 ? "On Air" : ""}</div>
                    </div>
                    <div class="phoneicons-Button newphone__chevron-back" ></div>
                </div>
				
				<div class="newphone__project_categorie" on:click={() => setRadioStation (2)}>
                    <div>{radioStationsNames[2]}</div>
                    <div class="box-flex">
                        <div class="newphone__radio_live"></div>
                        <div class="red">{selectedStation == 2 ? "On Air" : ""}</div>
                    </div>
                    <div class="phoneicons-Button newphone__chevron-back" ></div>
                </div>
				
				<div class="newphone__project_categorie" on:click={() => setRadioStation (3)}>
                    <div>{radioStationsNames[3]}</div>
                    <div class="box-flex">
                        <div class="newphone__radio_live"></div>
                        <div class="red">{selectedStation == 3 ? "On Air" : ""}</div>
                    </div>
                    <div class="phoneicons-Button newphone__chevron-back" ></div>
                </div>

                <div class="newphone__project_categorie" on:click={() => setRadioStation (4)}>
                    <div>{radioStationsNames[4]}</div>
                    <div class="box-flex">
                        <div class="newphone__radio_live"></div>
                        <div class="red">{selectedStation == 4 ? "On Air" : ""}</div>
                    </div>
                    <div class="phoneicons-Button newphone__chevron-back" ></div>
                </div>
				
                <div class="newphone__project_categorie" on:click={() => setRadioStation (5)}>
                    <div>{radioStationsNames[5]}</div>
                    <div class="box-flex">
                        <div class="newphone__radio_live"></div>
                        <div class="red">{selectedStation == 5 ? "On Air" : ""}</div>
                    </div>
                    <div class="phoneicons-Button newphone__chevron-back" ></div>
                </div>

            </div>
            <div class="newphone__radio_back purple" on:click={()=> selectPage = "radio"}>{translateText('player2', 'Назад')}</div>
        {/if}
    </div>
    <HomeButton />
</div>