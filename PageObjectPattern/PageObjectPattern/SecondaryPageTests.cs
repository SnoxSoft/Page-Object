using NUnit.Framework;
using PageObjectPattern.PageObjects;

namespace PageObjectPattern;

[TestFixture]
public class SecondaryPageTests : TestConfig
{
    private MainPage _mainPage = null!;
    private SecondaryPage _secondaryPage = null!;

    [Test]
    public async Task SecondaryPageListSearch()
    {
        _mainPage = new MainPage(Page);
        _secondaryPage = new SecondaryPage(Page);

        await LogIn_ToApplication();
        await _mainPage.Open_NavigationMenu();
        await _mainPage.SecondaryPageNavigation_IsVisible();
        await _mainPage.Goto_SecondaryPage();
        await _secondaryPage.SearchInList("Important");
        await _secondaryPage.MostImportantListItem_IsVisibleAndEnabled();
    }
}