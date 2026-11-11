<script>
    export let OnSelectViews;

    import { executeClient } from 'api/rage'
    import InputCustom from 'components/input/inputrega.svelte'
    import { accountLogin } from 'store/account'
    import { onMount } from 'svelte'

    const onKeyUp = (event) => {

		const { keyCode } = event;
        if (keyCode === 13)
            onSubmitRestore ();
    }

    let restoretype = false,
        placeholder = "Логин",
        restorelength = 50,
        restoreInput = "";

	const SetStep = (step) => {        
        if(step == 0) {
			restoretype = false;
			placeholder = "Логин";
			restorelength = 50;
			restoreInput = "";
		} else if(step == 1) {
			restoretype = true;
            restorelength = 6;
            placeholder = "Код из письма";
			restoreInput = "";
		} else if(step == 2) {
            
        }
		
    }
    window.events.addEvent("cef.authentication.restoreStep", SetStep);
	
	import { onDestroy } from 'svelte';

    onDestroy(() => {
        window.events.removeEvent("cef.authentication.restoreStep", SetStep);
    });
	
    const onSubmitRestore = event => {
        if(restoreInput) {
            if (restoretype == false) {
                if (restoreInput.length != 0) {
					executeClient ('restorepass', 0, restoreInput);
					restorelength = 0;
					placeholder = "Отправка сообщения...";
					restoreInput = "";
                }
            } else {
                if (restoreInput.length == 6) {
                    executeClient ('restorepass', 1, restoreInput);
					restorelength = 0;
					placeholder = "Происходит авторизация";
					restoreInput = "";
                } else restoreInput = "";
            }
            return false;
        }
    }

</script>
<svelte:window on:keyup={onKeyUp} />

<style>
    @import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200&display=swap');
</style>

<div id="auth">
    <div class="auth">
        <div class="auth-left" style="margin-top: 4vh;">
            <div class="auth-left-texted">
                <h1 style="font-size: 4.4vh;">Восстановление Пароля</h1>
                <h2>Восстановите, и начните творить чудеса на RAGECODE V3!</h2>
            </div>
            <div class="auth-left-inputs">
                <InputCustom setValue={(value) => restoreInput = value} placeholder="Email" value={restoreInput} type="text"/>
            </div>
            <div class="auth-left-buttons">
                <div class="auth-left-buttons-button" on:click={onSubmitRestore}>Восстановить</div>
                <div class="auth-left-buttons-button" on:click={() => OnSelectViews("Authentication")}>Назад</div>
            </div>
        </div>
    </div>
</div>

