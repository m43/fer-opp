var navbar = new Vue({
    el: "#navbar",
    data: {
        loggedIn: false,
        dummyUsername: "divGljiva"
    },
    methods: {
        switchLoginStatus: function () {
            this.loggedIn = !this.loggedIn;
        }
    }
});