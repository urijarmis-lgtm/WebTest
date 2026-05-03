using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebTest.Pages;

public class LoginPage : BasePage
{
    private readonly By UsernameInput = By.Id("user-name");
    private readonly By PasswordInput = By.Id("password");
    private readonly By LoginButton = By.Id("login-button");
    private readonly By InventoryList = By.ClassName("inventory_list");

    public LoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) {}
    
    public void Open(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }
    
    public void Login(string username, string password)
    {
        Type(UsernameInput, username);
        Type(PasswordInput, password);
        Click(LoginButton);

        Find(InventoryList);
    }
}