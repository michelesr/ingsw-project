controllers.controller 'CatalogCtrl', ($scope, Catalog, Meta) ->

  $scope.productsCount = 0
  $scope.stocksCount = 0

  $scope.export = ->

    console.log 'ciao'
    Catalog.export (res) ->
      console.log(res)
      console.log 'ciao'
    return
