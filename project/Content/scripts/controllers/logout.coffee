controllers.controller 'LogoutCtrl', ($state, Auth) ->

  Auth.logout()
  $state.go 'root.login'
