// Generated by CoffeeScript 1.7.1
(function() {
  var mainServices;

  mainServices = angular.module('mainServices', ['ngResource']);

  mainServices.factory('User', function($resource) {
    return $resource('/api/users/:action/:resourceId', {}, {
      query: {
        method: 'GET',
        params: {
          resourceId: '',
          action: 'list'
        },
        isArray: true
      }
    });
  });

  mainServices.factory('Product', function($resource) {
    return $resource('/api/products/:resourceId/:action', {}, {
      query: {
        method: 'GET',
        params: {
          resourceId: '',
          action: ''
        },
        isArray: true
      }
    });
  });

}).call(this);
