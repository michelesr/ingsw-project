controllers.controller('LoginCtrl', function($scope, $rootScope, AuthService) {
  $scope.msg = '';
  $scope.auth = '';
  $scope.master = {};
  $scope.credentials = {
    email: '',
    password: ''
  };
  $scope.credentials = {
    email: 'admin@example.org',
    password: 'admin'
  };
  return $scope.login = function(credentials) {
    AuthService.login(credentials).then(function(user) {
      return $scope.setCurrentUser(user);
    });
    return $scope.master = $scope.credentials;
  };
});

controllers.controller('ProductCtrl', function($scope, $stateParams, Product) {
  $scope.resourceMeta = {
    name: 'product',
    namePlural: 'products',
    icon: 'fa-coffee'
  };
  $scope.resourceList = Product.list();
  $scope.fields = ['id', 'name'];
  $scope.newResource = {};
  $scope.master = {};
  $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = Product.add(newResource);
  };
  return $scope.resource = Product.get({
    action: 'detail',
    id: $stateParams.id
  });
});

controllers.controller('UserCtrl', function($scope, $stateParams, User) {
  $scope.resourceMeta = {
    name: 'user',
    namePlural: 'users',
    icon: 'fa-users'
  };
  $scope.resourceList = User.list();
  $scope.fields = ['id', 'email', 'first_name', 'last_name'];
  $scope.newResource = {};
  $scope.master = {};
  $scope.fields = [
    {
      name: 'email',
      type: 'email',
      ph: 'user@example.org',
      isRequired: true
    }, {
      name: 'password',
      type: 'password',
      ph: 'password',
      isRequired: true
    }, {
      name: 'first_name',
      type: 'text',
      ph: 'Mario',
      isRequired: false
    }, {
      name: 'last_name',
      type: 'text',
      ph: 'Rossi',
      isRequired: false
    }
  ];
  $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = User.add(newResource);
  };
  return $scope.user = User.get({
    action: 'detail',
    id: $stateParams.id
  });
});
