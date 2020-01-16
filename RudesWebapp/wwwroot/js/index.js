/*Vue.component("post-component", {
    props: ["post"],    
    template: `
            <div>
                <h5>{{ post.title }}</h5>
                <p>{{ post.content.substring(0, 400) }}... </p><br><br> <a asp-controller="Home" asp-action="Post"> Pročitaj više </a>
            </div>
            `
});*/

var index = new Vue({
    el: '#index',
    data: {
        posts: []
    },
    methods: {
        getPosts: function () {
            axios.get("/api/post")
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