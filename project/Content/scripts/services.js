services.factory('AuthService', function($http, User, Session) {
  var authService;
  authService = {};
  authService.login = function(credentials) {
    return $http.post('/api/auth', credentials).then(function(res_auth) {
      console.log(res_auth);
      console.log(res_auth.data);
      console.log(res_auth.data.api_key);
      return User.detail({
        id: res_auth.data.user_id
      }, function(res_user) {
        var me;
        me = res_user;
        console.log(me);
        return Session.create(me, res_auth.data.api_key);
      });
    });
  };
  authService.logout = function() {
    return Session.destroy();
  };
  authService.isAuthenticated = function() {
    return !!Session.me.user_id;
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
        id: 0
      },
      isArray: true
    },
    add: {
      method: 'POST',
      params: {
        action: 'add',
        id: -1
      }
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
      method: 'POST',
      params: {
        action: 'delete'
      }
    }
  });
});

services.service('Session', function($http) {
  this.create = function(me, apiKey) {
    this.user_id = me.user_id;
    this.user_type = me.user_type;
    return $http.defaults.headers.common['api_key'] = apiKey;
  };
  this.destroy = function() {
    this.user_id = null;
    this.user_type = null;
    return $http.defaults.headers.common['api_key'] = '';
  };
  return this;
});

services.factory('User', function($resource) {
  return $resource('/api/users/:action/:id', {}, {
    list: {
      method: 'GET',
      params: {
        action: 'index',
        id: 0
      },
      isArray: true
    },
    add: {
      method: 'POST',
      params: {
        action: 'add',
        id: -1
      }
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
      method: 'POST',
      params: {
        action: 'delete'
      }
    }
  });
});
