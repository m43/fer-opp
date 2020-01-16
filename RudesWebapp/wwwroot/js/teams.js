Vue.component("player-component", {
    props: ["player"],
    template: `
            <div class="d-inline" style="margin-right: 0px; margin-left: 0px; margin-bottom: 13px; padding: 0;">
                <img :src="player.image==null?'/images/rudes-logo.jpg':player.image.path+'?width=100'" />
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
        players: [],
        chunkSize: 4
    },
    methods: {
        getPlayers: function () {
            axios.get("/api/player")
                .then(response => {
                    this.players = response.data;
                })
                .catch(error => {
                    console.log(error);
                });
        },
        groupPlayersByType: function (type) {
            return _.chunk(this.players.filter(p => p.playerType == type), this.chunkSize);
        }
    },
    beforeMount: function () {
        this.getPlayers();
    },
    computed: {
        seniorPlayers() {
            return this.groupPlayersByType("Seniors");
        },
        juniorPlayers() {
            return this.groupPlayersByType("Juniors");
        },
        cadetPlayers() {
            return this.groupPlayersByType("Cadets");
        },
        youngCadetPlayers() {
            return this.groupPlayersByType("YoungCadets");
        },
        sportSchoolPlayers() {
            return this.groupPlayersByType("SportSchools");
        },
        miniBasketballPlayers() {
            return this.groupPlayersByType("MiniBasketball");
        }
    }
});
