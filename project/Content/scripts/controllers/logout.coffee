controllers.controller 'LogoutCtrl', ($state, $rootScope, Auth) ->

  Auth.logout
  $rootScope.sidebar = []
  $state.go 'root.login'
