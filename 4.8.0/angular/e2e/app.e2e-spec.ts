import { CMS_BoilerplateTemplatePage } from './app.po';

describe('CMS_Boilerplate App', function() {
  let page: CMS_BoilerplateTemplatePage;

  beforeEach(() => {
    page = new CMS_BoilerplateTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
