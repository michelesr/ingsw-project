controllers.controller 'LoginCtrl', ($scope, $rootScope, $state, Auth) ->

  if $rootScope.debug
    $scope.credentials =
      email: 'admin@example.org'
      password: 'admin'

  $scope.login = (credentials) ->
    console.log '1111111111111111111'
    console.log $rootScope
    Auth.login(credentials)
      .then () ->
        console.log '2222222222222222'
        console.log $rootScope
        switch $rootScope.authType
          when 0 then $state.go 'root.supplier'
          when 1 then $state.go 'root.admin'
    console.log '33333333333333333'
    console.log $rootScope
