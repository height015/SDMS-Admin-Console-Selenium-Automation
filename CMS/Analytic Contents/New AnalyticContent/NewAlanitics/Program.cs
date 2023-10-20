using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SuccessLogin;
using Commons;
using Newtonsoft.Json;

namespace NewAnalytics;

public  class Program
{
    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    private static readonly string _URL = "http://197.255.51.104:9035";

    //http://197.255.51.104:9008
    //http://197.255.51.104:9035
    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var loginObj = new SuccessLogin.Program();
            var data = new Program();
            bool login = loginObj.LoginSuccess(driver);
            if (login)
            {
                #region Analytics Operations
                ClickCMS(driver);
                Utils.Sleep(3000);
                ClickAnalyticCard(driver);
                Utils.Sleep(3000);
                ClickNew(driver);
                Utils.Sleep(3000);
                PopUpDataSet(driver);
                Utils.Sleep(3000);
                ProcessDataSetSelector(driver);
                Utils.Sleep(3000);
                PopUpTimePeriod(driver);
                Utils.Sleep(3000);
                ProcessTimePeriodSelector(driver);
                #endregion
            }
        }
    }

    public static void ClickCMS(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Content Mgt."));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static bool ClickClear(IWebDriver driver)
    {
        try
        {
            var btnClear = driver.FindElement(By.LinkText("Clear"));
            btnClear.Click();
            Utils.Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    public static void PopUpDataSet(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            var dataSetLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Dataset")));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void PopUpTimePeriod(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            var dataSetLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Time-Period")));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    #region Analytics
    public static void ClickAnalyticCard(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Analytic Contents"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static bool ClickNew(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/analytics/add-content?sectorId=-1']"));
            btnNewReq.Click();
            Utils.Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public static void ProcessDataSetSelector(IWebDriver driver)
    {
        try
        {

    //         public IWebElement dropDownSector => _webDriver.FindElement(By.Id("DSectorId"));
    //public IWebElement dropDownCategory => _webDriver.FindElement(By.Id("DCategoryId"));
    //public IWebElement dropDownTable => _webDriver.FindElement(By.Id("DTableId"));


    var analytics = new Analytics(driver);
            var retVal = ReadJsonCMSAnalytis();
            driver.WaitForElementToBeClickable(analytics.dropDownSector, 10);
            analytics.dropDownSector.SelectDropDownByIndex(retVal.AnalyticsDataSector.SectorIndex);
            Utils.Sleep(1000);
            driver.WaitForElementToBeClickable(analytics.dropDownCategory, 10);
            analytics.dropDownCategory.SelectDropDownByIndex(retVal.AnalyticsDataSector.CategoryIndex);
            Utils.Sleep(1000);
            driver.WaitForElementToBeClickable(analytics.dropDownTable, 10);
            analytics.dropDownTable.SelectDropDownByIndex(retVal.AnalyticsDataSector.TableIndex);
            Utils.Sleep(4000);
            var indicatoros = analytics.table;
            var rowCount = 0;
            if (indicatoros != null)
            {
                var rowCounts = analytics.rows.Count();
                var rows = analytics.rows;
                var rowIndexes = retVal.AnalyticsDataSector.GetIndexArray();
                rowCount = rows.Count();
                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = analytics.rows[item].FindElement(By.Name("SelIndiIds"));
                    checkbox.Click();
                    rowCount--;
                    if (rowCount <= 0)
                    {
                        break;
                    }
                }
            }
            Utils.Sleep(3000);
            analytics.ClickContinue();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void ProcessTimePeriodSelector(IWebDriver driver)
    {
        try
        {
            var analytics = new Analytics(driver);
            var retVal = ReadJsonCMSAnalytis();
            var perodTypeValz = analytics.readonlyInput;
            var dataVal = perodTypeValz.GetAttribute("data-value");
            var perodTypeVal = perodTypeValz.GetAttribute("value");
            switch (perodTypeVal)
            {
                case "Daily":
                    break;
                case "Weekly":
                    int startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
                    int stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);
                    var startyr = driver.FindElement(By.Id("txtWeekStartPeriod"));
                    var stopyr = driver.FindElement(By.Id("txtWeekStopPeriod"));
                    DateTime startOfYear = new DateTime(startYear, 1, 1);
                    string formattedstartOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);
                    DateTime endOfYear = new DateTime(stopYear, 12, 31);
                    string formattedendOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);
                    startyr.Clear();
                    stopyr.Clear();
                    startyr.SendKeys(formattedstartOfYear);
                    perodTypeValz.Click();
                    stopyr.SendKeys(formattedendOfYear);
                    perodTypeValz.Click();
                    break;
                case "Monthly":
                    break;
                case "Quarterly":
                    break;
                case "Semiannual":
                    break;
                case "Annually":
                    startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
                    stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);
                    analytics.OpenStartDateDrpDwn.Clear();
                    analytics.OpenStopDateDrpDwn.Clear();
                    analytics.OpenStartDateDrpDwn.SendKeys(startYear.ToString());
                    perodTypeValz.Click();
                    analytics.OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
                    perodTypeValz.Click();
                    break;
                case "Biannual":
                    break;
                default:
                    break;
            }
            Utils.Sleep(3000);
            analytics.btnContinueSelection.Click();
            Utils.Sleep(3000);
            var dataSetLink = driver.FindElement(By.LinkText("Content"));
            dataSetLink.Click();
            Utils.Sleep(3000);
            ProcessAnalyticContent(driver);
            Utils.Sleep(3000);
            analytics.ClickOk();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void ProcessAnalyticContent(IWebDriver driver)
    {
        try
        {
            var analytics = new Analytics(driver);
            var retVal = ReadJsonCMSAnalytis();
            var name = retVal.AnalyticsDataSector?.Name;
            var title = retVal.AnalyticsDataSector?.Title;
            var titleSeries = retVal.AnalyticsDataSector?.SeriesTitle;
            var chatType = retVal.AnalyticsDataSector.ChartTypeIndex;
            var contentType = retVal.AnalyticsDataSector.ContentSpotIndex;
            var Note = retVal.AnalyticsDataSector?.Note;
            analytics.txtBoxName.SendKeys(name);
            analytics.txtBoxTitle.SendKeys(title);
            analytics.txtBoxSeries.SendKeys(titleSeries);
            analytics.dropDwnSeriesType.SelectDropDownByIndex(chatType);
            analytics.dropDwnContentSpot.SelectDropDownByIndex(contentType);
            analytics.txtNote.SendKeys(Note);
            analytics.btnSaveConten.Click();
            Utils.Sleep(3000);
            analytics.btnSaves.Click();
            Utils.Sleep(3000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => analytics.btnYes);
            dataSetLink.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }

    }
    #endregion



    #region Utility
    public static AnalyticsDataSectorContainer ReadJsonCMSAnalytis()
    {
        try
        {
             string jsonFileName = "Analytics.json";
             string jsonFilePath = Path.Combine(desktopPath, "SeleniumTest", jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<AnalyticsDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new AnalyticsDataSectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new AnalyticsDataSectorContainer();
        }

    }

    #endregion
}