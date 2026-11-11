var Demorgan = new Vue({
    el: "#Demorgan",
    data: {
        Opened: false,

		demorganadmin: "Parlament Adminov",
		demorgantime: "200",
		demorganreason: "By Parlament",
    },
    methods: {
        Open() {
            this.Opened = true;
        },
        Close() {
            this.Opened = false;
        },
    }
});