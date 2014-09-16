controllers.controller 'CatalogCtrl', ($scope, $rootScope, Catalog, Meta) ->

  $scope.productsCount = 0
  $scope.stocksCount = 0

  $scope.export = ->

    console.log 'ciao'
    Catalog.export {id: $rootScope.authSupplierId}, (res) ->
      console.log 'ciao2'
      console.log(res)
      console.log 'ciao3'
    return
