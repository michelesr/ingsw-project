services.factory 'Meta', () ->

  meta =

    user:
      name: 'user'
      namePlural: 'users'
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
        placeholder: 'user@example.org'
      ,
        human: 'Password'
        model: 'password'
        type: 'password'
        placeholder: 'your password'
      ,
        human: 'First name'
        model: 'first_name'
        type: 'text'
        placeholder: 'Mario'
      ,
        human: 'Last name'
        model: 'last_name'
        type: 'text'
        placeholder: 'Rossi'
      ]

    product:
      name: 'product'
      namePlural: 'products'
      icon: 'fa-coffee'
      fields: [
        'id'
        'name'
      ]
      form_fields: [
        human: 'Name'
        model: 'name'
        type: 'text'
        placeholder: 'coffee'
      ]
