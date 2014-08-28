controllers.controller 'LoginCtrl', ($scope, $rootScope, AuthService) ->

  if $rootScope.debug
    $scope.credentials =
      email: 'admin@example.org'
      password: 'admin'
  else
    $scope.credentials =
      email: ''
      password: ''

  $scope.msg = ''
  $scope.auth = ''
  $scope.master = {}
#  $scope.sidebar = []


  $scope.login = (credentials) ->
    AuthService.login(credentials)
      .then (res) ->
        $scope.user = User.detail(res.user_id)
        $scope.setCurrentUser(res.user_id)
        switch Session.user_type
          when 'admin' then $state.go 'root.admin'
          when 'supplier' then $state.go 'root.supplier'
          else $state.go 'root.login'

    $scope.master = $scope.credentials


#    switch AuthService.authData.type
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
