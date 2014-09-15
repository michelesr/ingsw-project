controllers.controller 'ProductCtrl', ($scope, $state, User, Category, Product, Meta) ->

  list = ->
    $scope.meta = _.cloneDeep(Meta.product)
    rfList = {}
    User.list (supplierList) ->
      rfList['supplier'] = supplierList
      Category.list (categoryList) ->
        rfList['category'] = categoryList
        Product.list (list) ->
          $scope.list = list
          $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]
          for res in list
            for rf in $scope.meta.related_fields
              for rfElem in rfList[rf.model]
                if rfElem.id == res[rf.related_model]
                  res[rf.related_model] = rfElem


  $scope.addForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.product)

    rfList = {}
    User.list (supplierList) ->
      rfList['supplier'] = supplierList
      Category.list (categoryList) ->
        rfList['category'] = categoryList
        for rf in $scope.meta.related_fields
          rf.values = rfList[rf.model]

        $state.go '^.add'


  $scope.add = (fields) ->
    $scope.resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      $scope.resource[k] = v
    for rf in $scope.meta.related_fields
      if _.has(rf, 'value')
        k = rf['related_model']
        v = _.parseInt(rf['value'])
        $scope.resource[k] = v
    Product.add $scope.resource, (res) ->
      list()
      $scope.msgSuccess = 'Added successfully'
      $state.go '^.list'


  $scope.detail = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    rfList = {}
    User.list (supplierList) ->
      rfList['supplier'] = supplierList
      Category.list (categoryList) ->
        rfList['category'] = categoryList
        Product.detail {id: id}, (resource) ->
          for f in $scope.meta.fields
            k = f['model']
            f['value'] = resource[k]
          for rf in $scope.meta.related_fields
            k = rf['model']
            rf['value'] = resource[k]
          for rf in $scope.meta.related_fields
            for rfElem in rfList[rf.model]
              if rfElem.id == resource[rf.related_model]
                rf[rf.related_model] = rfElem

          $state.go '^.detail', {id: id}


  $scope.editForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.product)

    rfList = {}
    User.list (supplierList) ->
      rfList['supplier'] = supplierList
      Category.list (categoryList) ->
        rfList['category'] = categoryList
        for rf in $scope.meta.related_fields
          rf.values = rfList[rf.model]

    $state.go '^.edit', {id: $state.params.id}


  $scope.edit = (fields) ->
    resource = {}
    for f in fields
      k = f['model']
      v = f['value']
      resource[k] = v
    Product.update {id: $state.params.id}, resource, (res) ->
      $scope.result = res
      list()
      $scope.msgSuccess = 'Updated successfully'
      $state.go '^.list'


  $scope.remove = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    Product.remove {id: id}, (res) ->
      $scope.msgSuccess = 'Removed successfully'
      list()


  list()
