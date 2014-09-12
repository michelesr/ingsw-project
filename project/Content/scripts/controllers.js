controllers.controller('AdminCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.adminSidebar;
});

controllers.controller('CategoryCtrl', function($scope, $state, Category, Meta) {
  var list;
  list = function() {
    $scope.meta = Meta.category;
    return Category.list(function(list) {
      $scope.list = list;
      return $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
    });
  };
  $scope.addForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = Meta.category;
    return $state.go('^.add');
  };
  $scope.add = function(fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return Category.add($scope.resource, function(res) {
      list();
      return $state.go('^.list');
    });
  };
  $scope.detail = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return Category.detail({
      id: id
    }, function(res) {
      var f, k, _i, _len, _ref;
      _ref = $scope.meta.fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        f = _ref[_i];
        k = f['model'];
        f['value'] = res[k];
      }
      return $state.go('^.detail', {
        id: id
      });
    });
  };
  $scope.editForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = Meta.category;
    return $state.go('^.edit', {
      id: $state.params.id
    });
  };
  $scope.edit = function(fields) {
    var f, k, resource, v, _i, _len;
    resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      resource[k] = v;
    }
    return Category.update({
      id: $state.params.id
    }, resource, function(res) {
      $scope.result = res;
      list();
      $scope.msgSuccess = 'Updated successfully';
      return $state.go('^.list');
    });
  };
  $scope.remove = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return Category.remove({
      id: id
    }, function(res) {
      $scope.msgSuccess = 'Removed successfully';
      return list();
    });
  };
  return list();
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

controllers.controller('ProductCtrl', function($scope, $state, User, Category, Product, Meta) {
  var list;
  list = function() {
    var rfList;
    $scope.meta = Meta.product;
    rfList = {};
    return User.list(function(supplierList) {
      rfList['supplier'] = supplierList;
      return Category.list(function(categoryList) {
        rfList['category'] = categoryList;
        return Product.list(function(list) {
          var res, rf, rfElem, _i, _len, _results;
          $scope.list = list;
          $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
          _results = [];
          for (_i = 0, _len = list.length; _i < _len; _i++) {
            res = list[_i];
            _results.push((function() {
              var _j, _len1, _ref, _results1;
              _ref = $scope.meta.related_fields;
              _results1 = [];
              for (_j = 0, _len1 = _ref.length; _j < _len1; _j++) {
                rf = _ref[_j];
                _results1.push((function() {
                  var _k, _len2, _ref1, _results2;
                  _ref1 = rfList[rf.model];
                  _results2 = [];
                  for (_k = 0, _len2 = _ref1.length; _k < _len2; _k++) {
                    rfElem = _ref1[_k];
                    if (rfElem.id === res[rf.related_model]) {
                      _results2.push(res[rf.related_model] = rfElem);
                    } else {
                      _results2.push(void 0);
                    }
                  }
                  return _results2;
                })());
              }
              return _results1;
            })());
          }
          return _results;
        });
      });
    });
  };
  $scope.addForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = Meta.product;
    return $state.go('^.add');
  };
  $scope.add = function(fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return Product.add($scope.resource, function(res) {
      list();
      return $state.go('^.list');
    });
  };
  $scope.detail = function(id) {
    var rfList;
    $scope.msgSuccess = '';
    $scope.msgError = '';
    rfList = {};
    return User.list(function(supplierList) {
      rfList['supplier'] = supplierList;
      return Category.list(function(categoryList) {
        rfList['category'] = categoryList;
        return Product.detail({
          id: id
        }, function(resource) {
          var f, k, rf, rfElem, _i, _j, _k, _l, _len, _len1, _len2, _len3, _ref, _ref1, _ref2, _ref3;
          _ref = $scope.meta.fields;
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            f = _ref[_i];
            k = f['model'];
            f['value'] = resource[k];
          }
          _ref1 = $scope.meta.related_fields;
          for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
            rf = _ref1[_j];
            k = rf['model'];
            rf['value'] = resource[k];
          }
          _ref2 = $scope.meta.related_fields;
          for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
            rf = _ref2[_k];
            _ref3 = rfList[rf.model];
            for (_l = 0, _len3 = _ref3.length; _l < _len3; _l++) {
              rfElem = _ref3[_l];
              if (rfElem.id === resource[rf.related_model]) {
                rf[rf.related_model] = rfElem;
              }
            }
          }
          return $state.go('^.detail', {
            id: id
          });
        });
      });
    });
  };
  $scope.editForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = Meta.product;
    return $state.go('^.edit', {
      id: $state.params.id
    });
  };
  $scope.edit = function(fields) {
    var f, k, resource, v, _i, _len;
    resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      resource[k] = v;
    }
    return Product.update({
      id: $state.params.id
    }, resource, function(res) {
      $scope.result = res;
      list();
      $scope.msgSuccess = 'Updated successfully';
      return $state.go('^.list');
    });
  };
  $scope.remove = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return Product.remove({
      id: id
    }, function(res) {
      $scope.msgSuccess = 'Removed successfully';
      return list();
    });
  };
  return list();
});

controllers.controller('RootCtrl', function($rootScope, $state) {
  $rootScope.debug = true;
  if ($rootScope.debug === true) {
    $rootScope.$state = $state;
  }
  if (!$rootScope.auth) {
    return $state.go('root.login');
  }
});

controllers.controller('SupplierCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.supplierSidebar;
});

controllers.controller('UserCtrl', function($scope, $state, User, Meta) {
  var list;
  list = function() {
    $scope.meta = Meta.user;
    return User.list(function(list) {
      $scope.list = list;
      return $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
    });
  };
  $scope.addForm = function() {
    var extraField;
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = Meta.user;
    extraField = {
      human: 'Password',
      model: 'password',
      type: 'password',
      required: true,
      placeholder: 'your password'
    };
    $scope.meta.fields.push(extraField);
    return $state.go('^.add');
  };
  $scope.add = function(fields) {
    var f, k, v, _i, _len;
    $scope.resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      $scope.resource[k] = v;
    }
    return User.add($scope.resource, function(res) {
      list();
      return $state.go('^.list');
    });
  };
  $scope.detail = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return User.detail({
      id: id
    }, function(res) {
      var f, k, _i, _len, _ref;
      _ref = $scope.meta.fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        f = _ref[_i];
        k = f['model'];
        f['value'] = res[k];
      }
      return $state.go('^.detail', {
        id: id
      });
    });
  };
  $scope.editForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = Meta.user;
    return $state.go('^.edit', {
      id: $state.params.id
    });
  };
  $scope.edit = function(fields) {
    var f, k, resource, v, _i, _len;
    resource = {};
    for (_i = 0, _len = fields.length; _i < _len; _i++) {
      f = fields[_i];
      k = f['model'];
      v = f['value'];
      resource[k] = v;
    }
    return User.update({
      id: $state.params.id
    }, resource, function(res) {
      $scope.result = res;
      list();
      $scope.msgSuccess = 'Updated successfully';
      return $state.go('^.list');
    });
  };
  $scope.remove = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return User.remove({
      id: id
    }, function(res) {
      $scope.msgSuccess = 'Removed successfully';
      return list();
    });
  };
  return list();
});
