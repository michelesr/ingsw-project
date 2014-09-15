controllers.controller 'ProductCtrl', ($scope, $state, User, Category, Product, Meta) ->

  list = ->
    $scope.meta = _.cloneDeep(Meta.product)

    # Get resource lists
    Product.list (productList) ->
      Category.list (categoryList) ->
        User.list (supplierList) ->

          $scope.list = productList
          lists =
            category: categoryList
            supplier: supplierList

          # Check whether there is no elements in product list
          $scope.empty = $scope.list.length <= 1 and _.isEmpty($scope.list[0])

          # Resolve the data relations and put into every product
          for res in $scope.list
            for rf in $scope.meta.related_fields
              for rfElem in lists[rf.model]
                if rfElem.id == res[rf.related_model]
                  res[rf.related_model] = rfElem


  $scope.addForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.product)

    # Get resource lists
    Category.list (categoryList) ->
      User.list (supplierList) ->

        lists =
          category: categoryList
          supplier: supplierList

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
      v = f.value
      resource[k] = v

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
        User.list (supplierList) ->

          lists =
            supplier: supplierList
            category: categoryList

          # Gather data of product
          for f in $scope.meta.fields
            k = f.model
            f.value = resource[k]

          # Resolve the data relations and put into every product
          for rf in $scope.meta.related_fields
            k = rf.model
            rf.value = resource[k]
            for rfElem in lists[rf.model]
              if rfElem.id == resource[rf.related_model]
                rf[rf.related_model] = rfElem

          # Move to detail page
          $state.go '^.detail', {id: id}


  $scope.editForm = () ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.product)

    # Get resource lists
    Product.detail {id: $state.params.id}, (resource) ->
      Category.list (categoryList) ->
        User.list (supplierList) ->

          lists =
            supplier: supplierList
            category: categoryList

          # Gather data of product
          for f in $scope.meta.fields
            k = f.model
            f.value = resource[k]

          # Resolve the data relations and put into every product
          for rf in $scope.meta.related_fields
            k = rf.model
            rf.value = resource[k]
            rf.values = lists[rf.model]
            for rfElem in lists[rf.model]
              if rfElem.id == resource[rf.related_model]
                rf.value = rfElem.id


    $state.go '^.edit', {id: $state.params.id}


  $scope.edit = () ->
    resource = {}

    # Gather data of resource to edit
    for f in $scope.meta.fields
      k = f.model
      v = f.value
      resource[k] = v

    # Gather relational data of resource to edit
    for rf in $scope.meta.related_fields
      if _.has(rf, 'value')
        k = rf.related_model
        v = _.parseInt(rf.value)
        resource[k] = v

    # Update the resource and return to list page
    Product.update {id: $state.params.id}, resource, (res) ->
#      $scope.result = res
      list()
      $scope.msgSuccess = 'Updated successfully'
      $state.go '^.list'


  $scope.remove = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Remove the resource and return to list page
    Product.remove {id: id}, (res) ->
      $scope.msgSuccess = 'Removed successfully'
      list()


  # By default, load the list data
  list()
