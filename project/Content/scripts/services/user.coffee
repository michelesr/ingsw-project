services.factory 'User', ($resource) ->

  $resource '/api/users/:action/:id', {},

    list:
      method: 'GET'
      params:
        action: 'index'
        id: -1
      isArray: true

    listSupplier:
      method: 'GET'
      params:
        action: 'indexsupplier'
        id: -1
      isArray: true

    listSession:
      method: 'GET'
      params:
        action: 'sessions'
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
