<script>
    import { translateText } from 'lang'
    import InputCustom from 'components/input/inputrega.svelte'
    import { accountLogin } from 'store/account'
    import { executeClient } from 'api/rage'
    import "./mainregister.css";

    let regusername = "",
        regemail = "",
        regpass1 = "",
        regpass2 = "",
        regpromo = "";

    const onRegister = () => {
        if(
            regusername &&
            regemail &&
            regpass1 &&
            regpass2 && $accountLogin !== -99) {

            executeClient ('client:OnSignUpv2', regusername, regemail, regpromo, regpass1, regpass2);
        }
    }

    const onKeyUp = (event) => {        
        const { keyCode } = event;
        if (keyCode === 13)
            onRegister ();
    }
    let language = "?";

</script>
<link href="https://fonts.googleapis.com/css?family=Uni+Neue-Trial&display=swap" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css?family=Montserrat&display=swap" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@100;200;300;400;500;600;700;800;900&display=swap" rel="stylesheet">
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@900&display=swap" rel="stylesheet">
<svelte:window on:keyup={onKeyUp} />


<div id="auth">
    <div class="auth">
        <div class="auth-left" style="margin-top: -5vh;">
            <div class="auth-left-texted">
                <h1>РЕГИСТРАЦИЯ</h1>
                <h2>Зарегистрируйтесь, и начните творить чудеса на RAGECODE V3!</h2>
            </div>
            <div class="auth-left-inputs">
                <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regemail = value} placeholder="Почта" value={regemail}  type="email"/>
                <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regusername = value} placeholder="Логин" value={regusername}  type="text"/>
                <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpass1 = value} placeholder="Пароль" value={regpass1} type="password"/>
                <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpass2 = value} placeholder="Повторите пароль" value={regpass2} type="password"/>
                <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpromo = value} placeholder="Реферал" value={regpromo} type="text"/>
            </div>
            <div class="auth-left-buttons">
                <div class="auth-left-buttons-button" on:click={onRegister}>{translateText('player2', 'Создать')}</div>
            </div>
        </div>
    </div>
</div>