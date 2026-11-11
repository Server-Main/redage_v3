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
<!-- <div class="main1">
    <div class="main__box">
        <div class="main__forms">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regusername = value} value={regusername} placeholder={translateText('player2', 'Логин..')} type="text" icon="auth-user"/>
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regemail = value} value={regemail} placeholder={translateText('player2', 'Почта..')} type="email" icon="auth-mail"/>
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpass1 = value} value={regpass1} placeholder={translateText('player2', 'Пароль..')} type="password" icon="auth-lock"/>
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpass2 = value} value={regpass2} placeholder={translateText('player2', 'Повторите пароль..')} type="password" icon="auth-pass-repeat"/>
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpromo = value} value={regpromo} placeholder={translateText('player2', 'Промокод..')} type="text" icon="auth-gift"/>
            <div class="main__buttons_box main__buttons_registration">
                <InputCustom setValue={(value) => regpromo = value} value={regpromo} placeholder="Промокод.." type="text" icon="auth-gift"/>
                <div class="main__button main_button_size_small">
                    <div class="main__button_top box-center">
                        <span class="auth-lock"/>
                    </div>
                    <div class="main__button_bottom box-center">
                        <div class="main__button_rectangle box-center">ALT</div>
                    </div>
                </div>
            </div>
            <div class="box-flex">
                <div class="main__button main_button_size_large" on:click={onRegister}>
                    <div class="main__button_left box-center">{translateText('player2', 'Создать')}</div>
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
        </div>
        <div class="main__scroll"/>
    </div>
</div> -->
<style>
    @font-face {
     font-family: Uni Neue-Trial;
     src: url(fonts/UniNeue-Trial.ttf);
    }
</style>
<div class="mainregister">
    <div class="backgroundregister"></div>
    <div class="backgroundregister1"></div>
    <div class="inputpoleregister">
        <div class="registerknp" on:click={onRegister}>
            <span class="regknptext">{translateText('player2', 'Создать')}</span>
        </div>
        <div class="emailpole">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regemail = value} value={regemail} placeholder={translateText('player2', 'Почта..')} type="email"/>
        </div>
        <div class="loginpole">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regusername = value} value={regusername} placeholder={translateText('player2', 'Логин..')} type="text"/>
        </div>
        <div class="passwordpole">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpass1 = value} value={regpass1} placeholder={translateText('player2', 'Пароль..')} type="password"/>
        </div>
        <div class="repeatpole">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpass2 = value} value={regpass2} placeholder={translateText('player2', 'Повторите пароль..')} type="password"/>
        </div>
        <div class="promopole">
            <InputCustom updateLang={(lang) => language = lang} setValue={(value) => regpromo = value} value={regpromo} placeholder={translateText('player2', 'Промокод..')} type="text"/>
        </div>
        <div class="email">
            <div class="emailinput">
                <span class="emailtext">Пошта</span>
            </div>
            <div class="ramka"></div>
            <div class="emailico"></div>
        </div>
        <div class="login">
            <div class="logininput">
                <span class="emailtext">Логін</span>
            </div>
            <div class="ramka"></div>
            <div class="loginico"></div>
        </div>
        <div class="password">
            <div class="passwordinput">
                <span class="emailtext">Пароль</span>
            </div>
            <div class="ramka"></div>
            <div class="passwordico"></div>
        </div>
        <div class="repeat">
            <div class="repeatinput">
                <span class="emailtext">Повторіть пароль</span>
            </div>
            <div class="ramka"></div>
            <div class="passwordico"></div>
        </div>
        <div class="promo">
            <div class="promoinput">
                <span class="emailtext">Промо-Код</span>
            </div>
            <div class="ramka"></div>
            <div class="promoico"></div>
        </div>
        <div class="regatext">
            <span class="regaakk">Реєстрація Акаунта</span>
            <span class="createpers">Створіть персонажа і почніть свій шлях на</span>
            <span class="infernoproject">United GTA</span>
            <span class="avtorizuites">Если у вас есть аккаунт</span>
            <!-- <div class="esliest">АВТОРИЗУЙТЕСЬ</div> -->
        </div>
    </div>
</div>
