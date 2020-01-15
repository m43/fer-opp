var shoppingCartModal = new Vue({
    el: "#shoppingCartModal",
    data: {
        allArticles: [],
        items: [],
        totalPrice: 0
    },
    methods: {
        updateCurrentCart: function () {
            axios.get("/Webshop/GetCurrentShoppingCartArticles")
                .then(response => {
                    this.items = response.data;
                    this.calculateTotalPrice();
                    finalPaymentModal.updateCurrentCart();
                })
                .catch(error => {
                    console.log(error);
                });
        },
        removeArticleFromCart: function (articleId, quantity, size) {
            axios({
                method: 'delete',
                url: '/Webshop/RemoveFromShoppingCart',
                data: 'articleId=' + articleId + '&quantity=' + quantity + '&size=' + size,
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(() => {
                this.updateCurrentCart();
                finalPaymentModal.updateCurrentCart();
            }).catch(error => {
                console.log(error);
            });
        },
        calculateTotalPrice: function () {
            let total = 0;
            this.items.forEach(item =>
                total += item.price * item.quantity * (100. - item.percentage) / 100
            );

            this.totalPrice = total;
        },
        clearShoppingCart: function () {
            axios.get("/Webshop/ClearShoppingCart")
                .then(response => {
                    this.updateCurrentCart();
                    finalPaymentModal.updateCurrentCart();
                })
                .catch(error => {
                    console.log(error);

                });
        }
    },
    beforeMount: function () {
        this.updateCurrentCart();
        // this.interval = setInterval(() => this.updateCurrentCart(), 10000);
    }
});

var finalPaymentModal = new Vue({
    el: "#finalPaymentModal",
    data: {
        allArticles: [],
        items: [],
        totalPrice: 0,
        address: "",
        city: "",
        postalCode: 0,
    },
    methods: {
        updateCurrentCart: function () {
            axios.get("/Webshop/GetCurrentShoppingCartArticles")
                .then(response => {
                    this.items = response.data;
                    this.calculateTotalPrice();
                })
                .catch(error => {
                    console.log(error);
                });
        },
        calculateTotalPrice: function () {
            let total = 0;
            this.items.forEach(item =>
                total += item.price * item.quantity * (100. - item.percentage) / 100
            );

            this.totalPrice = total;
        },
        getArticles: function () {
            axios.get("/Webshop/GetArticlesInStore")
                .then(response => {
                    this.allArticles = response.data;
                })
                .catch(error => {
                    console.log(error);
                });
        },
        addItemToCart: function (articleId, quantity, size) {
            axios({
                method: 'post',
                url: '/Webshop/AddToShoppingCart',
                data: 'articleId=' + articleId + '&quantity=' + quantity + '&size=' + size,
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(() => {
                paymentModal.updateCurrentCart();
            }).catch(error => {
                console.log(error);
            });
        },
        payment: function (address, city, postalCode, items) {
            console.log(address);
            console.log(city);
            console.log(postalCode);
            console.log(items);
            axios({
                method: 'post',
                url: '/Api/Order',
                data: {
                    address: address,
                    city: city,
                    postalCode: postalCode,
                    items: items
                },
                headers: {
                    'content-type':'application/json;charset=utf-8'
                }
            }).then(() => {  
                console.log("bravo");
            }).catch(error => {
                console.log("GLUPSI");
            });
        }
    },
    beforeMount: function () {
        this.getArticles();
    }
});