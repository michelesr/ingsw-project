services.factory 'Product', ($resource) ->

  $resource '/api/products/:action/:id', {},

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

    delete:
      method: 'GET'
      params:
        action: 'delete'
