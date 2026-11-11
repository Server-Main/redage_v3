<script>
    import { translateText } from 'lang'
    import { accountIsSession } from 'store/account'
    import { executeClient } from 'api/rage'
    import InputCustom from 'components/input/inputrega.svelte'
    import { accountLogin } from 'store/account'
    import { onMount } from 'svelte'
    import "./mainauth.css";
    export let OnSelectViews;
    let loginInput;
    let loginIsFocus = false;
    let passwordInput;
    let passwordIsFocus = false;

    onMount (() => {
        accountLogin.subscribe((value) => {
            if (value == -99) return;
            if (value == -1 || value == "-1") loginIsFocus = true;
            else {
                passwordIsFocus = true;
                loginInput = value;
            }
        });
    });

    const onLogin = () => {
        if (loginInput && passwordInput && !$accountIsSession && loginInput.length && passwordInput.length && $accountLogin !== -99) 
            executeClient ("client:OnSignInv2", loginInput, passwordInput);
    }


    let language = "?";

    const onKeyUp = (event) => {

		const { keyCode } = event;
        if (keyCode === 13)
            onLogin ();
    }
</script>
<svelte:window on:keyup={onKeyUp} />
<!-- <div class="main">
    <div class="main__box">
        <div class="main__forms">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => loginInput = value} value={loginInput} isFocus={loginIsFocus} placeholder={translateText('player2', 'Логин или почта')} type="text" icon="auth-user"/>
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => passwordInput = value} value={passwordInput} isFocus={passwordIsFocus} placeholder={translateText('player2', 'Пароль')} type="password" icon="auth-lock"/>
            <div class="box-flex">
                <div class="main__button main_button_size_large" on:click={onLogin}>
                    <div class="main__button_left box-center">{translateText('player2', 'Войти')}</div>
                    <div class="main__button_right box-center">
                        <div class="main__button_square box-center">
                            <span class="auth-arrow"/>
                        </div>
                    </div>
                </div>
                <div class="main__button main_button_size_small box-center">
                    <div class="main__button_bottom box-center">
                        <div class="main__button_rectangle box-center">{language}</div>
                    </div>
                </div>
            </div>
            <div class="main__button main_button_size_large main__button_restore" on:click={onLogin}>
                <div class="main__button_left box-center none" on:click={() => OnSelectViews("Restore")}>{translateText('player2', 'Восстановление')}</div>
            </div>
        </div>
        <div class="main__scroll"/>
    </div>
</div> -->
<!-- <div class="passwordpole1">
    <InputCustom updateLang={(lang) => language = lang} setValue={(value) => loginInput = value} value={loginInput} isFocus={loginIsFocus} placeholder={translateText('player2', 'Логин или почта')} type="text" icon="auth-user"/>
    <InputCustom updateLang={(lang) => language = lang} setValue={(value) => passwordInput = value} value={passwordInput} isFocus={passwordIsFocus} placeholder={translateText('player2', 'Пароль')} type="password" icon="auth-lock"/>
</div> -->


<link href="https://fonts.googleapis.com/css?family=Uni+Neue-Trial&display=swap" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css?family=Montserrat&display=swap" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@100;200;300;400;500;600;700;800;900&display=swap" rel="stylesheet">
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@900&display=swap" rel="stylesheet">
<style>
    @font-face {
     font-family: Uni Neue-Trial;
     src: url(fonts/UniNeue-Trial.ttf);
    }
</style>
<div class="mainauth">
    <div class="backgroundrega"></div>
    <div class="inputpoleauth">
        <div class="authknp" on:click={onLogin}>
            <span class="authknptext">{translateText('player2', 'Войти')}</span>
        </div>
        <div class="loginpole1">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => loginInput = value} value={loginInput} isFocus={loginIsFocus} placeholder={translateText('player2', 'Логин или почта')} type="text"/>
        </div>
        <div class="passwordpole1">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => passwordInput = value} value={passwordInput} isFocus={passwordIsFocus} placeholder={translateText('player2', 'Пароль')} type="password"/>
        </div>
        <div class="login1">
            <div class="logininput1">
                <span class="logintext1">Логін</span>
            </div>
            <div class="ramka"></div>
            <div class="loginico"></div>
        </div>
        <div class="password1">
            <div class="passwordinput1">
                <span class="logintext1">Пароль</span>
            </div>
            <div class="ramka"></div>
            <div class="passwordico"></div>
        </div>
        <div class="regatext1">
            <span class="regaakk1">Авторизація</span>
            <span class="createpers">Увійдіть, і почніть творити історію на</span>
            <span class="infernoproject1">United GTA</span>
            <!-- <span class="avtorizuites">Если у вас нет аккаунта</span> -->
            <!-- <span class="esliest">ЗАРЕГЕСТРИРУЙТЕСЬ</span> -->
        </div>
    </div>
</div>



