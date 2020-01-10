var shoppingCartModal = new Vue({
    el: "#shoppingCartModal",
    data: function () {
        return {
            allArticles: [],
            totalPrice: 0,
            currentIndex: 0,
            items: []
        }
    },
    methods: {
        updateCurrentCart: function () {
            axios.get("/Webshop/GetItems")
                .then(response => {
                    this.items = response.data;
                    this.calculateTotalPrice();
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
        removeArticleFromCart: function (articleId, size, quantity = 1) {
            axios({
                method: 'delete',
                url: '/Webshop/RemoveFromShoppingCart',
                data: 'articleId=' + articleId + '&quantity=' + quantity + '&size=' + size,
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(() => {
                this.updateCurrentCart();
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
        }
    },
    beforeMount: function () {
        this.updateCurrentCart();
        // this.interval = setInterval(() => this.updateCurrentCart(), 10000);
    }
});