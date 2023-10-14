using OpenQA.Selenium;

namespace AddNewSector;

public class Sector
{
    private readonly IWebDriver _webDriver;
    public Sector(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public virtual IWebElement txtName => _webDriver.FindElement(By.Id("Name"));
    public virtual IWebElement txtTitle => _webDriver.FindElement(By.Id("Title"));
    public virtual IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public virtual IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public virtual IWebElement textMsgRes => _webDriver.FindElement(By.CssSelector("p[style='display: block;']"));
    public virtual IWebElement btnClickNew => _webDriver.FindElement(By.CssSelector("a.item-button[data-modal='']"));
    public virtual IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public virtual void EnterNameAndTitle(string userName, string password)
    {
        txtName.SendKeys(userName);
        txtTitle.SendKeys(password);
    }
    public virtual void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public virtual void ClickClose()
    {
        btnClose.Clicks();
    }
    public virtual void ClickNew()
    {
        btnClickNew.Clicks();
    }
    public virtual void ClickOk()
    {
        btnClickOk.Clicks();
    }
}
