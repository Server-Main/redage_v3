<script>
    import './main.css'

    import { translateText } from 'lang'
    import { executeClient } from 'api/rage'
    import { fly } from 'svelte/transition';

    export let popupData;

    let title = popupData.title,
        elements = [];

    $: if (popupData.elements && typeof popupData.elements === "string")
        elements = JSON.parse(popupData.elements);

    let listId = 0;

    const HandleKeyDown = (event) => {
        const { keyCode } = event;
        switch (keyCode) {
            case 13:
                onSelected (elements[listId][1]);
                break;

            case 38: // up
                if(--listId < 0)
                    listId = elements.length - 1;
                break;
            case 40: // down
                if(++listId >= elements.length)
                    listId = 0;
                break;

            case 27:
                onSelected("null");
                break;
        }
    }

    const onSelected = (listItem) => {
        executeClient ('popup.list.selected', listItem)
    }
</script>

<link href="https://fonts.googleapis.com/css?family=Montserrat&display=swap" rel="stylesheet" />

<div class="clothesselect_main">
        <!-- <div class="clothesselect_main_background"></div> -->
        <div class="clothesselect_main_select">
            <div class="clothesselect_main_select_box"></div>
            <div class="clothesselect_main_select_upheader">
                <div class="clothesselect_main_select_upheader_box"></div>
                <div class="clothesselect_main_select_upheader_text">
                    <div class="clothesselect_main_select_upheader_text_icon"></div>
                    <span class="clothesselect_main_select_upheader_text_title">{title}</span>
                </div>
            </div>
            <div class="clothesselect_main_select_content">
                {#each elements as item, index}
                    <div class="clothesselect_main_select_content_content" on:click={() => onSelected (item[1])}>
                        <div class="clothesselect_main_select_content_content_line"></div>
                        <div class="clothesselect_main_select_content_content_block">
                            <div class="clothesselect_main_select_content_content_block_box"></div>
                            <div class="clothesselect_main_select_content_content_block_icon"></div>
                            <div class="clothesselect_main_select_content_content_block_text">{item [0]}</div>
                        </div>
                    </div>
                {/each}

                <div class="clothesselect_main_select_content_poloska">
                    <div class="clothesselect_main_select_content_poloska_main"></div>
                    <div class="clothesselect_main_select_content_poloska_selector"></div>
                </div>
            </div>
            <div class="clothesselect_main_select_help">
                <div class="clothesselect_main_select_help_background"></div>
                <span class="clothesselect_main_select_help_text">Используйте клавиатуру для перелистывания между категориями.
                    Клавиша: UP - вверх , DOWN - вниз</span>
            </div>
            <div class="clothesselect_main_select_button" on:click={() => onSelected("null")}>
                <div class="clothesselect_main_select_button_box"></div>
                <div class="clothesselect_main_select_button_text">Отмена</div>
            </div>
        </div>
    </div>