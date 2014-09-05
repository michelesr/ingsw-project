controllers.controller 'UserCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  User.list (list) ->
    $scope.list = list
    $scope.empty = _.isEmpty $scope.list[0]

  $scope.delete = (id) ->
    User.delete id


controllers.controller 'UserAddCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = []
  $scope.meta = Meta.user
  $scope.resource = {}
  $scope.result = {}

  $scope.add = (form_fields) ->
    $scope.resource = {}
    for f in form_fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = User.add($scope.resource)


controllers.controller 'UserDetailCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  $scope.resource = User.detail({ id: $stateParams.id })


controllers.controller 'UserEditCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
#  $scope.result = {}

  User.detail { id: $stateParams.id }, (res) ->
    $scope.user = res
    for f in $scope.meta['form_fields']
      model = f['model']
      $scope.result = model
      f['value'] = $scope.user[model]

  $scope.edit = (form_fields) ->
    $scope.resource = {}
    for f in form_fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    $scope.result = User.update(resource)
