using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SuccessLogin;

namespace NewQuickFlash;

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


                //QuickFlashes
                #region Quick Flashes Operations
                ClickCMS(driver);
                Sleep(3000);
                ClickQuickFlashCard(driver);
                Sleep(3000);
                ClickNewQuickFlashCard(driver);
                Sleep(3000);
                ProcessNewQFlash(driver);

                //PopUpDataSet(driver);
                //Sleep(3000);
                //ProcessQFlashDataSetSelector(driver);
                //Sleep(3000);
                //PopUpTimePeriod(driver);
                //Sleep(3000);
                //ProcessTimePeriodQFlashSelector(driver);

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
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static bool ClickNewQuickFlashCard(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.XPath("//a[contains(@href, '/shop/q-flashes/add')]"));
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
    public static void ProcessQFlashDataSetSelector(IWebDriver driver)
    {
        try
        {
            var Fcontent = new QuickFlashes(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonCMSQFlash();

            Fcontent.dropDownSector.SelectDropDown(retVal.QFlashDataSector.SectorIndex);
            Sleep(1000);
            Fcontent.dropDownCategory.SelectDropDown(retVal.QFlashDataSector.CategoryIndex);
            Sleep(1000);
            Fcontent.dropDownTable.SelectDropDown(retVal.QFlashDataSector.TableIndex);

            Sleep(4000);
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

            Sleep(3000);



            Fcontent.ClickContinue();


        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void ProcessTimePeriodQFlashSelector(IWebDriver driver)
    {
        try
        {
            var Fcontent = new QuickFlashes(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonCMSQFlash();

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

            Sleep(4000);

            Fcontent.btnContinueSelection.Click();

            Sleep(3000);
            var dataSetLink = driver.FindElement(By.LinkText("Content"));
            dataSetLink.Click();

            Sleep(3000);

            ProcessNewQFlash(driver);

            Sleep(3000);

            Fcontent.ClickOk();


        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static void ProcessNewQFlash(IWebDriver driver)
    {
        try
        {
            var Fcontent = new QuickFlashes(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonCMSQFlash();


            var name = retVal.QFlashDataSector?.Name;
            var title = retVal.QFlashDataSector?.Title;
            var arrType = retVal.QFlashDataSector?.ArrowType;
            var arrDirec = retVal.QFlashDataSector?.ArrowDirection;
            var value = retVal.QFlashDataSector.Value;
            var Note = retVal.QFlashDataSector?.Note;

            Fcontent.txtBoxName.SendKeys(name);
            Fcontent.txtBoxTitle.SendKeys(title);
            Fcontent.dropDwnArrow.SelectDropDown(arrType.Value);
            Fcontent.dropDwonArrDir.SelectDropDown(arrDirec.Value);
            Fcontent.txtValue.SendKeys(value.ToString());
            Fcontent.txtExplanation.SendKeys(Note);

            Fcontent.btnSaves.Click();

            Sleep(3000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => Fcontent.btnYes);

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