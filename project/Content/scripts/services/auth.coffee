services.factory 'AuthService', ($http, Session) ->
  authService = {}
  authService.login = (credentials) ->
    $http.post '/api/auth', credentials
      .then (res) ->
        $http.defaults.headers.common['api_key'] = res.api_key
        myself = User.detail(res.id)
        Session.create(res.user_id)
        res.user

  authService.isAuthenticated = () ->
    !!Session.userId

  authService
