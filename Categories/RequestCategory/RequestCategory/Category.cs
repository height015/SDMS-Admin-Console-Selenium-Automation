
using OpenQA.Selenium;
using SuccessLogin;

namespace RequestCategory;
public class Category
{
    private readonly IWebDriver _webDriver;
    public Category(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public IWebElement catalogLink => _webDriver.FindElement(By.CssSelector("a[class='card'][href='/data-catalogs']"));
    public IWebElement dropDownBox => _webDriver.FindElement(By.Id("SectorId"));
    public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnContinue"));
    public virtual IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public virtual IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public virtual void EnterDataCategory(string title, string reason)
    {
        txtBoxName?.SendKeys(title);
        txtBoxTitle?.SendKeys(reason);
    }
    public void ClickCatalogCard()
    {
        catalogLink.Clicks();
    }
    public void ClickContinue()
    {
        btnContinue.Clicks();
    }
    public virtual void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public void ClickClose()
    {
        btnClose.Clicks();
    }
    public void ClickOk()
    {
        btnClickOk.Clicks();
    }
}

