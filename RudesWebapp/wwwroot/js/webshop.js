Vue.component("article-component", {
    props: ["article"],
    template: `
            <div class="articleDiv">
                <img class="productImages" src="/images/rudes-logo.jpg">
                <figcaption class="articleDescription">{{ article.name }}</figcaption>
                <figcaption class="articleDescription">{{ article.description }}</figcaption>
                <figcaption class="articleDescription">{{ article.price }}</figcaption>
                <button class="buttonStyle" @click="passArticle(article.id, 1, 'XL')">Dodaj u košaricu</button>
                <hr />
            </div>
        `,
    // TODO one should be able to choose the size and quantity somehow
    // TODO size is currently hardcoded as XL in the template. articleDto should contain something like
    //      availableSizes = [["M",1],["S",10],["XL",3]]
    //        or
    //      availableSizes = {"M":1, "S":10, "XL":3}
    methods: {
        passArticle: function (articleId, quantity, size) {
            webshop.addItemToCart(articleId, quantity, size);
        }
    }
});

var webshop = new Vue({
    el: "#webshop",
    data: {
        allArticles: []
    },
    methods: {
        getArticles: function () {
            axios.get("/Webshop/GetArticles")
                .then(response => {
                    this.allArticles = response.data;
                    console.log(this.allArticles);
                })
                .catch(error => {
                    console.log(error);
                });
        },
        addItemToCart: function (articleId, size, quantity = 1) {
            axios({
                method: 'post',
                url: '/Webshop/AddToShoppingCart',
                data: 'articleId=' + articleId + '&quantity=' + quantity + '&size=' + size,
                headers: {
                    'content-type': 'application/x-www-form-urlencoded;charset=utf-8'
                }
            }).then(() => {
                shoppingCartModal.updateCurrentCart();
            }).catch(error => {
                console.log(error);
            });
        },
        prikaziFiltere: function () {
            const x = document.getElementById("myDIV");

            if (x.style.display === "none") {
                // x.style.transitionDelay = "all 2s";
                x.style.display = "";
            } else {
                x.style.display = "none";
            }
        },
        zatvoriFiltere: function () {
            const x = document.getElementById("myDIV");

            if (x.style.display === "none") {
                // x.style.transitionDelay = "all 2s";
                x.style.display = "";
            } else {
                x.style.display = "none";
            }
        }
    },
    computed: {
        groupedArticles: function () {
            return _.chunk(this.allArticles, 4);
        }
    },
    beforeMount: function () {
        this.getArticles();
    }
});