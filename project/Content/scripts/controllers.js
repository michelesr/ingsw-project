controllers.controller('LoginCtrl', function($scope, $rootScope, AuthService) {
  if ($rootScope.debug) {
    $scope.credentials = {
      email: 'admin@example.org',
      password: 'admin'
    };
  } else {
    $scope.credentials = {
      email: '',
      password: ''
    };
  }
  $scope.msg = '';
  $scope.auth = '';
  $scope.master = {};
  return $scope.login = function(credentials) {
    AuthService.login(credentials).then(function(res) {
      $scope.user = User.detail(res.user_id);
      $scope.setCurrentUser(res.user_id);
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
  $scope.resource = {};
  $scope.result = {};
  return $scope.add = function(form_fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = form_fields.length; _i < _len; _i++) {
      f = form_fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return $scope.result = Product.add($scope.resource);
  };
});

controllers.controller('ProductDetailCtrl', function($scope, $stateParams, Product, Meta) {
  $scope.meta = Meta.product;
  return $scope.resource = Product.detail({
    id: $stateParams.id
  });
});

controllers.controller('RootCtrl', function($rootScope, $scope, $state, AuthService) {
  $rootScope.debug = true;
  $scope.currentUser = null;
  $scope.setCurrentUser = function(user) {
    return $scope.currentUser = user;
  };
  if ($scope.currentUser === null) {
    return $state.go('root.login');
  }
});

controllers.controller('UserCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  return $scope.list = User.list();
});

controllers.controller('UserAddCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  $scope.resource = {};
  $scope.result = {};
  return $scope.add = function(form_fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = form_fields.length; _i < _len; _i++) {
      f = form_fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return $scope.result = User.add(resource);
  };
});

controllers.controller('UserDetailCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  return $scope.user = User.detail({
    id: $stateParams.id
  });
});
