mainServices = angular.module 'mainServices', ['ngResource']


mainServices.factory 'User', ($resource) ->
  $resource '/api/users/:action/:resourceId', {}, {
    query: {
      method: 'GET'
      params: { resourceId: '', action: 'list' }
      isArray: true
    }
  }


mainServices.factory 'Product', ($resource) ->
  $resource '/api/products/:resourceId/:action', {}, {
    query: {
      method: 'GET'
      params: { resourceId: '', action: '' }
      isArray: true
    }
  }
