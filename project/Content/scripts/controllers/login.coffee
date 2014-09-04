controllers.controller 'LoginCtrl', ($scope, $rootScope, $http, $state, AuthAPI, User) ->

  $scope.credentials =
    email: 'admin@example.org'
    password: 'admin'

  $scope.login = (credentials) ->
    AuthAPI.login credentials, (res_auth) ->
      $http.defaults.headers.common['api_key'] = res_auth.api_key
      User.detail { id: res_auth.user_id }, (res_user) ->
        $rootScope.authId = res_user.id
        $rootScope.authEmail = res_user.email
        $rootScope.authFirstName = res_user.first_name
        $rootScope.authLastName = res_user.last_name
        $rootScope.authType = res_user.type

        $rootScope.isAuth = true

        switch $rootScope.authType
          when 0 then $state.go 'root.supplier'
          when 1 then $state.go 'root.admin'
