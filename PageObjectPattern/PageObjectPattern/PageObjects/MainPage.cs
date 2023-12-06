using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PageObjectPattern.PageObjects;

public class MainPage : PageTest
{
    private readonly IPage _page;
    private ILocator _navigationMenuButton => _page.GetByText("Menu");
    private ILocator _secondaryPageNavigation => _page.GetByRole(AriaRole.Navigation, new PageGetByRoleOptions
    {
        Name = "Secondary Page"
    });
    
    public MainPage(IPage page) => _page = page;
    
    //actions
    public async Task Open_NavigationMenu() => await _navigationMenuButton.ClickAsync();
    public async Task Goto_SecondaryPage() => await _secondaryPageNavigation.ClickAsync();

    //asssertions
    public async Task SecondaryPageNavigation_IsVisible() => await Expect(_secondaryPageNavigation).ToBeVisibleAsync();
}