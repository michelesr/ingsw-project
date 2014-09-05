controllers.controller 'LogoutCtrl', ($scope, $rootScope, $http, $state, AuthAPI) ->

  AuthAPI.logout () ->
    delete $http.defaults.headers.common['api_key']

    $rootScope.authId = null
    $rootScope.authEmail = null
    $rootScope.authFistName = ''
    $rootScope.authLastName = null
    $rootScope.authType = null

    $rootScope.sidebar = null
    $rootScope.isAuth = false

    $state.go 'root.login'
