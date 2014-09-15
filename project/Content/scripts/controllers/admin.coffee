controllers.controller 'AdminCtrl', ($scope, $state, User, Meta) ->

  list = ->
    $scope.meta = _.cloneDeep(Meta.admin)

    # Get resource lists
    User.list (list) ->
      $scope.list = (user for user in list when user.type == 1)
      $scope.empty = $scope.list.length <= 1 and _.isEmpty $scope.list[0]


  $scope.addForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.admin)

    # Put fields (only for add) into $scope (password in this case)
    if _.has($scope.meta, 'extra_fields')
      for ef in $scope.meta.extra_fields
        $scope.meta.fields.push(ef)

    # Move to add form page
    $state.go '^.add'


  $scope.add = ->
    resource = {}

    # Gather date of resource to add
    for f in $scope.meta.fields
      resource[f.model] = f.value

    # Add the resource and return to list page
    User.add resource, (res) ->
      list()
      $state.go '^.list'


  $scope.detail = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Get admin data
    User.detail {id: id}, (admin) ->

      # Gather data of admin
      for f in $scope.meta.fields
        f.value = admin[f.model]

      # Move to detail page
      $state.go '^.detail', {id: id}


  $scope.editForm = ->
    $scope.msgSuccess = ''
    $scope.msgError = ''
    $scope.meta = _.cloneDeep(Meta.admin)

    # Get admin data and put into $scope
    User.detail {id: $state.params.id}, (admin) ->
      for f in $scope.meta.fields
        f.value = admin[f.model]

      # Move to edit form page
      $state.go '^.edit', {id: $state.params.id}


  $scope.edit = ->
    resource = {}

    # Gather data of resource to edit
    for f in $scope.meta.fields
      resource[f.model] = f.value

    # Update the admin
    User.update {id: $state.params.id}, resource, (res) ->
#      $scope.result = res
      list()
      $scope.msgSuccess = 'Updated successfully'

      # Return to list page
      $state.go '^.list'


  $scope.remove = (id) ->
    $scope.msgSuccess = ''
    $scope.msgError = ''

    # Remove the admin and return to list page
    User.remove {id: id}, (res) ->
      $scope.msgSuccess = 'Removed successfully'
      list()


  # By default, load the list data
  list()
