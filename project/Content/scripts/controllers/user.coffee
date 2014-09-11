controllers.controller 'UserCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  User.list (list) ->
    $scope.list = list
    $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]

  $scope.delete = (id) ->
    User.delete id, (res) ->
      $scope.msg = 'User deleted successfully'


controllers.controller 'UserAddCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  password_field =
    human: 'Password'
    model: 'password'
    type: 'password'
    required: true
    placeholder: 'your password'
  $scope.meta.fields.push password_field

  $scope.add = (fields) ->
    $scope.resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = {}
    $scope.result = User.add($scope.resource)


controllers.controller 'UserDetailCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  User.detail { id: $stateParams.id }, (res) ->
    for f in $scope.meta.fields
      k = f['model']
      f['value'] = res[k]


controllers.controller 'UserEditCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
#  $scope.result = {}

  User.detail { id: $stateParams.id }, (res) ->
    $scope.user = res
    for f in $scope.meta['fields']
      model = f['model']
      $scope.result = model
      f['value'] = $scope.user[model]

  $scope.edit = (fields) ->
    $scope.resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = User.update(resource)
