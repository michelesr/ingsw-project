controllers.controller 'RootCtrl', ($rootScope, $scope, $state, AuthService) ->

  $rootScope.debug = true

  $scope.currentUser = null

  $scope.setCurrentUser = (user) ->
    $scope.currentUser = user

  if $scope.currentUser is null
    $state.go 'root.login'
