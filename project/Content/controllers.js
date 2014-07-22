// Generated by CoffeeScript 1.7.1
(function() {
  var mainCtrl;

  mainCtrl = angular.module('mainCtrl', []);

  mainCtrl.controller('MainCtrl', function($scope, $http) {
    $scope.title = "Web project";
    return 0;
  });

  mainCtrl.controller('ProductListCtrl', function($scope, $http, $routeParams) {
    $http.get('/api/product_list').success(function(data) {
      $scope.product_list = data;
      return 0;
    });
    return 0;
  });

  mainCtrl.controller('ProductDetailCtrl', function($scope, $http, $routeParams) {
    $scope.productId = $routeParams.productId;
    $scope.product = {
      id: 1,
      name: "alimenti"
    };
    $scope.products_detail = [
      {
        id: 1,
        name: "alimenti"
      }, {
        id: 2,
        name: "saponi"
      }
    ];
    return 0;
  });

}).call(this);
