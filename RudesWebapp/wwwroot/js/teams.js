Vue.component("player-component", {
    props: ["player"],
    template: `
            <div class="d-inline" style="margin-right: 0px; margin-left: 0px; margin-bottom: 13px; padding: 0;">
                <img src="/images/rudes-logo.jpg" />
                <div class="tempDiv">
                    <p class="playerInfo">{{ player.name }} {{ player.lastName }}</p>
                </div>
                <div class="tempDiv">
                    <p class="playerInfo" style="margin-bottom: 10px;">{{ player.position }}</p>
                </div>
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
    },
    computed: {
        groupedPlayers() {
            return _.chunk(this.seniorPlayers, 4)
            // returns a nested array: 
            // [[article, article, article], [article, article, article], ...]
        }
    }
});
