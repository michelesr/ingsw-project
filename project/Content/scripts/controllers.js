controllers.controller('AdminCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.adminSidebar;
});

controllers.controller('CategoryCtrl', function($scope, $stateParams, Category, Meta) {
  $scope.meta = Meta.category;
  Category.list(function(list) {
    $scope.list = list;
    return $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
  });
  return $scope["delete"] = function(id) {
    return Category["delete"](id, function(res) {
      return $scope.msg = 'Category deleted successfully';
    });
  };
});

controllers.controller('CategoryAddCtrl', function($scope, $state, $stateParams, Category, Meta) {
  $scope.meta = {};
  $scope.meta = Meta.category;
  return $scope.add = function(fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return Category.add($scope.resource, function(res) {
      $scope.meta = {};
      return $state.go('root.categories.list');
    });
  };
});

controllers.controller('CategoryDetailCtrl', function($scope, $stateParams, Category, Meta) {
  $scope.meta = Meta.category;
  return Category.detail({
    id: $stateParams.id
  }, function(res) {
    var f, k, _i, _len, _ref, _results;
    _ref = $scope.meta.fields;
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f['model'];
      _results.push(f['value'] = res[k]);
    }
    return _results;
  });
});

controllers.controller('CategoryEditCtrl', function($scope, $stateParams, Category, Meta) {
  $scope.meta = Meta.category;
  Category.detail({
    id: $stateParams.id
  }, function(res) {
    var f, model, _i, _len, _ref, _results;
    $scope.category = res;
    _ref = $scope.meta['fields'];
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      model = f['model'];
      $scope.result = model;
      _results.push(f['value'] = $scope.category[model]);
    }
    return _results;
  });
  return $scope.edit = function(fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return $scope.result = Category.update(resource);
  };
});

controllers.controller('LoginCtrl', function($scope, $rootScope, $http, $state, Auth, User) {
  if ($rootScope.debug) {
    $scope.credentials = {
      email: 'admin@example.org',
      password: 'admin'
    };
  }
  return $scope.login = function(credentials) {
    return Auth.login(credentials, function(res_auth) {
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

controllers.controller('LogoutCtrl', function($scope, $rootScope, $http, $state, Auth) {
  return Auth.logout(function() {
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
  $scope.meta = {};
  $scope.meta = Meta.product;
  return Product.list(function(list) {
    $scope.list = list;
    return $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
  });
});

controllers.controller('ProductAddCtrl', function($scope, $state, $stateParams, Product, Meta) {
  $scope.meta = {};
  $scope.meta = Meta.product;
  $scope.resource = {};
  $scope.result = {};
  return $scope.add = function(fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    $scope.result = Product.add($scope.resource);
    $scope.meta = {};
    return $state.go('root.products.list');
  };
});

controllers.controller('ProductDetailCtrl', function($scope, $stateParams, Product, Meta) {
  $scope.meta = {};
  $scope.meta = Meta.product;
  return Product.detail({
    id: $stateParams.id
  }, function(res) {
    var f, k, _i, _len, _ref, _results;
    _ref = $scope.meta.fields;
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f['model'];
      _results.push(f['value'] = res[k]);
    }
    return _results;
  });
});

controllers.controller('ProductEditCtrl', function($scope, $state, $stateParams, Product, Meta) {
  $scope.meta = {};
  $scope.meta = Meta.product;
  Product.detail({
    id: $stateParams.id
  }, function(res) {
    var f, model, _i, _len, _ref, _results;
    $scope.product = res;
    _ref = $scope.meta['fields'];
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      model = f['model'];
      $scope.result = model;
      _results.push(f['value'] = $scope.product[model]);
    }
    return _results;
  });
  return $scope.edit = function(fields, $stateParams) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
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
    return $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
  });
  return $scope["delete"] = function(id) {
    return User["delete"](id, function(res) {
      return $scope.msg = 'User deleted successfully';
    });
  };
});

controllers.controller('UserAddCtrl', function($scope, $stateParams, User, Meta) {
  var password_field;
  $scope.meta = Meta.user;
  password_field = {
    human: 'Password',
    model: 'password',
    type: 'password',
    required: true,
    placeholder: 'your password'
  };
  $scope.meta.fields.push(password_field);
  return $scope.add = function(fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    $scope.result = {};
    return $scope.result = User.add($scope.resource);
  };
});

controllers.controller('UserDetailCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  return User.detail({
    id: $stateParams.id
  }, function(res) {
    var f, k, _i, _len, _ref, _results;
    _ref = $scope.meta.fields;
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f['model'];
      _results.push(f['value'] = res[k]);
    }
    return _results;
  });
});

controllers.controller('UserEditCtrl', function($scope, $stateParams, User, Meta) {
  $scope.meta = Meta.user;
  User.detail({
    id: $stateParams.id
  }, function(res) {
    var f, model, _i, _len, _ref, _results;
    $scope.user = res;
    _ref = $scope.meta['fields'];
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      model = f['model'];
      $scope.result = model;
      _results.push(f['value'] = $scope.user[model]);
    }
    return _results;
  });
  return $scope.edit = function(fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return $scope.result = User.update(resource);
  };
});
