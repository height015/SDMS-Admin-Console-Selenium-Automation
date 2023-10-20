using OpenQA.Selenium;
using SuccessLogin;
using Commons;


namespace NewQuickFlash;
public class QuickFlashes
{
    private readonly IWebDriver _webDriver;
    public QuickFlashes(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement dropDownSector => _webDriver.FindElement(By.Id("DSectorId"));
    public IWebElement dropDownCategory => _webDriver.FindElement(By.Id("DCategoryId"));
    public IWebElement dropDownTable => _webDriver.FindElement(By.Id("DTableId"));
    public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnDSContinue"));
    public IWebElement btnContinueSelection => _webDriver.FindElement(By.Id("btnContSelection"));
    public IWebElement OpenStartDateDrpDwn => _webDriver.FindElement(By.Id("txtAnnualStartPeriod"));  //txtWeekStartPeriod
    public IWebElement OpenStopDateDrpDwn => _webDriver.FindElement(By.Id("txtAnnualStopPeriod"));    //txtWeekStopPeriod
    public IWebElement readonlyInput => _webDriver.FindElement(By.XPath("//input[@type='text' and @aria-describedby='secname' and @data-value]"));
    public IWebElement contentPopUp => _webDriver.FindElement(By.CssSelector("a[href='/shop/analytics/manage'][data-modal].item-button"));
    public IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public IWebElement dropDwnArrow => _webDriver.FindElement(By.Id("ArrowType"));
    public IWebElement dropDwonArrDir => _webDriver.FindElement(By.Id("ArrowDirection"));
    public IWebElement txtValue => _webDriver.FindElement(By.Id("Value"));
    public IWebElement txtExplanation => _webDriver.FindElement(By.Id("Explanation"));
    //public IWebElement txtBoxSeries => _webDriver.FindElement(By.Id("SeriesTitle"));

    //public IWebElement dropDwnSeriesType => _webDriver.FindElement(By.Id("SeriesType"));
    //public IWebElement dropDwnContentSpot => _webDriver.FindElement(By.Id("ContentSpot"));
    //public IWebElement txtNote => _webDriver.FindElement(By.Id("Note"));

    //public IWebElement btnSaveConten => _webDriver.FindElement(By.Id("btnSaveAContentInfo"));

    //btnSaveQFlash
    public IWebElement btnSaves => _webDriver.FindElement(By.Id("btnSaveQFlash"));
    public IWebElement container => _webDriver.FindElement(By.ClassName("sa-confirm-button-container"));
    public IWebElement btnYes => container.FindElement(By.CssSelector("button.confirm"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public IWebElement table => _webDriver.FindElement(By.Id("lvsIndicators")).FindElement(By.TagName("table")) ?? null;
    // Get all the rows in the table
    public List<IWebElement> rows => table.FindElements(By.XPath(".//tbody/tr")).ToList();
    public void ClickContinue()
    {
        btnContinue.Clicks();
    }
    public void ClickContinueDate()
    {
        btnContinueSelection.Clicks();
    }
    public void ClickSave()
    {
        btnSave.Clicks();
    }
    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public void ClickContentPopUp()
    {
        contentPopUp.Clicks();
    }
    public void ClickYesPopUp()
    {
        btnYes.Clicks();
    }
    public void ClickOk()
    {
        btnClickOk.Clicks();
    }
}

