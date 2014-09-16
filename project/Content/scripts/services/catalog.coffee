services.factory 'Catalog', ($resource) ->

  $resource '/api/catalogs/:action/:id', {},

    export:
      method: 'GET'
      params:
        action: 'detail'
