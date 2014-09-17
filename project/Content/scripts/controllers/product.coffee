controllers.controller 'ProductCtrl', ($scope, $rootScope, $state, Category, Product, Meta) ->

  list = ->
    $scope.meta = _.cloneDeep(Meta.product)

    # Get resource lists
    Product.list (productList) ->
      Category.list (categoryList) ->

        $scope.list = (prod for prod in productList when prod.supplier_id == $rootScope.authSupplierId)
        lists =
          category: categoryList

        # Check whether there is no elements in product list
        $scope.empty = $scope.list.length <= 1 and _.isEmpty($scope.list[0])

        # Resolve the data relations and put into $scope
        for res in $scope.list
          for rf in $scope.meta.related_fields
            for elem in lists.category
              if elem.id == res[rf.related_model]
                res[rf.related_model] = elem


  $scope.addForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.product)

    # Get resource lists
    Category.list (categoryList) ->

      lists =
        category: categoryList

      # Put relational data into $scope
      for rf in $scope.meta.related_fields
        rf.values = lists[rf.model]

      # Move to add form page
      $state.go '^.add'


  $scope.add = ->
    resource =
      supplier_id: $rootScope.authSupplierId

    # Gather date of resource to add
    for f in $scope.meta.fields
      resource[f.model] = f.value

    # Gather relational data of resource to add
    for rf in $scope.meta.related_fields
      if _.has(rf, 'value')
        k = rf.related_model
        v = _.parseInt(rf.value)
        resource[k] = v

    # Add the resource and return to list page
    Product.add resource, (res) ->
      list()
      $scope.msgSuccess = 'Added successfully'
      $state.go '^.list'


  $scope.detail = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Get related resource lists and product data
    Product.detail {id: id}, (resource) ->
      Category.list (categoryList) ->

          lists =
            category: categoryList

          # Gather data of product
          for f in $scope.meta.fields
            f.value = resource[f.model]

          # Resolve the data relations and put into every product
          for rf in $scope.meta.related_fields
            rf.value = resource[rf.model]
            for elem in lists.category
              if elem.id == resource[rf.related_model]
                rf[rf.related_model] = elem

          # Move to detail page
          $state.go '^.detail', {id: id}


  $scope.editForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.product)

    # Get resource lists and product data
    Product.detail {id: $state.params.id}, (resource) ->
      Category.list (categoryList) ->

        lists =
          category: categoryList

        # Gather data of product
        for f in $scope.meta.fields
          f.value = resource[f.model]

        # Resolve the data relations and put into $scope
        for rf in $scope.meta.related_fields
          if rf.model == 'category'
            rf.value = resource[rf.model]
            rf.values = lists[rf.model]
            for elem in lists[rf.model]
              if elem.id == resource[rf.related_model]
                rf.value = elem.id
          else
            rf.value = $rootScope.authSupplierId

        # Move to edit form page
        $state.go '^.edit', {id: $state.params.id}


  $scope.edit = ->
    resource = {}

    # Gather data of resource to edit
    for f in $scope.meta.fields
      resource[f.model] = f.value

    # Gather relational data of resource to edit
    for rf in $scope.meta.related_fields
      if _.has(rf, 'value')
        k = rf.related_model
        v = _.parseInt(rf.value)
        resource[k] = v

    # Update the product
    Product.update {id: $state.params.id}, resource, (res) ->
      list()
      $scope.msgSuccess = 'Updated successfully'

      # Return to list page
      $state.go '^.list'


  $scope.remove = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Remove the product and return to list page
    Product.remove {id: id}, (res) ->
      $scope.msgSuccess = 'Removed successfully'
      list()


  # By default, load the list data
  list()
