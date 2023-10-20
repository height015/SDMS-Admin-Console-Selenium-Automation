
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Commons;
public static class MethodsExtentions
{
    public static void EnterText(this IWebElement element, string value)
    {
        element.SendKeys(value);
    }
    public static void Clicks(this IWebElement element)
    {
        element.Click();
    }
    public static void SelectDropDownByIndex(this IWebElement element, int value)
    {
        new SelectElement(element).SelectByIndex(value);
    }
    public static void SelectDropDownByValue(this IWebElement element, int value)
    {
        new SelectElement(element).SelectByValue(value.ToString());
    }
    public static void SelectDropDownByText(this IWebElement element, string value)
    {
        new SelectElement(element).SelectByText(value);
    }

    public static IWebElement WaitForElementToBeClickable(this IWebDriver driver, IWebElement element, int timeoutInSeconds)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        return wait.Until(ExpectedConditions.ElementToBeClickable(element));
    }
}


