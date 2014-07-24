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
        templateUrl: 'Content/templates/main.html'
        controller: 'MainCtl'

      .when '/product/list',
        templateUrl: 'Content/templates/product_list.html'
        controller: 'ProductListCtl'

      .when '/product/:productId/detail',
        templateUrl: 'Content/templates/product_detail.html'
        controller: 'ProductDetailCtl'

      .when '/product/:productId/create',
        templateUrl: 'Content/templates/product_form.html'
        controller: 'ProductCreateCtl'

      .otherwise
        redirectTo: '/'
]
