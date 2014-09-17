controllers.controller('AdminCtrl', function($scope, $state, User, Meta) {
  var list;
  list = function() {
    $scope.meta = _.cloneDeep(Meta.admin);
    return User.list(function(list) {
      var user;
      $scope.list = (function() {
        var _i, _len, _results;
        _results = [];
        for (_i = 0, _len = list.length; _i < _len; _i++) {
          user = list[_i];
          if (user.type === 1) {
            _results.push(user);
          }
        }
        return _results;
      })();
      return $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
    });
  };
  $scope.addForm = function() {
    var ef, _i, _len, _ref;
    $scope.msgSuccess = '';
    $scope.msgError = '';
    $scope.meta = _.cloneDeep(Meta.admin);
    if (_.has($scope.meta, 'extra_fields')) {
      _ref = $scope.meta.extra_fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        ef = _ref[_i];
        $scope.meta.fields.push(ef);
      }
    }
    return $state.go('^.add');
  };
  $scope.add = function() {
    var f, resource, _i, _len, _ref;
    resource = {
      type: 'admin',
      user_data: {}
    };
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      resource.user_data[f.model] = f.value;
    }
    return User.add(resource, function(res) {
      list();
      return $state.go('^.list');
    });
  };
  $scope.detail = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return User.listSession({
      id: id
    }, function(sessions) {
      return User.detail({
        id: id
      }, function(admin) {
        var f, s, _i, _j, _len, _len1, _ref;
        _ref = $scope.meta.fields;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          f = _ref[_i];
          f.value = admin[f.model];
        }
        $scope.sessions = [];
        for (_j = 0, _len1 = sessions.length; _j < _len1; _j++) {
          s = sessions[_j];
          if (s.user_id === id) {
            $scope.sessions.push(s);
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
    $scope.meta = _.cloneDeep(Meta.admin);
    return User.detail({
      id: $state.params.id
    }, function(admin) {
      var f, _i, _len, _ref;
      _ref = $scope.meta.fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        f = _ref[_i];
        f.value = admin[f.model];
      }
      return $state.go('^.edit', {
        id: $state.params.id
      });
    });
  };
  $scope.edit = function() {
    var f, resource, _i, _len, _ref;
    resource = {
      user_data: {}
    };
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      resource.user_data[f.model] = f.value;
    }
    return User.update({
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
    return User.remove({
      id: id
    }, function(res) {
      $scope.msgSuccess = 'Removed successfully';
      return list();
    });
  };
  return list();
});

controllers.controller('AdminHomeCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.adminSidebar;
});

controllers.controller('CatalogCtrl', function($scope, $rootScope, $http, Stock, Product, Catalog, Meta) {
  var count;
  count = function() {
    return Stock.list(function(stockList) {
      return Product.list(function(productList) {
        var lists, prod, stock, _i, _j, _len, _len1, _ref;
        $scope.list = [];
        lists = {
          product: (function() {
            var _i, _len, _results;
            _results = [];
            for (_i = 0, _len = productList.length; _i < _len; _i++) {
              prod = productList[_i];
              if (prod.supplier_id === $rootScope.authSupplierId) {
                _results.push(prod);
              }
            }
            return _results;
          })()
        };
        _ref = lists.product;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          prod = _ref[_i];
          for (_j = 0, _len1 = stockList.length; _j < _len1; _j++) {
            stock = stockList[_j];
            if (stock.product_id === prod.id) {
              $scope.list.push(stock);
            }
          }
        }
        if ($scope.list.length <= 1 && _.isEmpty($scope.list[0])) {
          $scope.stocksCount = 0;
        } else {
          $scope.stocksCount = $scope.list.length;
        }
        if (lists.product.length <= 1 && _.isEmpty(lists.product[0])) {
          return $scope.productsCount = 0;
        } else {
          return $scope.productsCount = lists.product.length;
        }
      });
    });
  };
  count();
  return $scope["export"] = function() {
    return Catalog["export"]({
      id: $rootScope.authSupplierId
    }, function(res) {
      var blob, x2js;
      x2js = new X2JS({
        attributePrefix: '$'
      });
      blob = new Blob([x2js.json2xml_str(res)], {
        type: "text/plain;charset=utf-8"
      });
      return saveAs(blob, 'export_supplier' + $rootScope.authSupplierId + '.xml');
    });
  };
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
    if ($rootScope.debugType === 'admin') {
      $scope.credentials = {
        email: 'admin@example.org',
        password: 'admin'
      };
    } else if ($rootScope.debugType === 'supplier') {
      $scope.credentials = {
        email: 'test@example.org',
        password: 'test'
      };
    }
  }
  return $scope.login = function(credentials) {
    return Auth.login(credentials, function(res_auth) {
      $http.defaults.headers.common['api_key'] = res_auth.api_key;
      return User.detail({
        id: res_auth.user_id
      }, function(res_user) {
        $rootScope.authEmail = res_user.email;
        $rootScope.authFirstName = res_user.first_name;
        $rootScope.authLastName = res_user.last_name;
        $rootScope.authType = res_user.type;
        $rootScope.isAuth = true;
        switch ($rootScope.authType) {
          case 0:
            $rootScope.authId = res_user.user_id;
            $rootScope.authSupplierId = res_user.id;
            return $state.go('root.supplierHome');
          case 1:
            $rootScope.authId = res_user.id;
            return $state.go('root.adminHome');
        }
      });
    });
  };
});

controllers.controller('LogoutCtrl', function($rootScope, $http, $state, Auth) {
  return Auth.logout(function() {
    delete $http.defaults.headers.common['api_key'];
    $rootScope.authId = null;
    $rootScope.authSupplierId = null;
    $rootScope.authEmail = null;
    $rootScope.authFistName = '';
    $rootScope.authLastName = null;
    $rootScope.authType = null;
    $rootScope.sidebar = null;
    $rootScope.isAuth = false;
    return $state.go('root.login');
  });
});

controllers.controller('ProductCtrl', function($scope, $rootScope, $state, Category, Product, Meta) {
  var list;
  list = function() {
    $scope.meta = _.cloneDeep(Meta.product);
    return Product.list(function(productList) {
      return Category.list(function(categoryList) {
        var elem, lists, prod, res, rf, _i, _len, _ref, _results;
        $scope.list = (function() {
          var _i, _len, _results;
          _results = [];
          for (_i = 0, _len = productList.length; _i < _len; _i++) {
            prod = productList[_i];
            if (prod.supplier_id === $rootScope.authSupplierId) {
              _results.push(prod);
            }
          }
          return _results;
        })();
        lists = {
          category: categoryList
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
                _ref2 = lists.category;
                _results2 = [];
                for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
                  elem = _ref2[_k];
                  if (elem.id === res[rf.related_model]) {
                    _results2.push(res[rf.related_model] = elem);
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
    $scope.meta = _.cloneDeep(Meta.product);
    return Category.list(function(categoryList) {
      var lists, rf, _i, _len, _ref;
      lists = {
        category: categoryList
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
    resource = {
      supplier_id: $rootScope.authSupplierId
    };
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      resource[f.model] = f.value;
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
        var elem, f, lists, rf, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
        lists = {
          category: categoryList
        };
        _ref = $scope.meta.fields;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          f = _ref[_i];
          f.value = resource[f.model];
        }
        _ref1 = $scope.meta.related_fields;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          rf = _ref1[_j];
          rf.value = resource[rf.model];
          _ref2 = lists.category;
          for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
            elem = _ref2[_k];
            if (elem.id === resource[rf.related_model]) {
              rf[rf.related_model] = elem;
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
    $scope.meta = _.cloneDeep(Meta.product);
    return Product.detail({
      id: $state.params.id
    }, function(resource) {
      return Category.list(function(categoryList) {
        var elem, f, lists, rf, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
        lists = {
          category: categoryList
        };
        _ref = $scope.meta.fields;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          f = _ref[_i];
          f.value = resource[f.model];
        }
        _ref1 = $scope.meta.related_fields;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          rf = _ref1[_j];
          if (rf.model === 'category') {
            rf.value = resource[rf.model];
            rf.values = lists[rf.model];
            _ref2 = lists[rf.model];
            for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
              elem = _ref2[_k];
              if (elem.id === resource[rf.related_model]) {
                rf.value = elem.id;
              }
            }
          } else {
            rf.value = $rootScope.authSupplierId;
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
      resource[f.model] = f.value;
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
  $rootScope.debugType = 'supplier';
  if ($rootScope.debug === true) {
    $rootScope.$state = $state;
  }
  if (!$rootScope.auth) {
    return $state.go('root.login');
  }
});

controllers.controller('StockCtrl', function($scope, $rootScope, $state, Product, Stock, Meta) {
  var list;
  list = function() {
    $scope.meta = _.cloneDeep(Meta.stock);
    return Stock.list(function(stockList) {
      return Product.list(function(productList) {
        var lists, prod, res, rf, rfElem, stock, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _results;
        $scope.list = [];
        lists = {
          product: (function() {
            var _i, _len, _results;
            _results = [];
            for (_i = 0, _len = productList.length; _i < _len; _i++) {
              prod = productList[_i];
              if (prod.supplier_id === $rootScope.authSupplierId) {
                _results.push(prod);
              }
            }
            return _results;
          })()
        };
        _ref = lists.product;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          prod = _ref[_i];
          for (_j = 0, _len1 = stockList.length; _j < _len1; _j++) {
            stock = stockList[_j];
            if (stock.product_id === prod.id) {
              $scope.list.push(stock);
            }
          }
        }
        $scope.empty = $scope.list.length <= 1 && _.isEmpty($scope.list[0]);
        _ref1 = $scope.list;
        _results = [];
        for (_k = 0, _len2 = _ref1.length; _k < _len2; _k++) {
          res = _ref1[_k];
          _results.push((function() {
            var _l, _len3, _ref2, _results1;
            _ref2 = $scope.meta.related_fields;
            _results1 = [];
            for (_l = 0, _len3 = _ref2.length; _l < _len3; _l++) {
              rf = _ref2[_l];
              _results1.push((function() {
                var _len4, _m, _ref3, _results2;
                _ref3 = lists[rf.model];
                _results2 = [];
                for (_m = 0, _len4 = _ref3.length; _m < _len4; _m++) {
                  rfElem = _ref3[_m];
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
    return Product.list(function(allProductList) {
      var lists, prod, rf, _i, _j, _len, _len1, _ref;
      lists = {
        product: []
      };
      for (_i = 0, _len = allProductList.length; _i < _len; _i++) {
        prod = allProductList[_i];
        if (prod.supplier_id === $rootScope.authSupplierId) {
          lists.product.push(prod);
        }
      }
      _ref = $scope.meta.related_fields;
      for (_j = 0, _len1 = _ref.length; _j < _len1; _j++) {
        rf = _ref[_j];
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
      v = k === 'price' ? f.value * 100 : f.value;
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
          f.value = k === 'price' ? resource[k] / 100 : resource[k];
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
      v = k === 'price' ? f.value * 100 : f.value;
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

controllers.controller('SupplierCtrl', function($scope, $rootScope, $state, City, User, Meta) {
  var list;
  list = function() {
    $scope.meta = _.cloneDeep(Meta.supplier);
    return City.list(function(cityList) {
      return User.listSupplier(function(list) {
        var lists, res, rf, rfElem, user, _i, _len, _ref, _results;
        $scope.list = (function() {
          var _i, _len, _results;
          _results = [];
          for (_i = 0, _len = list.length; _i < _len; _i++) {
            user = list[_i];
            if (user.type === 0) {
              _results.push(user);
            }
          }
          return _results;
        })();
        lists = {
          city: cityList
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
    $scope.meta = _.cloneDeep(Meta.supplier);
    return City.list(function(cityList) {
      var ef, lists, rf, _i, _j, _len, _len1, _ref, _ref1;
      lists = {
        city: cityList
      };
      _ref = $scope.meta.related_fields;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        rf = _ref[_i];
        rf.values = lists[rf.model];
      }
      if (_.has($scope.meta, 'extra_fields')) {
        _ref1 = $scope.meta.extra_fields;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          ef = _ref1[_j];
          $scope.meta.fields.push(ef);
        }
      }
      return $state.go('^.add');
    });
  };
  $scope.add = function() {
    var f, k, resource, rf, v, _i, _j, _len, _len1, _ref, _ref1;
    resource = {
      type: 'supplier',
      user_data: {},
      supplier_data: {}
    };
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      if (_.has(f, 'supplier') && f.supplier === true) {
        resource.supplier_data[f.model] = f.value;
      } else {
        resource.user_data[f.model] = f.value;
      }
    }
    _ref1 = $scope.meta.related_fields;
    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
      rf = _ref1[_j];
      if (_.has(rf, 'value')) {
        k = rf.related_model;
        v = _.parseInt(rf.value);
        if (_.has(rf, 'supplier') && rf.supplier === true) {
          resource.supplier_data[k] = v;
        } else {
          resource.user_data[k] = v;
        }
      }
    }
    return User.add(resource, function(res) {
      list();
      return $state.go('^.list');
    });
  };
  $scope.detail = function(id) {
    $scope.msgSuccess = '';
    $scope.msgError = '';
    return City.list(function(cityList) {
      return User.detail({
        id: id
      }, function(resource) {
        var f, lists, rf, rfElem, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
        lists = {
          city: cityList
        };
        _ref = $scope.meta.fields;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          f = _ref[_i];
          f.value = resource[f.model];
        }
        _ref1 = $scope.meta.related_fields;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          rf = _ref1[_j];
          rf.value = resource[rf.model];
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
    $scope.meta = _.cloneDeep(Meta.supplier);
    return City.list(function(cityList) {
      return User.detail({
        id: $state.params.id
      }, function(resource) {
        var f, lists, rf, _i, _j, _len, _len1, _ref, _ref1;
        lists = {
          city: cityList
        };
        _ref = $scope.meta.fields;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          f = _ref[_i];
          f.value = resource[f.model];
        }
        _ref1 = $scope.meta.related_fields;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          rf = _ref1[_j];
          rf.value = resource[rf.model];
          rf.values = lists[rf.model];
        }
        return $state.go('^.edit', {
          id: $state.params.id
        });
      });
    });
  };
  $scope.edit = function() {
    var f, resource, rf, _i, _j, _len, _len1, _ref, _ref1;
    resource = {
      user_data: {},
      supplier_data: {}
    };
    _ref = $scope.meta.fields;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      f = _ref[_i];
      if (_.has(f, 'supplier') && f.supplier === true) {
        resource.supplier_data[f.model] = f.value;
      } else {
        resource.user_data[f.model] = f.value;
      }
    }
    _ref1 = $scope.meta.related_fields;
    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
      rf = _ref1[_j];
      if (_.has(rf, 'supplier') && rf.supplier === true) {
        resource.supplier_data[rf.model] = rf.value;
      } else {
        resource.user_data[rf.model] = rf.value;
      }
    }
    return User.update({
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
    return User.remove({
      id: id
    }, function(res) {
      $scope.msgSuccess = 'Removed successfully';
      return list();
    });
  };
  return list();
});

controllers.controller('SupplierHomeCtrl', function($rootScope, Meta) {
  return $rootScope.sidebar = Meta.supplierSidebar;
});
