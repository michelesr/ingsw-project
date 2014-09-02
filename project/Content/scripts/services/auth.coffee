services.factory 'AuthService', ($http, User, Session) ->

  authService = {}

  authService.login = (credentials) ->
    $http.post '/api/auth', credentials
      .then (res_auth) ->
        console.log res_auth
        console.log res_auth.data
        console.log res_auth.data.api_key
        User.detail { id: res_auth.data.user_id }, (res_user) ->
          me = res_user
          console.log me
          Session.create(me, res_auth.data.api_key)

  authService.logout = () ->
    Session.destroy()

  authService.isAuthenticated = () ->
    !!Session.me.user_id

  authService
