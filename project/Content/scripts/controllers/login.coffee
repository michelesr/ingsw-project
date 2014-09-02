controllers.controller 'LoginCtrl', ($scope, $rootScope, $state, Auth, Session) ->

  if $rootScope.debug
    $scope.credentials =
      email: 'admin@example.org'
      password: 'admin'

  $scope.login = (credentials) ->
    Auth.login(credentials)
      .then (res) ->
        console.log Session.type
        $rootScope.isAuth = true

        switch Session.type
          when 0 then $state.go 'root.supplier'
          when 1 then $state.go 'root.admin'
