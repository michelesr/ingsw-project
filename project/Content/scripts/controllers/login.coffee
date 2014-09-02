controllers.controller 'LoginCtrl', ($scope, $rootScope, Auth) ->

  if $rootScope.debug
    $scope.credentials =
      email: 'admin@example.org'
      password: 'admin'
#  else
#    $scope.credentials =
#      email: ''
#      password: ''

  $scope.login = (credentials) ->
    Auth.login(credentials)
      .then (res) ->
        switch Session.type
          when 'admin' then $state.go 'root.admin'
          when 'supplier' then $state.go 'root.supplier'
          else $state.go 'root.login'

#    switch Auth.authData.type
#      when 'admin'
#        $scope.sidebar = [
#          name: 'Users'
#          state: 'root.users.list'
#          icon: 'fa-users'
#        ]
#      when 'supplier'
#        $scope.sidebar = [
#          name: 'Products'
#          state: 'root.products.list'
#          icon: 'fa-coffee'
#        ]
#      else
#        $scope.sidebar = []

#    name: 'Overview'
#    state: 'root.home'
#    icon: 'fa-square'
