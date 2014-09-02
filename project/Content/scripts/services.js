services.factory('AuthService', function($http, User, Session) {
  var authService;
  authService = {};
  authService.login = function(credentials) {
    return $http.post('/api/auth', credentials).then(function(res_auth) {
      $http.defaults.headers.common['api_key'] = res_auth.data.api_key;
      return User.detail({
        id: res_auth.data.user_id
      }, function(res_user) {
        return Session.create(res_user);
      });
    });
  };
  authService.logout = function() {
    $http.defaults.headers.common['api_key'] = '';
    return Session.destroy();
  };
  return authService;
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
    }
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
  this.create = function(user, apiKey) {
    this.id = user.id;
    this.email = user.email;
    this.name = user.first_name;
    return this.type = user.user_type;
  };
  this.destroy = function() {
    this.id = null;
    this.email = null;
    this.name = null;
    return this.type = null;
  };
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
