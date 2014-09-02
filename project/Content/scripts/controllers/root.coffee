controllers.controller 'RootCtrl', ($rootScope, $scope, $state, Auth, Session) ->

  $rootScope.debug = true

  if not $rootScope.auth
    $state.go 'root.login'
