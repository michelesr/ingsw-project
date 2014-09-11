controllers.controller 'CategoryCtrl', ($scope, $stateParams, Category, Meta) ->

  $scope.meta = Meta.category
  Category.list (list) ->
    $scope.list = list
    $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]

  $scope.delete = (id) ->
    Category.delete id, (res) ->
      $scope.msg = 'Category deleted successfully'


controllers.controller 'CategoryAddCtrl', ($scope, $state, $stateParams, Category, Meta) ->

  $scope.meta = {}
  $scope.meta = Meta.category

  $scope.add = (fields) ->
    $scope.resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    Category.add $scope.resource, (res) ->
      $scope.meta = {}
#      $state.go 'root.categories.list', {reload: true, inherit: true, notify: true}
#      $state.transitionTo 'root.categories.list', {}, {reload: true, inherit: true, notify: true}
#      $state.transitionTo 'root.categories.list', {}, {inherit: true, notify: true}
#      $state.go 'root.categories.list', {}, {inherit: true, notify: true}
      $state.go 'root.categories.list'


controllers.controller 'CategoryDetailCtrl', ($scope, $stateParams, Category, Meta) ->

  $scope.meta = Meta.category
  Category.detail { id: $stateParams.id }, (res) ->
    for f in $scope.meta.fields
      k = f['model']
      f['value'] = res[k]


controllers.controller 'CategoryEditCtrl', ($scope, $stateParams, Category, Meta) ->

  $scope.meta = Meta.category
#  $scope.result = {}

  Category.detail { id: $stateParams.id }, (res) ->
    $scope.category = res
    for f in $scope.meta['fields']
      model = f['model']
      $scope.result = model
      f['value'] = $scope.category[model]

  $scope.edit = (fields) ->
    $scope.resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = Category.update(resource)
