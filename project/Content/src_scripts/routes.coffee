app.config (stateHelperProvider, $urlRouterProvider) ->
  $urlRouterProvider
    .otherwise('/error')

  stateHelperProvider.setNestedState
    name: 'root'
    template: '<ui-view/>'
    abstract: true
    children: [

    # General ---------------------------------------------
      name: 'error'
      url: '/error'
      templateUrl: 'Content/partials/error.html'
    ,
      name: 'home'
      url: '/'
      templateUrl: 'Content/partials/home.html'
      controller: 'HomeCtrl'
    ,
      name: 'login'
      url: '/login'
      templateUrl: 'Content/partials/login.html'
      controller: 'LoginCtrl'
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
        templateUrl: 'Content/partials/resource.list.html'
      ,
        name: 'add'
        url: '/add'
        templateUrl: 'Content/partials/user_form.html'
        controller: 'UserCtrl'
      ,
        name: 'detail'
        url: '/detail/:id'
        templateUrl: 'Content/partials/resource.detail.html'
        controller: 'UserDetailCtrl'
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
        templateUrl: 'Content/partials/resource.list.html'
      ,
        name: 'add'
        url: '/add'
        templateUrl: 'Content/partials/product_form.html'
      ,
        name: 'detail'
        url: '/detail/:id'
        templateUrl: 'Content/partials/resource.detail.html'
      ]

    # End -------------------------------------------------
    ]
