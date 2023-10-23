using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Commons;
using Newtonsoft.Json;

namespace BulkTable;
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
                #region Tables
                data.ClickDataSet(driver);
                ClickTableCard(driver);
                TableCatalogueSelectorPopUp(driver);
                Utils.Sleep(3000);
                ClickTableBulk(driver);
                Utils.Sleep(3000);
                TableUploadBulkFile(driver);
                Utils.Sleep(3000);
                #endregion
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
    #region Tables
    public static void ClickTableCard(IWebDriver driver)
    {
        try
        {
            var tableCard = driver.FindElement(By.LinkText("Tables"));
            tableCard.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void TableCatalogueSelectorPopUp(IWebDriver driver)
    {
        try
        {
            Utils.Sleep(1000);
            var table = new Tables(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileForTableDataSector();
            driver.WaitForElementToBeClickable(table.dropDownCascadeSecor, 10);
            table.dropDownCascadeSecor.SelectDropDownByIndex(retVal.TableDataSelector.DataSectorIndexToSelect);
            Utils.Sleep(2000);
            driver.WaitForElementToBeClickable(table.dropDownCat, 10);
            table.dropDownCat.SelectDropDownByIndex(retVal.TableDataSelector.DataCategoryIndexToSelect);
            table.ClickContinue();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void ClickTableBulk(IWebDriver driver)
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
    public static void TableUploadBulkFile(IWebDriver driver)
    {
        try
        {
            var table = new Tables(driver);
           
            string fileName = "Table_Template.xlsx";
            string filePath = Path.Combine(desktopPath, "SeleniumTest", fileName);
            Utils.Sleep(3000);
            table.btnBrowseFile.SendKeys(filePath);
            table.btnUpload.Click();
            Utils.Sleep(3000);
            driver.WaitForElementToBeClickable(table.btnClickOk, 10);
            table.btnClickOk.Click();
            Utils.Sleep(3000);
            var retVal = ReadJsonBulkTabe();
            bool applyAll = retVal.BulkTableNewDataCon.ApplyAll;
            var bulkTableNewDataList = retVal.BulkTableNewDataCon.BulkTableNewData;
            var tableRes = table.table;
            var rowCount = 0;
            if (tableRes != null && applyAll == false)
            {
                var rows = table.rows;
                rowCount = rows.Count();
                for (int item = 1; item < rowCount; item++)
                {
                    var updateLink = table.rows[item].FindElement(By.LinkText("Update"));
                    updateLink.Click();
                    Utils.Sleep(3000);
                    table.dropDownFeq.SelectDropDownByIndex(bulkTableNewDataList[item].FreqIndexToSelect);
                    table.dropDownUnit.SelectDropDownByIndex(bulkTableNewDataList[item].UnitIndexToSelect);
                    table.ClickUpdate();
                    Utils.Sleep(2000);
                    table.ClickOk();
                }
            }
            else if (applyAll)
            {
                var updateLink = table.rows[1].FindElement(By.LinkText("Update"));
                updateLink.Click();
                Utils.Sleep(3000);
                table.dropDownFeq.SelectDropDownByIndex(bulkTableNewDataList[1].FreqIndexToSelect);
                table.dropDownUnit.SelectDropDownByIndex(bulkTableNewDataList[1].UnitIndexToSelect);
                table.ClickUpdate();
                Utils.Sleep(2000);
                table.ClickOk();
                var applyBtn = table.btnApply;
                Utils.Sleep(3000);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", applyBtn);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", applyBtn);
                Utils.Sleep(3000);
                table.ClickOk();
            }
            Utils.Sleep(4000);
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            table.ClickSave();
            Utils.Sleep(3000);
            driver.WaitForElementToBeClickable(table.btnClickOk, 10);
            table.btnClickOk.Click();
            Utils.LogSuccess($"Bulk Table Creation", "Tables");

            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    #endregion


    public static BulkTableNewDataContainer ReadJsonBulkTabe()
    {
        try
        {
            string jsonFileName = "Tables.json";
            string jsonFilePath = Path.Combine(desktopPath,
                "SeleniumTest", jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<BulkTableNewDataContainer>(jsonContent);
                return retVal;
            }
            return new BulkTableNewDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new BulkTableNewDataContainer();
        }

    }

}