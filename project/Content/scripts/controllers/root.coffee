controllers.controller 'RootCtrl', ($rootScope, $state) ->

  $rootScope.debug = true

  if $rootScope.debug == true
    $rootScope.$state = $state

  if not $rootScope.auth
    $state.go 'root.login'
