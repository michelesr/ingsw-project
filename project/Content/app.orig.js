(function() {

    var app = angular.module('myApp', []);

    app.controller('myController', function() {
        this.product = gem;
    });

    var gem = {
        name: "kobe"
    };
})();
