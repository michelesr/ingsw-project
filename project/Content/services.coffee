mainServices = angular.module 'mainServices', ['ngResource']


mainServices.factory 'User', ($resource) ->
  $resource '/api/users/:action/:id', {}, {
    query: {
      method: 'GET'
      params: { id: '', action: 'list' }
      isArray: true
    }
  }


mainServices.factory 'Product', ($resource) ->
  $resource '/api/products/:action/:id', {}, {
    list: { method: 'GET', params: { action: 'list' }, isArray: true }
    add: { method: 'POST', params: { action: 'add' } }
  }
