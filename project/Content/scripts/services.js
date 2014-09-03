services.factory('AuthAPI', function($resource) {
  return $resource('/api/auth/:action', {}, {
    login: {
      method: 'POST',
      params: {
        action: ''
      }
    },
    logout: {
      method: 'GET',
      params: {
        action: 'logout'
      }
    }
  });
});

services.factory('Auth', function($http, $rootScope, User, AuthAPI) {
  var auth;
  auth = {};
  auth.login = function(credentials) {
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
        console.log('1bissssssssssssssssss');
        return console.log($rootScope);
      });
    });
  };
  auth.logout = function() {
    return AuthAPI.logout(function() {
      $http.defaults.headers.common['api_key'] = '';
      $rootScope.authId = null;
      $rootScope.authEmail = null;
      $rootScope.authFistName = '';
      $rootScope.authLastName = null;
      $rootScope.authType = null;
      $rootScope.sidebar = null;
      return $rootScope.isAuth = false;
    });
  };
  return auth;
});

services.factory('Meta', function() {
  var meta;
  return meta = {
    user: {
      name: 'user',
      namePlural: 'users',
      icon: 'fa-users',
      fields: ['id', 'email', 'first_name', 'last_name'],
      form_fields: [
        {
          human: 'Email',
          model: 'email',
          type: 'email',
          required: true,
          placeholder: 'user@example.org'
        }, {
          human: 'Password',
          model: 'password',
          type: 'password',
          required: true,
          placeholder: 'your password'
        }, {
          human: 'First name',
          model: 'first_name',
          type: 'text',
          required: false,
          placeholder: 'Mario'
        }, {
          human: 'Last name',
          model: 'last_name',
          type: 'text',
          required: false,
          placeholder: 'Rossi'
        }
      ]
    },
    product: {
      name: 'product',
      namePlural: 'products',
      icon: 'fa-coffee',
      fields: ['id', 'name'],
      form_fields: [
        {
          human: 'Name',
          model: 'name',
          type: 'text',
          required: true,
          placeholder: 'coffee'
        }
      ]
    },
    adminSidebar: [
      {
        name: 'Users',
        state: 'root.users.list',
        icon: 'fa-users'
      }, {
        name: 'Categories',
        state: 'root.categories.list',
        icon: 'fa-square'
      }, {
        name: 'Products',
        state: 'root.products.list',
        icon: 'fa-coffee'
      }
    ],
    supplierSidebar: [
      {
        name: 'Products',
        state: 'root.products.list',
        icon: 'fa-coffee'
      }, {
        name: 'Catalog',
        state: 'root.catalog',
        icon: 'fa-list'
      }
    ]
  };
});

services.factory('Product', function($resource) {
  return $resource('/api/products/:action/:id', {}, {
    list: {
      method: 'GET',
      params: {
        action: 'index',
        id: -1
      },
      isArray: true
    },
    add: {
      method: 'POST'
    },
    detail: {
      method: 'GET',
      params: {
        action: 'detail'
      }
    },
    update: {
      method: 'POST',
      params: {
        action: 'update'
      }
    },
    "delete": {
      method: 'GET',
      params: {
        action: 'delete'
      }
    }
  });
});

services.service('Session', function() {
  this.create = function(user) {
    this.id = user.id;
    this.email = user.email;
    this.name = user.first_name;
    return this.type = user.type;
  };
  this.destroy = function() {
    this.id = null;
    this.email = null;
    this.name = null;
    return this.type = null;
  };
  return this;
});

services.service('Sidebar', function() {
  this.admin = function() {};
  this.supplier = function() {};
  return this;
});

services.factory('User', function($resource) {
  return $resource('/api/users/:action/:id', {}, {
    list: {
      method: 'GET',
      params: {
        action: 'index',
        id: -1
      },
      isArray: true
    },
    add: {
      method: 'POST'
    },
    detail: {
      method: 'GET',
      params: {
        action: 'detail'
      }
    },
    update: {
      method: 'POST',
      params: {
        action: 'update'
      }
    },
    "delete": {
      method: 'GET',
      params: {
        action: 'delete'
      }
    }
  });
});
