controllers.controller 'UserCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  $scope.list = User.list()


controllers.controller 'UserAddCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  $scope.resource = {}
  $scope.result = {}

  $scope.add = (form_fields) ->

    $scope.resource = {}
    for f in form_fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v

    $scope.result = User.add(resource)


controllers.controller 'UserDetailCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  $scope.user = User.detail({ id: $stateParams.id })


controllers.controller 'UserEditCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  $scope.resource = {}
  $scope.result = {}
#  $scope.user = User.detail({ id: $stateParams.id })


  $scope.edit = (form_fields) ->

    $scope.resource = {}
    for f in form_fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v

    $scope.result = User.add(resource)
