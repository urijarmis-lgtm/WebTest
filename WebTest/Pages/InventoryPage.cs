using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebTest.Models;

namespace WebTest.Pages;

public class InventoryPage : BasePage
{
    private readonly By SortDropdown = By.ClassName("product_sort_container");
    private readonly By InventoryItems = By.ClassName("inventory_item");
    private readonly By ItemName = By.ClassName("inventory_item_name");
    private readonly By ItemPrice = By.ClassName("inventory_item_price");

    public InventoryPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) {}
    
    public void SortByPriceHighToLow()
    {
        var dropdown = Find(SortDropdown);
        var select = new SelectElement(dropdown);

        select.SelectByValue("hilo");
    }
    
    public ProductInfo GetProductInfoByName(string productName)
    {
        var product = FindProductByName(productName);

        var name = product.FindElement(ItemName).Text;
        var price = product.FindElement(ItemPrice).Text;

        return new ProductInfo(name, price);
    }

    public void OpenProductDetails(string productName)
    {
        var product = FindProductByName(productName);
        product.FindElement(ItemName).Click();
    }

    private IWebElement FindProductByName(string productName)
    {
        return Wait.Until(driver =>
        {
            var products = driver.FindElements(InventoryItems);

            return products.FirstOrDefault(product =>
                product.FindElement(ItemName).Text == productName);
        });
    }
}