app.config (stateHelperProvider, $urlRouterProvider) ->
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
      name: 'login'
      url: '/login'
      templateUrl: 'Content/partials/login.html'
      controller: 'LoginCtrl'
    ,
      name: 'admin'
      url: '/admin'
      templateUrl: 'Content/partials/admin.html'
    ,
      name: 'supplier'
      url: '/supplier'
      templateUrl: 'Content/partials/supplier.html'
    ,
#      name: 'sidebar'
#      url: '/supplier'
#      templateUrl: 'Content/partials/supplier.html'
#      controller: 'SidebarCtrl'
#    ,

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
      ,
        name: 'detail'
        url: '/detail/:id'
        templateUrl: 'Content/partials/resource.detail.html'
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


app.run ($state) ->
  $state.transitionTo 'root.login'
