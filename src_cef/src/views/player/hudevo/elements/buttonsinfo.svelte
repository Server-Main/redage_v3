<script>
    import { charIsPet } from 'store/chars';
    import { isHelp } from 'store/hud';
    import keys from 'store/keys';
    import keysName from 'json/keys.js';
    
    import hot_voice from '../img/hot_voice.2d110bcc.svg';
    import hot_inter from '../img/hot_inter.4e156c63.svg';
    import hot_menu from '../img/hot_menu.e8d85bb6.svg';
    import hot_inv from '../img/hot_inv.35c7e83c.svg';
    import hot_cursor from '../img/hot_cursor.881afa88.svg';

    let HideHelp = false;
    window.hudStore.HideHelp = (value) => HideHelp = value;
</script>

{#if !$charIsPet && $isHelp}
    <div id="safezone" class:newhud__hide={HideHelp || !$isHelp}>
    </div>
{/if}