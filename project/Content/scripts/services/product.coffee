services.factory 'Product', ($resource) ->

  $resource '/api/products/:action/:id', {},

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
