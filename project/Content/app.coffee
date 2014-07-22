mainApp = angular.module 'mainApp', [
  'ngRoute'
  'mainCtrl'
]

mainApp.config ['$routeProvider'
  ($routeProvider) ->
    $routeProvider

      .when '/product/list',
        templateUrl: 'Content/product_list.html'
        controller: 'ProductListCtrl'

      .when '/product/:productId/detail',
        templateUrl: 'Content/product_detail.html'
        controller: 'ProductDetailCtrl'

      .otherwise
        redirectTo: '/'
    0
]
