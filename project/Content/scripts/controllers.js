controllers.controller('AdminCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.adminSidebar;
});

controllers.controller('CategoryCtrl', function($scope, $state, Category, Meta) {
  var list;
  list = function() {
    $scope.meta = _.cloneDeep(Meta.category);
    return Category.list(function(categoryList) {
      $scope.list = categoryList;
      return $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
    });
  };
  $scope.addForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = _.cloneDeep(Meta.category);
    return $state.go('^.add');
  };
  $scope.add = function() {
    var f, k, v, _i, _len, _ref;
    $scope.resource = {};
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f.model;
      v = f.value;
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
        k = f.model;
        f.value = res[k];
      }
      return $state.go('^.detail', {
        id: id
      });
    });
  };
  $scope.editForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = _.cloneDeep(Meta.category);
    return Category.detail({
      id: $state.params.id
    }, function(resource) {
      var f, k, _i, _len, _ref;
      _ref = $scope.meta.fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        f = _ref[_i];
        k = f.model;
        f.value = resource[k];
      }
      return $state.go('^.edit', {
        id: $state.params.id
      });
    });
  };
  $scope.edit = function() {
    var f, k, resource, v, _i, _len, _ref;
    resource = {};
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f.model;
      v = f.value;
      resource[k] = v;
    }
    return Category.update({
      id: $state.params.id
    }, resource, function(res) {
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

controllers.controller('CityCtrl', function($scope, $state, City, Meta) {
  var list;
  list = function() {
    $scope.meta = _.cloneDeep(Meta.city);
    return City.list(function(cityList) {
      $scope.list = cityList;
      return $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
    });
  };
  $scope.addForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = _.cloneDeep(Meta.city);
    return $state.go('^.add');
  };
  $scope.add = function() {
    var f, k, v, _i, _len, _ref;
    $scope.resource = {};
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f.model;
      v = f.value;
      $scope.resource[k] = v;
    }
    return City.add($scope.resource, function(res) {
      list();
      return $state.go('^.list');
    });
  };
  $scope.detail = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return City.detail({
      id: id
    }, function(res) {
      var f, k, _i, _len, _ref;
      _ref = $scope.meta.fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        f = _ref[_i];
        k = f.model;
        f.value = res[k];
      }
      return $state.go('^.detail', {
        id: id
      });
    });
  };
  $scope.editForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = _.cloneDeep(Meta.city);
    return City.detail({
      id: $state.params.id
    }, function(resource) {
      var f, k, _i, _len, _ref;
      _ref = $scope.meta.fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        f = _ref[_i];
        k = f.model;
        f.value = resource[k];
      }
      return $state.go('^.edit', {
        id: $state.params.id
      });
    });
  };
  $scope.edit = function() {
    var f, k, resource, v, _i, _len, _ref;
    resource = {};
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f.model;
      v = f.value;
      resource[k] = v;
    }
    return City.update({
      id: $state.params.id
    }, resource, function(res) {
      list();
      $scope.msgSuccess = 'Updated successfully';
      return $state.go('^.list');
    });
  };
  $scope.remove = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return City.remove({
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
    $scope.meta = _.cloneDeep(Meta.product);
    return Product.list(function(productList) {
      return Category.list(function(categoryList) {
        return User.list(function(supplierList) {
          var lists, res, rf, rfElem, _i, _len, _ref, _results;
          $scope.list = productList;
          lists = {
            category: categoryList,
            supplier: supplierList
          };
          $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
          _ref = $scope.list;
          _results = [];
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            res = _ref[_i];
            _results.push((function() {
              var _j, _len1, _ref1, _results1;
              _ref1 = $scope.meta.related_fields;
              _results1 = [];
              for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
                rf = _ref1[_j];
                _results1.push((function() {
                  var _k, _len2, _ref2, _results2;
                  _ref2 = lists[rf.model];
                  _results2 = [];
                  for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
                    rfElem = _ref2[_k];
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
    $scope.meta = _.cloneDeep(Meta.product);
    return Category.list(function(categoryList) {
      return User.list(function(supplierList) {
        var lists, rf, _i, _len, _ref;
        lists = {
          category: categoryList,
          supplier: supplierList
        };
        _ref = $scope.meta.related_fields;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          rf = _ref[_i];
          rf.values = lists[rf.model];
        }
        return $state.go('^.add');
      });
    });
  };
  $scope.add = function() {
    var f, k, resource, rf, v, _i, _j, _len, _len1, _ref, _ref1;
    resource = {};
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f.model;
      v = f.value;
      resource[k] = v;
    }
    _ref1 = $scope.meta.related_fields;
    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
      rf = _ref1[_j];
      if (_.has(rf, 'value')) {
        k = rf.related_model;
        v = _.parseInt(rf.value);
        resource[k] = v;
      }
    }
    return Product.add(resource, function(res) {
      list();
      $scope.msgSuccess = 'Added successfully';
      return $state.go('^.list');
    });
  };
  $scope.detail = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return Product.detail({
      id: id
    }, function(resource) {
      return Category.list(function(categoryList) {
        return User.list(function(supplierList) {
          var f, k, lists, rf, rfElem, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
          lists = {
            supplier: supplierList,
            category: categoryList
          };
          _ref = $scope.meta.fields;
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            f = _ref[_i];
            k = f.model;
            f.value = resource[k];
          }
          _ref1 = $scope.meta.related_fields;
          for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
            rf = _ref1[_j];
            k = rf.model;
            rf.value = resource[k];
            _ref2 = lists[rf.model];
            for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
              rfElem = _ref2[_k];
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
    $scope.meta = _.cloneDeep(Meta.product);
    return Product.detail({
      id: $state.params.id
    }, function(resource) {
      return Category.list(function(categoryList) {
        return User.list(function(supplierList) {
          var f, k, lists, rf, rfElem, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
          lists = {
            supplier: supplierList,
            category: categoryList
          };
          _ref = $scope.meta.fields;
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            f = _ref[_i];
            k = f.model;
            f.value = resource[k];
          }
          _ref1 = $scope.meta.related_fields;
          for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
            rf = _ref1[_j];
            k = rf.model;
            rf.value = resource[k];
            rf.values = lists[rf.model];
            _ref2 = lists[rf.model];
            for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
              rfElem = _ref2[_k];
              if (rfElem.id === resource[rf.related_model]) {
                rf.value = rfElem.id;
              }
            }
          }
          return $state.go('^.edit', {
            id: $state.params.id
          });
        });
      });
    });
  };
  $scope.edit = function() {
    var f, k, resource, rf, v, _i, _j, _len, _len1, _ref, _ref1;
    resource = {};
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f.model;
      v = f.value;
      resource[k] = v;
    }
    _ref1 = $scope.meta.related_fields;
    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
      rf = _ref1[_j];
      if (_.has(rf, 'value')) {
        k = rf.related_model;
        v = _.parseInt(rf.value);
        resource[k] = v;
      }
    }
    return Product.update({
      id: $state.params.id
    }, resource, function(res) {
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

controllers.controller('StockCtrl', function($scope, $state, Product, Stock, Meta) {
  var list;
  list = function() {
    $scope.meta = _.cloneDeep(Meta.stock);
    return Stock.list(function(stockList) {
      return Product.list(function(productList) {
        var lists, res, rf, rfElem, _i, _len, _ref, _results;
        $scope.list = stockList;
        lists = {
          product: productList
        };
        $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
        _ref = $scope.list;
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          res = _ref[_i];
          _results.push((function() {
            var _j, _len1, _ref1, _results1;
            _ref1 = $scope.meta.related_fields;
            _results1 = [];
            for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
              rf = _ref1[_j];
              _results1.push((function() {
                var _k, _len2, _ref2, _results2;
                _ref2 = lists[rf.model];
                _results2 = [];
                for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
                  rfElem = _ref2[_k];
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
  };
  $scope.addForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = _.cloneDeep(Meta.stock);
    return Product.list(function(productList) {
      var lists, rf, _i, _len, _ref;
      lists = {
        product: productList
      };
      _ref = $scope.meta.related_fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        rf = _ref[_i];
        rf.values = lists[rf.model];
      }
      return $state.go('^.add');
    });
  };
  $scope.add = function() {
    var f, k, resource, rf, v, _i, _j, _len, _len1, _ref, _ref1;
    resource = {};
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f.model;
      v = f.value;
      resource[k] = v;
    }
    _ref1 = $scope.meta.related_fields;
    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
      rf = _ref1[_j];
      if (_.has(rf, 'value')) {
        k = rf.related_model;
        v = _.parseInt(rf.value);
        resource[k] = v;
      }
    }
    return Stock.add(resource, function(res) {
      list();
      $scope.msgSuccess = 'Added successfully';
      return $state.go('^.list');
    });
  };
  $scope.detail = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return Stock.detail({
      id: id
    }, function(resource) {
      return Product.list(function(productList) {
        var f, k, lists, rf, rfElem, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
        lists = {
          product: productList
        };
        _ref = $scope.meta.fields;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          f = _ref[_i];
          k = f.model;
          f.value = resource[k];
        }
        _ref1 = $scope.meta.related_fields;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          rf = _ref1[_j];
          k = rf.model;
          rf.value = resource[k];
          _ref2 = lists[rf.model];
          for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
            rfElem = _ref2[_k];
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
  };
  $scope.editForm = function() {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = _.cloneDeep(Meta.stock);
    return Stock.detail({
      id: $state.params.id
    }, function(resource) {
      return Product.list(function(productList) {
        var f, k, lists, rf, rfElem, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
        lists = {
          product: productList
        };
        _ref = $scope.meta.fields;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          f = _ref[_i];
          k = f.model;
          f.value = resource[k];
        }
        _ref1 = $scope.meta.related_fields;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          rf = _ref1[_j];
          k = rf.model;
          rf.value = resource[k];
          rf.values = lists[rf.model];
          _ref2 = lists[rf.model];
          for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
            rfElem = _ref2[_k];
            if (rfElem.id === resource[rf.related_model]) {
              rf.value = rfElem.id;
            }
          }
        }
        return $state.go('^.edit', {
          id: $state.params.id
        });
      });
    });
  };
  $scope.edit = function() {
    var f, k, resource, rf, v, _i, _j, _len, _len1, _ref, _ref1;
    resource = {};
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      k = f.model;
      v = f.value;
      resource[k] = v;
    }
    _ref1 = $scope.meta.related_fields;
    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
      rf = _ref1[_j];
      if (_.has(rf, 'value')) {
        k = rf.related_model;
        v = _.parseInt(rf.value);
        resource[k] = v;
      }
    }
    return Stock.update({
      id: $state.params.id
    }, resource, function(res) {
      list();
      $scope.msgSuccess = 'Updated successfully';
      return $state.go('^.list');
    });
  };
  $scope.remove = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return Stock.remove({
      id: id
    }, function(res) {
      $scope.msgSuccess = 'Removed successfully';
      return list();
    });
  };
  return list();
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
