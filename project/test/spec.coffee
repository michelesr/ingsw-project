describe 'angularjs homepage', ->

  it 'should have a title', ->
    browser.get '/'

    expect browser.getTitle()
      .toEqual 'Project'


