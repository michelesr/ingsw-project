var app, controllers, services;

app = angular.module('app', ['ui.router', 'ui.router.stateHelper', 'ui.bootstrap', 'controllers', 'services']);

controllers = angular.module('controllers', []);

controllers.controller('RootCtrl', function($scope, $state, AuthService) {
  $scope.currentUser = null;
  return $scope.setCurrentUser = function(user) {
    return $scope.currentUser = user;
  };
});

app.config(function(stateHelperProvider, $urlRouterProvider) {
  $urlRouterProvider.when('', '/').otherwise('/error');
  return stateHelperProvider.setNestedState({
    name: 'root',
    template: '<ui-view/>',
    abstract: true,
    controller: 'RootCtrl',
    children: [
      {
        name: 'error',
        url: '/error',
        templateUrl: 'Content/partials/error.html'
      }, {
        name: 'home',
        url: '/',
        template: ''
      }, {
        name: 'login',
        url: '/login',
        templateUrl: 'Content/partials/login.html',
        controller: 'LoginCtrl'
      }, {
        name: 'users',
        url: '/users',
        abstract: true,
        template: '<ui-view/>',
        controller: 'UserCtrl',
        children: [
          {
            name: 'list',
            url: '',
            templateUrl: 'Content/partials/resource.list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/user_form.html'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource.detail.html'
          }
        ]
      }, {
        name: 'products',
        url: '/products',
        template: '<ui-view/>',
        controller: 'ProductCtrl',
        abstract: true,
        children: [
          {
            name: 'list',
            url: '',
            templateUrl: 'Content/partials/resource.list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/product_form.html'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource.detail.html'
          }
        ]
      }
    ]
  });
});

app.run(function($state) {
  return $state.transitionTo('root.login');
});

services = angular.module('services', ['ngResource']);

services.factory('User', function($resource) {
  return $resource('/api/users/:action/:id', {}, {
    list: {
      method: 'GET',
      params: {
        action: 'index',
        id: -1
      },
      isArray: true
    },
    add: {
      method: 'POST',
      params: {
        action: 'add',
        id: -1
      }
    }
  });
});

services.factory('Product', function($resource, AuthService) {
  return $resource('/api/products/:action/:id', {}, {
    list: {
      method: 'GET',
      params: {
        action: 'index',
        id: -1
      },
      isArray: true
    },
    add: {
      method: 'POST',
      params: {
        action: 'add',
        id: -1
      }
    }
  });
});
