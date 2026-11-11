
import axios from 'axios';
import { accountData } from 'store/account';

const code = 'UA-138889592-2';
const ec = 'ra_game';

let data = {};
accountData.subscribe(value => {
    data = value;
});


window.inAdvertisement = (event1, event2) => {

    try {
        const cid = Math.round (new Date().getTime() * Math.random());
        let url = `https://www.google-analytics.com/collect?v=1&tid=${code}&cid=${cid}&el=${data.Email}&t=event&ec=${ec}`;
        //console.log(url)
        url += `&ea=${event1}`
        if (event2 && event2 !== undefined && event2 !== "undefined")
            url += `&ev=${event2}`

        console.log(url)
        axios.get(url);
    } catch (err) {
        //return false;
    }
}