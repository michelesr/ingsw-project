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

      .when '/users/:resourceId/detail',
        templateUrl: 'Content/templates/user_detail.html'
        controller: 'UserDetailCtl'

      .when '/users/:resourceId/create',
        templateUrl: 'Content/templates/user_form.html'
        controller: 'UserCreateCtl'


      .when '/products/list',
        templateUrl: 'Content/templates/resource_list.html'
        controller: 'ProductListCtl'

      .when '/products/:resourceId/detail',
        templateUrl: 'Content/templates/product_detail.html'
        controller: 'ProductDetailCtl'

      .when '/products/:resourceId/create',
        templateUrl: 'Content/templates/product_form.html'
        controller: 'ProductCreateCtl'


      .otherwise
        redirectTo: '/'
]
