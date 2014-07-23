mainServices = angular.module 'mainServices', ['ngResource']

mainServices.factory 'Product', ($resource) ->
  $resource 'api/product/:productId', {}, {
    query: {
      method: 'GET'
      params: { productId: 'list' }
      isArray: true
    }
  }
