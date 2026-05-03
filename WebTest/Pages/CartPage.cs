using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebTest.Models;

namespace WebTest.Pages;

public class CartPage : BasePage
{
    private readonly By CartItem = By.ClassName("cart_item");
    private readonly By ItemName = By.ClassName("inventory_item_name");
    private readonly By ItemPrice = By.ClassName("inventory_item_price");

    public CartPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) {}

    public ProductInfo GetProductInfo()
    {
        var cartItem = Find(CartItem);

        var name = cartItem.FindElement(ItemName).Text;
        var price = cartItem.FindElement(ItemPrice).Text;

        return new ProductInfo(name, price);
    }
}