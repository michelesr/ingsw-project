var app, mainCtrl, mainServices;

app = angular.module('app', ['ui.router', 'ui.router.stateHelper', 'mainCtrl', 'mainServices']);

mainCtrl = angular.module('mainCtrl', []);

mainCtrl.controller('HomeCtrl', function($scope, $http) {
  return $scope.title = 'Web project';
});

mainCtrl.controller('LoginCtrl', function($scope, $http) {
  $scope.auth = {};
  $scope.master = {};
  return $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = Product.add(newResource);
  };
});

mainCtrl.controller('AdminCtrl', function($scope) {});

mainCtrl.controller('SupplierCtrl', function($scope) {});

mainCtrl.controller('UserCtrl', function($scope, $stateParams, User) {
  $scope.resourceMeta = {
    name: 'user',
    namePlural: 'users',
    icon: 'fa-users'
  };
  $scope.resourceList = User.list();
  $scope.fields = ['id', 'email', 'first_name', 'last_name'];
  $scope.newResource = {};
  $scope.master = {};
  $scope.fields = [
    {
      name: 'email',
      type: 'email',
      ph: 'user@example.org',
      isRequired: true
    }, {
      name: 'password',
      type: 'password',
      ph: 'password',
      isRequired: true
    }, {
      name: 'first_name',
      type: 'text',
      ph: 'Mario',
      isRequired: false
    }, {
      name: 'last_name',
      type: 'text',
      ph: 'Rossi',
      isRequired: false
    }
  ];
  return $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = User.add(newResource);
  };
});

mainCtrl.controller('UserDetailCtrl', function($scope, $stateParams, User) {
  return $scope.user = User.get({
    action: 'detail',
    id: $stateParams.id
  });
});

mainCtrl.controller('ProductCtrl', function($scope, $stateParams, Product) {
  $scope.resourceMeta = {
    name: 'product',
    namePlural: 'products',
    icon: 'fa-coffee'
  };
  $scope.resourceList = Product.list();
  $scope.fields = ['id', 'name'];
  $scope.newResource = {};
  $scope.master = {};
  $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = Product.add(newResource);
  };
  return $scope.resource = Product.get({
    action: 'detail',
    id: $stateParams.id
  });
});

app.config(function(stateHelperProvider, $urlRouterProvider) {
  $urlRouterProvider.otherwise('/error');
  return stateHelperProvider.setNestedState({
    name: 'root',
    template: '<ui-view/>',
    abstract: true,
    children: [
      {
        name: 'error',
        url: '/error',
        templateUrl: 'Content/partials/error.html'
      }, {
        name: 'home',
        url: '/',
        templateUrl: 'Content/partials/home.html',
        controller: 'HomeCtrl'
      }, {
        name: 'login',
        url: '/login',
        templateUrl: 'Content/partials/login.html',
        controller: 'LoginCtrl'
      }, {
        name: 'admin',
        url: '/admin',
        templateUrl: 'Content/partials/admin.html',
        controller: 'AdminCtrl'
      }, {
        name: 'supplier',
        url: '/supplier',
        templateUrl: 'Content/partials/supplier.html',
        controller: 'SupplierCtrl'
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
            templateUrl: 'Content/partials/user_form.html',
            controller: 'UserCtrl'
          }, {
            name: 'detail',
            url: '/detail/:id',
            templateUrl: 'Content/partials/resource.detail.html',
            controller: 'UserDetailCtrl'
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

mainServices = angular.module('mainServices', ['ngResource']);

mainServices.factory('User', function($resource) {
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

mainServices.factory('Product', function($resource) {
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
