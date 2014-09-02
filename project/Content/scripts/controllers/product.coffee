controllers.controller 'ProductCtrl', ($scope, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product
  $scope.list = Product.list()


controllers.controller 'ProductAddCtrl', ($scope, $stateParams, Product, Meta) ->

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


controllers.controller 'ProductDetailCtrl', ($scope, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product
  $scope.resource = Product.detail({ id: $stateParams.id })


controllers.controller 'ProductEditCtrl', ($scope, $stateParams, Product, Meta) ->

  $scope.meta = Meta.product
  $scope.result = {}

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
