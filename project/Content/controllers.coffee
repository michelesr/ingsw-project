mainCtrl = angular.module 'mainCtrl', []

mainCtrl.controller 'MainCtrl', ($scope, $http) ->
  $scope.title = "Web project"
  0

mainCtrl.controller 'ProductListCtrl', ($scope, $http, $routeParams) ->
  $http.get('/api/product_list').success (data) ->
    $scope.product_list = data
    0
  0

mainCtrl.controller 'ProductDetailCtrl', ($scope, $http, $routeParams) ->
  $scope.productId = $routeParams.productId
  $scope.product = {
    id: 1
    name: "alimenti"
  }

  $scope.products_detail = [
    {
      id: 1
      name: "alimenti"
    }, {
      id: 2
      name: "saponi"
    }
  ]
#  $http.get('/api/product/1/detail').success (data) ->
#    $scope.product_detail = data
#    0
  0
