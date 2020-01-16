Vue.component("article-component", {
    props: ["article"],
    template: `
            <div class="articleDiv">
                <img class="productImages" src="/images/rudes-logo.jpg" >
                <figcaption class="articleDescription">
                    {{ article.name }}
                </figcaption>
                <figcaption class="articleDescription">{{ article.price }} kn </figcaption>
                <figcaption>
                    <div>
                        <label class="checkbox-inline" style="margin: 0.5%"><input type="checkbox" value="">XS</label>
                        <label class="checkbox-inline" style="margin: 0.5%"><input type="checkbox" value="">S</label>
                        <label class="checkbox-inline" style="margin: 0.5%"><input type="checkbox" value="">M</label>
                        <label class="checkbox-inline" style="margin: 0.5%"><input type="checkbox" value="">L</label>
                        <label class="checkbox-inline" style="margin: 0.5%"><input type="checkbox" value="">XL</label>
                    </div>
                </figcaption>
                <figcaption>
                    <button type="button" class="priceButton" @click="updateSelectedArticle(article)" data-toggle="modal" data-target="#articleDetailsModal">
                        Više detalja
                     </button>
                </figcaption>
                <button class="buttonStyle" @click="passArticle(article)">Dodaj u košaricu</button>
                <hr />
            </div>
        `,
    methods: {
        passArticle: function (article) {
            webshop.addItemToCart(article.id, 1, "XL");
        },
        updateSelectedArticle: function (article) {
            articleDetailsModal.selectedArticle = article;
        }
    }
});

// TODO one should be able to choose the size and quantity somehow
// TODO size is currently hardcoded as XL in the template. articleDto should contain something like
//      availableSizes = [["M",1],["S",10],["XL",3]]
//        or
//      availableSizes = {"M":1, "S":10, "XL":3}

var webshop = new Vue({
    el: "#webshop",
    data: {
        allArticles: [],
        filteredArticles: [],
        hoodieFilter: false,
        tshirtFilter: false,
        maxPrice: 0,
        sortOrder: ""
    },
    methods: {
        getArticles: function () {
            axios.get("/api/webshop/GetArticlesInStore")
                .then(response => {
                    this.allArticles = response.data;
                    this.filteredArticles = this.allArticles;
                })
                .catch(error => {
                    console.log(error);
                });
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
                shoppingCartModal.updateCurrentCart();
                finalPaymentModal.updateCurrentCart();
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
        },
        groupArticles: function (array, subArraySize) {
            return _.chunk(array, subArraySize);
        },
        filterArticles: function () {
            if (this.isFilteringActive) {
                let filteredArray = [];
                for (var i = 0; i < this.allArticles.length; ++i) {
                    let article = this.allArticles[i];
                    let filterPass = true;

                    if (article.type == "hoodie" && this.tshirtFilter) filterPass = false;
                    if (article.type == "t-shirt" && this.hoodieFilter) filterPass = false;

                    if (this.maxPrice != 0 && article.price >= this.maxPrice) filterPass = false;

                    if (filterPass) {
                        filteredArray.push(article);
                    }
                }

                if (this.sortOrder == "asc") filteredArray.sort((a1, a2) => a1.price - a2.price);
                else if (this.sortOrder == "desc") filteredArray.sort((a1, a2) => a2.price - a1.price);

                this.filteredArticles = filteredArray;
            } else {
                this.filteredArticles = this.allArticles;
            }
        }
    },
    computed: {
        groupedFilteredArticles: function () {
            return this.groupArticles(this.filteredArticles, 4);
        },
        isFilteringActive: function () {
            return !(!this.hoodieFilter && !this.tshirtFilter && this.maxPrice == 0 && this.sortOrder == "");
        }
    },
    beforeMount: function () {
        this.getArticles();
    }
});