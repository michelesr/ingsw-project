controllers.controller 'LogoutCtrl', ($rootScope, $http, $state, Auth) ->

  Auth.logout () ->
    delete $http.defaults.headers.common['api_key']

    $rootScope.authId = null
    $rootScope.authSupplierId = null
    $rootScope.authEmail = null
    $rootScope.authFistName = ''
    $rootScope.authLastName = null
    $rootScope.authType = null

    $rootScope.sidebar = null
    $rootScope.isAuth = false

    $state.go 'root.login'
