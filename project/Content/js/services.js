// Generated by CoffeeScript 1.7.1
(function() {
  var mainServices;

  mainServices = angular.module('mainServices', ['ngResource']);

  mainServices.factory('Product', function($resource) {
    return $resource('/api/product/:productId/:action', {}, {
      query: {
        method: 'GET',
        params: {
          productId: '',
          action: 'list'
        },
        isArray: true
      }
    });
  });

  mainServices.factory('User', function($resource) {
    return $resource('/api/user/:userId/:action', {}, {
      query: {
        method: 'GET',
        params: {
          productId: '',
          action: 'list'
        },
        isArray: true
      }
    });
  });

}).call(this);
