services.factory('AuthService', function($http, Session) {
  var authService;
  authService = {};
  authService.login = function(credentials) {
    return $http.post('/api/auth', credentials).then(function(res) {
      var me;
      console.log(res.api_key);
      me = User.detail({
        id: res.user_id
      });
      return Session.create(me, res.api_key);
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
          placeholder: 'user@example.org'
        }, {
          human: 'Password',
          model: 'password',
          type: 'password',
          placeholder: 'your password'
        }, {
          human: 'First name',
          model: 'first_name',
          type: 'text',
          placeholder: 'Mario'
        }, {
          human: 'Last name',
          model: 'last_name',
          type: 'text',
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
    }
  });
});
