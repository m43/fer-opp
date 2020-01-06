var entirePost = new Vue({
    el: '#entirePost',
    data: {
        entirePost: {},
    },
    methods: {
        setPost: function (post) {
            this.entirePost = post;
        }
    }
});