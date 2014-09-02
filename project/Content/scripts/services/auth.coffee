services.factory 'AuthService', ($http, User, Session) ->

  authService = {}

  authService.login = (credentials) ->
    $http.post '/api/auth', credentials
      .then (res_auth) ->
        $http.defaults.headers.common['api_key'] = res_auth.data.api_key
        User.detail { id: res_auth.data.user_id }, (res_user) ->
          Session.create(res_user)

  authService.logout = () ->
    $http.defaults.headers.common['api_key'] = ''
    Session.destroy()

#  authService.isAuthenticated = () ->
#    !!Session.me.user_id

  authService
