controllers.controller 'CategoryCtrl', ($scope, $state, Category, Meta) ->

  list = ->
    $scope.meta = Meta.category
    Category.list (list) ->
      $scope.list = list
      $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]

  $scope.addForm = () ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = Meta.category
    $state.go '^.add'

  $scope.add = (fields) ->
    $scope.resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    Category.add $scope.resource, (res) ->
      list()
      $state.go '^.list'

  $scope.detail = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    Category.detail {id: id}, (res) ->
      for f in $scope.meta.fields
        k = f['model']
        f['value'] = res[k]
      $state.go '^.detail', {id: id}

  $scope.editForm = () ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = Meta.category
    $state.go '^.edit', {id: $state.params.id}

  $scope.edit = (fields) ->
    resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      resource[k] = v
    Category.update {id: $state.params.id}, resource, (res) ->
      $scope.result = res
      list()
      $scope.msgSuccess = 'Updated successfully'
      $state.go '^.list'

  $scope.remove = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    Category.remove {id: id}, (res) ->
      $scope.msgSuccess = 'Removed successfully'
      list()

  list()
