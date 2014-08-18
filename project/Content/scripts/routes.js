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
            templateUrl: 'Content/partials/user_form.html'
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
