var app, mainCtrl, mainServices;

app = angular.module('app', ['ui.router', 'ui.router.stateHelper', 'ui.bootstrap', 'mainCtrl', 'mainServices']);

mainCtrl = angular.module('mainCtrl', []);

mainCtrl.controller('RootCtrl', function($scope, $state) {
  $scope.auth = {};
  $scope.master = {};
  $scope.sidebar = [];
  $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = Product.add(newResource);
  };
  return $scope.login = function($scope) {
    if ($scope.auth.type === 'admin') {
      return $scope.sidebar = [
        {
          name: 'Users',
          state: 'root.users.list',
          icon: 'fa-users'
        }
      ];
    } else if ($scope.auth.type === 'supplier') {
      return $scope.sidebar = [
        {
          name: 'Products',
          state: 'root.products.list',
          icon: 'fa-coffee'
        }
      ];
    } else {
      return $scope.sidebar = [];
    }
  };
});

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
  $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = User.add(newResource);
  };
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
    controller: 'RootCtrl',
    children: [
      {
        name: 'error',
        url: '/error',
        templateUrl: 'Content/partials/error.html'
      }, {
        name: 'home',
        url: '/',
        templateUrl: 'Content/partials/home.html'
      }, {
        name: 'login',
        url: '/login',
        templateUrl: 'Content/partials/login.html'
      }, {
        name: 'admin',
        url: '/admin',
        templateUrl: 'Content/partials/admin.html'
      }, {
        name: 'supplier',
        url: '/supplier',
        templateUrl: 'Content/partials/supplier.html'
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
