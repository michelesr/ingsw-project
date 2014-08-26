controllers = angular.module 'controllers', []


controllers.controller 'RootCtrl', ($scope, $state, AuthService) ->

  $scope.currentUser = null

  $scope.setCurrentUser = (user) ->
    $scope.currentUser = user


#  $scope.add = (newResource) ->
#    $scope.master = angular.copy(newResource)
#    $scope.newResource = Product.add(newResource)
