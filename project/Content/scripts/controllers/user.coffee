controllers.controller 'UserCtrl', ($scope, $stateParams, User) ->
  $scope.resourceMeta =
    name: 'user'
    namePlural: 'users'
    icon: 'fa-users'
  $scope.resourceList = User.list()
  $scope.fields = [
    'id'
    'email'
    'first_name'
    'last_name'
  ]

  # Add
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

  # Detail
  $scope.user = User.get({
    action: 'detail'
    id: $stateParams.id
  })
