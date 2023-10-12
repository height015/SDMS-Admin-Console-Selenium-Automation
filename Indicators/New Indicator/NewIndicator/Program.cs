using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;
using System.Net;

namespace NewIndicator;

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

                //Indicator
                data.ClickDataSet(driver);
                Sleep(3000);
                ClickIndicators(driver);
                Sleep(3000);
                IndicatorCataloguePopUp(driver);
                Sleep(3000);
                CreateNewDataIndicatorPopUp(driver);
                Sleep(3000);

                //ClickNewRequest(driver);
                //Sleep(3000);
                //ClickRequestType(driver);
                //Sleep(3000);
                //CreateNewReqIndicatorPopUp(driver);
                //Sleep(3000);
                //ClickClose(driver);
                //Sleep(3000);
                //ClickIndicatorBulk(driver);
                //Sleep(3000);
                //IndicatorUploadBulkFile(driver);
            }



        }

    }

    public bool ClickDataSet(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Datasets"));
            dataSetLink.Click();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    #region Indicators
    public static void ClickIndicators(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Indicators"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void IndicatorCataloguePopUp(IWebDriver driver)
    {
        try
        {
            var inidi = new Indicator(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileDataIndicator();

            Sleep(2000);
            //Access the Sector
            SelectElement dropdownSec = new SelectElement(inidi.dropDownCascadeSecor);
            dropdownSec.SelectByIndex(retVal.IndicatorDataSelector.SectorIndex);

            Sleep(2000);
            //Access the Category
            SelectElement dropdownCat = new SelectElement(inidi.dropDownCat);
            dropdownCat.SelectByIndex(retVal.IndicatorDataSelector.CategoryIndex);

            Sleep(2000);
            //Access the Category
            SelectElement dropdownTab = new SelectElement(inidi.dropDownTable);
            dropdownTab.SelectByIndex(retVal.IndicatorDataSelector.TableIndex);

            Sleep(3000);

            inidi.ClickContinue();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void CreateNewDataIndicatorPopUp(IWebDriver driver)
    {
        try
        {
            var inidi = new Indicator(driver);

            Sleep(2000);

            inidi.ClickNew();

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileNewDataIndicator();

            Sleep(4000);

            //SelectElement dropdown = new SelectElement(inidi.IndiComboTree);

            //dropdown.SelectByIndex(retVal.NewDataIndicator.TopLevelIndi);

            string textName = retVal.NewDataIndicator.Name;
            inidi.txtBoxName.SendKeys(textName);

            string textTit = retVal.NewDataIndicator.Title;
            inidi.txtBoxTitle.SendKeys(textTit);

            if (retVal.NewDataIndicator.EmboldenIndicatorTitle == true)
            {

                Sleep(3000);
                var switBox = inidi.checkBoxEmph;
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
            }

            int dispOrd = retVal.NewDataIndicator.DisplayOrder;
            inidi.displayOrder.Clear();
            inidi.displayOrder.SendKeys(dispOrd.ToString());

            if (retVal.NewDataIndicator.DisplayInChart == true)
            {
                var switBox = inidi.DisplayInChart;
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                Sleep(3000);
                var texttoDispay = retVal.NewDataIndicator.TitleToDisplay;
                inidi.txtGrapTit.SendKeys(texttoDispay);
            }

            Sleep(3000);
            inidi.ClickSubmit();
            Sleep(3000);
            inidi.ClickOk();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    #endregion


    private static void Sleep(int time)
    {
        Thread.Sleep(time);
    }


}