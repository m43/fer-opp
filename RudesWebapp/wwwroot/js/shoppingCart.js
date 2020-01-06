var shoppingCartModal = new Vue({
    el: "#shoppingCartModal",
    data: {
        cartContent: [],
        allArticles: [],
        totalPrice: 0,
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
            //axios.post("/Webshop/AddToShoppingCart", {
            //    articleId: 3,
            //    quantity: 5,
            //    size: "XL"
            //})
            //    .then(response => {
            //        cartContent.push(response.data);
            //    })
            //    .catch(error => {
            //        console.log(error);
            //    });

            this.cartContent.push(this.allArticles[this.currentIndex++]);
            this.calculateTotalPrice();
        },
        removeArticleFromCart: function (article) {
            //axios.delete("/Webshop/RemoveFromShoppingCart", {
            //    data: {
            //        articleId: article.id,
            //        quantity: article.quantity,
            //        size: article.size
            //    }
            //})
            //    .then(response => { })
            //    .catch(error => {
            //        console.log(error);
            //    });

            this.cartContent.splice(this.cartContent.indexOf(article), 1);
            this.calculateTotalPrice();
        },
        calculateTotalPrice: function () {
            var sum = 0;
            for (var i = 0; i < this.cartContent.length; i++) {
                sum += this.cartContent[i].price;
            }

            this.totalPrice = sum;
        }
    },
    beforeMount: function () {
        this.getArticles();
    }
});
