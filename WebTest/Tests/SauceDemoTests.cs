using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebTest.Config;
using WebTest.Pages;

namespace WebTest.Tests;

[TestFixture]
public class SauceDemoTests
{
    private IWebDriver driver = null!;
    private WebDriverWait wait = null!;
    private TestSettings settings = null!;

    [SetUp]
    public void SetUp()
    {
        settings = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build()
            .Get<TestSettings>()!;

        var options = new ChromeOptions();

        if (settings.Headless)
        {
            options.AddArgument("--headless=new");
        }

        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-save-password-bubble");
        options.AddArgument("--disable-features=PasswordManagerOnboarding,PasswordLeakDetection");
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);
        options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

        driver = new ChromeDriver(options);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(settings.TimeoutSeconds));
    }

    [Test]
    public void SauceLabsBikeLight_ShouldHaveSameNameAndPrice_InListDetailsAndCart()
    {
        var loginPage = new LoginPage(driver, wait);
        var inventoryPage = new InventoryPage(driver, wait);
        var productDetailsPage = new ProductDetailsPage(driver, wait);
        var cartPage = new CartPage(driver, wait);

        loginPage.Open(settings.BaseUrl);
        loginPage.Login(settings.Username, settings.Password);

        inventoryPage.SortByPriceHighToLow();

        var productFromList = inventoryPage.GetProductInfoByName(settings.ProductName);

        inventoryPage.OpenProductDetails(settings.ProductName);

        var productFromDetails = productDetailsPage.GetProductInfo();

        Assert.That(productFromDetails.Name, Is.EqualTo(productFromList.Name));
        Assert.That(productFromDetails.Price, Is.EqualTo(productFromList.Price));

        productDetailsPage.AddToCart();

        Assert.That(productDetailsPage.GetCartBadgeCount(), Is.EqualTo(settings.ExpectedCartCount));

        productDetailsPage.OpenCart();

        var productFromCart = cartPage.GetProductInfo();

        Assert.That(productFromCart.Name, Is.EqualTo(productFromList.Name));
        Assert.That(productFromCart.Price, Is.EqualTo(productFromList.Price));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}