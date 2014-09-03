services.factory 'Auth', ($http, $rootScope, User) ->

  auth = {}

  auth.login = (credentials) ->
    $http.post '/api/auth', credentials
      .then (res_auth) ->
        $http.defaults.headers.common['api_key'] = res_auth.data.api_key
        User.detail { id: res_auth.data.user_id }, (res_user) ->
          $rootScope.authId = res_user.id
          $rootScope.authEmail = res_user.email
          $rootScope.authFirstName = res_user.first_name
          $rootScope.authLastName = res_user.last_name
          $rootScope.authType = res_user.type

          $rootScope.isAuth = true
          console.log '1bissssssssssssssssss'
          console.log $rootScope

  auth.logout = () ->
    $http.get '/api/auth/logout'
      .then () ->
        $http.defaults.headers.common['api_key'] = ''

        $rootScope.authId = null
        $rootScope.authEmail = null
        $rootScope.authFistName = ''
        $rootScope.authLastName = null
        $rootScope.authType = null

        $rootScope.sidebar = [{}]
        $rootScope.isAuth = false

  auth
