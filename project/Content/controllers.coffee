mainCtls = angular.module 'mainCtls', []


mainCtls.controller 'HomeCtl', ($scope, $http) ->
  $scope.title = 'Web project'

mainCtls.controller 'LoginCtl', ($scope, $http) ->
  $scope.auth = {}
  $scope.master = {}
  $scope.
  $scope.add = (resource) ->
    $scope.master = angular.copy(resource)
    $scope.resource = Product.add(resource)

mainCtls.controller 'AdminCtl', ($scope) ->
  return

mainCtls.controller 'SupplierCtl', ($scope) ->
  return


# User
mainCtls.controller 'UserListCtl', ($scope, User) ->
  $scope.resourceName = 'user'
  $scope.resourcesName = 'users'
  $scope.resourceList = User.list()
  $scope.fields = [
    'id'
    'email'
    'first_name'
    'last_name'
  ]


mainCtls.controller 'UserAddCtl', ($scope, User) ->
  $scope.resourceName = 'user'
  $scope.resourcesName = 'users'
  $scope.resource = {}
  $scope.master = {}
  $scope.fields = [
    { name: 'email', type: 'email', ph: 'user@example.org', isRequired: true }
    { name: 'password', type: 'password', ph: 'password', isRequired: true }
    { name: 'first_name', type: 'text', ph: 'Mario', isRequired: false }
    { name: 'last_name', type: 'text', ph: 'Rossi', isRequired: false }
  ]

  $scope.add = (resource) ->
    $scope.master = angular.copy(resource)
    $scope.resource = User.add(resource)


mainCtls.controller 'UserDetailCtl', ($scope, $routeParams, User) ->
  $scope.user = User.get({
    action: 'detail'
    id: $routeParams.id
  })


# Product
mainCtls.controller 'ProductListCtl', ($scope, Product) ->
  $scope.resourceName = 'product'
  $scope.resourcesName = 'products'
  $scope.resourceList = Product.list()
  $scope.fields = [
    'id'
    'name'
  ]


mainCtls.controller 'ProductAddCtl', ($scope, Product) ->
  $scope.resourceName = 'product'
  $scope.resourcesName = 'products'
  $scope.resource = {}
  $scope.master = {}

  $scope.add = (resource) ->
    $scope.master = angular.copy(resource)
    $scope.resource = Product.add(resource)


mainCtls.controller 'ProductDetailCtl', ($scope, $routeParams, Product) ->
  $scope.resource = Product.get({
    action: 'detail'
    id: $routeParams.id
  })
