﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SuccessLogin;

namespace AddNewSector;

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
                #region Sector
                Sleep(3000);
                data.ClickDataSet(driver);
                Sleep(3000);
                data.ClickSector(driver);
                Sleep(3000);
                data.CreateNewDataSectorSuccess(driver);
                Sleep(3000);
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
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    #region Sector Creation
    public bool ClickSector(IWebDriver driver)
    {
        try
        {
            var dataSetLinkSec = driver.FindElement(By.LinkText("Sectors"));

            dataSetLinkSec.Click();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    public string CreateNewDataSectorSuccess(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var loginVal = jsonFileReader.ReadJsonFileCreateSector();

            var createSec =  new Sector(driver);


            createSec.ClickNew();

            Sleep(2000);

            createSec.EnterNameAndTitle(loginVal.SectorField.Name, loginVal.SectorField.Title);

            createSec.ClickSubmit();

            Sleep(3000);

            var teaxVal = createSec.textMsgRes.Text;

            Sleep(3000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By
                .CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']")));

            createSec.ClickOk();

            return teaxVal;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.StackTrace} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }
    #endregion

    private static void Sleep(int time)
    {
        Thread.Sleep(time);
    }


}