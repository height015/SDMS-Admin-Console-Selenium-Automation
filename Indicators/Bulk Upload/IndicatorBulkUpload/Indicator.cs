using OpenQA.Selenium;
using SuccessLogin;

namespace BulkIndicator;
public class Indicator
{
    private readonly IWebDriver _webDriver;
    public Indicator(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement dropDownCascadeSecor => _webDriver.FindElement(By.Id("SectorId"));
    public IWebElement dropDownCat => _webDriver.FindElement(By.Id("CategoryId"));
    public IWebElement dropDownTable => _webDriver.FindElement(By.Id("TableId"));
    public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnContinue"));
    public IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dataset/indicators/add']"));
    public IWebElement IndiComboTree => _webDriver.FindElement(By.CssSelector("div#comboTree846411InputWrapper input.comboTreeInputBox"));
    public IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public IWebElement checkBoxEmph => _webDriver.FindElement(By.Id("IsEmphasized"));
    public IWebElement displayOrder => _webDriver.FindElement(By.Id("DisplayOrder"));
    public IWebElement DisplayInChart => _webDriver.FindElement(By.Id("ChkGraphTitle"));
    public IWebElement txtGrapTit => _webDriver.FindElement(By.Id("GraphTitle"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));
    public virtual IWebElement btnBrowseFile => _webDriver.FindElement(By.Id("fileUpload"));
    public virtual IWebElement btnUpload => _webDriver.FindElement(By.Id("btn_upload"));
    public IWebElement btnUpdate => _webDriver.FindElement(By.Id("btnUpdateLine"));
    public virtual IWebElement table => _webDriver.FindElement(By.ClassName("table"));
    public virtual List<IWebElement> rows => table.
        FindElements(By.TagName("tr")).ToList();
    public virtual IWebElement btnApply => _webDriver
        .FindElement(By.PartialLinkText("Apply"));

    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReason => _webDriver.FindElement(By.Id("txtReason"));

    public IWebElement chkBtnDisplayInChart => _webDriver.FindElement(By.Id("ChkGraphTitle"));

    public IWebElement txtDisplayInChart => _webDriver.FindElement(By.Id("GraphTitle"));


    public IWebElement btnUpdateModify => _webDriver.FindElement(By.Id("btnSaveLineItem"));

    public IWebElement comboTree => _webDriver.FindElement(By.ClassName("comboTreeDropDownContainer"));
    public IWebElement liTree => comboTree.FindElement(By.TagName("li"));

    public IWebElement txtTopLevelBox => _webDriver.FindElement
        (By.Id("topLevelInd"));

    public IList<IWebElement> boxSel => _webDriver.FindElements(By.CssSelector("span.comboTreeItemTitle.selectable"));
    public void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReason.SendKeys(reason);
    }
    public void ClickContinue()
    {
        btnContinue.Clicks();
    }
    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public void ClickNew()
    {
        Newlink.Clicks();
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


