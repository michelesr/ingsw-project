controllers.controller 'LoginCtrl', ($scope, $rootScope, $state, Auth) ->

  if $rootScope.debug
    $scope.credentials =
      email: 'admin@example.org'
      password: 'admin'

  $scope.login = (credentials) ->
    Auth.login(credentials)
      .then () ->
        switch $rootScope.authType
          when 0 then $state.go 'root.supplier'
          when 1 then $state.go 'root.admin'
