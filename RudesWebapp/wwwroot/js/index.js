Vue.component("post-component", {
    props: ["post"],
    template: `
            <div>
                <h5>{{ post.title }}</h5>
                <p>{{ post.content.substring(0, 400) }}... <br><br> Pročitajte više.</p>
            </div>
            `
});

var index = new Vue({
    el: '#index',
    data: {
        posts : []
    },
    methods: {
        getPosts: function () {
            axios.get("/Home/GetPosts")
                .then(response => {
                    this.posts = response.data;
                    console.log(this.posts);
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
