﻿
using OpenQA.Selenium;
using Commons;

namespace NewDataEntity;

public class DataEntities
{
    private readonly IWebDriver _webDriver;

    public DataEntities(IWebDriver webDriver)
    {
        _webDriver = webDriver;

    }

    public IWebElement txtName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtShort => _webDriver.FindElement(By.Id("ShortName"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public void NewDatadataEntityEntry(string name, string shortName)
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
