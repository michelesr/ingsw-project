mainServices = angular.module 'mainServices', ['ngResource']


mainServices.factory 'User', ($resource) ->
  $resource '/api/user/:userId/:action', {}, {
    query: {
      method: 'GET'
      params: { userId: '', action: 'list' }
      isArray: true
    }
  }


mainServices.factory 'Product', ($resource) ->
  $resource '/api/product/:productId/:action', {}, {
    query: {
      method: 'GET'
      params: { productId: '', action: 'list' }
      isArray: true
    }
  }
