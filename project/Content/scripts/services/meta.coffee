services.factory 'Meta', () ->

  meta =

    # Resources -------------------------------------------
    admin:
      name: 'admin'
      namePlural: 'admins'
      nameHuman: 'Admins'
      icon: 'fa fa-users'
      fields: [
          human: 'Email'
          model: 'email'
          type: 'email'
          required: true
          placeholder: 'user@example.org'
        ,
          human: 'First name'
          model: 'first_name'
          type: 'text'
          required: true
          placeholder: 'Mario'
        ,
          human: 'Last name'
          model: 'last_name'
          type: 'text'
          required: true
          placeholder: 'Rossi'
      ]
      related_fields: []
      extra_fields: [
          human: 'Password'
          model: 'password'
          type: 'password'
          required: true
          placeholder: 'your password'
      ]

    supplier:
      name: 'supplier'
      namePlural: 'suppliers'
      nameHuman: 'Suppliers'
      icon: 'fa fa-building'
      fields: [
          human: 'Supplier Name'
          model: 'supplier_name'
          type: 'text'
          required: true
          placeholder: 'company name'
          supplier: true
        ,
          human: 'Email'
          model: 'email'
          type: 'email'
          required: true
          placeholder: 'user@example.org'
        ,
          human: 'First name'
          model: 'first_name'
          type: 'text'
          required: true
          placeholder: 'Mario'
        ,
          human: 'Last name'
          model: 'last_name'
          type: 'text'
          required: true
          placeholder: 'Rossi'
        ,
          human: 'VAT'
          model: 'vat'
          type: 'text'
          required: true
          placeholder: ''
          supplier: true
      ]
      related_fields: [
          related_model: 'city'
          related_human: 'name'
          model: 'city'
          human: 'City'
          required: true
          supplier: true
      ]
      extra_fields: [
          human: 'Password'
          model: 'password'
          type: 'password'
          required: true
          placeholder: 'your password'
      ]

    product:
      name: 'product'
      namePlural: 'products'
      nameHuman: 'Products'
      icon: 'fa fa-coffee'
      fields: [
          model: 'name'
          human: 'Name'
          type: 'text'
          required: true
          placeholder: 'coffee'
      ]
      related_fields: [
          related_model: 'product_category'
          related_human: 'name'
          model: 'category'
          human: 'Category'
          required: true
        ,
          related_model: 'supplier_id'
          related_human: 'email'
          model: 'supplier'
          human: 'Supplier'
          required: true
      ]

    stock:
      name: 'stock'
      namePlural: 'stocks'
      nameHuman: 'Product Stocks'
      icon: 'fa fa-shopping-cart'
      fields: [
          model: 'price'
          human: 'Price'
          type: 'text'
          required: true
          placeholder: '2.50'
        ,
          model: 'min'
          human: 'Min'
          type: 'text'
          required: true
          placeholder: '1'
        ,
          model: 'max'
          human: 'Max'
          type: 'text'
          required: true
          placeholder: '20'
        ,
          model: 'availability'
          human: 'Availability'
          type: 'text'
          required: true
          placeholder: '100'
      ]
      related_fields: [
          related_model: 'product_id'
          related_human: 'name'
          model: 'product'
          human: 'Product'
          required: true
      ]

    category:
      name: 'category'
      namePlural: 'categories'
      nameHuman: 'Categories'
      icon: 'glyphicon glyphicon-tags'
      fields: [
          human: 'Name'
          model: 'name'
          type: 'text'
          required: true
          placeholder: 'food'
      ]
      related_fields: []

    city:
      name: 'city'
      namePlural: 'cities'
      nameHuman: 'Cities'
      icon: 'fa fa-home'
      fields: [
          human: 'Name'
          model: 'name'
          type: 'text'
          required: true
          placeholder: 'Urbino'
      ]
      related_fields: []


    # Sidebars --------------------------------------------
    adminSidebar: [
        name: 'Admins'
        state: 'root.admins.list'
        icon: 'fa fa-users'
      ,
        name: 'Suppliers'
        state: 'root.suppliers.list'
        icon: 'fa fa-building'
      ,
        name: 'Cities'
        state: 'root.cities.list'
        icon: 'fa fa-home'
      ,
        name: 'Categories'
        state: 'root.categories.list'
        icon: 'glyphicon glyphicon-tags'
    ]

    supplierSidebar: [
        name: 'Products'
        state: 'root.products.list'
        icon: 'fa fa-coffee'
      ,
        name: 'Product Stocks'
        state: 'root.stocks.list'
        icon: 'fa fa-shopping-cart'
     ,
        name: 'Catalog'
        state: 'root.catalog'
        icon: 'fa fa-list'
    ]
