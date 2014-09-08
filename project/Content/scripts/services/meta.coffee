services.factory 'Meta', () ->

  meta =

    # Resources -------------------------------------------
    user:
      name: 'user'
      namePlural: 'users'
      nameHuman: 'Users'
      icon: 'fa fa-users'
      list_fields: [
        model: 'first_name'
        human: 'First name'
      ,
        model: 'last_name'
        human: 'Last name'
      ,
        model: 'email'
        human: 'Email'
      ]
      form_fields: [
        human: 'Email'
        model: 'email'
        type: 'email'
        required: true
        placeholder: 'user@example.org'
      ,
        human: 'Password'
        model: 'password'
        type: 'password'
        required: true
        placeholder: 'your password'
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
      list_fields: [
        model: 'name'
        human: 'Name'
      ]
      form_fields: [
        human: 'Name'
        model: 'name'
        type: 'text'
        required: true
        placeholder: 'coffee'
      ]

    category:
      name: 'category'
      namePlural: 'categories'
      nameHuman: 'Categories'
      icon: 'glyphicon glyphicon-tags'
      list_fields: [
        model: 'name'
        human: 'Name'
      ]
      form_fields: [
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
