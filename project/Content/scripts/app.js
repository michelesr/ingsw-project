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
        name: 'logout',
        url: '/logout',
        template: '<ui-view/>',
        controller: 'LogoutCtrl'
      }, {
        name: 'adminHome',
        url: '/adminhome',
        templateUrl: 'Content/partials/admin.html',
        controller: 'AdminHomeCtrl'
      }, {
        name: 'supplierHome',
        url: '/supplierhome',
        templateUrl: 'Content/partials/supplier.html',
        controller: 'SupplierHomeCtrl'
      }, {
        name: 'catalog',
        url: '/catalog',
        templateUrl: 'Content/partials/catalog.html',
        controller: 'CatalogCtrl'
      }, {
        name: 'admins',
        url: '/admins',
        abstract: true,
        template: '<ui-view/>',
        controller: 'AdminCtrl',
        children: [
          {
            name: 'list',
            url: '',
            templateUrl: 'Content/partials/resource/list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/resource/add.html'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource/detail.html'
          }, {
            name: 'edit',
            url: '/edit/:id',
            templateUrl: 'Content/partials/resource/edit.html'
          }
        ]
      }, {
        name: 'suppliers',
        url: '/suppliers',
        abstract: true,
        template: '<ui-view/>',
        controller: 'SupplierCtrl',
        children: [
          {
            name: 'list',
            url: '',
            templateUrl: 'Content/partials/resource/list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/resource/add.html'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource/detail.html'
          }, {
            name: 'edit',
            url: '/edit/:id',
            templateUrl: 'Content/partials/resource/edit.html'
          }
        ]
      }, {
        name: 'products',
        url: '/products',
        abstract: true,
        template: '<ui-view/>',
        controller: 'ProductCtrl',
        children: [
          {
            name: 'list',
            url: '',
            templateUrl: 'Content/partials/resource/list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/resource/add.html'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource/detail.html'
          }, {
            name: 'edit',
            url: '/edit/:id',
            templateUrl: 'Content/partials/resource/edit.html'
          }
        ]
      }, {
        name: 'categories',
        url: '/categories',
        abstract: true,
        template: '<ui-view/>',
        controller: 'CategoryCtrl',
        children: [
          {
            name: 'list',
            url: '',
            templateUrl: 'Content/partials/resource/list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/resource/add.html'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource/detail.html'
          }, {
            name: 'edit',
            url: '/edit/:id',
            templateUrl: 'Content/partials/resource/edit.html'
          }
        ]
      }, {
        name: 'stocks',
        url: '/stocks',
        abstract: true,
        template: '<ui-view/>',
        controller: 'StockCtrl',
        children: [
          {
            name: 'list',
            url: '',
            templateUrl: 'Content/partials/resource/list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/resource/add.html'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource/detail.html'
          }, {
            name: 'edit',
            url: '/edit/:id',
            templateUrl: 'Content/partials/resource/edit.html'
          }
        ]
      }, {
        name: 'cities',
        url: '/cities',
        abstract: true,
        template: '<ui-view/>',
        controller: 'CityCtrl',
        children: [
          {
            name: 'list',
            url: '',
            templateUrl: 'Content/partials/resource/list.html'
          }, {
            name: 'add',
            url: '/add',
            templateUrl: 'Content/partials/resource/add.html'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource/detail.html'
          }, {
            name: 'edit',
            url: '/edit/:id',
            templateUrl: 'Content/partials/resource/edit.html'
          }
        ]
      }
    ]
  });
});
