mainServices = angular.module 'mainServices', ['ngResource']


mainServices.factory 'User', ($resource) ->
  $resource '/api/users/:action/:id', {}, {
    list: { method: 'GET', params: { action: 'index', id: -1 }, isArray: true }
    add: { method: 'POST', params: { action: 'add', id: -1 } }
  }


mainServices.factory 'Product', ($resource) ->
  $resource '/api/products/:action/:id', {}, {
    list: { method: 'GET', params: { action: 'index', id: -1 }, isArray: true }
    add: { method: 'POST', params: { action: 'add', id: -1 } }
  }