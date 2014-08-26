services.factory('AuthService', function($http, Session) {
  var authService;
  authService = {};
  authService.login = function(credentials) {
    return $http.post('/api/auth', credentials).then(function(res) {
      var myself;
      $http.defaults.headers.common['api_key'] = res.api_key;
      myself = User.detail(res.id);
      Session.create(res.user_id);
      return res.user;
    });
  };
  authService.isAuthenticated = function() {
    return !!Session.userId;
  };
  return authService;
});

services.service('Session', function() {
  this.create = function(userId) {
    return this.id = userId;
  };
  this.destroy = function() {
    return this.id = null;
  };
  return this;
});
