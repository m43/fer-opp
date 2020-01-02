Vue.component("post-component", {
    props: ["post"],
    template:`
            <div>
                <h5>{{ post.title }}</h5>
                <p>{{ post.content }}</p>
            </div>
            `
});

var posts = new Vue({
    el: "#posts",
    data: {
        posts: []
    },
    methods: {
        getPosts: function () {
            axios.get("/Home/GetPosts")
                .then(response => {
                    this.posts = response.data;
                })
                .catch(error => {
                    console.log(error);
                });
        }
    },
    beforeMount: function () {
        this.getPosts();
    }
});