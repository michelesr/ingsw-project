controllers.controller 'CityCtrl', ($scope, $state, City, Meta) ->

  list = ->
    $scope.meta = _.cloneDeep(Meta.city)

    # Get resource list
    City.list (cityList) ->
      $scope.list = cityList

      # Check whether there is no elements in city list
      $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]


  $scope.addForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.city)

    # Move to add form page
    $state.go '^.add'


  $scope.add = ->
    $scope.resource = {}

    # Gather date of resource to add
    for f in $scope.meta.fields
      k = f.model
      v = f.value
      $scope.resource[k] = v

    # Add the resource and return to list page
    City.add $scope.resource, (res) ->
      list()
      $scope.msgError = ''
      $scope.msgSuccess = 'Added successfully'

      # Return to list page
      $state.go '^.list'

    , ->
      $scope.msgError = 'Adding error'


  $scope.detail = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Get city data
    City.detail {id: id}, (res) ->

      # Gather data of city
      for f in $scope.meta.fields
        k = f.model
        f.value = res[k]

      # Move to detail page
      $state.go '^.detail', {id: id}


  $scope.editForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.city)

    # Get city data
    City.detail {id: $state.params.id}, (resource) ->

      # Gather data of city
      for f in $scope.meta.fields
        k = f.model
        f.value = resource[k]

      # Move to edit form page
      $state.go '^.edit', {id: $state.params.id}


  $scope.edit = ->
    resource = {}

    # Gather data of resource to edit
    for f in $scope.meta.fields
      k = f.model
      v = f.value
      resource[k] = v

    # Update the resource
    City.update {id: $state.params.id}, resource, (res) ->
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
    City.remove {id: id}, (res) ->
      $scope.msgError = ''
      $scope.msgSuccess = 'Removed successfully'
      list()

    , ->
      $scope.msgError = 'Removing error'


  # By default, load the list data
  list()
