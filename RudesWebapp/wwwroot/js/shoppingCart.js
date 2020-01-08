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
                    this.shoppingCartArticles = response.data;
                    axios.get("/Webshop/GetArticles") // TODO it is not efficient to call getArticles each time, imagine transfering each time data about 1000 articles F.
                        .then(response => {
                            this.allArticles = response.data;
                            this.createCartContent();
                            this.calculateTotalPrice();
                        })
                        .catch(error => {
                            console.log(error);
                        });
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
                data: 'articleId=' + article.id + '&quantity=1&size=XL', // TODO ...
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(response => {
                thisVue.getCurrentCart();
                // // thisVue.cartContent.push(article);
                // for (var i = 0; i < thisVue.shoppingCartArticles.length; i++) {
                //     if (thisVue.shoppingCartArticles[i].articleId === response.data.articleId)
                //         thisVue.shoppingCartArticles.splice(i, 1);
                // }
                // thisVue.shoppingCartArticles.push(response.data);
                // thisVue.cartContent.push(article);
                // thisVue.calculateTotalPrice();
            }).catch(error => {
                console.log(error);
            });
        },
        removeArticleFromCart: function (article) {
            var thisVue = this;

            axios({
                method: 'delete',
                url: '/Webshop/RemoveFromShoppingCart',
                data: 'articleId=' + article.id + '&quantity=1&size=XL', // TODO ...
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(response => {
                thisVue.getCurrentCart();
                // // thisVue.cartContent.splice(thisVue.cartContent.indexOf(article), 1);
                // //console.log('remove - sc articles:', thisVue.shoppingCartArticles);
                // for (var j = 0; j < this.allArticles.length; j++) {
                //     if (this.allArticles[j].id === this.shoppingCartArticles[j].articleId) {
                //         thisVue.cartContent.splice(j, 1);
                //         thisVue.shoppingCartArticles.splice(j, 1);
                //         break;
                //     }
                // }
                // console.log('responsee:', response);
                // thisVue.calculateTotalPrice();
            }).catch(error => {
                console.log(error);
            });
        },
        calculateTotalPrice: function () {
            let sum = 0;
            for (let i = 0; i < this.shoppingCartArticles.length; i++) {
                let price = 0;
                for (let j = 0; j < this.allArticles.length; j++) {
                    if (this.allArticles[j].id === this.shoppingCartArticles[i].articleId) {
                        price = this.allArticles[j].price;
                        break;
                    }
                }

                let quantity = this.shoppingCartArticles[i].quantity;
                // console.log('price:', price);
                // console.log('quantity:', quantity);
                sum = price * quantity;
            }
            // console.log('sum:', sum);
            this.totalPrice = sum;
        },
        createCartContent() {
            this.cartContent = [];
            for (let i = 0; i < this.allArticles.length; i++) {
                for (let j = 0; j < this.shoppingCartArticles.length; j++) {
                    if (this.shoppingCartArticles[j].articleId === this.allArticles[i].id) {
                        let cartItem = {};
                        cartItem.article = this.allArticles[i];
                        cartItem.shoppingCartArticle = this.shoppingCartArticles[j];
                        this.cartContent.push(cartItem);
                    }
                }
            }
        }
    },
    beforeMount: function () {
        this.getArticles();
        this.getCurrentCart();
    }
});
