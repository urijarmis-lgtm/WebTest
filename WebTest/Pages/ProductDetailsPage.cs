using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebTest.Models;

namespace WebTest.Pages;

public class ProductDetailsPage : BasePage
{
    private readonly By ProductName = By.ClassName("inventory_details_name");
    private readonly By ProductPrice = By.ClassName("inventory_details_price");
    private readonly By AddToCartButton = By.Id("add-to-cart");
    private readonly By CartBadge = By.ClassName("shopping_cart_badge");
    private readonly By CartLink = By.ClassName("shopping_cart_link");
    
    public ProductDetailsPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) {}
    
    public ProductInfo GetProductInfo()
    {
        return new ProductInfo(
            GetText(ProductName),
            GetText(ProductPrice)
        );
    }
    
    public void AddToCart()
    {
        Click(AddToCartButton);
    }
    
    public string GetCartBadgeCount()
    {
        return GetText(CartBadge);
    }
    
    public void OpenCart()
    {
        Click(CartLink);
    }
}