services.factory 'Meta', () ->

  meta =

    # Resources -------------------------------------------
    user:
      name: 'user'
      namePlural: 'users'
      nameHuman: 'Users'
      icon: 'fa-users'
      fields: [
        'id'
        'email'
        'first_name'
        'last_name'
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
      icon: 'fa-coffee'
      fields: [
        'id'
        'name'
      ]
      form_fields: [
        human: 'Name'
        model: 'name'
        type: 'text'
        required: true
        placeholder: 'coffee'
      ]


    # Sidebars --------------------------------------------
    adminSidebar: [
      name: 'Users'
      state: 'root.users.list'
      icon: 'fa-users'
    ,
      name: 'Categories'
      state: 'root.categories.list'
      icon: 'fa-square'

    # dev
    ,
      name: 'Products'
      state: 'root.products.list'
      icon: 'fa-coffee'
    # end dev
    ]

    supplierSidebar: [
      name: 'Products'
      state: 'root.products.list'
      icon: 'fa-coffee'
    ,
      name: 'Catalog'
      state: 'root.catalog'
      icon: 'fa-list'
    ]
