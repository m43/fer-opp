var articleDetailsModal = new Vue({
    el: "#articleDetailsModal",
    data: {
        selectedArticle: {},
        probneDostupneVelicine: [3, 0, 5, 0, 7],
        quantity: 0,
    },
    methods: {
        addItemToCart: function (articleId, quantity, size) {
            console.log(articleId);
            console.log(quantity);
            console.log(size);
            axios({
                method: 'post',
                url: '/api/webshop/AddToShoppingCart',
                data: 'articleId=' + articleId + '&quantity=' + quantity + '&size=' + size,
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(() => {
                shoppingCartModal.updateCurrentCart();
                finalPaymentModal.updateCurrentCart();
            }).catch(error => {
                console.log(error);
            });
        },
    },
    beforeMount: function () {

    }
});