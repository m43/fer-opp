var articleDetailsModal = new Vue({
    el: "#articleDetailsModal",
    data: {
        selectedArticle: {},
        probneDostupneVelicine: [3, 0, 5, 0, 7],
        kolicina1: 3,
        kolicina2: 0
        //kolicina
    },
  
    computed: {
        isDisabled1: function () {
            return (this.kolicina1 <= 0);
        },
        isDisabled2: function () {
            return (this.kolicina2 <= 0);
        },
        isDisabled: function () {
            return true;
        },
        isNotDisabled: function () {
            return false;
        }

       /*
        isDisabled: function(){
            return (this.kolicina <=0);
        }
        */
    }

});