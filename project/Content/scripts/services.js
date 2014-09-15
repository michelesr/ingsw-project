services.factory('Auth', function($resource) {
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

services.factory('Category', function($resource) {
  return $resource('/api/categories/:action/:id', {}, {
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
    remove: {
      method: 'GET',
      params: {
        action: 'delete'
      }
    }
  });
});

services.factory('City', function($resource) {
  return $resource('/api/cities/:action/:id', {}, {
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
    remove: {
      method: 'GET',
      params: {
        action: 'delete'
      }
    }
  });
});

services.factory('Meta', function() {
  var meta;
  return meta = {
    user: {
      name: 'user',
      namePlural: 'users',
      nameHuman: 'Users',
      icon: 'fa fa-users',
      fields: [
        {
          human: 'Email',
          model: 'email',
          type: 'email',
          required: true,
          placeholder: 'user@example.org'
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
      ],
      related_fields: []
    },
    product: {
      name: 'product',
      namePlural: 'products',
      nameHuman: 'Products',
      icon: 'fa fa-coffee',
      fields: [
        {
          model: 'name',
          human: 'Name',
          type: 'text',
          required: true,
          placeholder: 'coffee'
        }
      ],
      related_fields: [
        {
          related_model: 'product_category',
          related_human: 'name',
          model: 'category',
          human: 'Category',
          required: true
        }, {
          related_model: 'supplier_id',
          related_human: 'email',
          model: 'supplier',
          human: 'Supplier',
          required: true
        }
      ]
    },
    stock: {
      name: 'stock',
      namePlural: 'stocks',
      nameHuman: 'Product Stocks',
      icon: 'fa fa-shopping-cart',
      fields: [
        {
          model: 'price',
          human: 'Price',
          type: 'text',
          required: true,
          placeholder: '2.50'
        }, {
          model: 'min',
          human: 'Min',
          type: 'text',
          required: true,
          placeholder: '1'
        }, {
          model: 'max',
          human: 'Max',
          type: 'text',
          required: true,
          placeholder: '20'
        }, {
          model: 'availability',
          human: 'Availability',
          type: 'text',
          required: true,
          placeholder: '100'
        }
      ],
      related_fields: [
        {
          related_model: 'product_id',
          related_human: 'name',
          model: 'product',
          human: 'Product',
          required: true
        }
      ]
    },
    category: {
      name: 'category',
      namePlural: 'categories',
      nameHuman: 'Categories',
      icon: 'glyphicon glyphicon-tags',
      fields: [
        {
          human: 'Name',
          model: 'name',
          type: 'text',
          required: true,
          placeholder: 'food'
        }
      ],
      related_fields: []
    },
    city: {
      name: 'city',
      namePlural: 'cities',
      nameHuman: 'Cities',
      icon: 'fa fa-home',
      fields: [
        {
          human: 'Name',
          model: 'name',
          type: 'text',
          required: true,
          placeholder: 'Urbino'
        }
      ],
      related_fields: []
    },
    adminSidebar: [
      {
        name: 'Users',
        state: 'root.users.list',
        icon: 'fa fa-users'
      }, {
        name: 'Cities',
        state: 'root.cities.list',
        icon: 'fa fa-home'
      }, {
        name: 'Categories',
        state: 'root.categories.list',
        icon: 'glyphicon glyphicon-tags'
      }, {
        name: 'Products',
        state: 'root.products.list',
        icon: 'fa fa-coffee'
      }, {
        name: 'Product Stocks',
        state: 'root.stocks.list',
        icon: 'fa fa-shopping-cart'
      }, {
        name: 'Catalog',
        state: 'root.catalog',
        icon: 'fa fa-list'
      }
    ],
    supplierSidebar: [
      {
        name: 'Products',
        state: 'root.products.list',
        icon: 'fa fa-coffee'
      }, {
        name: 'Product Stocks',
        state: 'root.stocks.list',
        icon: 'fa fa-shopping-cart'
      }, {
        name: 'Catalog',
        state: 'root.catalog',
        icon: 'fa fa-list'
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
    remove: {
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

services.factory('Stock', function($resource) {
  return $resource('/api/stocks/:action/:id', {}, {
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
    remove: {
      method: 'GET',
      params: {
        action: 'delete'
      }
    }
  });
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
    remove: {
      method: 'GET',
      params: {
        action: 'delete'
      }
    }
  });
});
