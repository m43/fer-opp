Vue.component("player-component", {
    props: ["player"],
    template: `
            <div class="d-inline" style="margin-right: 10px; margin-left: 5px; margin-bottom: 13px; box-shadow: rgb(100, 96, 96) 2px 2px" >
                <img src="/images/rudes-logo.jpg" />
                <p class="playerInfo">{{ player.name }} {{ player.lastName }}</p>
                <p class="playerInfo" style="margin-bottom: 0;">{{ player.position }}</p>
            </div>
        `
});

var teams = new Vue({
    el: "#teams",
    data: {
        seniorPlayers: []
    },
    methods: {
        getPlayers: function () {
            axios.get("/Coach/GetPlayers")
                .then(response => {
                    this.seniorPlayers = response.data;
                })
                .catch(error => {
                    console.log(error);
                });
        }
    },
    beforeMount: function () {
        this.getPlayers();
    }
});