services.factory 'Catalog', ($resource) ->

  $resource '/api/catalog/:action', {},

    export:
      method: 'GET'
      params:
        action: 'index'
