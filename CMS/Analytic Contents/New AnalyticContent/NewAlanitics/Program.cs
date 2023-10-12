using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SuccessLogin;

namespace NewAlanitics;

public class Program
{
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
                Sleep(3000);
                ClickAnalyticCard(driver);
                Sleep(3000);
                ClickNew(driver);
                Sleep(3000);
                PopUpDataSet(driver);
                Sleep(3000);
                ProcessDataSetSelector(driver);
                Sleep(3000);
                PopUpTimePeriod(driver);
                Sleep(3000);
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
            Sleep(2000);
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

            Sleep(2000);
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
            var analytics = new Analytics(driver);

            var jsonFileReader = new JsonFileReader();

            var retVal = jsonFileReader.ReadJsonCMSAnalytis();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("DSectorId")));

            analytics.dropDownSector.SelectDropDown(retVal.AnalyticsDataSector.SectorIndex);
            Sleep(1000);

            var wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait2.Until(ExpectedConditions.ElementIsVisible(By.Id("DCategoryId")));

            analytics.dropDownCategory.SelectDropDown(retVal.AnalyticsDataSector.CategoryIndex);
            Sleep(1000);

            var wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait3.Until(ExpectedConditions.ElementIsVisible(By.Id("DTableId")));
            analytics.dropDownTable.SelectDropDown(retVal.AnalyticsDataSector.TableIndex);

            Sleep(4000);
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

            Sleep(3000);
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

            var jsonFileReader = new JsonFileReader();

            var retVal = jsonFileReader.ReadJsonCMSAnalytis();

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

            Sleep(4000);


            analytics.btnContinueSelection.Click();

            Sleep(3000);

            var dataSetLink = driver.FindElement(By.LinkText("Content"));
            dataSetLink.Click();

            Sleep(3000);

            ProcessAnalyticContent(driver);

            Sleep(3000);

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

            var  jsonFileReader = new JsonFileReader();

            var retVal = jsonFileReader.ReadJsonCMSAnalytis();


            var name = retVal.AnalyticsDataSector?.Name;
            var title = retVal.AnalyticsDataSector?.Title;
            var titleSeries = retVal.AnalyticsDataSector?.SeriesTitle;
            var chatType = retVal.AnalyticsDataSector.ChartTypeIndex;
            var contentType = retVal.AnalyticsDataSector.ContentSpotIndex;
            var Note = retVal.AnalyticsDataSector?.Note;

            analytics.txtBoxName.SendKeys(name);
            analytics.txtBoxTitle.SendKeys(title);
            analytics.txtBoxSeries.SendKeys(titleSeries);
            analytics.dropDwnSeriesType.SelectDropDown(chatType);
            analytics.dropDwnContentSpot.SelectDropDown(contentType);
            analytics.txtNote.SendKeys(Note);

            analytics.btnSaveConten.Click();
            Sleep(3000);

            analytics.btnSaves.Click();

            Sleep(3000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => analytics.btnYes);

            dataSetLink.Click();
            Sleep(3000);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }

    }
    #endregion



    private static void Sleep(int timeVal)
    {
        Thread.Sleep(timeVal);
    }

}