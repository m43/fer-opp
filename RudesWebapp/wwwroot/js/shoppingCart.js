var shoppingCartModal = new Vue({
    el: "#shoppingCartModal",
    data: {
        cartContent: [],
        shoppingCartArticles: [],
        allArticles: [],
        totalPrice: 0,
        currentIndex: 0
    },
    methods: {
        getCurrentCart: function () {
            axios.get("/Webshop/GetCurrentShoppingCartArticles")
                .then(response => {
                    // this.cartContent = response.data.shoppingCartArticle;
                    this.shoppingCartArticles = response.data;
                    // console.log('SC ART RESPONSE:', response);
                    // console.log('SC ARTICLES:', this.shoppingCartArticles);
                    // console.log('cartContent:', this.cartContent);
                    axios.get("/Webshop/GetArticles")
                        .then(response => {
                            // console.log('RESPONSE:', response.data)
                            this.allArticles = response.data;
                            for (var i = 0; i < this.allArticles.length; i++) {
                                if (this.shoppingCartArticles[0].articleId == this.allArticles[i].id)
                                    this.cartContent.push(this.allArticles[i]);
                            }
                        })
                        .catch(error => {
                            console.log(error);
                        });
                    // console.log('ALL ARTICLES:', this.allArticles);
                    // console.log('CART:', this.cartContent);
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
                console.log('response:', response.data);
                // thisVue.cartContent.push(article);
                for (var i = 0; i < thisVue.shoppingCartArticles.length; i++) {
                    if (thisVue.shoppingCartArticles[i].articleId == response.data.articleId)
                        thisVue.shoppingCartArticles.splice(i, 1);
                }
                thisVue.shoppingCartArticles.push(response.data);
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
                // thisVue.cartContent.splice(thisVue.cartContent.indexOf(article), 1);
                //console.log('remove - sc articles:', thisVue.shoppingCartArticles);
                for (var j = 0; j < this.allArticles.length; j++) {
                    if (this.allArticles[j].id == this.shoppingCartArticles[j].articleId) {
                        thisVue.cartContent.splice(j, 1);
                        thisVue.shoppingCartArticles.splice(j, 1);
                        break;
                    }
                }
                console.log('responsee:', response);
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
            //console.log('sc articles:', this.shoppingCartArticles);
            for (var i = 0; i < this.shoppingCartArticles.length; i++) {

                var price = 0;
                for (var j = 0; j < this.allArticles.length; j++) {
                    if (this.allArticles[j].id == this.shoppingCartArticles[i].articleId) {
                        price = this.allArticles[j].price;
                        break;
                    }
                }

                var quantity = this.shoppingCartArticles[i].quantity;

                console.log('price:', price);
                console.log('quantity:', quantity);
                //console.log('cart:', this.shoppingCartArticles[i])

                // sum += this.cartContent[i].price * this.cartContent[i].quantity;
                sum = price * quantity;
            }
            console.log('sum:', sum);
            this.totalPrice = sum;
        }
    },
    beforeMount: function () {
        this.getArticles();
        this.getCurrentCart();
    }
});
