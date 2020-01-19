var shoppingCartModal = new Vue({
    el: "#shoppingCartModal",
    data: {
        items: [],
        totalPrice: 0
    },
    methods: {
        updateCurrentCart: function () {
            axios.get("/api/webshop/GetCurrentShoppingCartArticles")
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
                url: '/api/webshop/RemoveFromShoppingCart',
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
            axios.get("/api/webshop/ClearShoppingCart")
                .then(response => {
                    this.updateCurrentCart();
                    finalPaymentModal.updateCurrentCart();
                })
                .catch(error => {
                    console.log(error);

                });
        }
    },
    computed: {
        isCartEmpty: function () {
            return this.items.length == 0;
        }
    },
    beforeMount: function () {
        this.updateCurrentCart();
    }
});

var finalPaymentModal = new Vue({
    el: "#finalPaymentModal",
    data: {
        items: [],
        totalPrice: 0,
        address: "",
        city: "",
        postalCode: 0,
    },
    methods: {
        updateCurrentCart: function () {
            axios.get("/api/webshop/GetCurrentShoppingCartArticles")
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
        addItemToCart: function (articleId, quantity, size) {
            axios({
                method: 'post',
                url: '/api/webshop/AddToShoppingCart',
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
            axios({
                method: 'post',
                url: '/api/order',
                data: {
                    address: address,
                    city: city,
                    postalCode: postalCode,
                    items: items
                },
                headers: {
                    'content-type': 'application/json;charset=utf-8'
                }
            }).then(response => {
                if (response == null) {
                    toastr.error("Transakcija neuspješna!", "Transaction Info");
                } else if (response.status == 200) {
                    toastr.success("Transakcija uspješno izvršena!", "Transaction Info");
                } else if (response.status == 204) {
                    toastr.error("Transakcija neuspješna! Nema dovoljno artikala na skladištu!", "Transaction Info");
                }
            }).catch(error => {
                console.log(error);
            });
        }
    },
    computed: {
        isCartEmpty: function () {
            return this.items.length == 0;
        }
    },
    beforeMount: function () {
        this.updateCurrentCart();
    }
});