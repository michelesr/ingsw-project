mainApp = angular.module 'mainApp', [
  'ngRoute'
  'ui.boostrap'
  'mainCtls'
  'mainServices'
]

mainApp.config ['$routeProvider'
  ($routeProvider) ->
    $routeProvider

      .when '/',
        templateUrl: 'Content/templates/main.html'
        controller: 'MainCtl'

      .when '/product/list',
        templateUrl: 'Content/templates/product_list.html'
        controller: 'ProductListCtl'

      .when '/product/:productId',
        templateUrl: 'Content/templates/product_detail.html'
        controller: 'ProductDetailCtl'

      .otherwise
        redirectTo: '/'
]
