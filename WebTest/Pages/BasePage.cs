using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebTest.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;

    protected BasePage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    protected IWebElement Find(By locator)
    {
        return Wait.Until(driver => driver.FindElement(locator));
    }

    protected IReadOnlyCollection<IWebElement> FindAll(By locator)
    {
        return Wait.Until(driver =>
        {
            var elements = driver.FindElements(locator);
            return elements.Count > 0 ? elements : null;
        });
    }

    protected void Click(By locator)
    {
        Find(locator).Click();
    }

    protected void Type(By locator, string text)
    {
        var element = Find(locator);
        element.Clear();
        element.SendKeys(text);
    }

    protected string GetText(By locator)
    {
        return Find(locator).Text;
    }
}