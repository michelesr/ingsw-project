services = angular.module 'services', ['ngResource']


services.factory 'User', ($resource) ->
  $resource '/api/users/:action/:id', {}, {
    list: { method: 'GET', params: { action: 'index', id: -1 }, isArray: true }
    add: { method: 'POST', params: { action: 'add', id: -1 } }
  }


services.factory 'Product', ($resource, AuthService) ->
  $resource '/api/products/:action/:id', {}, {
    list: { method: 'GET', params: { action: 'index', id: -1 }, isArray: true }
    add: { method: 'POST', params: { action: 'add', id: -1 } }
  }
