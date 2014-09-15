services.factory 'City', ($resource) ->

  $resource '/api/cities/:action/:id', {},

    list:
      method: 'GET'
      params:
        action: 'index'
        id: -1
      isArray: true

    add:
      method: 'POST'

    detail:
      method: 'GET'
      params:
        action: 'detail'

    update:
      method: 'POST'
      params:
        action: 'update'

    remove:
      method: 'GET'
      params:
        action: 'delete'
