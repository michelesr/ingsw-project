var app, controllers, services;

app = angular.module('app', ['ui.router', 'ui.router.stateHelper', 'ui.bootstrap', 'controllers', 'services']);

controllers = angular.module('controllers', []);

services = angular.module('services', ['ngResource']);

app.config(function(stateHelperProvider, $urlRouterProvider, $httpProvider) {
  $httpProvider.defaults.headers.post['Content-Type'] = '';
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
            templateUrl: 'Content/partials/resource/list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/resource/add.html',
            controller: 'UserAddCtrl'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource/detail.html',
            controller: 'UserDetailCtrl'
          }, {
            name: 'edit',
            url: '/edit/:id',
            templateUrl: 'Content/partials/resource/edit.html',
            controller: 'UserEditCtrl'
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
            templateUrl: 'Content/partials/resource/list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/resource/add.html',
            controller: 'ProductAddCtrl'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource/detail.html',
            controller: 'ProductDetailCtrl'
          }, {
            name: 'edit',
            url: '/edit/:id',
            templateUrl: 'Content/partials/resource/edit.html',
            controller: 'ProductEditCtrl'
          }
        ]
      }
    ]
  });
});
