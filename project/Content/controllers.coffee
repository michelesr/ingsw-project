mainCtls = angular.module 'mainCtls', []

mainCtls.controller 'MainCtl', ($scope, $http) ->
  $scope.title = "Web project"
  0

mainCtls.controller 'ProductListCtl', ($scope, Product) ->
  $scope.product_list = Product.query()

mainCtls.controller 'ProductDetailCtl', ($scope, $routeParams, Product) ->
  $scope.product = Product.get(
    {
      productId: $routeParams.productId
      action: 'detail'
    }
  )
