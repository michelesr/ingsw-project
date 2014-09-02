services.factory 'User', ($resource) ->

  $resource '/api/users/:action/:id', {},

    list:
      method: 'GET'
      params:
        action: 'index'
        id: 0
      isArray: true

    add:
      method: 'POST'
      params:
        action: 'add'
        id: -1

    detail:
      method: 'GET'
      params:
        action: 'detail'

    update:
      method: 'POST'
      params:
        action: 'update'

    delete:
      method: 'POST'
      params:
        action: 'delete'
