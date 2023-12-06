using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PageObjectPattern.PageObjects;

public class SecondaryPage : PageTest
{
    private readonly IPage _page;
    private const string _mostImportantThing = "Most Important Thing";
    private ILocator _listSearchBox => _page.GetByTestId("listOfImportantThings");
    private ILocator _mostImportantListItem => _page.GetByRole(AriaRole.Listitem, new PageGetByRoleOptions
    {
        Name = _mostImportantThing
    });
    
    public SecondaryPage(IPage page) => _page = page;
    
    //actions
    public async Task SearchInList(string item) => await _listSearchBox.FillAsync(item);

    //asssertions
    public async Task MostImportantListItem_IsVisibleAndEnabled()
    {
        await Expect(_mostImportantListItem).ToBeVisibleAsync();
        await Expect(_mostImportantListItem).ToBeEnabledAsync();
    }
}