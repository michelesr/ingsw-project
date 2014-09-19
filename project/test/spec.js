describe('angularjs homepage', function() {
  return it('should have a title', function() {
    browser.get('/');
    return expect(browser.getTitle()).toEqual('Project');
  });
});
