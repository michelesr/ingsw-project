services.factory 'Auth', ($http, User, Session) ->

  auth = {}

  auth.login = (credentials) ->
    $http.post '/api/auth', credentials
      .then (res_auth) ->
        $http.defaults.headers.common['api_key'] = res_auth.data.api_key
        User.detail { id: res_auth.data.user_id }, (res_user) ->
          Session.create(res_user)

  auth.logout = () ->
    $http.defaults.headers.common['api_key'] = ''
    Session.destroy

  auth.isAuthenticated = () ->
    Session.auth

  auth
