using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SuccessLogin;
using SuccessLogin.Utils;


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
                Utils.Sleep(3000);
                ClickIndicators(driver);
                Utils.Sleep(3000);
                IndicatorCataloguePopUp(driver);
                Utils.Sleep(3000);
                CreateNewDataIndicatorPopUp(driver);
                Utils.Sleep(3000);
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
            Utils.Sleep(2000);
            //Access the Sector
            SelectElement dropdownSec = new SelectElement(inidi.dropDownCascadeSecor);
            dropdownSec.SelectByIndex(retVal.IndicatorDataSelector.SectorIndex);
            Utils.Sleep(2000);
            //Access the Category
            SelectElement dropdownCat = new SelectElement(inidi.dropDownCat);
            dropdownCat.SelectByIndex(retVal.IndicatorDataSelector.CategoryIndex);
            Utils.Sleep(2000);
            //Access the Category
            SelectElement dropdownTab = new SelectElement(inidi.dropDownTable);
            dropdownTab.SelectByIndex(retVal.IndicatorDataSelector.TableIndex);
            Utils.Sleep(3000);
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
            Utils.Sleep(2000);
            inidi.ClickNew();
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileNewDataIndicator();
            Utils.Sleep(4000);
            string textName = retVal.NewDataIndicator.Name;
            inidi.txtBoxName.SendKeys(textName);
            string textTit = retVal.NewDataIndicator.Title;
            inidi.txtBoxTitle.SendKeys(textTit);
            if (retVal.NewDataIndicator.EmboldenIndicatorTitle == true)
            {
                Utils.Sleep(3000);
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
                Utils.Sleep(3000);
                var texttoDispay = retVal.NewDataIndicator.TitleToDisplay;
                inidi.txtGrapTit.SendKeys(texttoDispay);
            }
            Utils.Sleep(3000);
            inidi.ClickSubmit();
            Utils.Sleep(3000);
            inidi.ClickOk();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    #endregion
}