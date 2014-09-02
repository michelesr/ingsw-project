controllers.controller 'RootCtrl', ($rootScope, $scope, $state, Auth, Session) ->

  $rootScope.debug = true

  switch Session.type
    when 'admin' then $state.go 'root.admin'
    when 'supplier' then $state.go 'root.supplier'
    else $state.go 'root.login'
