services.factory 'Auth', ($http, $rootScope, User, AuthAPI) ->

  auth = {}

  auth.login = (credentials) ->
    AuthAPI.login credentials, (res_auth) ->
      $http.defaults.headers.common['api_key'] = res_auth.api_key
      User.detail { id: res_auth.user_id }, (res_user) ->
        $rootScope.authId = res_user.id
        $rootScope.authEmail = res_user.email
        $rootScope.authFirstName = res_user.first_name
        $rootScope.authLastName = res_user.last_name
        $rootScope.authType = res_user.type

        $rootScope.isAuth = true
        console.log '1bissssssssssssssssss'
        console.log $rootScope

  auth.logout = () ->
    AuthAPI.logout () ->
      $http.defaults.headers.common['api_key'] = ''

      $rootScope.authId = null
      $rootScope.authEmail = null
      $rootScope.authFistName = ''
      $rootScope.authLastName = null
      $rootScope.authType = null

      $rootScope.sidebar = null
      $rootScope.isAuth = false

  auth
