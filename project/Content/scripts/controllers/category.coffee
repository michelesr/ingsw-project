controllers.controller 'CategoryCtrl', ($scope, $stateParams, Category, Meta) ->

  $scope.meta = Meta.category
  Category.list (list) ->
    $scope.list = list
    $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]

  $scope.delete = (id) ->
    Category.delete id, (res) ->
      $scope.msg = 'Category deleted successfully'


controllers.controller 'CategoryAddCtrl', ($scope, $stateParams, Category, Meta) ->

  $scope.meta = []
  $scope.meta = Meta.category
  $scope.resource = {}
  $scope.result = {}

  $scope.add = (form_fields) ->
    $scope.resource = {}
    for f in form_fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = Category.add($scope.resource)


controllers.controller 'CategoryDetailCtrl', ($scope, $stateParams, Category, Meta) ->

  $scope.meta = Meta.category
  $scope.resource = Category.detail({ id: $stateParams.id })


controllers.controller 'CategoryEditCtrl', ($scope, $stateParams, Category, Meta) ->

  $scope.meta = Meta.category
#  $scope.result = {}

  Category.detail { id: $stateParams.id }, (res) ->
    $scope.category = res
    for f in $scope.meta['form_fields']
      model = f['model']
      $scope.result = model
      f['value'] = $scope.category[model]

  $scope.edit = (form_fields) ->
    $scope.resource = {}
    for f in form_fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = Category.update(resource)
