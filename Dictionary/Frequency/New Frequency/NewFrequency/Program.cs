﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SuccessLogin;

namespace NewFrequency;

public class Program
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var loginObj = new SuccessLogin.Program();

            var data = new Program();

            bool login = loginObj.LoginSuccess(driver);

            if (login)
            {
                Sleep(3000);
                ClickDictionary(driver);
                Sleep(3000);
                ClickFrequency(driver);
                Sleep(3000);
                ClickFrequencyNewRequest(driver);
                Sleep(3000);
                data.DataEntryFrequency(driver);
                Sleep(3000);

                //ClickNewRequest(driver);
                //Sleep(3000);
                //ClickRequestType(driver);
                //CreateNewReqGenericPopUp(driver);
            }


        }

    }

    public static void ClickDictionary(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Dictionaries"));
            dataSetLink.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public static bool ClickNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/workflow/requests/requests?reqType=1'][onclick*='showLoader()']"));
            dataSetLinkNewReq.Click();

            Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }


    #region Frequency
    public static void ClickFrequency(IWebDriver driver)
    {
        try
        {
            var freqLink = driver.FindElement(By.LinkText("Frequencies"));
            freqLink.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static bool ClickFrequencyNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/frequencies/add']"));
            dataSetLinkNewReq.Click();
            Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public bool DataEntryFrequency(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var freqVal = jsonFileReader.ReadJsonFileDataFrequency();

            var createSec = new Frequency(driver);

            Sleep(3000);

            createSec.NewDataFrequencyEntery(freqVal.DataFrequency.Name, freqVal.DataFrequency.ShortName);

            Sleep(3000);

            createSec.ClickSubmit();

            Sleep(3000);

            createSec.ClickOk();

            return true;

        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    #endregion


    private static void Sleep(int timeVal)
    {
        Thread.Sleep(timeVal);
    }
}