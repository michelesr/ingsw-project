services.factory 'AuthService', ($http, Session) ->
  authService = {}
  authService.login = (credentials) ->
    $http.post '/api/auth', credentials
      .then (res) ->
        console.log(res.api_key)
        me = User.detail({ id: res.user_id })
        Session.create(me, res.api_key)

  authService.logout = () ->
    Session.destroy()

  authService.isAuthenticated = () ->
    !!Session.me.user_id

  authService
