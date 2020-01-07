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
                    this.cartContent = response.data.shoppingCartArticle;
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
        addArticleToCart: function (article) {
            var thisVue = this;

            axios({
                method: 'post',
                url: '/Webshop/AddToShoppingCart',
                data: 'articleId=' + article.id + '&quantity=1&size=XL',
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(response => {
                thisVue.cartContent.push(article);
                thisVue.calculateTotalPrice();
            }).catch(error => {
                console.log(error);
            });

            //axios.post("/Webshop/AddToShoppingCart", {
            //    articleId: 4,
            //    quantity: 5,
            //    size: "XL"
            //})
            //    .then(response => {
            //        thisVue.cartContent.push(article);
            //        thisVue.calculateTotalPrice();
            //    })
            //    .catch(error => {
            //        console.log(error);
            //    });

            //this.cartContent.push(this.allArticles[this.currentIndex++]);
            //this.calculateTotalPrice();
        },
        removeArticleFromCart: function (article) {
            var thisVue = this;

            axios({
                method: 'delete',
                url: '/Webshop/RemoveFromShoppingCart',
                data: 'articleId=' + article.id + '&quantity=1&size=XL',
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(response => {
                thisVue.cartContent.splice(thisVue.cartContent.indexOf(article), 1);
                thisVue.calculateTotalPrice();
            }).catch(error => {
                console.log(error);
            });

            //axios.delete("/Webshop/RemoveFromShoppingCart", {
            //    data: {
            //        articleid: article.id,
            //        quantity: article.quantity,
            //        size: article.size
            //    }
            //})
            //    .then(response => {
            //        thisVue.cartContent.splice(thisVue.cartContent.indexOf(article), 1);
            //        thisVue.calculateTotalPrice();
            //    })
            //    .catch(error => {
            //        console.log(error);
            //    });

            //this.cartContent.splice(this.cartContent.indexOf(article), 1);
            //this.calculateTotalPrice();
        },
        calculateTotalPrice: function () {
            var sum = 0;
            for (var i = 0; i < this.cartContent.length; i++) {
                sum += this.cartContent[i].price * this.cartContent[i].quantity;
            }

            this.totalPrice = sum;
        }
    },
    beforeMount: function () {
        this.getArticles();
        this.getCurrentCart();
    }
});
