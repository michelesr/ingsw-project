controllers.controller 'RootCtrl', ($rootScope, $scope, $state, Auth) ->

  $rootScope.debug = true

  if not Auth.isAuthenticated
    $state.go 'root.login'

#  switch Session.type
#    when 'admin' then $state.go 'root.admin'
#    when 'supplier' then $state.go 'root.supplier'
#    else $state.go 'root.login'
