mainCtrl = angular.module 'mainCtrl', []

mainCtrl.controller 'HomeCtrl', ($scope, $http) ->
  $scope.title = 'Web project'

mainCtrl.controller 'LoginCtrl', ($scope, $http) ->
  $scope.auth = {}
  $scope.master = {}
  $scope.add = (newResource) ->
    $scope.master = angular.copy(newResource)
    $scope.newResource = Product.add(newResource)

mainCtrl.controller 'AdminCtrl', ($scope) ->
  return

mainCtrl.controller 'SupplierCtrl', ($scope) ->
  return


# Users ---------------------------------------------------
mainCtrl.controller 'UserCtrl', ($scope, $stateParams, User) ->
  $scope.resourceMeta =
    name: 'user'
    namePlural: 'users'
    icon: 'fa-users'
  $scope.resourceList = User.list()
  $scope.fields = [
    'id'
    'email'
    'first_name'
    'last_name'
  ]

  # Add
  $scope.newResource = {}
  $scope.master = {}
  $scope.fields = [
    { name: 'email', type: 'email', ph: 'user@example.org', isRequired: true }
    { name: 'password', type: 'password', ph: 'password', isRequired: true }
    { name: 'first_name', type: 'text', ph: 'Mario', isRequired: false }
    { name: 'last_name', type: 'text', ph: 'Rossi', isRequired: false }
  ]
  $scope.add = (newResource) ->
    $scope.master = angular.copy(newResource)
    $scope.newResource = User.add(newResource)

mainCtrl.controller 'UserDetailCtrl', ($scope, $stateParams, User) ->
  # Detail
  $scope.user = User.get({
    action: 'detail'
    id: $stateParams.id
  })


# Product -------------------------------------------------
mainCtrl.controller 'ProductCtrl', ($scope, $stateParams, Product) ->
  $scope.resourceMeta =
    name: 'product'
    namePlural: 'products'
    icon: 'fa-coffee'

  $scope.resourceList = Product.list()
  $scope.fields = [
    'id'
    'name'
  ]

  # Add
  $scope.newResource = {}
  $scope.master = {}
  $scope.add = (newResource) ->
    $scope.master = angular.copy(newResource)
    $scope.newResource = Product.add(newResource)

#mainCtrl.controller 'ProductDetailCtrl', ($scope, $stateParams, Product) ->
  # Detail
  $scope.resource = Product.get({
    action: 'detail'
    id: $stateParams.id
  })
