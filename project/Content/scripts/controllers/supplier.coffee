controllers.controller 'SupplierCtrl', ($scope, $rootScope, $state, City, User, Meta) ->

  list = ->
    $scope.meta = _.cloneDeep(Meta.supplier)

    # Get resource lists
    City.list (cityList) ->
      User.listSupplier (list) ->
        $scope.list = (user for user in list when user.type == 0)
        lists =
          city: cityList

        # Check whether there is no elements in supplier list
        $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]


        # Resolve the data relations and put into $scope
        for res in $scope.list
          for rf in $scope.meta.related_fields
            for rfElem in lists[rf.model]
              if rfElem.id == res[rf.related_model]
                res[rf.related_model] = rfElem


  $scope.addForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.supplier)

    # Get resource lists
    City.list (cityList) ->

      lists =
        city: cityList

      # Put relational data into $scope
      for rf in $scope.meta.related_fields
        rf.values = lists[rf.model]

      # Put fields (only for add) into $scope (password in this case)
      if _.has($scope.meta, 'extra_fields')
        for ef in $scope.meta.extra_fields
          $scope.meta.fields.push(ef)

      # Move to add form page
      $state.go '^.add'


  $scope.add = ->
    resource =
      type: 'supplier'
      user_data: {}
      supplier_data: {}

    # Gather date of resource to add
    for f in $scope.meta.fields

      # Put the params in the corrent sub-dictionary
      if _.has(f, 'supplier') and f.supplier == true
        resource.supplier_data[f.model] = f.value
      else
        resource.user_data[f.model] = f.value

    # Gather relational data of resource to add
    for rf in $scope.meta.related_fields
      if _.has(rf, 'value')
        k = rf.related_model
        v = _.parseInt(rf.value)

        # Put the params in the corrent sub-dictionary
        if _.has(rf, 'supplier') and rf.supplier == true
          resource.supplier_data[k] = v
        else
          resource.user_data[k] = v

    # Add the resource and return to list page
    User.add resource, (res) ->
      list()
      $scope.msgError = ''
      $scope.msgSuccess = 'Added successfully'
      $state.go '^.list'
    , ->
      $scope.msgError = 'Adding error'


  $scope.detail = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Get supplier data
    User.listSession (sessions) ->
      City.list (cityList) ->
        User.detail {id: id}, (resource) ->

          lists =
            city: cityList

          # Gather data of resource
          for f in $scope.meta.fields
            f.value = resource[f.model]

          # Resolve the data relations and put into $scope
          for rf in $scope.meta.related_fields
            rf.value = resource[rf.model]
            for rfElem in lists[rf.model]
              if rfElem.id == resource[rf.related_model]
                rf[rf.related_model] = rfElem

          $scope.sessions = []
          for s in sessions
            if s.user_id == id
              $scope.sessions.push(s)

          # Move to detail page
          $state.go '^.detail', {id: id}


  $scope.editForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.supplier)

    # Get supplier data and put into $scope
    City.list (cityList) ->
      User.detail {id: $state.params.id}, (resource) ->

        lists =
          city: cityList

        # Gather data of resource
        for f in $scope.meta.fields
          f.value = resource[f.model]

        # Resolve the data relations and put into $scope
        for rf in $scope.meta.related_fields
          rf.value = resource[rf.model]
          rf.values = lists[rf.model]

        # Move to edit form page
        $state.go '^.edit', {id: $state.params.id}


  $scope.edit = ->
    resource =
      user_data: {}
      supplier_data: {}

    # Gather data of resource to edit
    for f in $scope.meta.fields

      # Put the params in the corrent sub-dictionary
      if _.has(f, 'supplier') and f.supplier == true
        resource.supplier_data[f.model] = f.value
      else
        resource.user_data[f.model] = f.value

    # Gather relational data of resource to edit
    for rf in $scope.meta.related_fields

      # Put the params in the corrent sub-dictionary
      if _.has(rf, 'supplier') and rf.supplier == true
        resource.supplier_data[rf.model] = rf.value
      else
        resource.user_data[rf.model] = rf.value


    # Update the supplier
    User.update {id: $state.params.id}, resource, (res) ->
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

    # Remove the supplier and return to list page
    User.remove {id: id}, (res) ->
      $scope.msgError = ''
      $scope.msgSuccess = 'Removed successfully'
      list()

    , ->
      $scope.msgError = 'Removing error'


  # By default, load the list data
  list()
