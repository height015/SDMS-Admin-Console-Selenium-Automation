using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SuccessLogin;
using SuccessLogin.Utils;
namespace NewFeaturedContent;

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
                //FeaturedContent
                #region FeaturedContent Operations
                ClickCMS(driver);
                Utils.Sleep(3000);
                ClickFeaturedContentCard(driver);
                Utils.Sleep(3000);
                ClickNewFContentCard(driver);
                Utils.Sleep(3000);
                PopUpDataSet(driver);
                Utils.Sleep(3000);
                ProcessFContentDataSetSelector(driver);
                Utils.Sleep(3000);
                PopUpTimePeriod(driver);
                Utils.Sleep(3000);
                ProcessTimePeriodFContentSelector(driver);
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

    #region FeaturedContent
    public static void ClickFeaturedContentCard(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Featured Contents"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static bool ClickNewFContentCard(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/f-contents/add-content?sectorId=-1']"));
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
    public static void ProcessFContentDataSetSelector(IWebDriver driver)
    {
        try
        {
            var Fcontent = new FeaturedContents(driver);
            var jsonFileReader = new JsonFileReader();
            var retVal = jsonFileReader.ReadJsonCMSFeaturedContent();
            Fcontent.dropDownSector.SelectDropDownByIndex(retVal.FeaturedContentDataSector.SectorIndex);
            Utils.Sleep(1000);
            Fcontent.dropDownCategory.SelectDropDownByIndex(retVal.FeaturedContentDataSector.CategoryIndex);
            Utils.Sleep(1000);
            Fcontent.dropDownTable.SelectDropDownByIndex(retVal.FeaturedContentDataSector.TableIndex);
            Utils.Sleep(4000);
            var indicatoros = Fcontent.table;
            var rowCount = 0;
            if (indicatoros != null)
            {
                var rowCounts = Fcontent.rows.Count();
                var rows = Fcontent.rows;
                var rowIndexes = retVal.FeaturedContentDataSector.GetIndexArray();
                rowCount = rows.Count();
                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = Fcontent.rows[item].FindElement(By.Name("SelIndiIds"));
                    checkbox.Click();
                    rowCount--;
                    if (rowCount <= 0)
                    {
                        break;
                    }
                }
            }

            Utils.Sleep(3000);
            Fcontent.ClickContinue();


        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void ProcessTimePeriodFContentSelector(IWebDriver driver)
    {
        try
        {
            var Fcontent = new FeaturedContents(driver);
            var jsonFileReader = new JsonFileReader();
            var retVal = jsonFileReader.ReadJsonCMSFeaturedContent();
            var perodTypeValz = Fcontent.readonlyInput;
            var dataVal = perodTypeValz.GetAttribute("data-value");
            var perodTypeVal = perodTypeValz.GetAttribute("value");
            switch (perodTypeVal)
            {
                case "Daily":
                    break;
                case "Weekly":
                    int startYear = int.Parse(retVal.FeaturedContentDataSector.StartDate);
                    int stopYear = int.Parse(retVal.FeaturedContentDataSector.StopDate);
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
                    startYear = int.Parse(retVal.FeaturedContentDataSector.StartDate);
                    stopYear = int.Parse(retVal.FeaturedContentDataSector.StopDate);
                    Fcontent.OpenStartDateDrpDwn.Clear();
                    Fcontent.OpenStopDateDrpDwn.Clear();
                    Fcontent.OpenStartDateDrpDwn.SendKeys(startYear.ToString());
                    perodTypeValz.Click();
                    Fcontent.OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
                    perodTypeValz.Click();
                    break;
                case "Biannual":
                    break;
                default:
                    break;
            }

            Utils.Sleep(4000);
            Fcontent.btnContinueSelection.Click();
            Utils.Sleep(3000);
            var dataSetLink = driver.FindElement(By.LinkText("Content"));
            dataSetLink.Click();
            Utils.Sleep(3000);
            ProcessFContent(driver);
            Utils.Sleep(3000);
            Fcontent.ClickOk();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static void ProcessFContent(IWebDriver driver)
    {
        try
        {
            var Fcontent = new FeaturedContents(driver);
            var jsonFileReader = new JsonFileReader();
            var retVal = jsonFileReader.ReadJsonCMSFeaturedContent();
            var name = retVal.FeaturedContentDataSector?.Name;
            var title = retVal.FeaturedContentDataSector?.Title;
            var titleSeries = retVal.FeaturedContentDataSector?.SeriesTitle;
            var chatType = retVal.FeaturedContentDataSector.ChartTypeIndex;
            var contentType = retVal.FeaturedContentDataSector.ContentSpotIndex;
            var Note = retVal.FeaturedContentDataSector?.Note;
            Fcontent.txtBoxName.SendKeys(name);
            Fcontent.txtBoxTitle.SendKeys(title);
            Fcontent.txtBoxSeries.SendKeys(titleSeries);
            Fcontent.dropDwnSeriesType.SelectDropDownByIndex(chatType);
            Fcontent.dropDwnContentSpot.SelectDropDownByIndex(contentType);
            Fcontent.txtNote.SendKeys(Note);
            Fcontent.btnSaveConten.Click();
            Utils.Sleep(3000);
            Fcontent.btnSaves.Click();
            Utils.Sleep(3000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => Fcontent.btnYes);
            dataSetLink.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }

    }
    #endregion
}