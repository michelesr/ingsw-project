controllers.controller 'ProductCtrl', ($scope, $stateParams, Product) ->
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

  # Detail
  $scope.resource = Product.get({
    action: 'detail'
    id: $stateParams.id
  })
