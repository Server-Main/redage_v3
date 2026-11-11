<script>
    import { translateText } from 'lang'
    import { fly } from 'svelte/transition';
    export let SafeSone;

    let
        isCase = false,
        caseId = -1;

    const caseData = [
        { color: "green", image: "250", hour: translateText ('player2', '3 часа ')},
        { color: "yellow", image: "251", hour: translateText ('player2', '5 часов') },
        { color: "red", image: "252", hour: translateText ('player2', '8 часов')},
    ]

    window.updateCase = (_caseId) => {
        if (caseId === _caseId) return;
        isCase = true;
        caseId = _caseId;

        setTimeout(() => {
            isCase = false;
        }, 1000 * 30)
    }
</script>
{#if isCase}
{/if}