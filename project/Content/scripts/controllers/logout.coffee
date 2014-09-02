controllers.controller 'LogoutCtrl', ($state, $rootScope, Auth) ->

# implementa post /api/logout

  Auth.logout
  $rootScope.sidebar = []
  $rootScope.isAuth = false
  $state.go 'root.login'
