<script>
    import { format } from 'api/formatter'

    import bonus from '../img/bonus.svg';

    let cashValue = 0;

    window.PayDay = async (cash) => {
        cashValue = cash;
        if (cashValue > 0) {
            window.hudStore.HideHelp (true);            
            await window.wait(5000);
            window.hudStore.HideHelp (false);
            cashValue = 0;
        } else {
            window.hudStore.HideHelp (false);
        }
    }

</script>

{#if cashValue > 0}
    <div class="bonus">
        <img class="bonus_img" src={bonus} alt="money">
        <a class="money">+${format("money", cashValue)}</a>
    </div>
{/if}