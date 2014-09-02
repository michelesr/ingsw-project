controllers.controller('AdminCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.adminSidebar;
});

controllers.controller('LoginCtrl', function($scope, $rootScope, $state, Auth, Session) {
  if ($rootScope.debug) {
    $scope.credentials = {
      email: 'admin@example.org',
      password: 'admin'
    };
  }
  return $scope.login = function(credentials) {
    return Auth.login(credentials).then(function(res) {
      return $state.go('root.admin');
    });
  };
});

controllers.controller('LogoutCtrl', function($state, $rootScope, Auth) {
  Auth.logout;
  $rootScope.sidebar = [];
  return $state.go('root.login');
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

controllers.controller('ProductEditCtrl', function($scope, $stateParams, Product, Meta) {
  $scope.meta = Meta.product;
  Product.detail({
    id: $stateParams.id
  }, function(res) {
    var f, model, _i, _len, _ref, _results;
    $scope.product = res;
    _ref = $scope.meta['form_fields'];
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      model = f['model'];
      $scope.result = model;
      _results.push(f['value'] = $scope.product[model]);
    }
    return _results;
  });
  return $scope.edit = function(form_fields, $stateParams) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = form_fields.length; _i < _len; _i++) {
      f = form_fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return $scope.result = Product.update(resource);
  };
});

controllers.controller('RootCtrl', function($rootScope, $scope, $state, Auth) {
  $rootScope.debug = true;
  if (!Auth.isAuthenticated) {
    return $state.go('root.login');
  }
});

controllers.controller('SupplierCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.supplierSidebar;
});

controllers.controller('UserCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  return $scope.list = User.list();
});

controllers.controller('UserAddCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = [];
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
    return $scope.result = User.add($scope.resource);
  };
});

controllers.controller('UserDetailCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  return $scope.resource = User.detail({
    id: $stateParams.id
  });
});

controllers.controller('UserEditCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  User.detail({
    id: $stateParams.id
  }, function(res) {
    var f, model, _i, _len, _ref, _results;
    $scope.user = res;
    _ref = $scope.meta['form_fields'];
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      model = f['model'];
      $scope.result = model;
      _results.push(f['value'] = $scope.user[model]);
    }
    return _results;
  });
  return $scope.edit = function(form_fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = form_fields.length; _i < _len; _i++) {
      f = form_fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return $scope.result = User.update(resource);
  };
});
