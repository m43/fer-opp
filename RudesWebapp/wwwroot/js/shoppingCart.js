var shoppingCartModal = new Vue({
    el: "#shoppingCartModal",
    data: {
        cartContent: [],
        allArticles: [],
        currentIndex: 0
    },
    methods: {
        getCurrentCart: function () {
            axios.get("/Webshop/GetCurrentShoppingCart")
                .then(response => {
                    this.cartContent = response.data;
                })
                .catch(error => {
                    console.log(error);
                });
        },
        getArticles: function () {
            axios.get("/Webshop/GetArticles")
                .then(response => {
                    this.allArticles = response.data;
                })
                .catch(error => {
                    console.log(error);
                });
        },
        addArticleToCart: function () {
            this.cartContent.push(this.allArticles[this.currentIndex++]);
        }
    },
    beforeMount: function () {
        this.getArticles();
    }
});
