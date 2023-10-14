using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace SuccessLogin;

public class Program
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var customMethod = new Program();
            bool login = customMethod.LoginSuccess(driver);
        }

    }

    public bool LoginSuccess(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var loginVal = jsonFileReader.ReadJsonFileSuccesLogin();
            string loginUrl = _URL + "/account/sign-in";
            driver.Navigate().GoToUrl(loginUrl);
            Utils.Utils.Sleep(3000);
            var loginPage = new LoginPage(driver);
            loginPage.EnterUserNameAndPassword(loginVal.LoginParameters.Username, loginVal.LoginParameters.Password);
            
            Utils.Utils.Sleep(3000);
            loginPage.ClickLogin();
            Utils.Utils.Sleep(3000);
            return true;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }



    public bool LoginEmptyUserName(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var loginVal = jsonFileReader.ReadJsonFileEmptyUserNameLoginCredential();
            string loginUrl = _URL + "/account/sign-in";
            driver.Navigate().GoToUrl(loginUrl);
            var loginPage = new LoginPage(driver);
            loginPage.EnterUserNameAndPassword(loginVal.LoginEmptyUserName.Username, loginVal.LoginEmptyUserName.Password);
            loginPage.ClickLogin();
            Utils.Utils.Sleep(2000);
            return driver.Url == _URL + "/dashboard";
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public bool LoginEmptyPassword(IWebDriver driver)
    {
        try
        {
            string loginUrl = _URL + "/account/sign-in";
            driver.Navigate().GoToUrl(loginUrl);
            var loginPage = new LoginPage(driver);
            loginPage.EnterUserNameAndPassword("useradmin@xplugng.com", "Pasxw0rd");
            loginPage.ClickLogin();
            Utils.Utils.Sleep(2000);
            return driver.Url == _URL + "/dashboard";
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }


}
