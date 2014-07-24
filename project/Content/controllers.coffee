mainCtls = angular.module 'mainCtls', []

mainCtls.controller 'MainCtl', ($scope, $http) ->
  $scope.title = 'Web project'


# User
mainCtls.controller 'UserListCtl', ($scope, User) ->
  $scope.users = User.query()
  $scope.headers = [
    'email'
    'first_name'
    'last_name'
  ]

mainCtls.controller 'UserDetailCtl', ($scope, $routeParams, User) ->
  $scope.user = User.get(
    {
      userId: $routeParams.userId
      action: 'index'
    }
  )


# Product
mainCtls.controller 'ProductListCtl', ($scope, Product) ->
  $scope.products = Product.query()
  $scope.headers = [
    'name'
  ]

mainCtls.controller 'ProductDetailCtl', ($scope, $routeParams, Product) ->
  $scope.product = Product.get(
    {
      productId: $routeParams.productId
      action: 'index'
    }
  )