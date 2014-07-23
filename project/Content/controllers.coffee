mainCtls = angular.module 'mainCtls', []

mainCtls.controller 'MainCtl', ($scope, $http) ->
  $scope.title = "Web project"
  0

#mainCtls.controller 'ProductListCtl', ($scope, Product) ->
#  $scope.product_list = Phone.query()
mainCtls.controller 'ProductListCtl', ($scope, $http) ->
  $http.get('/api/product/list').success (data) ->
    $scope.product_list = data
    0
  0

#mainCtls.controller 'ProductDetailCtl', ($scope, $routeParams, Product) ->
#  $scope.product = Phone.get(
#    { productId: $routeParams.productId }
#  )

#  $http.get('/api/product/' + $routeParams.productId).success (data) ->
#    $scope.product = data
#    0
#  0
