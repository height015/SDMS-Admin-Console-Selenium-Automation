using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SuccessLogin;

namespace NewRequest;

public class NewRequest
{
    private readonly IWebDriver _webDriver;
    public NewRequest(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReson => _webDriver.FindElement(By.Id("txtReason"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.Id("btnSave"));
    public virtual IWebElement table => _webDriver.FindElement(By.ClassName("table"));

    // Get all the rows in the table
    public virtual List<IWebElement> rows => table.FindElements(By.TagName("tr")).ToList();
    public virtual IWebElement btnProcessSelected => _webDriver.FindElement(By.Id("btnReqSelect"));
    public IWebElement btnClickOk
    {
        get
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".sa-confirm-button-container .confirm")));
        }
    }
    public virtual IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public virtual void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReson.SendKeys(reason);
    }

    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public void ClickClose()
    {
        btnClose.Clicks();
    }
    public void ProcessSelectedCheckedBoxes()
    {
        btnProcessSelected.Clicks();
    }
    public void ClickOk()
    {
        btnClickOk.Clicks();
    }
}

