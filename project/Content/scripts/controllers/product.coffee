controllers.controller 'ProductCtrl', ($scope, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product
  Product.list (list) ->
    $scope.list = list
    $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]


controllers.controller 'ProductAddCtrl', ($scope, $state, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product
  $scope.resource = {}
  $scope.result = {}

  $scope.add = (form_fields) ->
    $scope.resource = {}
    for f in form_fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = Product.add($scope.resource)
    $state.go 'root.products.list'


controllers.controller 'ProductDetailCtrl', ($scope, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product
  $scope.resource = Product.detail({ id: $stateParams.id })


controllers.controller 'ProductEditCtrl', ($scope, $state, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product

  Product.detail { id: $stateParams.id }, (res) ->
    $scope.product = res
    for f in $scope.meta['form_fields']
      model = f['model']
      $scope.result = model
      f['value'] = $scope.product[model]

  $scope.edit = (form_fields, $stateParams) ->
    $scope.resource = {}
    for f in form_fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = Product.update(resource)
    $state.go 'root.products.list'
