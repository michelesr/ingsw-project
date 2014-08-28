services.service 'Session', ($http) ->
  this.create = (me, apiKey) ->
    this.user_id = me.user_id
    this.user_type = me.user_type
    $http.defaults.headers.common['api_key'] = apiKey

  this.destroy = () ->
    this.user_id = null
    this.user_type = null
    $http.defaults.headers.common['api_key'] = ''

  this
