using OpenQA.Selenium;
using SuccessLogin;


namespace BulkTable;

public class Tables
{
    private readonly IWebDriver _webDriver;
    public Tables(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public virtual IWebElement dropDownCascadeSecor => _webDriver.FindElement(By.Id("SectorId"));
    public virtual IWebElement dropDownCat => _webDriver.FindElement(By.Id("CategoryId"));
    public virtual IWebElement btnContinue => _webDriver.FindElement(By.Id("btnContinue"));
    public virtual IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dataset/tables/add']"));
    public virtual IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public virtual IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public virtual IWebElement txtDescription => _webDriver.FindElement(By.Id("Description"));
    public virtual IWebElement dropDownFeq => _webDriver.FindElement(By.Id("FrequencyId"));
    public virtual IWebElement dropDownUnit => _webDriver.FindElement(By.Id("UnitId"));
    public virtual IWebElement btnBrowseFile => _webDriver.FindElement(By.Id("fileUpload"));
    public virtual IWebElement btnUpload => _webDriver.FindElement(By.Id("btn_upload"));
    public virtual IWebElement checkBox1 => _webDriver.FindElement(By.Id("ChkDataLabel1"));
    public IWebElement checkBox2 => _webDriver.FindElement(By.Id("ChkDataLabel2"));
    public IWebElement checkBox3 => _webDriver.FindElement(By.Id("ChkDataLabel3"));
    public IWebElement checkBox4 => _webDriver.FindElement(By.Id("ChkDataLabel4"));
    public IWebElement checkBox5 => _webDriver.FindElement(By.Id("ChkDataLabel5"));
    public IWebElement txtDataLab1 => _webDriver.FindElement(By.Id("DataLabel1"));
    public IWebElement txtDataLab2 => _webDriver.FindElement(By.Id("DataLabel2"));
    public IWebElement txtDataLab3 => _webDriver.FindElement(By.Id("DataLabel3"));
    public IWebElement txtDataLab4 => _webDriver.FindElement(By.Id("DataLabel4"));
    public IWebElement txtDataLab5 => _webDriver.FindElement(By.Id("DataLabel5"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));

    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));
    public IWebElement btnUpdate => _webDriver.FindElement(By.Id("btnUpdateLine"));

    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));

    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReason => _webDriver.FindElement(By.Id("txtReason"));
    public virtual IWebElement table => _webDriver.FindElement(By.ClassName("table")) ?? null;
    public virtual IWebElement btnApply => _webDriver.FindElement(By.PartialLinkText("Apply"));
    public virtual List<IWebElement> rows => table.FindElements(By.TagName("tr")).ToList();
    public virtual void EnterTableInfoData(string name, string titile, string desc)
    {
        txtBoxName.SendKeys(name);
        txtBoxTitle.SendKeys(titile);
        txtDescription.SendKeys(desc);
    }
    public void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReason.SendKeys(reason);
    }
    public void Enable(IWebElement webElement)
    {
        webElement.Clicks();
    }
    public void ClickContinue()
    {
        btnContinue.Clicks();
    }
    public void ClickSave()
    {
        btnSave.Clicks();
    }
    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public void ClickUpdate()
    {
        btnUpdate.Clicks();
    }
    public void ClickNew()
    {
        Newlink.Clicks();
    }
    public void ClickOk()
    {
        btnClickOk.Clicks();
    }
}

