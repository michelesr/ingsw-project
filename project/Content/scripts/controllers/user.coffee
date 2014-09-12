controllers.controller 'UserCtrl', ($scope, $state, User, Meta) ->

  list = ->
    $scope.meta = Meta.user
    User.list (list) ->
      $scope.list = list
      $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]

  $scope.addForm = () ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = Meta.user
    extraField =
      human: 'Password'
      model: 'password'
      type: 'password'
      required: true
      placeholder: 'your password'
    $scope.meta.fields.push extraField
    $state.go '^.add'

  $scope.add = (fields) ->
    $scope.resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    User.add $scope.resource, (res) ->
      list()
      $state.go '^.list'

  $scope.detail = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    User.detail {id: id}, (res) ->
      for f in $scope.meta.fields
        k = f['model']
        f['value'] = res[k]
      $state.go '^.detail', {id: id}

  $scope.editForm = () ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = Meta.user
    $state.go '^.edit', {id: $state.params.id}

  $scope.edit = (fields) ->
    resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      resource[k] = v
    User.update {id: $state.params.id}, resource, (res) ->
      $scope.result = res
      list()
      $scope.msgSuccess = 'Updated successfully'
      $state.go '^.list'

  $scope.remove = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    User.remove {id: id}, (res) ->
      $scope.msgSuccess = 'Removed successfully'
      list()

  list()
