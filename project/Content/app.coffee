mainApp = angular.module 'mainApp', [
  'ngRoute'
#  'ui.boostrap'
  'mainCtls'
  'mainServices'
]


mainApp.config ['$routeProvider'
  ($routeProvider) ->
    $routeProvider

      .when '/',
        templateUrl: 'Content/templates/home.html'
        controller: 'HomeCtl'


      .when '/users/list',
        templateUrl: 'Content/templates/resource_list.html'
        controller: 'UserListCtl'

      .when '/users/add',
        templateUrl: 'Content/templates/user_form.html'
        controller: 'UserAddCtl'

      .when '/users/detail/:id',
        templateUrl: 'Content/templates/resource_detail.html'
        controller: 'UserDetailCtl'


      .when '/products/list',
        templateUrl: 'Content/templates/resource_list.html'
        controller: 'ProductListCtl'

      .when '/products/add',
        templateUrl: 'Content/templates/product_form.html'
        controller: 'ProductAddCtl'

      .when '/products/detail/:id',
        templateUrl: 'Content/templates/resource_detail.html'
        controller: 'ProductDetailCtl'


      .otherwise
        redirectTo: '/'
]
