controllers.controller('AdminCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.adminSidebar;
});

controllers.controller('LoginCtrl', function($scope, $rootScope, $http, $state, AuthAPI, User) {
  $scope.credentials = {
    email: 'admin@example.org',
    password: 'admin'
  };
  return $scope.login = function(credentials) {
    return AuthAPI.login(credentials, function(res_auth) {
      $http.defaults.headers.common['api_key'] = res_auth.api_key;
      return User.detail({
        id: res_auth.user_id
      }, function(res_user) {
        $rootScope.authId = res_user.id;
        $rootScope.authEmail = res_user.email;
        $rootScope.authFirstName = res_user.first_name;
        $rootScope.authLastName = res_user.last_name;
        $rootScope.authType = res_user.type;
        $rootScope.isAuth = true;
        switch ($rootScope.authType) {
          case 0:
            return $state.go('root.supplier');
          case 1:
            return $state.go('root.admin');
        }
      });
    });
  };
});

controllers.controller('LogoutCtrl', function($scope, $rootScope, $http, $state, AuthAPI) {
  return AuthAPI.logout(function() {
    delete $http.defaults.headers.common['api_key'];
    $rootScope.authId = null;
    $rootScope.authEmail = null;
    $rootScope.authFistName = '';
    $rootScope.authLastName = null;
    $rootScope.authType = null;
    $rootScope.sidebar = null;
    $rootScope.isAuth = false;
    return $state.go('root.login');
  });
});

controllers.controller('ProductCtrl', function($scope, $stateParams, Product, Meta) {
  $scope.meta = Meta.product;
  return Product.list(function(list) {
    $scope.list = list;
    return $scope.empty = _.isEmpty($scope.list[0]);
  });
});

controllers.controller('ProductAddCtrl', function($scope, $state, $stateParams, Product, Meta) {
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
    $scope.result = Product.add($scope.resource);
    return $state.go('root.products.list');
  };
});

controllers.controller('ProductDetailCtrl', function($scope, $stateParams, Product, Meta) {
  $scope.meta = Meta.product;
  return $scope.resource = Product.detail({
    id: $stateParams.id
  });
});

controllers.controller('ProductEditCtrl', function($scope, $state, $stateParams, Product, Meta) {
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
    $scope.result = Product.update(resource);
    return $state.go('root.products.list');
  };
});

controllers.controller('RootCtrl', function($rootScope, $scope, $state, Auth, Session) {
  $rootScope.debug = true;
  if (!$rootScope.auth) {
    return $state.go('root.login');
  }
});

controllers.controller('SupplierCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.supplierSidebar;
});

controllers.controller('UserCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  User.list(function(list) {
    $scope.list = list;
    return $scope.empty = _.isEmpty($scope.list[0]);
  });
  return $scope["delete"] = function(id) {
    return User["delete"](id);
  };
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
