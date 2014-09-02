services.service 'Session', () ->

  this.create = (user) ->
    this.id = user.id
    this.email = user.email
    this.name = user.first_name
    this.type = user.user_type
    this.auth = true

  this.destroy = () ->
    this.id = null
    this.email = null
    this.name = null
    this.type = null
    this.auth = false

  this
