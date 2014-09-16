controllers.controller 'CatalogCtrl', ($scope, $rootScope, $http, Stock, Product, Catalog, Meta) ->

  count = ->
    # Get resource lists
    Stock.list (stockList) ->
      Product.list (productList) ->

        $scope.list = []
        lists =
          product: (prod for prod in productList when prod.supplier_id == $rootScope.authSupplierId)
        for prod in lists.product
          for stock in stockList
            if stock.product_id == prod.id
              $scope.list.push(stock)

        # Check whether there is no elements in stock list
        if $scope.list.length <= 1 and _.isEmpty($scope.list[0])
          $scope.stocksCount = 0
        else
          $scope.stocksCount = $scope.list.length

        # Check whether there is no elements in product list
        if lists.product.length <= 1 and _.isEmpty(lists.product[0])
          $scope.productsCount = 0
        else
          $scope.productsCount = lists.product.length

  count()

  $scope.export = ->

    console.log 'ciao'
    Catalog.export {id: $rootScope.authSupplierId}, (res)->
      console.log 'ciao2'
      console.log(res)
      console.log 'ciao3'

  $scope.export2 = ->
    $http.get('/api/catalogs/detail/' + $rootScope.authSupplierId)
