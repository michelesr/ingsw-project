mainCtls = angular.module 'mainCtls', []

mainCtls.controller 'MainCtl', ($scope, $http) ->
  $scope.title = "Web project"
  0


mainCtls.controller 'ProductListCtl', ($scope, Product) ->
  $scope.products = Product.query()

mainCtls.controller 'ProductDetailCtl', ($scope, $routeParams, Product) ->
  $scope.product = Product.get(
    {
      productId: $routeParams.productId
      action: 'detail'
    }
  )


mainCtls.controller 'UserListCtl', ($scope, User) ->
  $scope.users = User.query()

mainCtls.controller 'UserDetailCtl', ($scope, $routeParams, User) ->
  $scope.user = User.get(
    {
      userId: $routeParams.userId
      action: 'detail'
    }
  )
