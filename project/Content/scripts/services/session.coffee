services.service 'Session', () ->
  this.create = (userId) ->
    this.id = userId

  this.destroy = () ->
    this.id = null

  this
