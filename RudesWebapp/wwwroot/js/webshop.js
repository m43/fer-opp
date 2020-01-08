Vue.component("article-component", {
    props: ["article"],
    template: `
            <div class="articleDiv">
                <img class="productImages" src="/images/rudes-logo.jpg">
                <figcaption class="articleDescription">{{ article.name }}</figcaption>
                <figcaption class="articleDescription">{{ article.description }}</figcaption>
                <figcaption class="articleDescription">{{ article.price }}</figcaption>
                <button class="buttonStyle" @click="passArticle(article)">Dodaj u košaricu</button>
                <hr />
            </div>
        `,
    methods: {
        passArticle: function (article) {
            webshop.passToShoppingCart(article);
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
                })
                .catch(error => {
                    console.log(error);
                });
        },
        passToShoppingCart: function (article) {
            shoppingCartModal.addArticleToCart(article);
        },
        prikaziFiltere: function () {
            var x = document.getElementById("myDIV");
            
            if (x.style.display === "none") {
               // x.style.transitionDelay = "all 2s";
                x.style.display = "";
            } else {
                x.style.display = "none";
            }
        },

        zatvoriFiltere: function () {
            var x = document.getElementById("myDIV");

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