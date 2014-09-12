services.factory 'Meta', () ->

  meta =

    # Resources -------------------------------------------
    user:
      name: 'user'
      namePlural: 'users'
      nameHuman: 'Users'
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
        required: false
        placeholder: 'Mario'
      ,
        human: 'Last name'
        model: 'last_name'
        type: 'text'
        required: false
        placeholder: 'Rossi'
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
          required: false
        ,
          related_model: 'supplier_id'
          related_human: 'email'
          model: 'supplier'
          human: 'Supplier'
          required: true
      ]

    category:
      name: 'category'
      namePlural: 'categories'
      nameHuman: 'Categories'
      icon: 'glyphicon glyphicon-tags'
      fields: [
        model: 'name'
        human: 'Name'
      ]
      fields: [
        human: 'Name'
        model: 'name'
        type: 'text'
        required: true
        placeholder: 'food'
      ]


    # Sidebars --------------------------------------------
    adminSidebar: [
        name: 'Users'
        state: 'root.users.list'
        icon: 'fa fa-users'
      ,
        name: 'Categories'
        state: 'root.categories.list'
        icon: 'glyphicon glyphicon-tags'
      ,
      # dev
        name: 'Products'
        state: 'root.products.list'
        icon: 'fa fa-coffee'
      # end dev
    ]

    supplierSidebar: [
        name: 'Products'
        state: 'root.products.list'
        icon: 'fa fa-coffee'
      ,
        name: 'Catalog'
        state: 'root.catalog'
        icon: 'fa fa-list'
    ]
