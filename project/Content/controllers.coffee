mainCtls = angular.module 'mainCtls', []

mainCtls.controller 'HomeCtl', ($scope, $http) ->
  $scope.title = 'Web project'


# User
mainCtls.controller 'UserListCtl', ($scope, User) ->
  $scope.resourceName = 'users'
  $scope.resourceList = User.query()
  $scope.fields = [
    'id'
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
  $scope.resourceName = 'products'
  $scope.resourceList = Product.query()
  $scope.fields = [
    'id'
    'name'
  ]

mainCtls.controller 'ProductDetailCtl', ($scope, $routeParams, Product) ->
  $scope.resource = Product.get(
    {
      resourceId: $routeParams.resourceId
      action: ''
    }
  )
