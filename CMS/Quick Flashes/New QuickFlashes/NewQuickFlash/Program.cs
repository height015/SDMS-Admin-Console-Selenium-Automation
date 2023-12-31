﻿using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Commons;
using Newtonsoft.Json;

namespace NewQuickFlash;

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
                //QuickFlashes
                #region Quick Flashes Operations
                ClickCMS(driver);
                Utils.Sleep(3000);
                ClickQuickFlashCard(driver);
                Utils.Sleep(3000);
                ClickNewQuickFlashCard(driver);
                Utils.Sleep(3000);
                ProcessNewQFlash(driver);
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
    #region Quick Flashes
    public static void ClickQuickFlashCard(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Quick Flashes"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static bool ClickNewQuickFlashCard(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.XPath("//a[contains(@href, '/shop/q-flashes/add')]"));
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
    public static void ProcessQFlashDataSetSelector(IWebDriver driver)
    {
        try
        {
            var Fcontent = new QuickFlashes(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonCMSQFlash();
            Fcontent.dropDownSector.SelectDropDownByIndex(retVal.QFlashDataSector.SectorIndex);
            Utils.Sleep(1000);
            Fcontent.dropDownCategory.SelectDropDownByIndex(retVal.QFlashDataSector.CategoryIndex);
            Utils.Sleep(1000);
            Fcontent.dropDownTable.SelectDropDownByIndex(retVal.QFlashDataSector.TableIndex);
            Utils.Sleep(4000);
            var indicatoros = Fcontent.table;
            var rowCount = 0;
            if (indicatoros != null)
            {
                var rowCounts = Fcontent.rows.Count();
                var rows = Fcontent.rows;
                var rowIndexes = retVal.QFlashDataSector.GetIndexArray();
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void ProcessTimePeriodQFlashSelector(IWebDriver driver)
    {
        try
        {
            var Fcontent = new QuickFlashes(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonCMSQFlash();
            var perodTypeValz = Fcontent.readonlyInput;
            var dataVal = perodTypeValz.GetAttribute("data-value");
            var perodTypeVal = perodTypeValz.GetAttribute("value");
            switch (perodTypeVal)
            {
                case "Daily":
                    break;
                case "Weekly":
                    int startYear = int.Parse(retVal.QFlashDataSector.StartDate);
                    int stopYear = int.Parse(retVal.QFlashDataSector.StopDate);
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
                    startYear = int.Parse(retVal.QFlashDataSector.StartDate);
                    stopYear = int.Parse(retVal.QFlashDataSector.StopDate);
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
            ProcessNewQFlash(driver);
            Utils.Sleep(3000);
            Fcontent.ClickOk();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

    public static void ProcessNewQFlash(IWebDriver driver)
    {
        try
        {
            var Fcontent = new QuickFlashes(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonCMSQFlash();
            var name = retVal.QFlashDataSector?.Name;
            var title = retVal.QFlashDataSector?.Title;
            var arrType = retVal.QFlashDataSector?.ArrowType;
            var arrDirec = retVal.QFlashDataSector?.ArrowDirection;
            var value = retVal.QFlashDataSector.Value;
            var Note = retVal.QFlashDataSector?.Note;
            Fcontent.txtBoxName.SendKeys(name);
            Fcontent.txtBoxTitle.SendKeys(title);
            Fcontent.dropDwnArrow.SelectDropDownByIndex(arrType.Value);
            Fcontent.dropDwonArrDir.SelectDropDownByIndex(arrDirec.Value);
            Fcontent.txtValue.SendKeys(value.ToString());
            Fcontent.txtExplanation.SendKeys(Note);
            Fcontent.btnSaves.Click();
            Utils.Sleep(3000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => Fcontent.btnYes);
            dataSetLink.Click();
            Utils.Sleep(3000);
            Utils.LogSuccess($"Create QuickFlashes", "CMS QuickFlashe");

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }

    }
    #endregion



    #region Utility
    private static QFlashDataSectorContainer ReadJsonCMSQFlash()
    {
        try
        {
            string jsonFileName = "QFlashData.json";
            string jsonFilePath = Path.Combine(desktopPath,
                "SeleniumTest", jsonFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                QFlashDataSectorContainer retVal = JsonConvert.DeserializeObject<QFlashDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new QFlashDataSectorContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new QFlashDataSectorContainer();
        }

    }

    #endregion
}