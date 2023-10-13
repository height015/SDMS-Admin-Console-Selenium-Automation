using OpenQA.Selenium;
using SuccessLogin;
namespace NewDataEntity;

public class NewRequest
{
    private readonly IWebDriver _webDriver;
    public NewRequest(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dictionary/units/add']"));
    public IWebElement txtName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtShort => _webDriver.FindElement(By.Id("ShortName"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));
    public IWebElement table => _webDriver.FindElement(By.ClassName("table")) ?? null;
    // Get all the rows in the table
    public List<IWebElement> rows => table.FindElements(By.TagName("tr")).ToList();
    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReason => _webDriver.FindElement(By.Id("txtReason"));
    public void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReason.SendKeys(reason);
    }
    public void ClickNew()
    {
        Newlink.Clicks();
    }
    public void NewDataUnitEntery(string name, string shortName)
    {
        txtName.SendKeys(name);
        txtShort.SendKeys(shortName);
    }
    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public void ClickSave()
    {
        btnSave.Clicks();
    }
    public void ClickOk()
    {
        btnClickOk.Clicks();
    }
}
