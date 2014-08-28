controllers.controller 'ProductCtrl', ($scope, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product
  $scope.list = Product.list()


controllers.controller 'ProductAddCtrl', ($scope, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product

  $scope.newResource = {}
  $scope.master = {}
  $scope.add = (newResource) ->
    $scope.master = angular.copy(newResource)
    $scope.newResource = Product.add(newResource)


controllers.controller 'ProductDetailCtrl', ($scope, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product
  $scope.resource = Product.detail({ id: $stateParams.id })
