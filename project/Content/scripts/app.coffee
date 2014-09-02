app = angular.module 'app', [
  'ui.router'
  'ui.router.stateHelper'
  'ui.bootstrap'
  'controllers'
  'services'
]

controllers = angular.module 'controllers', []
services = angular.module 'services', ['ngResource']

app.run ($state) ->
  $state.go 'root.login'
