using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Commons;
using Newtonsoft.Json;

namespace CreateNewCategory;
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
                #region Category
                Utils.Sleep(3000);
                ClickDashBorad(driver);
                Utils.Sleep(3000);
                ClickDataCatalogCard(driver);
                Utils.Sleep(3000);
                ClickCategoryCard(driver);
                ClickNewDataCategoryButton(driver);
                #endregion
            }
        }
    }

    public static void ClickDashBorad(IWebDriver driver)
    {
        try
        {
            var dashBoard = driver.FindElement(By.LinkText("Dashboard"));
            dashBoard.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    #region Data Category
    public static void ClickDataCatalogCard(IWebDriver driver)
    {
        try
        {
            var cartCard = driver.FindElement(By.LinkText("Data Catalogues."));
            cartCard.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static void ClickCategoryCard(IWebDriver driver)
    {
        try
        {
            Utils.Sleep(3000);
            var cartCard = driver.FindElement(By.LinkText("Categories"));
            Utils.Sleep(3000);
            cartCard.Click();
            Utils.Sleep(5000);
            var catSec = new Category(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileForSelectOptionCatalogSelector();
            catSec.dropDownBox.SelectDropDownByIndex(retVal.CatalogueSelector.OptionToSelect);
            Utils.Sleep(3000);
            catSec.ClickContinue();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static bool ClickNewDataCategoryButton(IWebDriver driver)
    {
        try
        {
            var newDataCategoryButton = driver.FindElement(By.CssSelector("a.item-button[href*='/dataset/categories/add']"));
            Utils.Sleep(3000);
            newDataCategoryButton.Click();
            Utils.Sleep(2000);
            var catSec = new Category(driver);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonFileForEnterNewDataCategory();
            catSec.EnterDataCategory(retVal.DataCategory.Name, retVal.DataCategory.Title);
            Utils.Sleep(3000);
            catSec.ClickSubmit();
            Utils.Sleep(5000);
            catSec.ClickOk();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    #endregion

    #region Utility
    public static DataCategoryContainer ReadJsonFileForEnterNewDataCategory()
    {
        try
        {
              string jsonFileName = "Category.json";
              string jsonFilePath = Path.Combine(desktopPath, "SeleniumTest", jsonFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<DataCategoryContainer>(jsonContent);
                return retVal;
            }
            return new DataCategoryContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new DataCategoryContainer();
        }
    }

    #endregion
}