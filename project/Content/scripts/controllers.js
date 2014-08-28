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
      $scope.setCurrentUser(user);
      switch (Session.user_type) {
        case 'admin':
          return $state.go('root.admin');
        case 'supplier':
          return $state.go('root.supplier');
        default:
          return $state.go('root.login');
      }
    });
    return $scope.master = $scope.credentials;
  };
});

controllers.controller('ProductCtrl', function($scope, $stateParams, Product, Meta) {
  $scope.meta = Meta.product;
  return $scope.list = Product.list();
});

controllers.controller('ProductAddCtrl', function($scope, $stateParams, Product, Meta) {
  $scope.meta = Meta.product;
  $scope.newResource = {};
  $scope.master = {};
  return $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = Product.add(newResource);
  };
});

controllers.controller('ProductDetailCtrl', function($scope, $stateParams, Product, Meta) {
  $scope.meta = Meta.product;
  return $scope.resource = Product.detail({
    id: $stateParams.id
  });
});

controllers.controller('RootCtrl', function($scope, $state, AuthService) {
  $scope.currentUser = null;
  return $scope.setCurrentUser = function(user) {
    return $scope.currentUser = user;
  };
});

controllers.controller('UserCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  return $scope.list = User.list();
});

controllers.controller('UserAddCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
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
  return $scope.add = function(newResource) {
    $scope.master = angular.copy(newResource);
    return $scope.newResource = User.add(newResource);
  };
});

controllers.controller('UserDetailCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  return $scope.user = User.detail({
    id: $stateParams.id
  });
});
