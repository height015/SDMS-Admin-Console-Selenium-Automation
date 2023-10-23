using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SuccessLogin;
using Commons;
using Newtonsoft.Json;

namespace NewFlashCard;

public class Program
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

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
                #region FlashCard Operations
                ClickCMS(driver);
                Utils.Sleep(3000);
                ClickFlashCard(driver);
                Utils.Sleep(3000);
                ClickNewFcard(driver);
                Utils.Sleep(3000);
                PopUpDataSet(driver);
                Utils.Sleep(3000);
                ProcessFlashCardDataSetSelector(driver);
                Utils.Sleep(3000);
                PopUpTimePeriod(driver);
                Utils.Sleep(3000);
                ProcessTimePeriodFlashCardSelector(driver);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

    #region Flash Cards
    public static void ClickFlashCard(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Flash Cards"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static bool ClickNewFcard(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/f-cards/add-content?sectorId=-1']"));
            btnNewReq.Click();
            Utils.Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    public static void ProcessFlashCardDataSetSelector(IWebDriver driver)
    {
        try
        {
            var flashcard = new FlashCards(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonCMSFlashCard();
            flashcard.dropDownSector.SelectDropDownByIndex(retVal.FlashCardDataSector.SectorIndex);
            Utils.Sleep(1000);
            flashcard.dropDownCategory.SelectDropDownByIndex(retVal.FlashCardDataSector.CategoryIndex);
            Utils.Sleep(1000);
            flashcard.dropDownTable.SelectDropDownByIndex(retVal.FlashCardDataSector.TableIndex);
            Utils.Sleep(4000);
            var indicatoros = flashcard.table;
            var rowCount = 0;
            if (indicatoros != null)
            {
                var rowCounts = flashcard.rows.Count();
                var rows = flashcard.rows;
                var rowIndexes = retVal.FlashCardDataSector.GetIndexArray();
                rowCount = rows.Count();
                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = flashcard.rows[item].FindElement(By.Name("SelIndiIds"));
                    checkbox.Click();
                    rowCount--;
                    if (rowCount <= 0)
                    {
                        break;
                    }
                }
            }
            Utils.Sleep(3000);
            flashcard.ClickContinue();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void ProcessTimePeriodFlashCardSelector(IWebDriver driver)
    {
        try
        {
            var analytics = new FlashCards(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonCMSFlashCard();
            var perodTypeValz = analytics.readonlyInput;
            var dataVal = perodTypeValz.GetAttribute("data-value");
            var perodTypeVal = perodTypeValz.GetAttribute("value");
            switch (perodTypeVal)
            {
                case "Daily":
                    break;
                case "Weekly":
                    int startYear = int.Parse(retVal.FlashCardDataSector.StartDate);
                    int stopYear = int.Parse(retVal.FlashCardDataSector.StopDate);
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
                    startYear = int.Parse(retVal.FlashCardDataSector.StartDate);
                    stopYear = int.Parse(retVal.FlashCardDataSector.StopDate);
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
            Utils.Sleep(4000);
            analytics.btnContinueSelection.Click();
            Utils.Sleep(3000);
            var dataSetLink = driver.FindElement(By.LinkText("Content"));
            dataSetLink.Click();
            Utils.Sleep(3000);
            ProcessFlashCardContent(driver);
            Utils.Sleep(3000);
            analytics.ClickOk();
            Utils.LogSuccess($"Create FlashCard", "CMS FlashCard");

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void ProcessFlashCardContent(IWebDriver driver)
    {
        try
        {
            var analytics = new FlashCards(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonCMSFlashCard();
            var name = retVal.FlashCardDataSector?.Name;
            var title = retVal.FlashCardDataSector?.Title;
            var titleSeries = retVal.FlashCardDataSector?.SeriesTitle;
            var chatType = retVal.FlashCardDataSector.ChartTypeIndex;
            var contentType = retVal.FlashCardDataSector.ContentSpotIndex;
            var Note = retVal.FlashCardDataSector?.Note;
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    #endregion


    #region Utility
    public static FlashCardDataSectorContainer ReadJsonCMSFlashCard()
    {
        try
        {
              string jsonFileName = "FCard.json";
              string jsonFilePath = Path.Combine(desktopPath,
                  "SeleniumTest", jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<FlashCardDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new FlashCardDataSectorContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new FlashCardDataSectorContainer();
        }

    }

    #endregion
}
