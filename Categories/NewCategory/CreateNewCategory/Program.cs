using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;
using System.Net;

namespace CreateNewCategory;

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

                #region Category
                Sleep(3000);
                ClickDashBorad(driver);
                Sleep(3000);
                ClickDataCatalogCard(driver);
                Sleep(3000);
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

    //This is used when on the dashboard
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
            Sleep(3000);
            var cartCard = driver.FindElement(By.LinkText("Categories"));
            Sleep(3000);
            cartCard.Click();
            Sleep(5000);

            var catSec = new Category(driver);

            SelectElement dropdown = new SelectElement(catSec.dropDownBox);


            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForSelectOptionCatalogSelector();

            dropdown.SelectByIndex(retVal.CatalogueSelector.OptionToSelect);
            Sleep(3000);
            catSec.ClickContinue();
            Sleep(3000);

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

            Sleep(3000);
            newDataCategoryButton.Click();
            Sleep(2000);

            var catSec = new Category(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForEnterNewDataCategory();

            catSec.EnterDataCategory(retVal.DataCategory.Name, retVal.DataCategory.Title);

            Sleep(3000);

            catSec.ClickSubmit();

            Sleep(5000);

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
    private static void Sleep(int time)
    {
        Thread.Sleep(time);
    }


}