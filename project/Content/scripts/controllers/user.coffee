controllers.controller 'UserCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  $scope.list = User.list()


controllers.controller 'UserAddCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  $scope.newResource = {}
  $scope.master = {}
  $scope.fields = [
    { name: 'email', type: 'email', ph: 'user@example.org', isRequired: true }
    { name: 'password', type: 'password', ph: 'password', isRequired: true }
    { name: 'first_name', type: 'text', ph: 'Mario', isRequired: false }
    { name: 'last_name', type: 'text', ph: 'Rossi', isRequired: false }
  ]
  $scope.add = (newResource) ->
    $scope.master = angular.copy(newResource)
    $scope.newResource = User.add(newResource)


controllers.controller 'UserDetailCtrl', ($scope, $stateParams, User, Meta) ->

  $scope.meta = Meta.user
  $scope.user = User.detail({ id: $stateParams.id })
