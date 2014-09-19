controllers.controller 'StockCtrl', ($scope, $rootScope, $state, Product, Stock, Meta) ->

  list = ->
    $scope.meta = _.cloneDeep(Meta.stock)

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
#          $scope.list = stockList

        # Check whether there is no elements in stock list
        $scope.empty = $scope.list.length <= 1 and _.isEmpty($scope.list[0])

        # Resolve the data relations and put into every stock
        for res in $scope.list
          for rf in $scope.meta.related_fields
            for rfElem in lists[rf.model]
              if rfElem.id == res[rf.related_model]
                res[rf.related_model] = rfElem


  $scope.addForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.stock)

    # Get resource lists
    Product.list (allProductList) ->

      lists =
        product: []

      # Show only products of this supplier
      for prod in allProductList
        if prod.supplier_id == $rootScope.authSupplierId
          lists.product.push(prod)

      # Put relational data into $scope
      for rf in $scope.meta.related_fields
        rf.values = lists[rf.model]

      # Move to add form page
      $state.go '^.add'


  $scope.add = ->
    resource = {}

    # Gather date of resource to add
    for f in $scope.meta.fields
      k = f.model
      # Price decimal/integer conversion
      v = if k == 'price' then f.value * 100 else f.value
      resource[k] = v

    # Gather relational data of resource to add
    for rf in $scope.meta.related_fields
      if _.has(rf, 'value')
        k = rf.related_model
        v = _.parseInt(rf.value)
        resource[k] = v

    # Add the resource and return to list page
    Stock.add resource, (res) ->
      list()
      $scope.msgError = ''
      $scope.msgSuccess = 'Added successfully'
      $state.go '^.list'

    , ->
      $scope.msgError = 'Adding error'


  $scope.detail = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Get related resource lists and stock data
    Stock.detail {id: id}, (resource) ->
      Product.list (productList) ->

        lists =
          product: productList

        # Gather data of stock
        for f in $scope.meta.fields
          k = f.model
          f.value = resource[k]

        # Resolve the data relations and put into every stock
        for rf in $scope.meta.related_fields
          k = rf.model
          rf.value = resource[k]
          for rfElem in lists[rf.model]
            if rfElem.id == resource[rf.related_model]
              rf[rf.related_model] = rfElem

        # Move to detail page
        $state.go '^.detail', {id: id}


  $scope.editForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.stock)

    # Get resource lists and stock data
    Stock.detail {id: $state.params.id}, (resource) ->
      Product.list (productList) ->

        lists =
          product: productList

        # Gather data of stock
        for f in $scope.meta.fields
          k = f.model
          # Price decimal/integer conversion
          f.value = if k == 'price' then resource[k] / 100 else resource[k]

        # Resolve the data relations and put into every stock
        for rf in $scope.meta.related_fields
          k = rf.model
          rf.value = resource[k]
          rf.values = lists[rf.model]
          for rfElem in lists[rf.model]
            if rfElem.id == resource[rf.related_model]
              rf.value = rfElem.id

        # Move to edit form page
        $state.go '^.edit', {id: $state.params.id}


  $scope.edit = ->
    resource = {}

    # Gather data of resource to edit
    for f in $scope.meta.fields
      k = f.model
      # Price decimal/integer conversion
      v = if k == 'price' then f.value * 100 else f.value
      resource[k] = v

    # Gather relational data of resource to edit
    for rf in $scope.meta.related_fields
      if _.has(rf, 'value')
        k = rf.related_model
        v = _.parseInt(rf.value)
        resource[k] = v

    # Update the resource
    Stock.update {id: $state.params.id}, resource, (res) ->
      list()
      $scope.msgError = ''
      $scope.msgSuccess = 'Updated successfully'

      # Return to list page
      $state.go '^.list'

    , ->
      $scope.msgError = 'Updating error'


  $scope.remove = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Remove the resource and return to list page
    Stock.remove {id: id}, (res) ->
      $scope.msgError = ''
      $scope.msgSuccess = 'Removed successfully'
      list()

    , ->
      $scope.msgError = 'Removing error'


  # By default, load the list data
  list()
