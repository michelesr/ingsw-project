// Generated by CoffeeScript 1.7.1
(function() {
  var mainServices;

  mainServices = angular.module('mainServices', ['ngResource']);

  mainServices.factory('Product', function($resource) {
    return $resource('api/product/:productId', {}, {
      query: {
        method: 'GET',
        params: {
          productId: 'list'
        },
        isArray: true
      }
    });
  });

}).call(this);
