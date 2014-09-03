services.factory 'AuthAPI', ($resource) ->

  $resource '/api/auth/:action', {},

    login:
      method: 'POST'
      params:
        action: ''

    logout:
      method: 'GET'
      params:
        action: 'logout'
