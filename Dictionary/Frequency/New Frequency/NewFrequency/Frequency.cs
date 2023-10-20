
using OpenQA.Selenium;
using Commons;

namespace NewFrequency;
public class Frequency
{
    private readonly IWebDriver _webDriver;
    public Frequency(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dictionary/frequencies/add']"));
    public IWebElement txtName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtShort => _webDriver.FindElement(By.Id("ShortName"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public void ClickNew()
    {
        Newlink.Clicks();
    }
    public void NewDataFrequencyEntery(string name, string shortName)
    {
        txtName.SendKeys(name);
        txtShort.SendKeys(shortName);
    }
    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public void ClickOk()
    {
        btnClickOk.Clicks();
    }
}


