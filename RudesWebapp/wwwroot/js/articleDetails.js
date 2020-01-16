var articleDetailsModal = new Vue({
    el: "#articleDetailsModal",
    data: {
        selectedArticle: {},
        selectedArticleReview: {},
        sizes: [],
        probneDostupneVelicine: [3, 0, 5, 0, 7],
        quantity: 1,
        size: '',
        review: '',
    },
    methods: {
        addItemToCart: function (articleId, quantity, size) {
            axios({
                method: 'post',
                url: '/api/webshop/AddToShoppingCart',
                data: 'articleId=' + articleId + '&quantity=' + quantity + '&size=' + size,
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(response => {
                shoppingCartModal.updateCurrentCart();
                finalPaymentModal.updateCurrentCart();
                console.log(response);
                if (response == null) {
                    toastr.error("Dodavanje u košaricu neuspješno!", "");
                } else if (response.status == 200) {
                    toastr.success("Dodavanje u košaricu uspješno izvršeno!", "");
                } else if (response.status == 204) {
                    toastr.error("Dodavanje artikla u košaricu neuspješno! Nema dovoljno artikala na skladištu!", "");
                }
            }).catch(error => {
                console.log(error);
            });
        },
        getReview: function (id) {
            axios.get("/api/Review/" + id)
                .then(response => {
                    console.log(response);
                    this.selectedArticleReview = response.data;
                })
                .catch(error => {
                    console.log(error);
                });
        },
    }
});