app.config (stateHelperProvider, $urlRouterProvider, $httpProvider) ->

  $httpProvider.defaults.headers.post['Content-Type'] = ''

  $urlRouterProvider
    .when '', '/'
    .otherwise '/error'

  stateHelperProvider.setNestedState
    name: 'root'
    template: '<ui-view/>'
    abstract: true
    controller: 'RootCtrl'
    children: [

    # General ---------------------------------------------
      name: 'error'
      url: '/error'
      templateUrl: 'Content/partials/error.html'
    ,
      name: 'home'
      url: '/'
      template: ''
    ,
      name: 'about'
      url: '/about'
      templateUrl: 'Content/partials/about.html'
    ,
      name: 'login'
      url: '/login'
      templateUrl: 'Content/partials/login.html'
      controller: 'LoginCtrl'
    ,
      name: 'logout'
      url: '/logout'
      template: '<ui-view/>'
      controller: 'LogoutCtrl'
    ,
      name: 'admin'
      url: '/admin'
      templateUrl: 'Content/partials/admin.html'
      controller: 'AdminCtrl'
    ,
      name: 'supplier'
      url: '/supplier'
      templateUrl: 'Content/partials/supplier.html'
      controller: 'SupplierCtrl'
    ,

    # Users -----------------------------------------------
      name: 'users'
      url: '/users'
      abstract: true
      template: '<ui-view/>'
      controller: 'UserCtrl'
      children: [
        name: 'list'
        url: ''
        templateUrl: 'Content/partials/resource/list.html'
      ,
        name: 'add'
        url: '/add'
        templateUrl: 'Content/partials/resource/add.html'
        controller: 'UserAddCtrl'
      ,
        name: 'detail'
        url: '/detail/:id'
        templateUrl: 'Content/partials/resource/detail.html'
        controller: 'UserDetailCtrl'
      ,
        name: 'edit'
        url: '/edit/:id'
        templateUrl: 'Content/partials/resource/edit.html'
        controller: 'UserEditCtrl'
      ]
    ,

    # Products --------------------------------------------
      name: 'products'
      url: '/products'
      template: '<ui-view/>'
      controller: 'ProductCtrl'
      abstract: true
      children: [
        name: 'list'
        url: ''
        templateUrl: 'Content/partials/resource/list.html'
      ,
        name: 'add'
        url: '/add'
        templateUrl: 'Content/partials/resource/add.html'
        controller: 'ProductAddCtrl'
      ,
        name: 'detail'
        url: '/detail/:id'
        templateUrl: 'Content/partials/resource/detail.html'
        controller: 'ProductDetailCtrl'
      ,
        name: 'edit'
        url: '/edit/:id'
        templateUrl: 'Content/partials/resource/edit.html'
        controller: 'ProductEditCtrl'
      ]

    ]
