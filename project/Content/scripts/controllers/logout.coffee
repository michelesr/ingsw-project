controllers.controller 'LogoutCtrl', ($state, $rootScope, Auth) ->

  Auth.logout
  $rootScope.sidebar = []
  $rootScope.isAuth = false
  $state.go 'root.login'
