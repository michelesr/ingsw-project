controllers.controller 'LoginCtrl', ($scope, $rootScope, $http, $state, Auth, User) ->

  if $rootScope.debug
    if $rootScope.debugType == 'admin'
      $scope.credentials =
        email: 'admin@example.org'
        password: 'admin'
    else if $rootScope.debugType == 'supplier'
      $scope.credentials =
        email: 'test@example.org'
        password: 'test'

  $scope.login = (credentials) ->
    Auth.login credentials, (res_auth) ->
      $scope.loginError = ''
      if not res_auth.auth == true
        $scope.loginError = 'Authentication error'
      else
          $http.defaults.headers.common['api_key'] = res_auth.api_key
          User.detail { id: res_auth.user_id }, (res_user) ->
            $rootScope.authEmail = res_user.email
            $rootScope.authFirstName = res_user.first_name
            $rootScope.authLastName = res_user.last_name
            $rootScope.authType = res_user.type

            $rootScope.isAuth = true

            switch $rootScope.authType
              when 0
                $rootScope.authId = res_user.user_id
                $rootScope.authSupplierId = res_user.id
                $state.go 'root.supplierHome'
              when 1
                $rootScope.authId = res_user.id
                $state.go 'root.adminHome'
