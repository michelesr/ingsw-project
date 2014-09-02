controllers.controller 'LoginCtrl', ($scope, $rootScope, $state, Auth, Session) ->

  if $rootScope.debug
    $scope.credentials =
      email: 'admin@example.org'
      password: 'admin'

  $scope.login = (credentials) ->
    Auth.login(credentials)
      .then (res) ->
        $rootScope.isAuth = Auth.isAuthenticated
        $state.go 'root.admin'
#        switch Session.type
#          when 'admin' then $state.go 'admin'
#          when 'supplier' then $state.go 'root.supplier'
#          else $state.go 'root.login'
