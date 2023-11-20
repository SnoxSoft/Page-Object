using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Reflection;

namespace PageObjectPattern;

public class TestConfig
{
    private IPage Page = null!;
    private string _appURL { get; set; } = null!;
    private string _username { get; set; } = null!;
    private string _password { get; set; } = null!;
    private ILocator _usernameLoginTextField => Page.Locator("id=username_login");
    private ILocator _passwordLoginTextField => Page.Locator("id=password_login");
    private ILocator _loginButton => Page.Locator("text=Log in");
    
    private static readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddUserSecrets(Assembly.GetExecutingAssembly())
        .Build();
    
    [SetUp]
    public async Task SetUpBrowserContext()
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Timeout = 60000
        });
        Page = await browser.NewPageAsync();
    }
    
    [SetUp]
    public void SetUpAppsettingsContext()
    {
        _appURL = _configuration["AppURL"]!;
        _username = _configuration["Credentials:Username"]!;
        _password = _configuration["Credentials:Password"]!;
    }

    protected async Task LogInToApplication()
    {
        await Page.GotoAsync(_appURL);
        await _usernameLoginTextField.FillAsync(_username);
        await _passwordLoginTextField.FillAsync(_password);
        await _loginButton.ClickAsync();
    }
}