using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Commons;
using Newtonsoft.Json;

namespace BulkIndicator;

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
                //Indicator
                data.ClickDataSet(driver);
                Utils.Sleep(3000);
                ClickIndicators(driver);
                Utils.Sleep(3000);
                IndicatorCataloguePopUp(driver);
                Utils.Sleep(3000);
                ClickIndicatorBulk(driver);
                Utils.Sleep(3000);
                IndicatorUploadBulkFile(driver);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public static void ClickClose(IWebDriver driver)
    {
        try
        {
            var closeBtn = driver.FindElement(By.CssSelector("a[href='/dataset/indicators']"));
            closeBtn.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
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
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void ClickIndicatorBulk(IWebDriver driver)
    {
        try
        {
            var tableBulk = driver.FindElement(By.LinkText("Bulk"));
            tableBulk.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void IndicatorUploadBulkFile(IWebDriver driver)
    {
        var indi = new Indicator(driver);
        try
        {
            string fileName = "Indicator_Today.xlsx";
            string filePath = Path.Combine(desktopPath, "SeleniumTest", fileName);
            Utils.Sleep(3000);
            indi.btnBrowseFile.SendKeys(filePath);
            indi.btnUpload.Click();
            Utils.Sleep(3000);
            indi.btnClickOk.Click();
            Utils.Sleep(3000);
            var retVal = ReadJsonBulkIndicator();
            bool modify = retVal.BulkIndicatorNewDataCon.Modify;
            var bulkTableNewDataList = retVal.BulkIndicatorNewDataCon.BulkIndicatorNewData;
            var tableRes = indi.table;
            var rowCount = 0;
            if (modify)
            {
                if (tableRes != null)
                {
                    var rows = indi.rows;
                    rowCount = rows.Count();
                    for (int item = 1; item < rowCount; item++)
                    {
                        IWebElement updateLink = indi.rows[item].FindElement(By.LinkText("Modify"));
                        updateLink.Click();
                        Utils.Sleep(3000);
                        indi.displayOrder.SendKeys(bulkTableNewDataList[item].DisplayOrder.ToString());
                        if (bulkTableNewDataList[item].DisplayInChart == true)
                        {
                            Utils.Sleep(3000);
                            var switBox = indi.DisplayInChart;
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                            Utils.Sleep(2000);
                            indi.txtGrapTit.SendKeys(bulkTableNewDataList[item].GraphTitle);
                        }
                        Utils.Sleep(2000);
                        indi.ClickOk();

                    }
                }
            }
            if (indi.txtTopLevelBox != null)
            {
                indi.txtTopLevelBox.Click();
                int indexToSelect = retVal.BulkIndicatorNewDataCon.indexToSelect;
                Utils.Sleep(3000);
                indi.boxSel[indexToSelect].Click();
            }
            Utils.Sleep(3000);
            Utils.LogSuccess($"Bulk Indicator Creation", "Indicator");

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
        finally
        {
            Utils.Sleep(3000);
            indi.btnSave.Click();
            Utils.Sleep(3000);
            indi.btnClickOk.Click();
            Utils.Sleep(9000);
        }
    }
    public static BulkIndicatorNewDataContainer ReadJsonBulkIndicator()
    {
        try
        {
              string jsonFileName = "Indicator.json";
              string jsonFilePath = Path.Combine(desktopPath, 
                  "SeleniumTest", jsonFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<BulkIndicatorNewDataContainer>(jsonContent);
                return retVal;
            }
            return new BulkIndicatorNewDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new BulkIndicatorNewDataContainer();
        }

    }
    #endregion
}